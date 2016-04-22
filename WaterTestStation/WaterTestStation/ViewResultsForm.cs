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

		// track user setting of column width
		int[] colWidth = new int[30];


		public ViewResultsForm()
		{
			InitializeComponent();
			this.ResizeRedraw = true;

			colWidth[0] = 50;
			colWidth[1] = 100;
			for (int i = 2; i < colWidth.Length; i++)
				colWidth[i] = 70;
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

			// calculate the number of columns
			int nDataShown = Util.CountTrue(chkShowARefVoltage.Checked, chkShowBRefVoltage.Checked, chkShowABCurrent.Checked,
			                           chkShowABVoltage.Checked);
			var nColumns = SetupColumns(nDataShown);

			matrix.Clear();
			int datasetNum = 0;
			foreach (int testRecordId in selectedDataSet)
			{
				IList<TestData> dataSet = testRecordDao.GetTestData(testRecordId);
				TestRecord testRecord = testRecordDao.FindById(testRecordId);

				SetColumnNames(datasetNum, nDataShown, testRecord);

				int rowNum = 0;
				foreach (TestData testData in dataSet)
				{
					// expand the number of rows if necessary
					if (matrix.Count <= rowNum)
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

					int col = datasetNum*nDataShown + 2;
					if (chkShowARefVoltage.Checked)
						matrix[rowNum][col++] = Util.formatNumber(testData.ARefVoltage, "V");
					if (chkShowBRefVoltage.Checked)
						matrix[rowNum][col++] = Util.formatNumber(testData.BRefVoltage, "V");
					if (chkShowABVoltage.Checked)
						matrix[rowNum][col++] = Util.formatNumber(testData.ABVoltage, "V");
					if (chkShowABCurrent.Checked)
						matrix[rowNum][col] = Util.formatNumber(testData.ABCurrent, "A");
					
					rowNum++;
				}
				datasetNum++;
			}
		}

		private int SetupColumns(int nDataShown)
		{
			int nColumns = (nDataShown*selectedDataSet.Count) + 2;
			dataTable.Columns.Clear();

			for (int a = 0; a < nColumns; a++)
			{
				dataTable.Columns.Add(new DataColumn());
			}
			return nColumns;
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

			for (int n = 0; n < dgvDataGrid.Columns.Count; n++)
			{
				dgvDataGrid.Columns[n].SortMode = DataGridViewColumnSortMode.NotSortable;
				dgvDataGrid.Columns[n].Width = colWidth[n];
			}
		}

		void dgvDataGrid_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
		{
			DataGridViewColumn column = e.Column;
			colWidth[column.Index] = column.Width;
		}

		private void lbtnRefresh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			RefreshData();
		}
	}
}
