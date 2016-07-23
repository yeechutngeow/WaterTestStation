using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WaterTestStation.dao;

namespace WaterTestStation
{
	public partial class SelectDataForm : Form
	{
		public IList<int> selectedData = new List<int>();

		public SelectDataForm()
		{
			InitializeComponent();
		}

		private void SelectDataForm_Load(object sender, EventArgs e)
		{
			dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
			dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
			dataGridView1.AutoGenerateColumns = false;
			dataGridView1.Columns[1].DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" };
			TestRecordDao testRecordDao = new TestRecordDao();
			dataGridView1.DataSource = testRecordDao.FindAll();
		}

		private void lnkOK_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			foreach (DataGridViewRow  row in dataGridView1.SelectedRows)
			{
				selectedData.Add((int)row.Cells[0].Value);
			}
			
			this.Close();
		}
	}
}
