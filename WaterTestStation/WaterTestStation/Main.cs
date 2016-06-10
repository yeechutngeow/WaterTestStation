using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Windows.Forms;
using WaterTestStation.dao;
using WaterTestStation.hardware;
using WaterTestStation.model;

namespace WaterTestStation
{
	public partial class Main : Form
	{
		public static Main MainForm;
		public double Temperature;

		// for development & debugging purposes

		public const int NStations = 7;
		//----- the hardware components ---------------------------------------------------------

		UsbRelay usbRelay1;
		UsbRelay usbRelay2;
		// switch between charge/discharge, one for each station
		private readonly RelayMux[] switches = new RelayMux[NStations];
		//
		private readonly MultiPoleSwitch[] toggles = new MultiPoleSwitch[NStations];
		// 4 multiplexer to select measuring points. Each mux has #station channels.

		private readonly MultiPoleSwitch[] currentSwitch = new MultiPoleSwitch[NStations];

		// the 4 multiplexers for switching the multiplexer to the right channel
		public static readonly RelayMux[] mux = new RelayMux[4];

		public readonly TestStation[] stations = new TestStation[NStations];

		public static Multimeter Multimeter;

		// multimeter queue 
		public static ConcurrentQueue<MeterRequest> MultimeterQueue = new ConcurrentQueue<MeterRequest>();

		public Main()
		{
			InitializeComponent();
			InitializeHardware();

			DrawForm();

			cboSamplingRate.SelectedValue = "20";

			cboTestType.DataSource = Enum.GetValues(typeof(TestType));
			cboTestProgram.ValueMember = "Id";
			cboTestProgram.DisplayMember = "Title";
			cboTestProgram.DataSource = new TestProgramDao().GetProgramsList();

			usbRelay1.OpenComPort(Config.RelayCom1);
			usbRelay2.OpenComPort(Config.RelayCom2);

			MeterRequest.StartServiceQueue();
			TemperatureSensor.StartSensor();
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			usbRelay1.OpenComPort(Config.RelayCom1);
			usbRelay2.OpenComPort(Config.RelayCom2);

			Button btn = (Button)sender;
			int index = int.Parse(btn.Name.Substring(8));
			stations[index].ExecuteProgram();
		}

		private void btnStop_Click(object sender, EventArgs e)
		{
			DialogResult dialogResult = MessageBox.Show("Are you sure you want to abort this execution?", 
				"Confirm abort", MessageBoxButtons.YesNo);
			if (dialogResult == DialogResult.Yes)
			{
				Button btn = (Button)sender;
				int index = int.Parse(btn.Name.Substring(7));
				stations[index].StopExecution();
			}
		}

		private void btnAdhocReading_Click(object sender, EventArgs e)
		{
			int stationNumber = int.Parse(cboStationNumber.Text) - 1;
			if (!stations[stationNumber].btnStart.Enabled)
				// disallow taking adhoc readings when a program is running
				return;
			usbRelay1.OpenComPort(Config.RelayCom1);
			usbRelay2.OpenComPort(Config.RelayCom2);

			string testType = cboTestType.Text;
			TestType eTestType = (TestType) Enum.Parse(typeof (TestType), testType);

			TestProgramStep testStep = new TestProgramStep {TestType = testType};
			stations[stationNumber]._switchTestType(eTestType);
			MultimeterQueue.Enqueue(new MeterRequest(stations[stationNumber], testStep, 0, 0, 0, false));
		}

