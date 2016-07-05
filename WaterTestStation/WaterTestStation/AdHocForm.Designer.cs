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
			this.btnStartLogging = new System.Windows.Forms.Button();
			this.btnStopLogging = new System.Windows.Forms.Button();
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
			this.label1.Size = new System.Drawing.Size(40, 17);
			this.label1.TabIndex = 11;
			this.label1.Text = "Test:";
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
			this.cboSamplingRate.Location = new System.Drawing.Point(154, 75);
			this.cboSamplingRate.Name = "cboSamplingRate";
			this.cboSamplingRate.Size = new System.Drawing.Size(56, 24);
			this.cboSamplingRate.TabIndex = 24;
			this.cboSamplingRate.Text = "20";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 81);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(141, 17);
			this.label2.TabIndex = 23;
			this.label2.Text = "Sampling interval (s):";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(13, 119);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(55, 17);
			this.label4.TabIndex = 25;
			this.label4.Text = "Test Id:";
			// 
			// txtTestId
			// 
			this.txtTestId.Location = new System.Drawing.Point(104, 119);
			this.txtTestId.Name = "txtTestId";
			this.txtTestId.Size = new System.Drawing.Size(167, 22);
			this.txtTestId.TabIndex = 26;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(14, 218);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(83, 17);
			this.label5.TabIndex = 27;
			this.label5.Text = "Description:";
			// 
			// txtDescription
			// 
			this.txtDescription.Location = new System.Drawing.Point(104, 218);
			this.txtDescription.Multiline = true;
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.Size = new System.Drawing.Size(333, 67);
			this.txtDescription.TabIndex = 28;
			// 
			// btnStartLogging
			// 
			this.btnStartLogging.Location = new System.Drawing.Point(104, 297);
			this.btnStartLogging.Name = "btnStartLogging";
			this.btnStartLogging.Size = new System.Drawing.Size(123, 31);
			this.btnStartLogging.TabIndex = 29;
			this.btnStartLogging.Text = "Start Logging";
			this.btnStartLogging.UseVisualStyleBackColor = true;
			this.btnStartLogging.Click += new System.EventHandler(this.btnStartLogging_Click);
			// 
			// btnStopLogging
			// 
			this.btnStopLogging.Location = new System.Drawing.Point(243, 297);
			this.btnStopLogging.Name = "btnStopLogging";
			this.btnStopLogging.Size = new System.Drawing.Size(123, 31);
			this.btnStopLogging.TabIndex = 30;
			this.btnStopLogging.Text = "Stop Logging";
			this.btnStopLogging.UseVisualStyleBackColor = true;
			// 
			// txtStatus
			// 
			this.txtStatus.Enabled = false;
			this.txtStatus.Location = new System.Drawing.Point(255, 379);
			this.txtStatus.Multiline = true;
			this.txtStatus.Name = "txtStatus";
			this.txtStatus.ReadOnly = true;
			this.txtStatus.Size = new System.Drawing.Size(202, 88);
			this.txtStatus.TabIndex = 31;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(252, 359);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(52, 17);
			this.label6.TabIndex = 32;
			this.label6.Text = "Status:";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(15, 154);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(59, 17);
			this.label7.TabIndex = 33;
			this.label7.Text = "Sample:";
			// 
			// txtSample
			// 
			this.txtSample.Location = new System.Drawing.Point(104, 154);
			this.txtSample.Name = "txtSample";
			this.txtSample.Size = new System.Drawing.Size(167, 22);
			this.txtSample.TabIndex = 34;
			// 
			// chkReferenceElectrode
			// 
			this.chkReferenceElectrode.AutoSize = true;
			this.chkReferenceElectrode.Checked = true;
			this.chkReferenceElectrode.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkReferenceElectrode.Location = new System.Drawing.Point(104, 183);
			this.chkReferenceElectrode.Name = "chkReferenceElectrode";
			this.chkReferenceElectrode.Size = new System.Drawing.Size(160, 21);
			this.chkReferenceElectrode.TabIndex = 35;
			this.chkReferenceElectrode.Text = "Reference Electrode";
			this.chkReferenceElectrode.UseVisualStyleBackColor = true;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(15, 363);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(76, 17);
			this.label8.TabIndex = 36;
			this.label8.Text = "A-Ref Volt:";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(14, 393);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(76, 17);
			this.label9.TabIndex = 37;
			this.label9.Text = "B-Ref Volt:";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(13, 421);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(62, 17);
			this.label10.TabIndex = 38;
			this.label10.Text = "AB Amp:";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(15, 450);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(58, 17);
			this.label11.TabIndex = 39;
			this.label11.Text = "AB Volt:";
			// 
			// lblARefVolt
			// 
			this.lblARefVolt.AutoSize = true;
			this.lblARefVolt.Location = new System.Drawing.Point(101, 363);
			this.lblARefVolt.Name = "lblARefVolt";
			this.lblARefVolt.Size = new System.Drawing.Size(37, 17);
			this.lblARefVolt.TabIndex = 40;
			this.lblARefVolt.Text = "0 pA";
			// 
			// lblBRefVolt
			// 
			this.lblBRefVolt.AutoSize = true;
			this.lblBRefVolt.Location = new System.Drawing.Point(101, 393);
			this.lblBRefVolt.Name = "lblBRefVolt";
			this.lblBRefVolt.Size = new System.Drawing.Size(37, 17);
			this.lblBRefVolt.TabIndex = 41;
			this.lblBRefVolt.Text = "0 pA";
			// 
			// lblABAmp
			// 
			this.lblABAmp.AutoSize = true;
			this.lblABAmp.Location = new System.Drawing.Point(101, 421);
			this.lblABAmp.Name = "lblABAmp";
			this.lblABAmp.Size = new System.Drawing.Size(37, 17);
			this.lblABAmp.TabIndex = 42;
			this.lblABAmp.Text = "0 pA";
			// 
			// lblABVolt
			// 
			this.lblABVolt.AutoSize = true;
			this.lblABVolt.Location = new System.Drawing.Point(101, 450);
			this.lblABVolt.Name = "lblABVolt";
			this.lblABVolt.Size = new System.Drawing.Size(29, 17);
			this.lblABVolt.TabIndex = 43;
			this.lblABVolt.Text = "0 V";
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
			this.cboTestType.Location = new System.Drawing.Point(104, 42);
			this.cboTestType.Name = "cboTestType";
			this.cboTestType.Size = new System.Drawing.Size(130, 24);
			this.cboTestType.TabIndex = 44;
			// 
			// AdHocForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(541, 490);
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
			this.Controls.Add(this.btnStopLogging);
			this.Controls.Add(this.btnStartLogging);
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
		private System.Windows.Forms.Button btnStartLogging;
		private System.Windows.Forms.Button btnStopLogging;
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
	}
}