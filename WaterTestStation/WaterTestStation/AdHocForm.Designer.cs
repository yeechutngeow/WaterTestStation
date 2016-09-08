namespace WaterTestStation
{
	partial class AdHocForm
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
			this.cboStationNumber = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.cboSamplingRate = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.txtTestId = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.txtDescription = new System.Windows.Forms.TextBox();
			this.btnStart = new System.Windows.Forms.Button();
			this.btnEnd = new System.Windows.Forms.Button();
			this.txtStatus = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.txtSample = new System.Windows.Forms.TextBox();
			this.chkReferenceElectrode = new System.Windows.Forms.CheckBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.lblARefVolt = new System.Windows.Forms.Label();
			this.lblBRefVolt = new System.Windows.Forms.Label();
			this.lblABAmp = new System.Windows.Forms.Label();
			this.lblABVolt = new System.Windows.Forms.Label();
			this.cboTestType = new System.Windows.Forms.ComboBox();
			this.label12 = new System.Windows.Forms.Label();
			this.lblStopwatch = new System.Windows.Forms.Label();
			this.btnChangeTestType = new System.Windows.Forms.Button();
			this.label13 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.chkContinuousSampling = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
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
            "7"});
			this.cboStationNumber.Location = new System.Drawing.Point(104, 12);
			this.cboStationNumber.Name = "cboStationNumber";
			this.cboStationNumber.Size = new System.Drawing.Size(47, 24);
			this.cboStationNumber.TabIndex = 9;
			this.cboStationNumber.Text = "1";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(13, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 17);
			this.label3.TabIndex = 8;
			this.label3.Text = "Station:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 45);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 17);
			this.label1.TabIndex = 11;
			this.label1.Text = "Step:";
			// 
			// cboSamplingRate
			// 
			this.cboSamplingRate.FormattingEnabled = true;
			this.cboSamplingRate.Items.AddRange(new object[] {
            "5",
            "10",
            "15",
            "20",
            "30"});
			this.cboSamplingRate.Location = new System.Drawing.Point(151, 113);
			this.cboSamplingRate.Name = "cboSamplingRate";
			this.cboSamplingRate.Size = new System.Drawing.Size(56, 24);
			this.cboSamplingRate.TabIndex = 24;
			this.cboSamplingRate.Text = "20";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(10, 119);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(141, 17);
			this.label2.TabIndex = 23;
			this.label2.Text = "Sampling interval (s):";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(10, 157);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(55, 17);
			this.label4.TabIndex = 25;
			this.label4.Text = "Test Id:";
			// 
			// txtTestId
			// 
			this.txtTestId.Location = new System.Drawing.Point(101, 157);
			this.txtTestId.Name = "txtTestId";
			this.txtTestId.Size = new System.Drawing.Size(167, 22);
			this.txtTestId.TabIndex = 26;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(11, 256);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(83, 17);
			this.label5.TabIndex = 27;
			this.label5.Text = "Description:";
			// 
			// txtDescription
			// 
			this.txtDescription.Location = new System.Drawing.Point(101, 256);
			this.txtDescription.Multiline = true;
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.Size = new System.Drawing.Size(333, 67);
			this.txtDescription.TabIndex = 28;
			// 
			// btnStart
			// 
			this.btnStart.Location = new System.Drawing.Point(101, 335);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(123, 31);
			this.btnStart.TabIndex = 29;
			this.btnStart.Text = "Start";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.btnStartLogging_Click);
			// 
			// btnEnd
			// 
			this.btnEnd.Location = new System.Drawing.Point(230, 335);
			this.btnEnd.Name = "btnEnd";
			this.btnEnd.Size = new System.Drawing.Size(123, 31);
			this.btnEnd.TabIndex = 30;
			this.btnEnd.Text = "End";
			this.btnEnd.UseVisualStyleBackColor = true;
			this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
			// 
			// txtStatus
			// 
			this.txtStatus.Enabled = false;
			this.txtStatus.Location = new System.Drawing.Point(252, 417);
			this.txtStatus.Multiline = true;
			this.txtStatus.Name = "txtStatus";
			this.txtStatus.ReadOnly = true;
			this.txtStatus.Size = new System.Drawing.Size(202, 88);
			this.txtStatus.TabIndex = 31;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(249, 397);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(52, 17);
			this.label6.TabIndex = 32;
			this.label6.Text = "Status:";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(12, 192);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(59, 17);
			this.label7.TabIndex = 33;
			this.label7.Text = "Sample:";
			// 
			// txtSample
			// 
			this.txtSample.Location = new System.Drawing.Point(101, 192);
			this.txtSample.Name = "txtSample";
			this.txtSample.Size = new System.Drawing.Size(167, 22);
			this.txtSample.TabIndex = 34;
			// 
			// chkReferenceElectrode
			// 
			this.chkReferenceElectrode.AutoSize = true;
			this.chkReferenceElectrode.Checked = true;
			this.chkReferenceElectrode.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkReferenceElectrode.Location = new System.Drawing.Point(101, 221);
			this.chkReferenceElectrode.Name = "chkReferenceElectrode";
			this.chkReferenceElectrode.Size = new System.Drawing.Size(160, 21);
			this.chkReferenceElectrode.TabIndex = 35;
			this.chkReferenceElectrode.Text = "Reference Electrode";
			this.chkReferenceElectrode.UseVisualStyleBackColor = true;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(12, 401);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(76, 17);
			this.label8.TabIndex = 36;
			this.label8.Text = "A-Ref Volt:";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(11, 431);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(76, 17);
			this.label9.TabIndex = 37;
			this.label9.Text = "B-Ref Volt:";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(10, 459);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(62, 17);
			this.label10.TabIndex = 38;
			this.label10.Text = "AB Amp:";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(12, 488);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(58, 17);
			this.label11.TabIndex = 39;
			this.label11.Text = "AB Volt:";
			// 
			// lblARefVolt
			// 
			this.lblARefVolt.AutoSize = true;
			this.lblARefVolt.Location = new System.Drawing.Point(98, 401);
			this.lblARefVolt.Name = "lblARefVolt";
			this.lblARefVolt.Size = new System.Drawing.Size(37, 17);
			this.lblARefVolt.TabIndex = 40;
			this.lblARefVolt.Text = "0 pA";
			// 
			// lblBRefVolt
			// 
			this.lblBRefVolt.AutoSize = true;
			this.lblBRefVolt.Location = new System.Drawing.Point(98, 431);
			this.lblBRefVolt.Name = "lblBRefVolt";
			this.lblBRefVolt.Size = new System.Drawing.Size(37, 17);
			this.lblBRefVolt.TabIndex = 41;
			this.lblBRefVolt.Text = "0 pA";
			// 
			// lblABAmp
			// 
			this.lblABAmp.AutoSize = true;
			this.lblABAmp.Location = new System.Drawing.Point(98, 459);
			this.lblABAmp.Name = "lblABAmp";
			this.lblABAmp.Size = new System.Drawing.Size(37, 17);
			this.lblABAmp.TabIndex = 42;
			this.lblABAmp.Text = "0 pA";
			// 
			// lblABVolt
			// 
			this.lblABVolt.AutoSize = true;
			this.lblABVolt.Location = new System.Drawing.Point(98, 488);
			this.lblABVolt.Name = "lblABVolt";
			this.lblABVolt.Size = new System.Drawing.Size(29, 17);
			this.lblABVolt.TabIndex = 43;
			this.lblABVolt.Text = "0 V";
			// 
			// cboTestType
			// 
			this.cboTestType.FormattingEnabled = true;
			this.cboTestType.Items.AddRange(new object[] {
            "Open Circuit",
            "Discharge",
            "Forward Charge",
            "Reverse Charge"});
			this.cboTestType.Location = new System.Drawing.Point(104, 42);
			this.cboTestType.Name = "cboTestType";
			this.cboTestType.Size = new System.Drawing.Size(150, 24);
			this.cboTestType.TabIndex = 44;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(377, 15);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(77, 17);
			this.label12.TabIndex = 45;
			this.label12.Text = "Stopwatch:";
			// 
			// lblStopwatch
			// 
			this.lblStopwatch.AutoSize = true;
			this.lblStopwatch.Location = new System.Drawing.Point(460, 15);
			this.lblStopwatch.Name = "lblStopwatch";
			this.lblStopwatch.Size = new System.Drawing.Size(36, 17);
			this.lblStopwatch.TabIndex = 46;
			this.lblStopwatch.Text = "0:00";
			// 
			// btnChangeTestType
			// 
			this.btnChangeTestType.Location = new System.Drawing.Point(272, 39);
			this.btnChangeTestType.Name = "btnChangeTestType";
			this.btnChangeTestType.Size = new System.Drawing.Size(72, 27);
			this.btnChangeTestType.TabIndex = 47;
			this.btnChangeTestType.Text = "Change";
			this.btnChangeTestType.UseVisualStyleBackColor = true;
			this.btnChangeTestType.Click += new System.EventHandler(this.btnChangeTestType_Click);
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(13, 75);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(49, 17);
			this.label13.TabIndex = 48;
			this.label13.Text = "Notes:";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(104, 72);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(293, 22);
			this.textBox1.TabIndex = 49;
			// 
			// chkContinuousSampling
			// 
			this.chkContinuousSampling.AutoSize = true;
			this.chkContinuousSampling.Checked = true;
			this.chkContinuousSampling.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkContinuousSampling.Location = new System.Drawing.Point(230, 116);
			this.chkContinuousSampling.Name = "chkContinuousSampling";
			this.chkContinuousSampling.Size = new System.Drawing.Size(163, 21);
			this.chkContinuousSampling.TabIndex = 50;
			this.chkContinuousSampling.Text = "Continuous Sampling";
			this.chkContinuousSampling.UseVisualStyleBackColor = true;
			// 
			// AdHocForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(541, 521);
			this.Controls.Add(this.chkContinuousSampling);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.btnChangeTestType);
			this.Controls.Add(this.lblStopwatch);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.cboTestType);
			this.Controls.Add(this.lblABVolt);
			this.Controls.Add(this.lblABAmp);
			this.Controls.Add(this.lblBRefVolt);
			this.Controls.Add(this.lblARefVolt);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.chkReferenceElectrode);
			this.Controls.Add(this.txtSample);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.txtStatus);
			this.Controls.Add(this.btnEnd);
			this.Controls.Add(this.btnStart);
			this.Controls.Add(this.txtDescription);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.txtTestId);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.cboSamplingRate);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cboStationNumber);
			this.Controls.Add(this.label3);
			this.Name = "AdHocForm";
			this.Text = "AdHocForm";
			this.Load += new System.EventHandler(this.AdHocForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox cboStationNumber;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
		public System.Windows.Forms.ComboBox cboSamplingRate;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtTestId;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtDescription;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.Button btnEnd;
		private System.Windows.Forms.TextBox txtStatus;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox txtSample;
		private System.Windows.Forms.CheckBox chkReferenceElectrode;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label lblARefVolt;
		private System.Windows.Forms.Label lblBRefVolt;
		private System.Windows.Forms.Label lblABAmp;
		private System.Windows.Forms.Label lblABVolt;
		private System.Windows.Forms.ComboBox cboTestType;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label lblStopwatch;
		private System.Windows.Forms.Button btnChangeTestType;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.CheckBox chkContinuousSampling;
	}
}