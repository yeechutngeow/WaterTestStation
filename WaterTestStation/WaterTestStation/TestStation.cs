
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using WaterTestStation.dao;
using WaterTestStation.hardware;
using WaterTestStation.model;

namespace WaterTestStation
{
	public class TestStation : FormUtil
	{
		public readonly int StationNumber;
		private readonly MultiPoleSwitch powerSupplySwitch;
		private readonly RelayMux chargeDischargeSelect;

		// Execution of test program
		private Stopwatch stopwatch;

		private Thread executionThread;

		private Main frmMain;
		private TextBox txtTestDescription;
		private TextBox txtCycles;
		private TextBox txtLeadTime;
		private TextBox txtStatus;
		private Label lblARefVolt;
		private Label lblBRefVolt;
		private Label lblABAmp;
		private Label lblABVolt;
		private TextBox txtVesselId;
		private TextBox txtSample;
		public Button btnStart, btnStop;

		public int testProgramId;

		// Tracking progress for logging of test data
		private int testRecordId;
		private int curCycle;
		private string curCycleStr;
		private int curStepNumber;

		readonly TestRecordDao testRecordDao = new TestRecordDao();
		
		public TestStation(int stationNumber, MultiPoleSwitch powerSupplySwitch, RelayMux chargeDischargeSelect)
		{
			this.StationNumber = stationNumber;
			this.powerSupplySwitch = powerSupplySwitch;
			this.chargeDischargeSelect = chargeDischargeSelect;
		}

		public void SetFormControls(Main fMain, TextBox vesselId, TextBox tSample, TextBox tTestDescription, TextBox tCycles, TextBox tLeadTime,
			TextBox tStatus, Label lARefVolt, Label lBRefVolt, Label lABAmp, Label lABVolt, Button bStart, Button bStop)
		{
			this.frmMain = fMain;
			this.txtTestDescription = tTestDescription;
			this.txtCycles = tCycles;
			this.txtLeadTime = tLeadTime;
			this.txtVesselId = vesselId;
			this.txtSample = tSample;
			this.txtStatus = tStatus;
			this.lblARefVolt = lARefVolt;
			this.lblBRefVolt = lBRefVolt;
			this.lblABAmp = lABAmp;
			this.lblABVolt = lABVolt;
			this.btnStart = bStart;
			this.btnStop = bStop;
		}


		// This method is called by Multimeter class to log and display the readings obtained
		public void LogMeterReadings(TestProgramStep testStep, int pCycle, int pCycleStartTime, int pStepTime, 
				double ARefVoltage, double BRefVoltage, double ABVoltage, double ABCurrent, bool logFlag)
		{
			ThreadSafeSetLabel(lblARefVolt, Util.formatNumber(ARefVoltage, "V"));
			ThreadSafeSetLabel(lblBRefVolt, Util.formatNumber(BRefVoltage, "V"));
			ThreadSafeSetLabel(lblABVolt, Util.formatNumber(ABVoltage, "V"));
			ThreadSafeSetLabel(lblABAmp, Util.formatNumber(ABCurrent, "A"));

			// logs to database if this is not an adhoc reading
			if (logFlag)
			{
				testRecordDao.LogTestData(testRecordId, testStep.GetTestType(), pCycle, pCycleStartTime + pStepTime, pStepTime, 
					ARefVoltage, BRefVoltage, ABVoltage, ABCurrent);
			}
		}


		public void ExecuteProgram()
		{
			testProgramId = (int) ThreadSafeReadCombo(frmMain.cboTestProgram);

			TestRecord testRecord = new TestRecordDao().CreateTestRecord(testProgramId, ThreadSafeReadText(txtSample),
				ThreadSafeReadText(txtTestDescription), ThreadSafeReadText(txtVesselId), this.StationNumber, ThreadSafeReadText(txtLeadTime),
				frmMain.txtTestDataSet.Text);

			testRecordId = testRecord.Id;
			Thread thread = new Thread(_executeProgram);
			executionThread = thread;
			btnStart.Enabled = false;
			btnStop.Enabled = true;
			thread.Start();
		}

		public void StopExecution()
		{
			if (executionThread != null && executionThread.IsAlive)
			{
				executionThread.Abort();
				TogglePositivePower();
				SwitchTestType(TestType.OpenCircuit);
				ThreadSafeSetText(txtStatus, "Execution aborted");
				FinalizeExecution();
			}
		}

		private void FinalizeExecution()
		{
			btnStart.Enabled = true;
			btnStop.Enabled = false;

			TestRecord testRecord = testRecordDao.FindById(testRecordId);
			testRecord.TestEnd = DateTime.Now;
			testRecord.Sample = txtSample.Text;
			testRecord.Description = txtTestDescription.Text;
			testRecord.VesselId = txtVesselId.Text;
			testRecordDao.saveOrUpdate(testRecord);
		}

		private void _executeProgram()
		{
			testProgramId = (int) ThreadSafeReadCombo(frmMain.cboTestProgram);
			TestProgram testProgram = new TestProgramDao().FindById(testProgramId);

			stopwatch = new Stopwatch();
			stopwatch.Start();

			int stepStartTime = 0;
			stepStartTime = _runPreTest(stepStartTime);


			int cycles;
			if (!int.TryParse(txtCycles.Text, out cycles)) cycles = 1000;

			for (int i = 0; i < cycles; i++)
			{
				curCycleStr = (i+1).ToString();
				curCycle = i + 1;
				curStepNumber = 1;
				foreach (var step in testProgram.TestProgramSteps)
				{
					runstep(stepStartTime, step);
					stepStartTime += step.Duration;
					curStepNumber++;
				}
			}
			ThreadSafeSetText(txtStatus, "Test Completed");
			FinalizeExecution();
		}

