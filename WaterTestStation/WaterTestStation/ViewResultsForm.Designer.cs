namespace WaterTestStation
{
	partial class ViewResultsForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.dgvDataGrid = new System.Windows.Forms.DataGridView();
			this.dataGrid = new System.Windows.Forms.DataGridView();
			this.lbtnRefreshChart = new System.Windows.Forms.LinkLabel();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.txtY2Max = new System.Windows.Forms.TextBox();
			this.txtY2Min = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.txtY1Max = new System.Windows.Forms.TextBox();
			this.txtY1Min = new System.Windows.Forms.TextBox();
			this.chkInvertGraph2 = new System.Windows.Forms.CheckBox();
			this.chkPlotAverage = new System.Windows.Forms.CheckBox();
			this.chkAxis2Logarithmic = new System.Windows.Forms.CheckBox();
			this.cboYaxis2 = new System.Windows.Forms.ComboBox();
			this.cboYaxis1 = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.chkShowReferenceVoltage = new System.Windows.Forms.CheckBox();
			this.chkUseCustomColor = new System.Windows.Forms.CheckBox();
			this.lbtnRefresh = new System.Windows.Forms.LinkLabel();
			this.lbtnAddDataSet = new System.Windows.Forms.LinkLabel();
			this.chkIgnoreLeadTime = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.chkInvertGraph1 = new System.Windows.Forms.CheckBox();
			this.chkShowOpenCircuit = new System.Windows.Forms.CheckBox();
			this.chkAxis1Logarithmic = new System.Windows.Forms.CheckBox();
			this.chkShowDischarge = new System.Windows.Forms.CheckBox();
			this.chkShowReverseCharge = new System.Windows.Forms.CheckBox();
			this.chkShowForwardCharge = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.tableLayoutPanel1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvDataGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
			this.tabPage2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1072, 654);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(3, 3);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(1066, 648);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.splitContainer1);
			this.tabPage1.Location = new System.Drawing.Point(4, 25);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(1058, 619);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Data";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer1.Location = new System.Drawing.Point(3, 3);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.dgvDataGrid);
			this.splitContainer1.Panel1.Controls.Add(this.dataGrid);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.lbtnRefreshChart);
			this.splitContainer1.Panel2.Controls.Add(this.label7);
			this.splitContainer1.Panel2.Controls.Add(this.label8);
			this.splitContainer1.Panel2.Controls.Add(this.txtY2Max);
			this.splitContainer1.Panel2.Controls.Add(this.txtY2Min);
			this.splitContainer1.Panel2.Controls.Add(this.label6);
			this.splitContainer1.Panel2.Controls.Add(this.label5);
			this.splitContainer1.Panel2.Controls.Add(this.txtY1Max);
			this.splitContainer1.Panel2.Controls.Add(this.txtY1Min);
			this.splitContainer1.Panel2.Controls.Add(this.chkInvertGraph2);
			this.splitContainer1.Panel2.Controls.Add(this.chkPlotAverage);
			this.splitContainer1.Panel2.Controls.Add(this.chkAxis2Logarithmic);
			this.splitContainer1.Panel2.Controls.Add(this.cboYaxis2);
			this.splitContainer1.Panel2.Controls.Add(this.cboYaxis1);
			this.splitContainer1.Panel2.Controls.Add(this.label4);
			this.splitContainer1.Panel2.Controls.Add(this.label1);
			this.splitContainer1.Panel2.Controls.Add(this.chkShowReferenceVoltage);
			this.splitContainer1.Panel2.Controls.Add(this.chkUseCustomColor);
			this.splitContainer1.Panel2.Controls.Add(this.lbtnRefresh);
			this.splitContainer1.Panel2.Controls.Add(this.lbtnAddDataSet);
			this.splitContainer1.Panel2.Controls.Add(this.chkIgnoreLeadTime);
			this.splitContainer1.Panel2.Controls.Add(this.label2);
			this.splitContainer1.Panel2.Controls.Add(this.chkInvertGraph1);
			this.splitContainer1.Panel2.Controls.Add(this.chkShowOpenCircuit);
			this.splitContainer1.Panel2.Controls.Add(this.chkAxis1Logarithmic);
			this.splitContainer1.Panel2.Controls.Add(this.chkShowDischarge);
			this.splitContainer1.Panel2.Controls.Add(this.chkShowReverseCharge);
			this.splitContainer1.Panel2.Controls.Add(this.chkShowForwardCharge);
			this.splitContainer1.Panel2.Controls.Add(this.label3);
			this.splitContainer1.Panel2MinSize = 30;
			this.splitContainer1.Size = new System.Drawing.Size(1052, 613);
			this.splitContainer1.SplitterDistance = 842;
			this.splitContainer1.TabIndex = 0;
			// 
			// dgvDataGrid
			// 
			this.dgvDataGrid.AllowUserToAddRows = false;
			this.dgvDataGrid.AllowUserToDeleteRows = false;
			this.dgvDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvDataGrid.Location = new System.Drawing.Point(0, 0);
			this.dgvDataGrid.Name = "dgvDataGrid";
			this.dgvDataGrid.ReadOnly = true;
			this.dgvDataGrid.RowTemplate.Height = 24;
			this.dgvDataGrid.Size = new System.Drawing.Size(842, 613);
			this.dgvDataGrid.TabIndex = 1;
			this.dgvDataGrid.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvDataGrid_ColumnWidthChanged);
			// 
			// dataGrid
			// 
			this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGrid.Location = new System.Drawing.Point(0, 0);
			this.dataGrid.Name = "dataGrid";
			this.dataGrid.RowTemplate.Height = 24;
			this.dataGrid.Size = new System.Drawing.Size(842, 613);
			this.dataGrid.TabIndex = 0;
			// 
			// lbtnRefreshChart
			// 
			this.lbtnRefreshChart.AutoSize = true;
			this.lbtnRefreshChart.Location = new System.Drawing.Point(5, 579);
			this.lbtnRefreshChart.Name = "lbtnRefreshChart";
			this.lbtnRefreshChart.Size = new System.Drawing.Size(96, 17);
			this.lbtnRefreshChart.TabIndex = 34;
			this.lbtnRefreshChart.TabStop = true;
			this.lbtnRefreshChart.Text = "Refresh Chart";
			this.lbtnRefreshChart.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbtnRefreshChart_LinkClicked);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(15, 427);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(49, 17);
			this.label7.TabIndex = 33;
			this.label7.Text = "range:";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(133, 427);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(13, 17);
			this.label8.TabIndex = 32;
			this.label8.Text = "-";
			// 
			// txtY2Max
			// 
			this.txtY2Max.Location = new System.Drawing.Point(149, 422);
			this.txtY2Max.Name = "txtY2Max";
			this.txtY2Max.Size = new System.Drawing.Size(54, 22);
			this.txtY2Max.TabIndex = 31;
			this.txtY2Max.Text = "35";
			// 
			// txtY2Min
			// 
			this.txtY2Min.Location = new System.Drawing.Point(72, 423);
			this.txtY2Min.Name = "txtY2Min";
			this.txtY2Min.Size = new System.Drawing.Size(57, 22);
			this.txtY2Min.TabIndex = 30;
			this.txtY2Min.Text = "0";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(15, 292);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(49, 17);
			this.label6.TabIndex = 29;
			this.label6.Text = "range:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(133, 292);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(13, 17);
			this.label5.TabIndex = 28;
			this.label5.Text = "-";
			// 
			// txtY1Max
			// 
			this.txtY1Max.Location = new System.Drawing.Point(149, 287);
			this.txtY1Max.Name = "txtY1Max";
			this.txtY1Max.Size = new System.Drawing.Size(54, 22);
			this.txtY1Max.TabIndex = 27;
			this.txtY1Max.Text = "5E-6";
			// 
			// txtY1Min
			// 
			this.txtY1Min.Location = new System.Drawing.Point(72, 288);
			this.txtY1Min.Name = "txtY1Min";
			this.txtY1Min.Size = new System.Drawing.Size(57, 22);
			this.txtY1Min.TabIndex = 26;
			this.txtY1Min.Text = "-1E-6";
			// 
			// chkInvertGraph2
			// 
			this.chkInvertGraph2.AutoSize = true;
			this.chkInvertGraph2.Location = new System.Drawing.Point(8, 400);
			this.chkInvertGraph2.Name = "chkInvertGraph2";
			this.chkInvertGraph2.Size = new System.Drawing.Size(109, 21);
			this.chkInvertGraph2.TabIndex = 25;
			this.chkInvertGraph2.Text = "Invert Graph";
			this.chkInvertGraph2.UseVisualStyleBackColor = true;
			// 
			// chkPlotAverage
			// 
			this.chkPlotAverage.AutoSize = true;
			this.chkPlotAverage.Location = new System.Drawing.Point(8, 491);
			this.chkPlotAverage.Name = "chkPlotAverage";
			this.chkPlotAverage.Size = new System.Drawing.Size(149, 21);
			this.chkPlotAverage.TabIndex = 24;
			this.chkPlotAverage.Text = "Plot average curve";
			this.chkPlotAverage.UseVisualStyleBackColor = true;
			// 
			// chkAxis2Logarithmic
			// 
			this.chkAxis2Logarithmic.AutoSize = true;
			this.chkAxis2Logarithmic.Location = new System.Drawing.Point(6, 373);
			this.chkAxis2Logarithmic.Name = "chkAxis2Logarithmic";
			this.chkAxis2Logarithmic.Size = new System.Drawing.Size(142, 21);
			this.chkAxis2Logarithmic.TabIndex = 23;
			this.chkAxis2Logarithmic.Text = "Logarithmic Scale";
			this.chkAxis2Logarithmic.UseVisualStyleBackColor = true;
			// 
			// cboYaxis2
			// 
			this.cboYaxis2.FormattingEnabled = true;
			this.cboYaxis2.Items.AddRange(new object[] {
            "",
            "Voltage",
            "Current",
            "Temperature",
            "Light",
            "Cumulative Integral",
            "Impedance"});
			this.cboYaxis2.Location = new System.Drawing.Point(6, 343);
			this.cboYaxis2.Name = "cboYaxis2";
			this.cboYaxis2.Size = new System.Drawing.Size(179, 24);
			this.cboYaxis2.TabIndex = 22;
			this.cboYaxis2.SelectedIndexChanged += new System.EventHandler(this.cboYaxis2_SelectedIndexChanged);
			// 
			// cboYaxis1
			// 
			this.cboYaxis1.FormattingEnabled = true;
			this.cboYaxis1.Items.AddRange(new object[] {
            "Voltage",
            "Current",
            "Current Integral",
            "Impedance"});
			this.cboYaxis1.Location = new System.Drawing.Point(6, 209);
			this.cboYaxis1.Name = "cboYaxis1";
			this.cboYaxis1.Size = new System.Drawing.Size(179, 24);
			this.cboYaxis1.TabIndex = 21;
			this.cboYaxis1.SelectedIndexChanged += new System.EventHandler(this.cboYaxis1_SelectedIndexChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(3, 323);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(62, 17);
			this.label4.TabIndex = 20;
			this.label4.Text = "Y-axis 2:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(2, 189);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(62, 17);
			this.label1.TabIndex = 19;
			this.label1.Text = "Y-axis 1:";
			// 
			// chkShowReferenceVoltage
			// 
			this.chkShowReferenceVoltage.AutoSize = true;
			this.chkShowReferenceVoltage.Location = new System.Drawing.Point(6, 155);
			this.chkShowReferenceVoltage.Name = "chkShowReferenceVoltage";
			this.chkShowReferenceVoltage.Size = new System.Drawing.Size(179, 21);
			this.chkShowReferenceVoltage.TabIndex = 18;
			this.chkShowReferenceVoltage.Text = "Show reference voltage";
			this.chkShowReferenceVoltage.UseVisualStyleBackColor = true;
			// 
			// chkUseCustomColor
			// 
			this.chkUseCustomColor.AutoSize = true;
			this.chkUseCustomColor.Location = new System.Drawing.Point(8, 542);
			this.chkUseCustomColor.Name = "chkUseCustomColor";
			this.chkUseCustomColor.Size = new System.Drawing.Size(158, 21);
			this.chkUseCustomColor.TabIndex = 17;
			this.chkUseCustomColor.Text = "Match color to name";
			this.chkUseCustomColor.UseVisualStyleBackColor = true;
			// 
			// lbtnRefresh
			// 
			this.lbtnRefresh.AutoSize = true;
			this.lbtnRefresh.Location = new System.Drawing.Point(121, 0);
			this.lbtnRefresh.Name = "lbtnRefresh";
			this.lbtnRefresh.Size = new System.Drawing.Size(92, 17);
			this.lbtnRefresh.TabIndex = 6;
			this.lbtnRefresh.TabStop = true;
			this.lbtnRefresh.Text = "Refresh Data";
			this.lbtnRefresh.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbtnRefresh_LinkClicked);
			// 
			// lbtnAddDataSet
			// 
			this.lbtnAddDataSet.AutoSize = true;
			this.lbtnAddDataSet.Location = new System.Drawing.Point(3, 0);
			this.lbtnAddDataSet.Name = "lbtnAddDataSet";
			this.lbtnAddDataSet.Size = new System.Drawing.Size(106, 17);
			this.lbtnAddDataSet.TabIndex = 5;
			this.lbtnAddDataSet.TabStop = true;
			this.lbtnAddDataSet.Text = "Select Data Set";
			this.lbtnAddDataSet.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbtnAddDataSet_LinkClicked);
			// 
			// chkIgnoreLeadTime
			// 
			this.chkIgnoreLeadTime.AutoSize = true;
			this.chkIgnoreLeadTime.Checked = true;
			this.chkIgnoreLeadTime.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkIgnoreLeadTime.Location = new System.Drawing.Point(7, 515);
			this.chkIgnoreLeadTime.Name = "chkIgnoreLeadTime";
			this.chkIgnoreLeadTime.Size = new System.Drawing.Size(141, 21);
			this.chkIgnoreLeadTime.TabIndex = 14;
			this.chkIgnoreLeadTime.Text = "Ignore Lead Time";
			this.chkIgnoreLeadTime.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 27);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(76, 17);
			this.label2.TabIndex = 11;
			this.label2.Text = "Show Data";
			// 
			// chkInvertGraph1
			// 
			this.chkInvertGraph1.AutoSize = true;
			this.chkInvertGraph1.Location = new System.Drawing.Point(6, 262);
			this.chkInvertGraph1.Name = "chkInvertGraph1";
			this.chkInvertGraph1.Size = new System.Drawing.Size(109, 21);
			this.chkInvertGraph1.TabIndex = 13;
			this.chkInvertGraph1.Text = "Invert Graph";
			this.chkInvertGraph1.UseVisualStyleBackColor = true;
			// 
			// chkShowOpenCircuit
			// 
			this.chkShowOpenCircuit.AutoSize = true;
			this.chkShowOpenCircuit.Location = new System.Drawing.Point(6, 47);
			this.chkShowOpenCircuit.Name = "chkShowOpenCircuit";
			this.chkShowOpenCircuit.Size = new System.Drawing.Size(108, 21);
			this.chkShowOpenCircuit.TabIndex = 7;
			this.chkShowOpenCircuit.Text = "Open Circuit";
			this.chkShowOpenCircuit.UseVisualStyleBackColor = true;
			this.chkShowOpenCircuit.CheckedChanged += new System.EventHandler(this.chkShowOpenCircuit_CheckedChanged);
			// 
			// chkAxis1Logarithmic
			// 
			this.chkAxis1Logarithmic.AutoSize = true;
			this.chkAxis1Logarithmic.Location = new System.Drawing.Point(6, 235);
			this.chkAxis1Logarithmic.Name = "chkAxis1Logarithmic";
			this.chkAxis1Logarithmic.Size = new System.Drawing.Size(142, 21);
			this.chkAxis1Logarithmic.TabIndex = 16;
			this.chkAxis1Logarithmic.Text = "Logarithmic Scale";
			this.chkAxis1Logarithmic.UseVisualStyleBackColor = true;
			// 
			// chkShowDischarge
			// 
			this.chkShowDischarge.AutoSize = true;
			this.chkShowDischarge.Checked = true;
			this.chkShowDischarge.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkShowDischarge.Location = new System.Drawing.Point(6, 74);
			this.chkShowDischarge.Name = "chkShowDischarge";
			this.chkShowDischarge.Size = new System.Drawing.Size(94, 21);
			this.chkShowDischarge.TabIndex = 8;
			this.chkShowDischarge.Text = "Discharge";
			this.chkShowDischarge.UseVisualStyleBackColor = true;
			this.chkShowDischarge.CheckedChanged += new System.EventHandler(this.chkShowDischarge_CheckedChanged);
			// 
			// chkShowReverseCharge
			// 
			this.chkShowReverseCharge.AutoSize = true;
			this.chkShowReverseCharge.Location = new System.Drawing.Point(6, 128);
			this.chkShowReverseCharge.Name = "chkShowReverseCharge";
			this.chkShowReverseCharge.Size = new System.Drawing.Size(133, 21);
			this.chkShowReverseCharge.TabIndex = 10;
			this.chkShowReverseCharge.Text = "Reverse Charge";
			this.chkShowReverseCharge.UseVisualStyleBackColor = true;
			this.chkShowReverseCharge.CheckedChanged += new System.EventHandler(this.chkShowReverseCharge_CheckedChanged);
			// 
			// chkShowForwardCharge
			// 
			this.chkShowForwardCharge.AutoSize = true;
			this.chkShowForwardCharge.Location = new System.Drawing.Point(6, 101);
			this.chkShowForwardCharge.Name = "chkShowForwardCharge";
			this.chkShowForwardCharge.Size = new System.Drawing.Size(131, 21);
			this.chkShowForwardCharge.TabIndex = 9;
			this.chkShowForwardCharge.Text = "Forward Charge";
			this.chkShowForwardCharge.UseVisualStyleBackColor = true;
			this.chkShowForwardCharge.CheckedChanged += new System.EventHandler(this.chkShowForwardCharge_CheckedChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(5, 465);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(114, 17);
			this.label3.TabIndex = 15;
			this.label3.Text = "Charting Options";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.chart1);
			this.tabPage2.Location = new System.Drawing.Point(4, 25);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(1058, 619);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Chart";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// chart1
			// 
			chartArea1.Name = "ChartArea1";
			this.chart1.ChartAreas.Add(chartArea1);
			this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
			legend1.Name = "Legend1";
			this.chart1.Legends.Add(legend1);
			this.chart1.Location = new System.Drawing.Point(3, 3);
			this.chart1.Name = "chart1";
			series1.ChartArea = "ChartArea1";
			series1.Legend = "Legend1";
			series1.Name = "Series1";
			this.chart1.Series.Add(series1);
			this.chart1.Size = new System.Drawing.Size(1052, 613);
			this.chart1.TabIndex = 0;
			this.chart1.Text = "chart1";
			// 
			// ViewResultsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1072, 654);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "ViewResultsForm";
			this.Text = "ViewResultsForm";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvDataGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
			this.tabPage2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.DataGridView dataGrid;
		private System.Windows.Forms.LinkLabel lbtnAddDataSet;
		private System.Windows.Forms.DataGridView dgvDataGrid;
		private System.Windows.Forms.LinkLabel lbtnRefresh;
		private System.Windows.Forms.CheckBox chkShowOpenCircuit;
		private System.Windows.Forms.CheckBox chkShowDischarge;
		private System.Windows.Forms.CheckBox chkShowForwardCharge;
		private System.Windows.Forms.CheckBox chkShowReverseCharge;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox chkInvertGraph1;
		private System.Windows.Forms.CheckBox chkIgnoreLeadTime;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox chkAxis1Logarithmic;
		private System.Windows.Forms.CheckBox chkUseCustomColor;
		private System.Windows.Forms.ComboBox cboYaxis2;
		private System.Windows.Forms.ComboBox cboYaxis1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox chkShowReferenceVoltage;
		private System.Windows.Forms.CheckBox chkAxis2Logarithmic;
		private System.Windows.Forms.CheckBox chkPlotAverage;
		private System.Windows.Forms.CheckBox chkInvertGraph2;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtY1Max;
		private System.Windows.Forms.TextBox txtY1Min;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox txtY2Max;
		private System.Windows.Forms.TextBox txtY2Min;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.LinkLabel lbtnRefreshChart;

	}
}