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
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.cboStationNumber = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.cboTestType = new System.Windows.Forms.ComboBox();
			this.btnAdhocReading = new System.Windows.Forms.Button();
			this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
			this.label2.Location = new System.Drawing.Point(448, 30);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(109, 17);
			this.label2.TabIndex = 3;
			this.label2.Text = "Adhoc Reading:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(573, 30);
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
			this.cboStationNumber.Location = new System.Drawing.Point(635, 28);
			this.cboStationNumber.Name = "cboStationNumber";
			this.cboStationNumber.Size = new System.Drawing.Size(45, 24);
			this.cboStationNumber.TabIndex = 5;
			this.cboStationNumber.Text = "1";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(686, 30);
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
			this.cboTestType.Location = new System.Drawing.Point(757, 28);
			this.cboTestType.Name = "cboTestType";
			this.cboTestType.Size = new System.Drawing.Size(141, 24);
			this.cboTestType.TabIndex = 7;
			// 
			// btnAdhocReading
			// 
			this.btnAdhocReading.Location = new System.Drawing.Point(904, 28);
			this.btnAdhocReading.Name = "btnAdhocReading";
			this.btnAdhocReading.Size = new System.Drawing.Size(100, 26);
			this.btnAdhocReading.TabIndex = 8;
			this.btnAdhocReading.Text = "Get Reading";
			this.btnAdhocReading.UseVisualStyleBackColor = true;
			this.btnAdhocReading.Click += new System.EventHandler(this.btnAdhocReading_Click);
			// 
			// propertiesToolStripMenuItem
			// 
			this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
			this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(88, 24);
			this.propertiesToolStripMenuItem.Text = "Properties";
			this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
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
	}
}