		private int _runPreTest(int stepStartTime)
		{
			TestProgramStep preTest = new TestProgramStep
			{
				Duration = Util.ParseInt(ThreadSafeReadText(txtLeadTime)),
				TestProgramId = testProgramId,
				TestType = TestType.OpenCircuit.ToString()
			};
			curCycleStr = "LeadTime";
			curCycle = 0;
			curStepNumber = 1;
			runstep(stepStartTime, preTest);
			stepStartTime += preTest.Duration;

			// Run 10 minutes of discharge to establish a baseline for imbalanceses 
			// that can be used for correction for later cycles
			TestProgramStep preTest2 = new TestProgramStep
			{
				Duration = 600,
				TestProgramId = testProgramId,
				TestType = TestType.Discharge.ToString()
			};
			runstep(stepStartTime, preTest2);
			curStepNumber = 2;
			stepStartTime += preTest2.Duration;
			return stepStartTime;
		}

		private readonly int[] _dischargeSamplingTime = { 0, 2, 5, 10, 30, 60, 120 };
		private readonly int[] _chargeSamplingTime = { 0, 2, 5, 10, 30, 60, 120 };
		private readonly int[] _openCircuitSamplingTime = { 0, 5, 10, 30, 60, 120 };
		private const int SamplingInterval = 150; // one sampling every 2.5 minutes

		private void runstep(int stepStartTime, TestProgramStep testStep)
		{
			//SwitchTestType(testStep.GetTestType());
			switch (testStep.GetTestType())
			{
				case TestType.OpenCircuit:
					_ExecuteStep(stepStartTime, testStep, _openCircuitSamplingTime);
					break;
				case TestType.ForwardCharge:
					_ExecuteStep(stepStartTime, testStep, _chargeSamplingTime);
					break;
				case TestType.ReverseCharge:
					_ExecuteStep(stepStartTime, testStep, _chargeSamplingTime);
					break;
				case TestType.Discharge:
					_ExecuteStep(stepStartTime, testStep, _dischargeSamplingTime);
					break;
			}
			SleepTill(testStep.Duration, stepStartTime);
		}

		private void _ExecuteStep(int stepStartTime, TestProgramStep testStep, IEnumerable<int> ReadingTimes)
		{
			foreach (var t in ReadingTimes)
			{
				Debug.WriteLine("ExecuteStep " + testStep.TestType + ":" + stepStartTime + " traget time:", t);
				if (t > testStep.Duration)
					break;
				_TakeReadings(t, stepStartTime, testStep);
			}

			int time = SamplingInterval;																																																																																																																										
			while (time < testStep.Duration)
			{
				_TakeReadings(time, stepStartTime, testStep);
				time += SamplingInterval; // add 5 mins 
				if (time == testStep.Duration) time -= 2; // last reading - bring forward 2 seconds
			}
		}
		
		private void _TakeReadings(int targetTime, int stepStartTime, TestProgramStep testStep)
		{
			Debug.WriteLine("TakeReading: TargetTime=" + targetTime + " stepStartTime=" + stepStartTime + " Stopwatch=" + stopwatch.ElapsedMilliseconds/1000);
			
			// allows for a slack of 500 ms
			if (stopwatch.ElapsedMilliseconds > (stepStartTime + targetTime + .5) * 1000)
				return;

			SleepTill(targetTime, stepStartTime);
			TimeSpan t = TimeSpan.FromSeconds(targetTime);
			string msg = "Cycle: " + curCycleStr + Environment.NewLine
						 + "Step: " + curStepNumber + "-" + testStep.TestType + Environment.NewLine
						 + "StepTime: " + t.ToString("mm\\:ss") + Environment.NewLine
						 + "Total Elapsed: " + stopwatch.Elapsed.ToString("hh\\:mm\\:ss");
			ThreadSafeSetText(txtStatus, msg);

			Main.MultimeterQueue.Enqueue(new MeterRequest(this, testStep, curCycle, stepStartTime, targetTime, true));
		}

		private void SleepTill(int targetTime, int baseTime)
		{
			int t = (baseTime + targetTime) * 1000 - (int)stopwatch.ElapsedMilliseconds;
			if (t > 0)
				Thread.Sleep(t);
		}

		public void SwitchTestType(TestType testType)
		{
			switch (testType)
			{
				case TestType.OpenCircuit:
					chargeDischargeSelect.SelectChannel(0);
					break;
				case TestType.ForwardCharge:
					TogglePositivePower();
					chargeDischargeSelect.SelectChannel(2);
					break;
				case TestType.ReverseCharge:
					ToggleNegatovePower();
					chargeDischargeSelect.SelectChannel(2);
					break;
				case TestType.Discharge:
					chargeDischargeSelect.SelectChannel(1);
					break;
			}
		}

		public void Initialize()
		{
			SwitchTestType(TestType.OpenCircuit);
		}

		public void TogglePositivePower()
		{
			powerSupplySwitch.ToggleOff();
		}

		private void ToggleNegatovePower()
		{
			powerSupplySwitch.ToggleOn();
		}
	}
}
