using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using WaterTestStation.dao;
using WaterTestStation.model;

namespace WaterTestStation
{
	public partial class AdHocForm : Form
	{
		private int StationNumber;
		private int testRecordId;
		private Thread executionThread;
		private Stopwatch stopwatch;
		private double lastReadingTime;
		private TestType lastTestType;
		private bool changeTestType;

		private readonly TestRecordDao testRecordDao = new TestRecordDao();

		public AdHocForm()
		{
			InitializeComponent();
			cboSamplingRate.SelectedIndex = 1; // default to 10 seconds
			cboTestType.DataSource = Enum.GetValues(typeof(TestType));
			btnEnd.Enabled = false;
		}

		private void btnStartLogging_Click(object sender, EventArgs e)
		{
			StationNumber = Util.ParseInt((String)cboStationNumber.SelectedItem);
			TestRecord testRecord = new TestRecordDao().CreateTestRecord(0, txtSample.Text,
						txtDescription.Text, "", StationNumber, "0", txtTestId.Text);

			testRecordId = testRecord.Id;
			executionThread = new Thread(_run);
			stopwatch = new Stopwatch();
			btnStart.Enabled = false;
			btnEnd.Enabled = true;
			executionThread.Start();
		}

		readonly FormUtil formUtil = new FormUtil();

		private void _run()
		{
			formUtil.ThreadSafeSetControlEnabled(cboStationNumber, false);
			formUtil.ThreadSafeSetControlEnabled(btnStart, false);

			stopwatch.Restart();
			changeTestType = false;
			changeTestType = false;
			lastReadingTime = 0;
			Stopwatch stepTimer = new Stopwatch();
			int stepStartTime = 0;
			while (true)
			{
				int samplingInterval = Util.ParseInt((string) formUtil.ThreadSafeReadCombo(cboSamplingRate));

				if (changeTestType) // do a measurement before changing the test type
				{
					Main.MultimeterQueue.Enqueue(new MeterRequest(Main.stations[StationNumber], this, lastTestType, 
						0, 0, stepTimer.Elapsed.Seconds, true));
					changeTestType = false;
					stepTimer.Stop();
					Thread.Sleep(2000);
				}

				if (!stepTimer.IsRunning || (chkContinuousSampling.Checked && stopwatch.Elapsed.TotalSeconds - lastReadingTime > samplingInterval))
				{
					if (!stepTimer.IsRunning)
					{
						stepTimer.Restart();
						stepStartTime = stopwatch.Elapsed.Seconds;
					}

					lastReadingTime = stopwatch.Elapsed.TotalSeconds;
					TestType testType = (TestType) formUtil.ThreadSafeReadComboItem(cboTestType);
					Main.MultimeterQueue.Enqueue(new MeterRequest(Main.stations[StationNumber], this, testType, 0, 
						stepStartTime, stepTimer.Elapsed.Seconds, true));
					lastTestType = testType;
					changeTestType = false;
				}
				Thread.Sleep(50);
			}
		}

		// This method is called by MeterRequest class to log and display the readings obtained
		public void LogMeterReadings(TestType testType, int pStepStartTime, int pStepTime,
				double ARefVoltage, double BRefVoltage, double ABVoltage, double ABCurrent, double temperature, double lightLevel, string notes)
		{
			formUtil.ThreadSafeSetText(lblARefVolt, Util.formatNumber(ARefVoltage, "V"));
			formUtil.ThreadSafeSetText(lblBRefVolt, Util.formatNumber(BRefVoltage, "V"));
			formUtil.ThreadSafeSetText(lblABVolt, Util.formatNumber(ABVoltage, "V"));
			formUtil.ThreadSafeSetText(lblABAmp, Util.formatNumber(ABCurrent, "A"));

			testRecordDao.LogTestData(testRecordId, testType, 0, pStepStartTime + pStepTime, pStepTime,
					ARefVoltage, BRefVoltage, ABVoltage, ABCurrent, temperature, lightLevel);
		}

		private void AdHocForm_Load(object sender, EventArgs e)
		{
		}

		private void btnChangeTestType_Click(object sender, EventArgs e)
		{
			changeTestType = true;
		}

		private void btnEnd_Click(object sender, EventArgs e)
		{
			if (executionThread != null && executionThread.IsAlive)
			{
				executionThread.Abort();
				Main.stations[StationNumber].TogglePositivePower();
				Main.stations[StationNumber].SwitchTestType(TestType.OpenCircuit);
				formUtil.ThreadSafeSetText(txtStatus, "Execution aborted");
				_updateTestRecord();

				formUtil.ThreadSafeSetControlEnabled(btnStart, true);
				formUtil.ThreadSafeSetControlEnabled(btnEnd, false);
			}
		}

		private void _updateTestRecord()
		{
			TestRecord testRecord = testRecordDao.FindById(testRecordId);
			testRecord.TestEnd = DateTime.Now;
			testRecord.Sample = txtSample.Text;
			testRecord.Description = txtDescription.Text;
			testRecordDao.saveOrUpdate(testRecord);
		}

	}
}
