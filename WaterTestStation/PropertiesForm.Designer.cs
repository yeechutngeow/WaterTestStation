namespace WaterTestStation
{
	partial class PropertiesForm
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
			this.label2 = new System.Windows.Forms.Label();
			this.txtCom1 = new System.Windows.Forms.TextBox();
			this.txtCom2 = new System.Windows.Forms.TextBox();
			this.chkHasRelay = new System.Windows.Forms.CheckBox();
			this.chkHasMultimeter = new System.Windows.Forms.CheckBox();
			this.btnSave = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.txtMultimeterDelay = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtTemperatureRefresh = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(36, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(158, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "USB Relay 1 Com (top):";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(36, 61);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(158, 17);
			this.label2.TabIndex = 1;
			this.label2.Text = "USB Relay 2 Com (bot):";
			// 
			// txtCom1
			// 
			this.txtCom1.Location = new System.Drawing.Point(200, 24);
			this.txtCom1.Name = "txtCom1";
			this.txtCom1.Size = new System.Drawing.Size(29, 22);
			this.txtCom1.TabIndex = 2;
			// 
			// txtCom2
			// 
			this.txtCom2.Location = new System.Drawing.Point(201, 61);
			this.txtCom2.Name = "txtCom2";
			this.txtCom2.Size = new System.Drawing.Size(29, 22);
			this.txtCom2.TabIndex = 3;
			// 
			// chkHasRelay
			// 
			this.chkHasRelay.AutoSize = true;
			this.chkHasRelay.Location = new System.Drawing.Point(39, 105);
			this.chkHasRelay.Name = "chkHasRelay";
			this.chkHasRelay.Size = new System.Drawing.Size(95, 21);
			this.chkHasRelay.TabIndex = 4;
			this.chkHasRelay.Text = "Has Relay";
			this.chkHasRelay.UseVisualStyleBackColor = true;
			// 
			// chkHasMultimeter
			// 
			this.chkHasMultimeter.AutoSize = true;
			this.chkHasMultimeter.Location = new System.Drawing.Point(39, 148);
			this.chkHasMultimeter.Name = "chkHasMultimeter";
			this.chkHasMultimeter.Size = new System.Drawing.Size(124, 21);
			this.chkHasMultimeter.TabIndex = 5;
			this.chkHasMultimeter.Text = "Has Multimeter";
			this.chkHasMultimeter.UseVisualStyleBackColor = true;
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(165, 290);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 27);
			this.btnSave.TabIndex = 6;
			this.btnSave.Text = "Save & Exit";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(36, 198);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(149, 17);
			this.label3.TabIndex = 7;
			this.label3.Text = "Multimeter Delay (ms):";
			// 
			// txtMultimeterDelay
			// 
			this.txtMultimeterDelay.Location = new System.Drawing.Point(209, 193);
			this.txtMultimeterDelay.Name = "txtMultimeterDelay";
			this.txtMultimeterDelay.Size = new System.Drawing.Size(40, 22);
			this.txtMultimeterDelay.TabIndex = 8;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(36, 235);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(165, 17);
			this.label4.TabIndex = 9;
			this.label4.Text = "Temperature Refresh (s)";
			// 
			// txtTemperatureRefresh
			// 
			this.txtTemperatureRefresh.Location = new System.Drawing.Point(209, 230);
			this.txtTemperatureRefresh.Name = "txtTemperatureRefresh";
			this.txtTemperatureRefresh.Size = new System.Drawing.Size(40, 22);
			this.txtTemperatureRefresh.TabIndex = 10;
			// 
			// PropertiesForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(297, 352);
			this.Controls.Add(this.txtTemperatureRefresh);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.txtMultimeterDelay);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.chkHasMultimeter);
			this.Controls.Add(this.chkHasRelay);
			this.Controls.Add(this.txtCom2);
			this.Controls.Add(this.txtCom1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "PropertiesForm";
			this.Text = "frmProperties";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtCom1;
		private System.Windows.Forms.TextBox txtCom2;
		private System.Windows.Forms.CheckBox chkHasRelay;
		private System.Windows.Forms.CheckBox chkHasMultimeter;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtMultimeterDelay;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtTemperatureRefresh;
	}
}