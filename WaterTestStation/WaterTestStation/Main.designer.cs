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
			this.label1 = new System.Windows.Forms.Label();
			this.txtUsbRelayComPort = new System.Windows.Forms.TextBox();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.programsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewTestResultMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.cboStationNumber = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.cboTestType = new System.Windows.Forms.ComboBox();
			this.btnAdhocReading = new System.Windows.Forms.Button();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(139, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "Usb Relay Com Port:";
			// 
			// txtUsbRelayComPort
			// 
			this.txtUsbRelayComPort.Location = new System.Drawing.Point(157, 32);
			this.txtUsbRelayComPort.Name = "txtUsbRelayComPort";
			this.txtUsbRelayComPort.Size = new System.Drawing.Size(34, 22);
			this.txtUsbRelayComPort.TabIndex = 1;
			this.txtUsbRelayComPort.Text = "7";
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.programsToolStripMenuItem,
            this.viewTestResultMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(1120, 28);
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
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(218, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(109, 17);
			this.label2.TabIndex = 3;
			this.label2.Text = "Adhoc Reading:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(333, 32);
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
            "6"});
			this.cboStationNumber.Location = new System.Drawing.Point(395, 30);
			this.cboStationNumber.Name = "cboStationNumber";
			this.cboStationNumber.Size = new System.Drawing.Size(45, 24);
			this.cboStationNumber.TabIndex = 5;
			this.cboStationNumber.Text = "1";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(446, 32);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(65, 17);
			this.label4.TabIndex = 6;
			this.label4.Text = "Reading:";
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
			this.cboTestType.Location = new System.Drawing.Point(517, 30);
			this.cboTestType.Name = "cboTestType";
			this.cboTestType.Size = new System.Drawing.Size(141, 24);
			this.cboTestType.TabIndex = 7;
			// 
			// btnAdhocReading
			// 
			this.btnAdhocReading.Location = new System.Drawing.Point(664, 30);
			this.btnAdhocReading.Name = "btnAdhocReading";
			this.btnAdhocReading.Size = new System.Drawing.Size(100, 26);
			this.btnAdhocReading.TabIndex = 8;
			this.btnAdhocReading.Text = "Get Reading";
			this.btnAdhocReading.UseVisualStyleBackColor = true;
			this.btnAdhocReading.Click += new System.EventHandler(this.btnAdhocReading_Click);
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1120, 446);
			this.Controls.Add(this.btnAdhocReading);
			this.Controls.Add(this.cboTestType);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.cboStationNumber);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtUsbRelayComPort);
			this.Controls.Add(this.label1);
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

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtUsbRelayComPort;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem programsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem viewTestResultMenuItem;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox cboStationNumber;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox cboTestType;
		private System.Windows.Forms.Button btnAdhocReading;
	}
}

