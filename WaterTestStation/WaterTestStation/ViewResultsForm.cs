using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using WaterTestStation.dao;
using WaterTestStation.model;

namespace WaterTestStation
{
	public partial class ViewResultsForm : Form
	{
		readonly SortedSet<int> selectedDataSet = new SortedSet<int>();
		readonly IList<string[]> matrix = new List<string[]>();
		readonly DataTable dataTable = new DataTable();

		readonly TestRecordDao testRecordDao = new TestRecordDao();

		public ViewResultsForm()
		{
			InitializeComponent();
			this.ResizeRedraw = true;
		}


		private void lbtnAddDataSet_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			var f = new SelectDataForm();
			f.ShowDialog();
			selectedDataSet.Clear();
			foreach (int dataSetId in f.selectedData)
			{
				selectedDataSet.Add(dataSetId);
			}
			RefreshData();
		}


		private void _compileData()
		{
			dataTable.Clear();
			dataTable.Columns.Clear();

			matrix.Clear();
			// calculate the number of columns
			int nDataShown = Util.CountTrue(chkShowARefVoltage.Checked, chkShowBRefVoltage.Checked, chkShowABCurrent.Checked,
			                           chkShowABVoltage.Checked);
			int nColumns = (nDataShown*selectedDataSet.Count) + 2;

			for (int a = 0; a < nColumns; a++)
			{
				DataColumn dc = new DataColumn();
				dataTable.Columns.Add(dc);
			}

			int j = 0;
			foreach (int testRecordId in selectedDataSet)
			{
				IList<TestData> dataSet = testRecordDao.GetTestData(testRecordId);
				TestRecord testRecord = testRecordDao.FindById(testRecordId);

				SetColumnNames(j, nDataShown, testRecord);

				int i = 0;
				foreach (TestData testData in dataSet)
				{
					// expand the number of rows if necessary
					if (matrix.Count <= i)
					{
						string[] row = new string[nColumns];
						TimeSpan ts = TimeSpan.FromSeconds(testData.ElapsedTime);
						row[0] = ts.ToString(@"hh\:mm\:ss");
						row[1] = testData.TestType;
						matrix.Add(row);
					}

					// if elapsed time is not aligned - skip the rest
					//if (matrix[i][0] != testData.ElapsedTime.ToString())
					//	break;

					int col = j*nDataShown + 2;
					if (chkShowARefVoltage.Checked)
						matrix[i][col++] = Util.formatNumber(testData.ARefVoltage, "V");
					if (chkShowBRefVoltage.Checked)
						matrix[i][col++] = Util.formatNumber(testData.BRefVoltage, "V");
					if (chkShowABVoltage.Checked)
						matrix[i][col++] = Util.formatNumber(testData.ABVoltage, "V");
					if (chkShowABCurrent.Checked)
						matrix[i][col] = Util.formatNumber(testData.ABCurrent, "A");
					
					i++;
				}
				j++;
			}
		}

		private void SetColumnNames(int setNumber, int nDataShown, TestRecord testRecord)
		{
			int col = setNumber*nDataShown + 2;
			if (chkShowARefVoltage.Checked)
				_setColName(dataTable.Columns[col++], testRecord.Sample + ":ARef");
			if (chkShowBRefVoltage.Checked)
				_setColName(dataTable.Columns[col++], testRecord.Sample + ":BRef");
			if (chkShowABVoltage.Checked)
				_setColName(dataTable.Columns[col++], testRecord.Sample + ":ABVolt");
			if (chkShowABCurrent.Checked)
				_setColName(dataTable.Columns[col], testRecord.Sample + ":ABAmp");
		}

		private void _setColName(DataColumn dc, string name)
		{
			try
			{
				dc.ColumnName = name;
			}
			catch
			{
			}
		}


		private void RefreshData()
		{
			_compileData();

			dataTable.Columns[0].ColumnName = "Elapsed";
			dataTable.Columns[1].ColumnName = "Step";
			foreach (string[] t in matrix)
				dataTable.LoadDataRow(t, true);

			dgvDataGrid.DataSource = dataTable;
			foreach (DataGridViewColumn col in dgvDataGrid.Columns)
				col.Width = 60;
		}

		private void lbtnRefresh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			RefreshData();
		}
	}
}
