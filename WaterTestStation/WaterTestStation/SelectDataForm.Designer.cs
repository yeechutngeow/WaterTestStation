namespace WaterTestStation
{
	partial class SelectDataForm
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
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.lnkOK = new System.Windows.Forms.LinkLabel();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.TestStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.DataSet = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Sample = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Desription = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.TestSummary = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.flowLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.label1);
			this.flowLayoutPanel1.Controls.Add(this.lnkOK);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(1053, 31);
			this.flowLayoutPanel1.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(400, 20);
			this.label1.TabIndex = 3;
			this.label1.Text = "Select Data Set";
			// 
			// lnkOK
			// 
			this.lnkOK.AutoSize = true;
			this.lnkOK.Location = new System.Drawing.Point(409, 0);
			this.lnkOK.Name = "lnkOK";
			this.lnkOK.Size = new System.Drawing.Size(28, 17);
			this.lnkOK.TabIndex = 4;
			this.lnkOK.TabStop = true;
			this.lnkOK.Text = "OK";
			this.lnkOK.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkOK_LinkClicked);
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.TestStart,
            this.DataSet,
            this.Sample,
            this.Desription,
            this.TestSummary});
			this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView1.Location = new System.Drawing.Point(0, 31);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.RowTemplate.Height = 24;
			this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView1.Size = new System.Drawing.Size(1053, 626);
			this.dataGridView1.TabIndex = 1;
			// 
			// Id
			// 
			this.Id.DataPropertyName = "Id";
			this.Id.HeaderText = "Id";
			this.Id.Name = "Id";
			this.Id.ReadOnly = true;
			this.Id.Visible = false;
			// 
			// TestStart
			// 
			this.TestStart.DataPropertyName = "TestStart";
			this.TestStart.HeaderText = "Test Date";
			this.TestStart.Name = "TestStart";
			this.TestStart.ReadOnly = true;
			this.TestStart.Width = 95;
			// 
			// DataSet
			// 
			this.DataSet.DataPropertyName = "DataSet";
			this.DataSet.HeaderText = "Data Set";
			this.DataSet.Name = "DataSet";
			this.DataSet.ReadOnly = true;
			this.DataSet.Width = 88;
			// 
			// Sample
			// 
			this.Sample.DataPropertyName = "Sample";
			this.Sample.HeaderText = "Sample";
			this.Sample.Name = "Sample";
			this.Sample.ReadOnly = true;
			this.Sample.Width = 80;
			// 
			// Desription
			// 
			this.Desription.DataPropertyName = "Description";
			this.Desription.HeaderText = "Description";
			this.Desription.Name = "Desription";
			this.Desription.ReadOnly = true;
			this.Desription.Width = 104;
			// 
			// TestSummary
			// 
			this.TestSummary.DataPropertyName = "TestSummary";
			this.TestSummary.HeaderText = "Test Summary";
			this.TestSummary.Name = "TestSummary";
			this.TestSummary.ReadOnly = true;
			this.TestSummary.Width = 124;
			// 
			// SelectDataForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1053, 657);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.flowLayoutPanel1);
			this.Name = "SelectDataForm";
			this.Text = "SelectDataForm";
			this.Load += new System.EventHandler(this.SelectDataForm_Load);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.LinkLabel lnkOK;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Id;
		private System.Windows.Forms.DataGridViewTextBoxColumn TestStart;
		private System.Windows.Forms.DataGridViewTextBoxColumn DataSet;
		private System.Windows.Forms.DataGridViewTextBoxColumn Sample;
		private System.Windows.Forms.DataGridViewTextBoxColumn Desription;
		private System.Windows.Forms.DataGridViewTextBoxColumn TestSummary;
	}
}