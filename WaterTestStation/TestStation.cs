﻿
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
		public readonly MultiPoleSwitch currentSwitch;

		// Execution of test program
		private Stopwatch stopwatch;

		private Thread executionThread;

		public Main frmMain;
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
		
		public TestStation(int stationNumber, MultiPoleSwitch powerSupplySwitch, RelayMux chargeDischargeSelect, MultiPoleSwitch currentSwitch)
		{
			this.StationNumber = stationNumber;
			this.powerSupplySwitch = powerSupplySwitch;
			this.chargeDischargeSelect = chargeDischargeSelect;
			this.currentSwitch = currentSwitch;
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
				double ARefVoltage, double BRefVoltage, double ABVoltage, double ABCurrent, double temperature, bool logFlag)
		{
			ThreadSafeSetLabel(lblARefVolt, Util.formatNumber(ARefVoltage, "V"));
			ThreadSafeSetLabel(lblBRefVolt, Util.formatNumber(BRefVoltage, "V"));
			ThreadSafeSetLabel(lblABVolt, Util.formatNumber(ABVoltage, "V"));
			ThreadSafeSetLabel(lblABAmp, Util.formatNumber(ABCurrent, "A"));

			// logs to database if this is not an adhoc reading
			if (logFlag)
			{
				testRecordDao.LogTestData(testRecordId, testStep.GetTestType(), pCycle, pCycleStartTime + pStepTime, pStepTime, 
					ARefVoltage, BRefVoltage, ABVoltage, ABCurrent, temperature);
			}
		}


		public void ExecuteProgram()
		{
			testProgramId = (int) ThreadSafeReadCombo(frmMain.cboTestProgram);

			TestRecord testRecord = new TestRecordDao().CreateTestRecord(testProgramId, ThreadSafeReadText(txtSample),
				ThreadSafeReadText(txtTestDescription), ThreadSafeReadText(txtVesselId), this.StationNumber, ThreadSafeReadText(txtLeadTime),
				frmMain.txtTestDataSet.Text, frmMain.chkReferenceElectrode.Checked);

			testRecordId = testRecord.Id;
			Thread thread = new Thread(_executeProgram);
			executionThread = thread;
			ThreadSafeSetButtonEnabled(btnStart, false);
			ThreadSafeSetButtonEnabled(btnStop, true);
			thread.Start();
		}

		public void StopExecution()
		{
			if (executionThread != null && executionThread.IsAlive)
			{
				executionThread.Abort();
				TogglePositivePower();
				_switchTestType(TestType.OpenCircuit);
				ThreadSafeSetText(txtStatus, "Execution aborted" + System.Environment.NewLine + ThreadSafeReadText(txtStatus));
				_finalizeExecution();
			}
		}

		private void _finalizeExecution()
		{
			ThreadSafeSetButtonEnabled(btnStart, true);
			ThreadSafeSetButtonEnabled(btnStop, false);

			_updateTestRecord();
		}

		private void _updateTestRecord()
		{
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

			int cyclesToRun;
			if (!int.TryParse(ThreadSafeReadText(txtCycles), out cyclesToRun)) cyclesToRun = 10000;

			curCycle = 1;
			for (; curCycle <= cyclesToRun; curCycle++)
			{
				curCycleStr = curCycle.ToString();
				curStepNumber = 1;
				foreach (var step in testProgram.TestProgramSteps)
				{
					runstep(stepStartTime, step);
					stepStartTime += step.Duration;
					curStepNumber++;
				}
				_updateTestRecord();
				if (!int.TryParse(ThreadSafeReadText(txtCycles), out cyclesToRun)) cyclesToRun = 10000;
			}
			ThreadSafeSetText(txtStatus, "Test Completed" + System.Environment.NewLine + ThreadSafeReadText(txtStatus));
			_finalizeExecution();
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

			// Run 5 minutes of discharge to establish a baseline for imbalanceses 
			// that could be used for correction for later cycles
			TestProgramStep preTest2 = new TestProgramStep
			{
				Duration = 300,
				TestProgramId = testProgramId,
				TestType = TestType.Discharge.ToString()
			};
			runstep(stepStartTime, preTest2);
			curStepNumber = 2;
			stepStartTime += preTest2.Duration;
			return stepStartTime;
		}

//		private readonly int[] _dischargeSamplingTime = { 0, 5, 10, 30, 60 };
//		private readonly int[] _chargeSamplingTime = { 0, 5, 10, 30, 60 };
//		private readonly int[] _openCircuitSamplingTime = { 0, 5, 10, 30, 60 };
        private readonly int[] _dischargeSamplingTime = { 0, 5, 10, 20, 40 };
        private readonly int[] _chargeSamplingTime = { 0, 5, 10, 20, 40 };
        private readonly int[] _openCircuitSamplingTime = { 0, 5, 10, 20, 40 };
        private const int DefaultSamplingInterval = 150; // one sampling every 2.5 minutes

		private const int FastSamplingInterval = 20; // fast sampling

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
			int samplingInterval = Main.MainForm.chkFastSampling.Checked ? FastSamplingInterval : DefaultSamplingInterval;

			foreach (var t in ReadingTimes)
			{
				Debug.WriteLine("ExecuteStep " + testStep.TestType + ":" + stepStartTime + " traget time:", t);
				if (t > testStep.Duration)
					break;
				if (t >= samplingInterval)
					break;
				_TakeReadings(t, stepStartTime, testStep);
			}


			int time = samplingInterval;

			while (time < testStep.Duration)
			{
				_TakeReadings(time, stepStartTime, testStep);
				time += samplingInterval; // advance to next sampling time
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

		public void _switchTestType(TestType testType)
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
			_switchTestType(TestType.OpenCircuit);
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