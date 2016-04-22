
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
	public class TestStation
	{
		public readonly int StationNumber;
		private readonly MultiPoleSwitch powerSupplySwitch;
		private readonly RelayMux chargeDischargeSelect;

		// Execution of test program
		private Stopwatch stopwatch;

		private Thread executionThread;

		private TextBox txtTestDescription;
		private ComboBox cboTestProgram;
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

		// Tracking progress for logging of test data
		private int testRecordId; 
		private string currentCycle;
		private int curCycle;
		private int curStepNumber;

		readonly TestRecordDao testRecordDao = new TestRecordDao();
		
		public TestStation(int stationNumber, MultiPoleSwitch powerSupplySwitch, RelayMux chargeDischargeSelect)
		{
			this.StationNumber = stationNumber;
			this.powerSupplySwitch = powerSupplySwitch;
			this.chargeDischargeSelect = chargeDischargeSelect;
		}

		public void SetFormControls(TextBox vesselId, TextBox tSample, TextBox tTestDescription, ComboBox cboTestProg, TextBox tCycles, TextBox tLeadTime,
			TextBox tStatus, Label lARefVolt, Label lBRefVolt, Label lABAmp, Label lABVolt, Button bStart, Button bStop)
		{
			this.cboTestProgram = cboTestProg;
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

		private delegate void SetLabelCallback(Label label, string text);

		private void ThreadSafeSetLabel(Label label, string text)
		{
			// InvokeRequired required compares the thread ID of the
			// calling thread to the thread ID of the creating thread.
			// If these threads are different, it returns true.
			if (label.InvokeRequired)
			{
				SetLabelCallback d = ThreadSafeSetLabel;
				label.Invoke(d, new object[] {label, text });
			}
			else
			{
				label.Text = text;
			}			
		}

		private delegate void SetTextCallback(TextBox txtBox, string text);

		private void ThreadSafeSetText(TextBox txtBox, string text)
		{
			// InvokeRequired required compares the thread ID of the
			// calling thread to the thread ID of the creating thread.
			// If these threads are different, it returns true.
			if (txtBox.InvokeRequired)
			{
				SetTextCallback d = ThreadSafeSetText;
				txtBox.Invoke(d, new object[] { txtBox, text });
			}
			else
			{
				txtBox.Text = text;
			}
		}

		private string ThreadSafeReadText(Control control)
		{
			string text = null;
			if (control.InvokeRequired)
			{
				//ReadTextCallback d = ThreadSafeReadText;
				control.Invoke((MethodInvoker)delegate
				{
					text = control.Text;
				});
			}
			else 
				text = control.Text;

			return text;
		}

		//private delegate Object ReadComboboxCallback(ComboBox cbo);

		private Object ThreadSafeReadCombo(ComboBox cbo)
		{
			Object value = null;
			if (cbo.InvokeRequired)
			{
				//ReadComboboxCallback d = (ReadComboboxCallback) ThreadSafeReadCombo(cbo);
				cbo.Invoke((MethodInvoker)delegate
				{
					value = cbo.SelectedValue;
				});
			}
			else
				value = cbo.SelectedValue;

			return value;
		}


		public void ExecuteProgram()
		{
			int testProgramId = (int) ThreadSafeReadCombo(cboTestProgram);

			TestRecord testRecord = new TestRecordDao().CreateTestRecord(testProgramId, ThreadSafeReadText(txtSample),
				ThreadSafeReadText(txtTestDescription), ThreadSafeReadText(txtVesselId), this.StationNumber, ThreadSafeReadText(txtLeadTime));

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
			int testProgramId = (int) ThreadSafeReadCombo(cboTestProgram);
			TestProgram testProgram = new TestProgramDao().FindById(testProgramId);

			stopwatch = new Stopwatch();
			stopwatch.Start();

			int stepStartTime = 0;
			TestProgramStep preTest = new TestProgramStep
			{
				Duration = Util.ParseInt(ThreadSafeReadText(txtLeadTime)),
				TestProgramId = testProgramId,
				TestType = TestType.OpenCircuit.ToString()
			};
			currentCycle = "LeadTime";
			curCycle = 0;
			runstep(stepStartTime, preTest);
			stepStartTime += Util.ParseInt(ThreadSafeReadText(txtLeadTime));

			int cycles;
			if (!int.TryParse(txtCycles.Text, out cycles)) cycles = 1000;

			for (int i = 0; i < cycles; i++)
			{
				currentCycle = (i+1).ToString();
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

		private readonly int[] DischargeReadingTime = { 0, 2, 5, 10, 30, 60, 120 };
		private readonly int[] ChargeReadingTime = { 0, 2, 5, 10, 30, 60, 120 };
		private readonly int[] OpenCircuitReadingTime = { 0, 5, 10, 30, 60, 120 };

		private void runstep(int stepStartTime, TestProgramStep testStep)
		{
			//SwitchTestType(testStep.GetTestType());
			switch (testStep.GetTestType())
			{
				case TestType.OpenCircuit:
					_ExecuteStep(stepStartTime, testStep, OpenCircuitReadingTime);
					break;
				case TestType.ForwardCharge:
					_ExecuteStep(stepStartTime, testStep, ChargeReadingTime);
					break;
				case TestType.ReverseCharge:
					_ExecuteStep(stepStartTime, testStep, ChargeReadingTime);
					break;
				case TestType.Discharge:
					_ExecuteStep(stepStartTime, testStep, DischargeReadingTime);
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

			int time = 300;
			while (time < testStep.Duration)
			{
				_TakeReadings(time, stepStartTime, testStep);
				time += 300; // add 5 mins 
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
			string msg = "Cycle: " + currentCycle + Environment.NewLine
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
