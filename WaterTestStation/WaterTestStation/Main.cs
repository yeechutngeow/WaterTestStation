using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
		public static bool HasMultimeter = true;
		public static bool HasRelay = true;

		public const int NStations = 4;
		//----- the hardware components ---------------------------------------------------------

		UsbRelay usbRelay;
		// switch between charge/discharge, one for each station
		private readonly RelayMux[] switches = new RelayMux[NStations];
		//
		private readonly MultiPoleSwitch[] toggles = new MultiPoleSwitch[NStations];
		// 4 multiplexer to select measuring points. Each mux has #station channels.

		// the 4 multiplexers for switching the multiplexer to the right channel
		public static readonly RelayMux[] mux = new RelayMux[4];

		readonly TestStation[] stations = new TestStation[6];

		public static Multimeter Multimeter;

		// multimeter queue 
		public static ConcurrentQueue<MeterRequest> MultimeterQueue = new ConcurrentQueue<MeterRequest>();

		public Main()
		{
			InitializeComponent();
			InitializeHardware();

			DrawForm();

			cboTestType.DataSource = Enum.GetValues(typeof(TestType));

			Multimeter.OpenSession();
			MeterRequest.StartServiceQueue();
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			int portNumber = int.Parse(txtUsbRelayComPort.Text);
			usbRelay.OpenComPort(portNumber);
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
			int portNumber = int.Parse(txtUsbRelayComPort.Text);
			usbRelay.OpenComPort(portNumber);
			int stationNumber = int.Parse(cboStationNumber.Text) - 1;
			string testType = cboTestType.Text;
			TestType eTestType = (TestType) Enum.Parse(typeof (TestType), testType);

			TestProgramStep testStep = new TestProgramStep {TestType = testType};
			stations[stationNumber].SwitchTestType(eTestType);
			MultimeterQueue.Enqueue(new MeterRequest(stations[stationNumber], testStep, 0, 0, 0, false));
		}

		private void DrawForm()
		{
			const int panelWidth = 400;
			const int panelHeight = 350;
			const int yOffSet = 60;
			const int xOffSet = 15;

			const int yLineHeight = 24;
			const int labelWidth = 90;
			const int col1 = 5;
			const int col2 = col1 + labelWidth;
			const int col3 = panelWidth/2 + 5;
			const int col4 = col3 + labelWidth;

			const int textWidth = panelWidth - col2 -5 ; 

			this.Height = panelHeight*2 + yOffSet + 50;
			this.Width = panelWidth*3 + xOffSet + 30;
			this.Location = new Point(10, 10);

			IList<TestProgram> testPrograms = new TestProgramDao().GetProgramsList();

			for (int i = 0; i < NStations; i++)
			{
				Panel panel = new Panel
				{
					Name = "panel" + (i), 
					Size = new Size(panelWidth, panelHeight),
					BorderStyle = BorderStyle.FixedSingle,
					Location = new Point(panelWidth * (i % 3) + xOffSet, panelHeight *(i / 3) + yOffSet)
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
					Text = "Program:",
					Location = new Point(col1, y),
					Width = labelWidth
				};
				panel.Controls.Add(label);

				ComboBox cboTestProgram = new ComboBox
				{
					DisplayMember = "Title",
					ValueMember = "Id",
					DataSource = testPrograms,
					Width = textWidth,
					Location = new Point(col2, y),
				};
				panel.Controls.Add(cboTestProgram);
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
					Text = "Cycles:",
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
					Text = "cycles to run",
					Font = new Font(this.Font.FontFamily, 7),
					Location = new Point(col2+55, y+4),
				};
				panel.Controls.Add(label);
				y += yLineHeight;

				//------------------------------------------------------------
				label = new Label
					{
						Text = "Run Status:",
						Location = new Point(col1, y),
						Width = labelWidth
					};
				panel.Controls.Add(label);
				Label lblStatus = new Label
					{
						Text = "Idle",
						Location = new Point(col2, y)
					};
				panel.Controls.Add(lblStatus);

				label = new Label
					{
						Text = "Cycle:",
						Width = labelWidth,
						Location = new Point(col3, y)
					};
				panel.Controls.Add(label);

				Label lblCycle = new Label
					{
						Width = 80,
						Location = new Point(col4, y)
					};
				panel.Controls.Add(lblCycle);
				y += yLineHeight;

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

				stations[i].SetFormControls(txtVesselId, txtSample, txtTestDescription, cboTestProgram, txtCycles, lblStatus, lblCycle,
					lblARefVolt, lblBRefVolt, lblABAmp, lblABVolt, btnStart, btnStop);
			}
		}

		private void InitializeHardware()
		{
			usbRelay = new UsbRelay(txtUsbRelayComPort.Text);

			switches[0] = new RelayMux(usbRelay, new[] {  0, 1});
			toggles[0] = new MultiPoleSwitch(usbRelay, new[] { 2, 3 });
			switches[1] = new RelayMux(usbRelay, new[] { 4, 5 });
			toggles[1] = new MultiPoleSwitch(usbRelay, new[] { 6, 7 });
			switches[2] = new RelayMux(usbRelay, new[] { 8, 9 });
			toggles[2] = new MultiPoleSwitch(usbRelay, new[] { 10, 11 });
			switches[3] = new RelayMux(usbRelay, new[] { 12, 13 });
			toggles[3] = new MultiPoleSwitch(usbRelay, new[] { 14, 15 });
//			switches[4] = new RelayMux(usbRelay, new[] { 18, 19 });
//			toggles[4] = new MultiPoleSwitch(usbRelay, new[] { 16, 17 });
//			switches[5] = new RelayMux(usbRelay, new[] { 22, 23 });
//			toggles[5] = new MultiPoleSwitch(usbRelay, new[] { 20, 21 });

			mux[0] = new RelayMux(usbRelay, new[] {20, 21, 22});
			mux[1] = new RelayMux(usbRelay, new[] {23, 24, 25 });
			mux[2] = new RelayMux(usbRelay, new[] {26, 27, 28 });
			mux[3] = new RelayMux(usbRelay, new[] {29, 30, 31});

			Multimeter = new Multimeter(usbRelay, new[] {16,17,18,19});

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
	}
}