		private void DrawForm()
		{
			const int stationsPerRow = 4;

			const int panelWidth = 350;
			const int panelHeight = 290;
			const int yOffSet = 60;
			const int xOffSet = 15;

			const int yLineHeight = 23;
			const int labelWidth = 90;
			const int col1 = 5;
			const int col2 = col1 + labelWidth;
			const int col3 = panelWidth/2 + 5;
			const int col4 = col3 + labelWidth;

			const int textWidth = panelWidth - col2 -5 ; 

			this.Height = panelHeight*2 + yOffSet + 70;
			this.Width = panelWidth*stationsPerRow + xOffSet + 30;

			for (int i = 0; i < NStations; i++)
			{
				Panel panel = new Panel
				{
					Name = "panel" + (i), 
					Size = new Size(panelWidth, panelHeight),
					BorderStyle = BorderStyle.FixedSingle,
					Location = new Point(panelWidth * (i % stationsPerRow) + xOffSet, panelHeight * (i / stationsPerRow) + yOffSet)
				};
				this.Controls.Add(panel);

				Label label;

				int y = 5;
				//------------------------------------------------------------
				label = new Label
				{
					Text = "Station " + (i+1),
					Location = new Point(col1, y),
					Font = new Font(this.Font, FontStyle.Bold),
					Width = labelWidth
				};
				panel.Controls.Add(label);
				y += yLineHeight;
	
				//------------------------------------------------------------
				label = new Label
				{
					Text = "Cubicle Id:",
					Location = new Point(col1, y),
					Width = labelWidth
				};
				panel.Controls.Add(label);

				TextBox txtVesselId = new TextBox
				{
					Location = new Point(col2, y)
				};
				panel.Controls.Add(txtVesselId);
				y += yLineHeight;

				//------------------------------------------------------------
				label = new Label
				{
					Text = "Sample:",
					Location = new Point(col1, y),
					Width = labelWidth
				};
				panel.Controls.Add(label);

				TextBox txtSample = new TextBox
				{
					Multiline = true,
					Width = textWidth,
					Location = new Point(col2, y)
				};
				panel.Controls.Add(txtSample);
				y += yLineHeight;

				//------------------------------------------------------------
				label = new Label
				{
					Text = "Test Desc:",
					Location = new Point(col1, y),
					Width = labelWidth
				};
				panel.Controls.Add(label);

				TextBox txtTestDescription = new TextBox
				{
					Multiline = true,
					Height = 60,
					Width = textWidth,
					Location = new Point(col2, y)
				};
				panel.Controls.Add(txtTestDescription);
				y += 65;

				//------------------------------------------------------------
				label = new Label
				{
					Text = "Run Cycles:",
					Location = new Point(col1, y),
					Width = labelWidth
				};
				panel.Controls.Add(label);

				TextBox txtCycles = new TextBox
				{
					Width = 50,
					Location = new Point(col2, y)
				};
				panel.Controls.Add(txtCycles);

				label = new Label
				{
					Text = "Lead Time(s):",
					Location = new Point(col3, y),
					Width = labelWidth
				};
				panel.Controls.Add(label);

				TextBox txtLeadTime = new TextBox
				{
					Width = 50,
					Location = new Point(col4, y),
					Text = "1200"
				};

				panel.Controls.Add(txtLeadTime);

				y += yLineHeight;

				//------------------------------------------------------------
				TextBox txtStatus = new TextBox
					                    {
						                    Multiline = true,
						                    Width = panelWidth/2 - 20,
						                    Height = 82,
						                    ReadOnly = true,
						                    Location = new Point(col3, y),
											Text = "Idle"
					                    };
				panel.Controls.Add(txtStatus);

				//------------------------------------------------------------
				label = new Label
					{
						Text = "A-Ref Volt:",
						Location = new Point(col1, y),
						Width = labelWidth
					};
				panel.Controls.Add(label);

				Label lblARefVolt = new Label
				{
					Location = new Point(col2, y)
				};
				panel.Controls.Add(lblARefVolt);
				y += yLineHeight;

				//------------------------------------------------------------

				label = new Label
				{
					Text = "B-Ref Volt:",
					Location = new Point(col1, y),
					Width = labelWidth
				};
				panel.Controls.Add(label);

				Label lblBRefVolt = new Label
				{
					Location = new Point(col2, y)
				};
				panel.Controls.Add(lblBRefVolt);
				y += yLineHeight;

				//------------------------------------------------------------

				label = new Label
				{
					Text = "A-B Amp:",
					Location = new Point(col1, y),
					Width = labelWidth
				};
				panel.Controls.Add(label);

				Label lblABAmp = new Label
				{
					Location = new Point(col2, y)
				};
				panel.Controls.Add(lblABAmp);
				y += yLineHeight;

				//------------------------------------------------------------
				label = new Label
				{
					Text = "A-B Volt:",
					Location = new Point(col1, y),
					Width = labelWidth
				};
				panel.Controls.Add(label);
				Label lblABVolt = new Label
				{
					Location = new Point(col2, y)
				};
				panel.Controls.Add(lblABVolt);
				y += yLineHeight;

				//------------------------------------------------------------
				Button btnStart = new Button
					{
						Text = "Start", 
						Name = "btnStart" + i,
						Location = new Point(col1, y),
						Height = 26
					};
				btnStart.Click += this.btnStart_Click;
				panel.Controls.Add(btnStart);

				Button btnStop = new Button
					{
						Text = "Stop", 
						Name = "btnStop" + i, 
						Location = new Point(80, y),
						Height = 26,
						Enabled = false
					};
				btnStop.Click += this.btnStop_Click;
				panel.Controls.Add(btnStop);

				//-----------------------------------------------------------------

				stations[i].SetFormControls(this, txtVesselId, txtSample, txtTestDescription,  txtCycles, txtLeadTime, txtStatus,
					lblARefVolt, lblBRefVolt, lblABAmp, lblABVolt, btnStart, btnStop);
			}
		}

