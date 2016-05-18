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
			this.lblRelayStatus1 = new System.Windows.Forms.Label();
			this.lblRelayStatus2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.btnRefresh = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.cboTestProgram = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.txtTestDataSet = new System.Windows.Forms.TextBox();
			this.menuStrip1.SuspendLayout();
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
			this.menuStrip1.Size = new System.Drawing.Size(1567, 28);
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
			this.cboTestType.Size = new System.Drawing.Size(141, 24);
			this.cboTestType.TabIndex = 7;
			// 
			// btnAdhocReading
			// 
			this.btnAdhocReading.Location = new System.Drawing.Point(428, 38);
			this.btnAdhocReading.Name = "btnAdhocReading";
			this.btnAdhocReading.Size = new System.Drawing.Size(100, 26);
			this.btnAdhocReading.TabIndex = 8;
			this.btnAdhocReading.Text = "Get Reading";
			this.btnAdhocReading.UseVisualStyleBackColor = true;
			this.btnAdhocReading.Click += new System.EventHandler(this.btnAdhocReading_Click);
			// 
			// lblRelayStatus1
			// 
			this.lblRelayStatus1.AutoSize = true;
			this.lblRelayStatus1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblRelayStatus1.Location = new System.Drawing.Point(1384, 33);
			this.lblRelayStatus1.Name = "lblRelayStatus1";
			this.lblRelayStatus1.Size = new System.Drawing.Size(160, 18);
			this.lblRelayStatus1.TabIndex = 9;
			this.lblRelayStatus1.Text = "0000 0000 0000 0000";
			// 
			// lblRelayStatus2
			// 
			this.lblRelayStatus2.AutoSize = true;
			this.lblRelayStatus2.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblRelayStatus2.Location = new System.Drawing.Point(1384, 50);
			this.lblRelayStatus2.Name = "lblRelayStatus2";
			this.lblRelayStatus2.Size = new System.Drawing.Size(160, 18);
			this.lblRelayStatus2.TabIndex = 10;
			this.lblRelayStatus2.Text = "0000 0000 0000 0000";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(1322, 33);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 18);
			this.label1.TabIndex = 11;
			this.label1.Text = "Relay1:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(1322, 50);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(64, 18);
			this.label5.TabIndex = 12;
			this.label5.Text = "Relay2:";
			// 
			// btnRefresh
			// 
			this.btnRefresh.Location = new System.Drawing.Point(942, 38);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(64, 26);
			this.btnRefresh.TabIndex = 13;
			this.btnRefresh.Text = "Refresh";
			this.btnRefresh.UseVisualStyleBackColor = true;
			this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(563, 43);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(98, 17);
			this.label6.TabIndex = 14;
			this.label6.Text = "Test Program:";
			// 
			// cboTestProgram
			// 
			this.cboTestProgram.FormattingEnabled = true;
			this.cboTestProgram.Location = new System.Drawing.Point(667, 39);
			this.cboTestProgram.Name = "cboTestProgram";
			this.cboTestProgram.Size = new System.Drawing.Size(267, 24);
			this.cboTestProgram.TabIndex = 15;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(1026, 43);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(99, 17);
			this.label7.TabIndex = 16;
			this.label7.Text = "Test Data Set:";
			// 
			// txtTestDataSet
			// 
			this.txtTestDataSet.Location = new System.Drawing.Point(1131, 38);
			this.txtTestDataSet.Name = "txtTestDataSet";
			this.txtTestDataSet.Size = new System.Drawing.Size(100, 22);
			this.txtTestDataSet.TabIndex = 17;
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1567, 446);
			this.Controls.Add(this.txtTestDataSet);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.cboTestProgram);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.btnRefresh);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lblRelayStatus2);
			this.Controls.Add(this.lblRelayStatus1);
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
		private System.Windows.Forms.Label lblRelayStatus1;
		private System.Windows.Forms.Label lblRelayStatus2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.Label label6;
		public System.Windows.Forms.ComboBox cboTestProgram;
		private System.Windows.Forms.Label label7;
		public System.Windows.Forms.TextBox txtTestDataSet;
	}
}

