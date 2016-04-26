using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using WaterTestStation.dao;
using WaterTestStation.model;

namespace WaterTestStation
{
	public partial class ViewResultsForm : Form
	{
		readonly SortedSet<int> selectedDataSet = new SortedSet<int>();

		// data matrix - rows of data, each row corresponds to an elapsed time
		readonly IList<string[]> matrix = new List<string[]>();
		// as above, storing un-formatted raw data
		readonly IList<string[]> matrix2 = new List<string[]>();
		// for converting data matrix into data source for grid - columns of data, each column is a data series.
		readonly DataTable dataTable = new DataTable();

		readonly TestRecordDao testRecordDao = new TestRecordDao();

		// track user setting of column width
		readonly int[] colWidth = new int[30];


		public ViewResultsForm()
		{
			InitializeComponent();
			this.ResizeRedraw = true;

			colWidth[0] = 55;
			colWidth[1] = 90;
			colWidth[2] = 35;
			for (int i = 3; i < colWidth.Length; i++)
				colWidth[i] = 70;


			//--- chart properties
			ChartArea ca = chart1.ChartAreas[0];

			ca.AxisX.ScrollBar.Enabled = true;
			ca.AxisY.ScrollBar.Enabled = true;

			//Set initial zoom
			ca.AxisX.ScaleView.Zoom(0, 4000);

			//Enable range selection and zooming end user interface
			ca.CursorX.IsUserEnabled = true;
			ca.CursorY.IsUserEnabled = true;
			ca.CursorX.IsUserSelectionEnabled = true;
			ca.CursorY.IsUserSelectionEnabled = true;
			ca.AxisX.ScaleView.Zoomable = true;
			ca.AxisY.ScaleView.Zoomable = true;
			ca.AxisX.ScrollBar.IsPositionedInside = true;
			ca.AxisY.ScrollBar.IsPositionedInside = true;
		}


		private void lbtnAddDataSet_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			var f = new SelectDataForm();
			f.ShowDialog();
			if (f.selectedData.Count == 0)
				return;

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
			matrix.Clear();
			matrix2.Clear();

			// calculate the number of columns
			int nDataShown = Util.CountTrue(chkShowARefVoltage.Checked, chkShowBRefVoltage.Checked, chkShowABCurrent.Checked,
			                           chkShowABVoltage.Checked);
			var nColumns = SetupColumns(nDataShown);

			int datasetNum = 0;
			foreach (int testRecordId in selectedDataSet)
			{
				IList<TestData> dataSet = testRecordDao.GetTestData(testRecordId);
				TestRecord testRecord = testRecordDao.FindById(testRecordId);

				SetColumnNames(datasetNum, nDataShown, testRecord);

				int rowNum = 0;
				foreach (TestData testData in dataSet)
				{
					if (testData.TestType == TestType.ForwardCharge.ToString() && !chkShowForwardCharge.Checked)
						continue;
					if (testData.TestType == TestType.ReverseCharge.ToString() && !chkShowReverseCharge.Checked)
						continue;
					if (testData.TestType == TestType.Discharge.ToString() && !chkShowDischarge.Checked)
						continue;
					if (testData.TestType == TestType.OpenCircuit.ToString() && !chkShowOpenCircuit.Checked)
						continue;

					// expand the number of rows if necessary
					if (matrix.Count <= rowNum)
					{
						string[] row = new string[nColumns];
						row[0] = Util.formatTime(testData.ElapsedTime);
						row[1] = testData.TestType;
						row[2] = testData.StepTime.ToString();
						matrix.Add(row);

						row = new string[nColumns];
						row[0] = Util.formatTime(testData.ElapsedTime);
						row[1] = testData.TestType;
						row[3] = testData.StepTime.ToString();
						matrix2.Add(row);
					}

					// if elapsed time is not aligned - skip the rest
					//if (matrix[i][0] != testData.ElapsedTime.ToString())
					//	break;

					int col = datasetNum*nDataShown + 3;
					if (chkShowARefVoltage.Checked)
					{
						matrix2[rowNum][col] = testData.ARefVoltage.ToString();
						matrix[rowNum][col++] = Util.formatNumber(testData.ARefVoltage, "V");
					}
					if (chkShowBRefVoltage.Checked)
					{
						matrix2[rowNum][col] = testData.BRefVoltage.ToString();
						matrix[rowNum][col++] = Util.formatNumber(testData.BRefVoltage, "V");
					}
					if (chkShowABVoltage.Checked)
					{
						matrix2[rowNum][col] = testData.ABVoltage.ToString();
						matrix[rowNum][col++] = Util.formatNumber(testData.ABVoltage, "V");
					}
					if (chkShowABCurrent.Checked)
					{
						matrix2[rowNum][col] = testData.ABCurrent.ToString();
						matrix[rowNum][col] = Util.formatNumber(testData.ABCurrent, "A");
					}
					rowNum++;
				}
				datasetNum++;
			}
		}

