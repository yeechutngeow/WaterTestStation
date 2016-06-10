namespace WaterTestStation
{
	partial class Main
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
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.programsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewTestResultMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.cboStationNumber = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.cboTestType = new System.Windows.Forms.ComboBox();
			this.btnAdhocReading = new System.Windows.Forms.Button();
			this.btnRefresh = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.cboTestProgram = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.txtTestDataSet = new System.Windows.Forms.TextBox();
			this.chkReferenceElectrode = new System.Windows.Forms.CheckBox();
			this.chkFastSampling = new System.Windows.Forms.CheckBox();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.lblRelayStatus1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
			this.lblRelayStatus2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.lblTemperature = new System.Windows.Forms.ToolStripStatusLabel();
			this.menuStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.programsToolStripMenuItem,
            this.viewTestResultMenuItem,
            this.propertiesToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(1805, 28);
			this.menuStrip1.TabIndex = 2;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// programsToolStripMenuItem
			// 
			this.programsToolStripMenuItem.Name = "programsToolStripMenuItem";
			this.programsToolStripMenuItem.Size = new System.Drawing.Size(84, 24);
			this.programsToolStripMenuItem.Text = "Programs";
			// 
			// viewTestResultMenuItem
			// 
			this.viewTestResultMenuItem.Name = "viewTestResultMenuItem";
			this.viewTestResultMenuItem.Size = new System.Drawing.Size(133, 24);
			this.viewTestResultMenuItem.Text = "View Test Results";
			this.viewTestResultMenuItem.Click += new System.EventHandler(this.viewTestResultMenuItem_Click);
			// 
			// propertiesToolStripMenuItem
			// 
			this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
			this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(88, 24);
			this.propertiesToolStripMenuItem.Text = "Properties";
			this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 43);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(109, 17);
			this.label2.TabIndex = 3;
			this.label2.Text = "Adhoc Reading:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(127, 43);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 17);
			this.label3.TabIndex = 4;
			this.label3.Text = "Station:";
			// 
			// cboStationNumber
			// 
			this.cboStationNumber.FormattingEnabled = true;
			this.cboStationNumber.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
			this.cboStationNumber.Location = new System.Drawing.Point(186, 39);
			this.cboStationNumber.Name = "cboStationNumber";
			this.cboStationNumber.Size = new System.Drawing.Size(47, 24);
			this.cboStationNumber.TabIndex = 5;
			this.cboStationNumber.Text = "1";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(239, 43);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(37, 17);
			this.label4.TabIndex = 6;
			this.label4.Text = "Step";
			// 
			// cboTestType
			// 
			this.cboTestType.FormattingEnabled = true;
			this.cboTestType.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
			this.cboTestType.Location = new System.Drawing.Point(281, 39);
			this.cboTestType.Name = "cboTestType";
			this.cboTestType.Size = new System.Drawing.Size(130, 24);
			this.cboTestType.TabIndex = 7;
			// 
			// btnAdhocReading
			// 
			this.btnAdhocReading.Location = new System.Drawing.Point(420, 38);
			this.btnAdhocReading.Name = "btnAdhocReading";
			this.btnAdhocReading.Size = new System.Drawing.Size(100, 26);
			this.btnAdhocReading.TabIndex = 8;
			this.btnAdhocReading.Text = "Get Reading";
			this.btnAdhocReading.UseVisualStyleBackColor = true;
			this.btnAdhocReading.Click += new System.EventHandler(this.btnAdhocReading_Click);
			// 
			// btnRefresh
			// 
			this.btnRefresh.Location = new System.Drawing.Point(926, 38);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(69, 26);
			this.btnRefresh.TabIndex = 13;
			this.btnRefresh.Text = "Refresh";
			this.btnRefresh.UseVisualStyleBackColor = true;
			this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(547, 43);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(98, 17);
			this.label6.TabIndex = 14;
			this.label6.Text = "Test Program:";
			// 
			// cboTestProgram
			// 
			this.cboTestProgram.FormattingEnabled = true;
			this.cboTestProgram.Location = new System.Drawing.Point(651, 39);
			this.cboTestProgram.Name = "cboTestProgram";
			this.cboTestProgram.Size = new System.Drawing.Size(267, 24);
			this.cboTestProgram.TabIndex = 15;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(1005, 43);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(82, 17);
			this.label7.TabIndex = 16;
			this.label7.Text = "Data Set Id:";
			// 
			// txtTestDataSet
			// 
			this.txtTestDataSet.Location = new System.Drawing.Point(1090, 38);
			this.txtTestDataSet.Name = "txtTestDataSet";
			this.txtTestDataSet.Size = new System.Drawing.Size(153, 22);
			this.txtTestDataSet.TabIndex = 17;
			// 
			// chkReferenceElectrode
			// 
			this.chkReferenceElectrode.AutoSize = true;
			this.chkReferenceElectrode.Location = new System.Drawing.Point(1266, 39);
			this.chkReferenceElectrode.Name = "chkReferenceElectrode";
			this.chkReferenceElectrode.Size = new System.Drawing.Size(116, 21);
			this.chkReferenceElectrode.TabIndex = 18;
			this.chkReferenceElectrode.Text = "Ref Electrode";
			this.chkReferenceElectrode.UseVisualStyleBackColor = true;
			// 
			// chkFastSampling
			// 
			this.chkFastSampling.AutoSize = true;
			this.chkFastSampling.Location = new System.Drawing.Point(1402, 38);
			this.chkFastSampling.Name = "chkFastSampling";
			this.chkFastSampling.Size = new System.Drawing.Size(156, 21);
			this.chkFastSampling.TabIndex = 19;
			this.chkFastSampling.Text = "Fast Sampling (30s)";
			this.chkFastSampling.UseVisualStyleBackColor = true;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lblRelayStatus1,
            this.toolStripStatusLabel3,
            this.lblRelayStatus2,
            this.toolStripStatusLabel2,
            this.lblTemperature});
			this.statusStrip1.Location = new System.Drawing.Point(0, 421);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(1805, 25);
			this.statusStrip1.TabIndex = 20;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(56, 20);
			this.toolStripStatusLabel1.Text = "Relay1:";
			// 
			// lblRelayStatus1
			// 
			this.lblRelayStatus1.Name = "lblRelayStatus1";
			this.lblRelayStatus1.Size = new System.Drawing.Size(149, 20);
			this.lblRelayStatus1.Text = "0000 0000 0000 0000";
			// 
			// toolStripStatusLabel3
			// 
			this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
			this.toolStripStatusLabel3.Size = new System.Drawing.Size(56, 20);
			this.toolStripStatusLabel3.Text = "Relay2:";
			// 
			// lblRelayStatus2
			// 
			this.lblRelayStatus2.Name = "lblRelayStatus2";
			this.lblRelayStatus2.Size = new System.Drawing.Size(149, 20);
			this.lblRelayStatus2.Text = "0000 0000 0000 0000";
			// 
			// toolStripStatusLabel2
			// 
			this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			this.toolStripStatusLabel2.Size = new System.Drawing.Size(96, 20);
			this.toolStripStatusLabel2.Text = "Temperature:";
			// 
			// lblTemperature
			// 
			this.lblTemperature.Name = "lblTemperature";
			this.lblTemperature.Size = new System.Drawing.Size(17, 20);
			this.lblTemperature.Text = "0";
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1805, 446);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.chkFastSampling);
			this.Controls.Add(this.chkReferenceElectrode);
			this.Controls.Add(this.txtTestDataSet);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.cboTestProgram);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.btnRefresh);
			this.Controls.Add(this.btnAdhocReading);
			this.Controls.Add(this.cboTestType);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.cboStationNumber);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.menuStrip1);
			this.Location = new System.Drawing.Point(10, 10);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "Main";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Water Test Control Panel";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
			this.Load += new System.EventHandler(this.Main_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem programsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem viewTestResultMenuItem;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox cboStationNumber;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox cboTestType;
		private System.Windows.Forms.Button btnAdhocReading;
		private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
		private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.Label label6;
		public System.Windows.Forms.ComboBox cboTestProgram;
		private System.Windows.Forms.Label label7;
		public System.Windows.Forms.TextBox txtTestDataSet;
		public System.Windows.Forms.CheckBox chkReferenceElectrode;
		public System.Windows.Forms.CheckBox chkFastSampling;
		public System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		public System.Windows.Forms.ToolStripStatusLabel lblRelayStatus1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
		private System.Windows.Forms.ToolStripStatusLabel lblRelayStatus2;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
		private System.Windows.Forms.ToolStripStatusLabel lblTemperature;
	}
}

