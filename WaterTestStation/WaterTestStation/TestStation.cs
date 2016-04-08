
using System;
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
		private Label lblStatus;
		private Label lblCycle;
		private Label lblARefVolt;
		private Label lblBRefVolt;
		private Label lblABAmp;
		private Label lblABVolt;
		private TextBox txtVesselId;
		private TextBox txtSample;
		private Button btnStart, btnStop;

		// Tracking progress for logging of test data
		private int testRecordId; 
		private int currentCycle;

		readonly TestRecordDao testRecordDao = new TestRecordDao();
		
		public TestStation(int stationNumber, MultiPoleSwitch powerSupplySwitch, RelayMux chargeDischargeSelect)
		{
			this.StationNumber = stationNumber;
			this.powerSupplySwitch = powerSupplySwitch;
			this.chargeDischargeSelect = chargeDischargeSelect;
		}

		public void SetFormControls(TextBox vesselId, TextBox tSample, TextBox tTestDescription, ComboBox cboTestProg, TextBox tCycles,
			Label lStatus, Label lCycle, Label lARefVolt, Label lBRefVolt, Label lABAmp, Label lABVolt, Button bStart, Button bStop)
		{
			this.cboTestProgram = cboTestProg;
			this.txtTestDescription = tTestDescription;
			this.txtCycles = tCycles;
			this.txtVesselId = vesselId;
			this.txtSample = tSample;
			this.lblStatus = lStatus;
			this.lblCycle = lCycle;
			this.lblARefVolt = lARefVolt;
			this.lblBRefVolt = lBRefVolt;
			this.lblABAmp = lABAmp;
			this.lblABVolt = lABVolt;
			this.btnStart = bStart;
			this.btnStop = bStop;
		}

		private string formatNumber(float n, string unit)
		{
			if (Math.Abs(n) < 0.001)
				return (n*1000).ToString("0.00000 ") + "m" + unit;
			else
				return n.ToString("0.00000 ") + unit;
		}

		// This method is called by Multimeter class to log and display the readings obtained
		public void LogMeterReadings(TestProgramStep testStep, int pCycle, int pCycleStartTime, int pStepTime, 
				float ARefVoltage, float BRefVoltage, float ABVoltage, float ABCurrent, bool logFlag)
		{
			ThreadSafeSetText(lblARefVolt, formatNumber(ARefVoltage, "V"));
			ThreadSafeSetText(lblBRefVolt, formatNumber(BRefVoltage, "V"));
			ThreadSafeSetText(lblABVolt, formatNumber(ABVoltage,"V"));
			ThreadSafeSetText(lblABAmp, formatNumber(ABCurrent,"A"));

			// logs to database if this is not an adhoc reading
			if (logFlag)
			{
				testRecordDao.LogTestData(testRecordId, testStep.GetTestType(), pCycle, pCycleStartTime + pStepTime, pStepTime, 
					ARefVoltage, BRefVoltage, ABVoltage, ABCurrent);
			}
		}

		private delegate void SetTextCallback(Label label, string text);

		private void ThreadSafeSetText(Label label, string text)
		{
			// InvokeRequired required compares the thread ID of the
			// calling thread to the thread ID of the creating thread.
			// If these threads are different, it returns true.
			if (label.InvokeRequired)
			{
				SetTextCallback d = ThreadSafeSetText;
				label.Invoke(d, new object[] {label, text });
			}
			else
			{
				label.Text = text;
			}			
		}

		//private delegate string ReadTextCallback(Control control);

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
				ThreadSafeReadText(txtTestDescription), ThreadSafeReadText(txtVesselId), this.StationNumber);

			testRecordId = testRecord.Id;
			Thread thread = new Thread(_executeProgram);
			executionThread = thread;
			btnStart.Enabled = false;
			btnStop.Enabled = true;
			thread.Start();
		}

		public void StopExecution()
		{
			executionThread.Abort();
			lblStatus.Text = "Execution aborted";
			FinalizeExecution();
		}

		private void FinalizeExecution()
		{
			btnStart.Enabled = true;
			btnStop.Enabled = false;

			TestRecord testRecord = testRecordDao.FindById(testRecordId);
			testRecord.TestEnd = DateTime.Now;
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
				Duration = testProgram.PreTestWait,
				TestProgramId = testProgramId,
				TestType = TestType.OpenCircuit.ToString()
			};
			ThreadSafeSetText(lblCycle, "PreTest");
			runstep(stepStartTime, preTest);
			stepStartTime += testProgram.PreTestWait;

			int cycles;
			if (!int.TryParse(txtCycles.Text, out cycles)) cycles = 1000;

			for (int i = 0; i < cycles; i++)
			{
				currentCycle = i+1;
				ThreadSafeSetText(lblCycle, (i + 1).ToString());
				foreach (var step in testProgram.TestProgramSteps)
				{
					runstep(stepStartTime, step);
					stepStartTime += step.Duration;
				}
			}
			ThreadSafeSetText(lblStatus, "Idle");
			ThreadSafeSetText(lblCycle, "Completed");
			FinalizeExecution();
		}

		private readonly int[] DischargeReadingTime = { 0, 2, 5, 10, 30, 60, 120 };
		private readonly int[] ChargeReadingTime = { 0, 2, 5, 10, 30, 60, 120 };
		private readonly int[] OpenCircuitReadingTime = { 0, 5, 10, 30, 60, 120 };

		private void runstep(int stepStartTime, TestProgramStep testStep)
		{
			ThreadSafeSetText(lblStatus, testStep.TestType);
			SwitchTestType(testStep.GetTestType());
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

		private void _ExecuteStep(int stepStartTime, TestProgramStep testStep, int[] ReadingTimes)
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
			}
		}
		
		private void _TakeReadings(int targetTime, int stepStartTime, TestProgramStep testStep)
		{
			Debug.WriteLine("TakeReading: TargetTime=" + targetTime + " stepStartTime=" + stepStartTime + " Stopwatch=" + stopwatch.ElapsedMilliseconds/1000);
			// allows for a slack of 200 ms
			if (stopwatch.ElapsedMilliseconds > (stepStartTime + targetTime + 200) * 1000)
				return;

			SleepTill(targetTime, stepStartTime);
			Main.MultimeterQueue.Enqueue(new MeterRequest(this, testStep, currentCycle, stepStartTime, targetTime, true));
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