		private int SetupColumns(int nDataShown)
		{
			int nColumns = (nDataShown*selectedDataSet.Count) + 3;
			dataTable.Columns.Clear();

			for (int a = 0; a < nColumns; a++)
			{
				dataTable.Columns.Add(new DataColumn());
			}
			return nColumns;
		}

		private void SetColumnNames(int setNumber, int nDataShown, TestRecord testRecord)
		{
			int col = setNumber*nDataShown + 3;
			if (chkShowARefVoltage.Checked)
				_setColName(dataTable.Columns[col++], testRecord.Sample, ":ARef");
			if (chkShowBRefVoltage.Checked)
				_setColName(dataTable.Columns[col++], testRecord.Sample, ":BRef");
			if (chkShowABVoltage.Checked)
				_setColName(dataTable.Columns[col++], testRecord.Sample, ":ABVolt");
			if (chkShowABCurrent.Checked)
				_setColName(dataTable.Columns[col], testRecord.Sample, ":ABAmp");
		}

		private void _setColName(DataColumn dc, string name, string suffix)
		{
			string withNumber = name;
			int number = 1;

			again:
			try
			{
				dc.ColumnName = withNumber + suffix;
			}
			catch
			{
				number++;
				withNumber = name + "(" + number + ")";
				goto again;
			}
		}


		private void RefreshData()
		{
			int scrollIndex = 0;
			if (dgvDataGrid.Columns.Count != 0)
				scrollIndex = dgvDataGrid.FirstDisplayedScrollingRowIndex;

			_compileData();

			dataTable.Columns[0].ColumnName = "Elapsed";
			dataTable.Columns[1].ColumnName = "Step";
			dataTable.Columns[2].ColumnName = "StepTime";
			foreach (string[] t in matrix)
				dataTable.LoadDataRow(t, true);

			dgvDataGrid.DataSource = dataTable;

			dgvDataGrid.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; 
			for (int n = 0; n < dgvDataGrid.Columns.Count; n++)
			{
				dgvDataGrid.Columns[n].SortMode = DataGridViewColumnSortMode.NotSortable;
				dgvDataGrid.Columns[n].Width = colWidth[n];
			}


			bool toggle = true;
			int nDataShown = Util.CountTrue(chkShowARefVoltage.Checked, chkShowBRefVoltage.Checked, chkShowABCurrent.Checked,
									   chkShowABVoltage.Checked);
			int col = 3;
			while (col < dgvDataGrid.Columns.Count)
			{
				for (int j = 0; j < nDataShown; j++)
						dgvDataGrid.Columns[col+j].DefaultCellStyle.BackColor = toggle? Color.Azure: Color.White;
				toggle = !toggle;
				col += nDataShown;
			}


			if (scrollIndex >= dgvDataGrid.RowCount && dgvDataGrid.RowCount > 0)
				scrollIndex = dgvDataGrid.RowCount-1;
			if (scrollIndex > 0)
				dgvDataGrid.FirstDisplayedScrollingRowIndex = scrollIndex;

			DrawChart();
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


		private void DrawChart()
		{
			chart1.Series.Clear();

			for (int i = 3; i < dataTable.Columns.Count; i++)
			{
				Series c = new Series
				{
					Name = dataTable.Columns[i].ColumnName,
					ChartType = SeriesChartType.Line,
					XValueType = ChartValueType.Int32,
				};
				chart1.Series.Add(c);

				foreach (string[] row in matrix2)
				{
					double x = Util.ParseInt(row[0]);
					double y = Util.ParseDouble(row[i]);
					c.Points.AddXY(x, y);
				}
			}

			chart1.ChartAreas[0].RecalculateAxesScale();
		}

	}
}