		private void InitializeHardware()
		{
			usbRelay1 = new UsbRelay(Config.RelayCom1, lblRelayStatus1);
			usbRelay2 = new UsbRelay(Config.RelayCom2, lblRelayStatus2);

			switches[0] = new RelayMux(usbRelay1, new[] { 0, 1 });
			toggles[0] = new MultiPoleSwitch(usbRelay1, new[] { 2, 3 });
			switches[1] = new RelayMux(usbRelay1, new[] { 4, 5 });
			toggles[1] = new MultiPoleSwitch(usbRelay1, new[] { 6, 7 });
			switches[2] = new RelayMux(usbRelay1, new[] { 8, 9 });
			toggles[2] = new MultiPoleSwitch(usbRelay1, new[] { 10, 11 });
			switches[3] = new RelayMux(usbRelay1, new[] { 12, 13 });
			toggles[3] = new MultiPoleSwitch(usbRelay1, new[] { 14, 15 });
			switches[4] = new RelayMux(usbRelay1, new[] { 16, 17 });
			toggles[4] = new MultiPoleSwitch(usbRelay1, new[] { 18, 19 });
			switches[5] = new RelayMux(usbRelay1, new[] { 20, 21 });
			toggles[5] = new MultiPoleSwitch(usbRelay1, new[] { 22, 23 });
			switches[6] = new RelayMux(usbRelay1, new[] { 24, 25 });
			toggles[6] = new MultiPoleSwitch(usbRelay1, new[] { 26, 27 });

			currentSwitch[0] = new MultiPoleSwitch(usbRelay2, new[] { 24 });
			currentSwitch[1] = new MultiPoleSwitch(usbRelay2, new[] { 25 });
			currentSwitch[2] = new MultiPoleSwitch(usbRelay2, new[] { 26 });
			currentSwitch[3] = new MultiPoleSwitch(usbRelay2, new[] { 27 });
			currentSwitch[4] = new MultiPoleSwitch(usbRelay2, new[] { 28 });
			currentSwitch[5] = new MultiPoleSwitch(usbRelay2, new[] { 29 });
			currentSwitch[6] = new MultiPoleSwitch(usbRelay2, new[] { 30 });


			mux[0] = new RelayMux(usbRelay2, new[] {0, 1, 2, 3, 4, 5});
			mux[1] = new RelayMux(usbRelay2, new[] {6, 7, 8, 9, 10, 11});
			mux[2] = new RelayMux(usbRelay2, new[] {12, 13, 14, 15, 16, 17});
			mux[3] = new RelayMux(usbRelay2, new[] {18, 19, 20, 21, 22, 23});

			Multimeter = new Multimeter(usbRelay1, new[] {29, 30, 31, 28});

			for (int i = 0; i < NStations; i++ )
				stations[i] = new TestStation(i, toggles[i], switches[i], currentSwitch[i]);
		}

		private void Main_Load(object sender, EventArgs e)
		{
		}

		private void viewTestResultMenuItem_Click(object sender, EventArgs e)
		{
			var f = new ViewResultsForm();
			f.Show();
		}

		private void Main_FormClosing(object sender, FormClosingEventArgs e)
		{
			foreach (var station in stations)
			{
				station.StopExecution();	
			}
			if (Config.HasMultimeter)
				Multimeter.CloseSession();
			TemperatureSensor.Stop();
			MeterRequest.AbortThread();
			usbRelay1.Close();
			usbRelay2.Close();
		}

		private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			PropertiesForm frm = new PropertiesForm();
			frm.Show();
		}

		private void btnRefresh_Click(object sender, EventArgs e)
		{
			cboTestProgram.DataSource = new TestProgramDao().GetProgramsList();
		}

		readonly FormUtil formUtil = new FormUtil();

		public void SetTemperature(double temperature)
		{
			this.Temperature = temperature;
			formUtil.ThreadSafeSetStatusStripLabel(this.statusStrip1, this.lblTemperature, temperature.ToString("0.00"));
		}
	}
}
