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
			this.chkIgnoreLeadTime = new System.Windows.Forms.CheckBox();
			this.chkShowReverseCharge = new System.Windows.Forms.CheckBox();
			this.chkShowForwardCharge = new System.Windows.Forms.CheckBox();
			this.chkShowDischarge = new System.Windows.Forms.CheckBox();
			this.chkShowOpenCircuit = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.chkShowBRefVoltage = new System.Windows.Forms.CheckBox();
			this.chkShowARefVoltage = new System.Windows.Forms.CheckBox();
			this.chkShowABCurrent = new System.Windows.Forms.CheckBox();
			this.chkShowABVoltage = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.lbtnAddDataSet = new System.Windows.Forms.LinkLabel();
			this.chkIntegrate = new System.Windows.Forms.CheckBox();
			this.chkInvertGraph = new System.Windows.Forms.CheckBox();
			this.lbtnRefresh = new System.Windows.Forms.LinkLabel();
			this.label3 = new System.Windows.Forms.Label();
			this.chkLogarithmic = new System.Windows.Forms.CheckBox();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.chkShowMolarConcentration = new System.Windows.Forms.CheckBox();
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
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1072, 612);
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
			this.tabControl1.Size = new System.Drawing.Size(1066, 606);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.splitContainer1);
			this.tabPage1.Location = new System.Drawing.Point(4, 25);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(1058, 577);
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
			this.splitContainer1.Panel2.Controls.Add(this.lbtnRefresh);
			this.splitContainer1.Panel2.Controls.Add(this.lbtnAddDataSet);
			this.splitContainer1.Panel2.Controls.Add(this.chkIgnoreLeadTime);
			this.splitContainer1.Panel2.Controls.Add(this.chkShowBRefVoltage);
			this.splitContainer1.Panel2.Controls.Add(this.chkShowMolarConcentration);
			this.splitContainer1.Panel2.Controls.Add(this.label2);
			this.splitContainer1.Panel2.Controls.Add(this.chkInvertGraph);
			this.splitContainer1.Panel2.Controls.Add(this.chkShowARefVoltage);
			this.splitContainer1.Panel2.Controls.Add(this.chkIntegrate);
			this.splitContainer1.Panel2.Controls.Add(this.chkShowOpenCircuit);
			this.splitContainer1.Panel2.Controls.Add(this.chkLogarithmic);
			this.splitContainer1.Panel2.Controls.Add(this.chkShowABCurrent);
			this.splitContainer1.Panel2.Controls.Add(this.chkShowDischarge);
			this.splitContainer1.Panel2.Controls.Add(this.chkShowReverseCharge);
			this.splitContainer1.Panel2.Controls.Add(this.chkShowABVoltage);
			this.splitContainer1.Panel2.Controls.Add(this.label1);
			this.splitContainer1.Panel2.Controls.Add(this.chkShowForwardCharge);
			this.splitContainer1.Panel2.Controls.Add(this.label3);
			this.splitContainer1.Panel2MinSize = 30;
			this.splitContainer1.Size = new System.Drawing.Size(1052, 571);
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
			this.dgvDataGrid.Size = new System.Drawing.Size(842, 571);
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
			this.dataGrid.Size = new System.Drawing.Size(842, 571);
			this.dataGrid.TabIndex = 0;
			// 
			// chkIgnoreLeadTime
			// 
			this.chkIgnoreLeadTime.AutoSize = true;
			this.chkIgnoreLeadTime.Checked = true;
			this.chkIgnoreLeadTime.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkIgnoreLeadTime.Location = new System.Drawing.Point(6, 412);
			this.chkIgnoreLeadTime.Name = "chkIgnoreLeadTime";
			this.chkIgnoreLeadTime.Size = new System.Drawing.Size(141, 21);
			this.chkIgnoreLeadTime.TabIndex = 14;
			this.chkIgnoreLeadTime.Text = "Ignore Lead Time";
			this.chkIgnoreLeadTime.UseVisualStyleBackColor = true;
			// 
			// chkShowReverseCharge
			// 
			this.chkShowReverseCharge.AutoSize = true;
			this.chkShowReverseCharge.Checked = true;
			this.chkShowReverseCharge.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkShowReverseCharge.Location = new System.Drawing.Point(5, 277);
			this.chkShowReverseCharge.Name = "chkShowReverseCharge";
			this.chkShowReverseCharge.Size = new System.Drawing.Size(133, 21);
			this.chkShowReverseCharge.TabIndex = 10;
			this.chkShowReverseCharge.Text = "Reverse Charge";
			this.chkShowReverseCharge.UseVisualStyleBackColor = true;
			// 
			// chkShowForwardCharge
			// 
			this.chkShowForwardCharge.AutoSize = true;
			this.chkShowForwardCharge.Checked = true;
			this.chkShowForwardCharge.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkShowForwardCharge.Location = new System.Drawing.Point(5, 250);
			this.chkShowForwardCharge.Name = "chkShowForwardCharge";
			this.chkShowForwardCharge.Size = new System.Drawing.Size(131, 21);
			this.chkShowForwardCharge.TabIndex = 9;
			this.chkShowForwardCharge.Text = "Forward Charge";
			this.chkShowForwardCharge.UseVisualStyleBackColor = true;
			// 
			// chkShowDischarge
			// 
			this.chkShowDischarge.AutoSize = true;
			this.chkShowDischarge.Checked = true;
			this.chkShowDischarge.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkShowDischarge.Location = new System.Drawing.Point(5, 223);
			this.chkShowDischarge.Name = "chkShowDischarge";
			this.chkShowDischarge.Size = new System.Drawing.Size(94, 21);
			this.chkShowDischarge.TabIndex = 8;
			this.chkShowDischarge.Text = "Discharge";
			this.chkShowDischarge.UseVisualStyleBackColor = true;
			// 
			// chkShowOpenCircuit
			// 
			this.chkShowOpenCircuit.AutoSize = true;
			this.chkShowOpenCircuit.Checked = true;
			this.chkShowOpenCircuit.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkShowOpenCircuit.Location = new System.Drawing.Point(5, 196);
			this.chkShowOpenCircuit.Name = "chkShowOpenCircuit";
			this.chkShowOpenCircuit.Size = new System.Drawing.Size(108, 21);
			this.chkShowOpenCircuit.TabIndex = 7;
			this.chkShowOpenCircuit.Text = "Open Circuit";
			this.chkShowOpenCircuit.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(2, 176);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(110, 17);
			this.label2.TabIndex = 11;
			this.label2.Text = "Show Test Type";
			// 
			// chkShowBRefVoltage
			// 
			this.chkShowBRefVoltage.AutoSize = true;
			this.chkShowBRefVoltage.Location = new System.Drawing.Point(6, 116);
			this.chkShowBRefVoltage.Name = "chkShowBRefVoltage";
			this.chkShowBRefVoltage.Size = new System.Drawing.Size(118, 21);
			this.chkShowBRefVoltage.TabIndex = 4;
			this.chkShowBRefVoltage.Text = "B-Ref Voltage";
			this.chkShowBRefVoltage.UseVisualStyleBackColor = true;
			// 
			// chkShowARefVoltage
			// 
			this.chkShowARefVoltage.AutoSize = true;
			this.chkShowARefVoltage.Location = new System.Drawing.Point(6, 89);
			this.chkShowARefVoltage.Name = "chkShowARefVoltage";
			this.chkShowARefVoltage.Size = new System.Drawing.Size(118, 21);
			this.chkShowARefVoltage.TabIndex = 3;
			this.chkShowARefVoltage.Text = "A-Ref Voltage";
			this.chkShowARefVoltage.UseVisualStyleBackColor = true;
			// 
			// chkShowABCurrent
			// 
			this.chkShowABCurrent.AutoSize = true;
			this.chkShowABCurrent.Checked = true;
			this.chkShowABCurrent.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkShowABCurrent.Location = new System.Drawing.Point(6, 62);
			this.chkShowABCurrent.Name = "chkShowABCurrent";
			this.chkShowABCurrent.Size = new System.Drawing.Size(99, 21);
			this.chkShowABCurrent.TabIndex = 2;
			this.chkShowABCurrent.Text = "AB Current";
			this.chkShowABCurrent.UseVisualStyleBackColor = true;
			// 
			// chkShowABVoltage
			// 
			this.chkShowABVoltage.AutoSize = true;
			this.chkShowABVoltage.Checked = true;
			this.chkShowABVoltage.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkShowABVoltage.Location = new System.Drawing.Point(6, 38);
			this.chkShowABVoltage.Name = "chkShowABVoltage";
			this.chkShowABVoltage.Size = new System.Drawing.Size(100, 21);
			this.chkShowABVoltage.TabIndex = 0;
			this.chkShowABVoltage.Text = "AB Voltage";
			this.chkShowABVoltage.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(76, 17);
			this.label1.TabIndex = 1;
			this.label1.Text = "Show Data";
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
			// chkIntegrate
			// 
			this.chkIntegrate.AutoSize = true;
			this.chkIntegrate.Location = new System.Drawing.Point(6, 331);
			this.chkIntegrate.Name = "chkIntegrate";
			this.chkIntegrate.Size = new System.Drawing.Size(131, 21);
			this.chkIntegrate.TabIndex = 12;
			this.chkIntegrate.Text = "Integrate values";
			this.chkIntegrate.UseVisualStyleBackColor = true;
			// 
			// chkInvertGraph
			// 
			this.chkInvertGraph.AutoSize = true;
			this.chkInvertGraph.Location = new System.Drawing.Point(6, 385);
			this.chkInvertGraph.Name = "chkInvertGraph";
			this.chkInvertGraph.Size = new System.Drawing.Size(109, 21);
			this.chkInvertGraph.TabIndex = 13;
			this.chkInvertGraph.Text = "Invert Graph";
			this.chkInvertGraph.UseVisualStyleBackColor = true;
			// 
			// lbtnRefresh
			// 
			this.lbtnRefresh.AutoSize = true;
			this.lbtnRefresh.Location = new System.Drawing.Point(3, 436);
			this.lbtnRefresh.Name = "lbtnRefresh";
			this.lbtnRefresh.Size = new System.Drawing.Size(58, 17);
			this.lbtnRefresh.TabIndex = 6;
			this.lbtnRefresh.TabStop = true;
			this.lbtnRefresh.Text = "Refresh";
			this.lbtnRefresh.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbtnRefresh_LinkClicked);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(10, 311);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(114, 17);
			this.label3.TabIndex = 15;
			this.label3.Text = "Charting Options";
			// 
			// chkLogarithmic
			// 
			this.chkLogarithmic.AutoSize = true;
			this.chkLogarithmic.Location = new System.Drawing.Point(6, 358);
			this.chkLogarithmic.Name = "chkLogarithmic";
			this.chkLogarithmic.Size = new System.Drawing.Size(142, 21);
			this.chkLogarithmic.TabIndex = 16;
			this.chkLogarithmic.Text = "Logarithmic Scale";
			this.chkLogarithmic.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.chart1);
			this.tabPage2.Location = new System.Drawing.Point(4, 25);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(1058, 577);
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
			this.chart1.Size = new System.Drawing.Size(1052, 571);
			this.chart1.TabIndex = 0;
			this.chart1.Text = "chart1";
			// 
			// chkShowMolarConcentration
			// 
			this.chkShowMolarConcentration.AutoSize = true;
			this.chkShowMolarConcentration.Location = new System.Drawing.Point(6, 143);
			this.chkShowMolarConcentration.Name = "chkShowMolarConcentration";
			this.chkShowMolarConcentration.Size = new System.Drawing.Size(157, 21);
			this.chkShowMolarConcentration.TabIndex = 16;
			this.chkShowMolarConcentration.Text = "Molar Concentration";
			this.chkShowMolarConcentration.UseVisualStyleBackColor = true;
			// 
			// ViewResultsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1072, 612);
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
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox chkShowBRefVoltage;
		private System.Windows.Forms.CheckBox chkShowABVoltage;
		private System.Windows.Forms.CheckBox chkShowARefVoltage;
		private System.Windows.Forms.CheckBox chkShowABCurrent;
		private System.Windows.Forms.LinkLabel lbtnAddDataSet;
		private System.Windows.Forms.DataGridView dgvDataGrid;
		private System.Windows.Forms.LinkLabel lbtnRefresh;
		private System.Windows.Forms.CheckBox chkShowOpenCircuit;
		private System.Windows.Forms.CheckBox chkShowDischarge;
		private System.Windows.Forms.CheckBox chkShowForwardCharge;
		private System.Windows.Forms.CheckBox chkShowReverseCharge;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox chkIntegrate;
		private System.Windows.Forms.CheckBox chkInvertGraph;
		private System.Windows.Forms.CheckBox chkIgnoreLeadTime;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox chkLogarithmic;
		private System.Windows.Forms.CheckBox chkShowMolarConcentration;

	}
}