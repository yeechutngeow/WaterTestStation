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
		// for development & debugging purposes

		public const int NStations = 8;
		//----- the hardware components ---------------------------------------------------------

		UsbRelay usbRelay;
		UsbRelay usbRelay2;
		// switch between charge/discharge, one for each station
		private readonly RelayMux[] switches = new RelayMux[NStations];
		//
		private readonly MultiPoleSwitch[] toggles = new MultiPoleSwitch[NStations];
		// 4 multiplexer to select measuring points. Each mux has #station channels.

		// the 4 multiplexers for switching the multiplexer to the right channel
		public static readonly RelayMux[] mux = new RelayMux[4];

		readonly TestStation[] stations = new TestStation[NStations];

		public static Multimeter Multimeter;

		// multimeter queue 
		public static ConcurrentQueue<MeterRequest> MultimeterQueue = new ConcurrentQueue<MeterRequest>();

		public Main()
		{
			InitializeComponent();
			InitializeHardware();

			DrawForm();

			cboTestType.DataSource = Enum.GetValues(typeof(TestType));
			cboTestProgram.ValueMember = "Id";
			cboTestProgram.DisplayMember = "Title";
			cboTestProgram.DataSource = new TestProgramDao().GetProgramsList();

			MeterRequest.StartServiceQueue();
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			usbRelay.OpenComPort(Config.RelayCom1);
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
			usbRelay.OpenComPort(Config.RelayCom1);
			usbRelay2.OpenComPort(Config.RelayCom2);

			string testType = cboTestType.Text;
			TestType eTestType = (TestType) Enum.Parse(typeof (TestType), testType);

			TestProgramStep testStep = new TestProgramStep {TestType = testType};
			stations[stationNumber].SwitchTestType(eTestType);
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

			this.Height = panelHeight*2 + yOffSet + 50;
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
						                    Height = 60,
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
			usbRelay = new UsbRelay(Config.RelayCom1, lblRelayStatus1);
			usbRelay2 = new UsbRelay(Config.RelayCom2, lblRelayStatus2);

			switches[0] = new RelayMux(usbRelay, new[] { 0, 1 });
			toggles[0] = new MultiPoleSwitch(usbRelay, new[] { 2, 3 });
			switches[1] = new RelayMux(usbRelay, new[] { 4, 5 });
			toggles[1] = new MultiPoleSwitch(usbRelay, new[] { 6, 7 });
			switches[2] = new RelayMux(usbRelay, new[] { 8, 9 });
			toggles[2] = new MultiPoleSwitch(usbRelay, new[] { 10, 11 });
			switches[3] = new RelayMux(usbRelay, new[] { 12, 13 });
			toggles[3] = new MultiPoleSwitch(usbRelay, new[] { 14, 15 });
			switches[4] = new RelayMux(usbRelay, new[] { 16, 17 });
			toggles[4] = new MultiPoleSwitch(usbRelay, new[] { 18, 19 });
			switches[5] = new RelayMux(usbRelay, new[] { 20, 21 });
			toggles[5] = new MultiPoleSwitch(usbRelay, new[] { 22, 23 });
			switches[6] = new RelayMux(usbRelay, new[] { 24, 25 });
			toggles[6] = new MultiPoleSwitch(usbRelay, new[] { 26, 27 });
			switches[7] = new RelayMux(usbRelay, new[] { 28, 29 });
			toggles[7] = new MultiPoleSwitch(usbRelay, new[] { 30, 31 });

			mux[0] = new RelayMux(usbRelay2, new[] {0, 1, 2, 3, 4, 5, 6});
			mux[1] = new RelayMux(usbRelay2, new[] {7, 8, 9, 10, 11, 12, 13 });
			mux[2] = new RelayMux(usbRelay2, new[] {14, 15, 16, 17, 18, 19, 20 });
			mux[3] = new RelayMux(usbRelay2, new[] {21, 22, 23, 24, 25, 26, 27});

			Multimeter = new Multimeter(usbRelay2, new[] {28, 29, 30, 31});

			for (int i = 0; i < NStations; i++ )
				stations[i] = new TestStation(i, toggles[i], switches[i]);
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
			MeterRequest.AbortThread();
			usbRelay.Close();
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
	}
}
