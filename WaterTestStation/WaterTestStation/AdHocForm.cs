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

		private readonly TestRecordDao testRecordDao = new TestRecordDao();

		public AdHocForm()
		{
			InitializeComponent();
			cboSamplingRate.SelectedIndex = 1; // default to 10 seconds
			cboTestType.DataSource = Enum.GetValues(typeof(TestType));
		}

		private void btnStartLogging_Click(object sender, EventArgs e)
		{
			StationNumber = Util.ParseInt((String)cboStationNumber.SelectedItem);
			TestRecord testRecord = new TestRecordDao().CreateTestRecord(0, txtSample.Text,
				txtDescription.Text, "", StationNumber, "0",
				txtTestId.Text, chkReferenceElectrode.Checked);

			testRecordId = testRecord.Id;
			Thread thread = new Thread(_executeProgram);
			executionThread = thread;
			stopwatch = new Stopwatch();
			btnStartLogging.Enabled = false;
			btnStopLogging.Enabled = true;
			thread.Start();
		}

		private void _executeProgram()
		{
			int targeTtime = 0;
			stopwatch.Start();
			while (true)
			{
				TestType testType = (TestType) Enum.Parse(typeof (TestType), (string) cboTestType.SelectedItem);
				_TakeReadings(targeTtime, 0, testType);
				targeTtime += Util.ParseInt((string) cboSamplingRate.SelectedItem);
			}
		}

		private void _TakeReadings(int targetTime, int stepStartTime, TestType testStep)
		{
			Debug.WriteLine("TakeReading: TargetTime=" + targetTime + " stepStartTime=" + stepStartTime + " Stopwatch=" + stopwatch.ElapsedMilliseconds / 1000);

			SleepTill(targetTime, stepStartTime);
			TimeSpan t = TimeSpan.FromSeconds(targetTime);
			txtStatus.Text = testStep + Environment.NewLine
						 + "StepTime: " + t.ToString("mm\\:ss") + Environment.NewLine
						 + "Total Elapsed: " + stopwatch.Elapsed.ToString("hh\\:mm\\:ss");

			Main.MultimeterQueue.Enqueue(new MeterRequest(Main.stations[StationNumber], this, testStep, 0, stepStartTime, targetTime, true));
		}

		private void SleepTill(int targetTime, int baseTime)
		{
			int t = (baseTime + targetTime) * 1000 - (int)stopwatch.ElapsedMilliseconds;
			if (t > 0)
				Thread.Sleep(t);
		}

		// This method is called by Multimeter class to log and display the readings obtained
		public void LogMeterReadings(TestType testType, int pCycle, int pCycleStartTime, int pStepTime,
				double ARefVoltage, double BRefVoltage, double ABVoltage, double ABCurrent, double temperature, double lightLevel, bool logFlag)
		{
			lblARefVolt.Text = Util.formatNumber(ARefVoltage, "V");
			lblBRefVolt.Text = Util.formatNumber(BRefVoltage, "V");
			lblABVolt.Text = Util.formatNumber(ABVoltage, "V");
			lblABAmp.Text = Util.formatNumber(ABCurrent, "A");

			testRecordDao.LogTestData(testRecordId, testType, pCycle, pCycleStartTime + pStepTime, pStepTime,
					ARefVoltage, BRefVoltage, ABVoltage, ABCurrent, temperature, lightLevel);
		}



	}
}
