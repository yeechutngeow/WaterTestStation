using System;
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

		private const int FIXED_COLS = 4;
		private const int TEMP_COL = 3;

		// data matrix - rows of data, each row corresponds to an elapsed time
		readonly IList<string[]> matrix = new List<string[]>();
		// as above, storing un-formatted raw data
		readonly IList<string[]> matrix2 = new List<string[]>();
		// for converting data matrix into data source for grid - columns of data, each column is a data series.
		readonly DataTable dataTable = new DataTable();

		readonly TestRecordDao testRecordDao = new TestRecordDao();

		// track user setting of column width
		readonly int[] colWidth = new int[30];

		private string chartTitle;

		public ViewResultsForm()
		{
			InitializeComponent();
			this.ResizeRedraw = true;

			colWidth[0] = 55;
			colWidth[1] = 90;
			colWidth[2] = 35;
			colWidth[3] = 35;
			for (int i = FIXED_COLS; i < colWidth.Length; i++)
				colWidth[i] = 70;

			//--- chart properties
			ChartArea ca = chart1.ChartAreas[0];

			ca.AxisX.ScrollBar.Enabled = true;
			ca.AxisY.ScrollBar.Enabled = true;

			//Set initial zoom
			ca.AxisX.ScaleView.Zoom(0, 4000);

			//Enable range selection and zooming end user interface
			ca.CursorX.IsUserEnabled = true;
			ca.CursorX.IsUserSelectionEnabled = true;
			ca.AxisX.ScaleView.Zoomable = true;
			ca.AxisX.ScrollBar.IsPositionedInside = true;

			ca.CursorY.IsUserEnabled = true;
			ca.CursorY.IsUserSelectionEnabled = true;
			ca.AxisY.ScaleView.Zoomable = true;
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
		}


		private void _compileData()
		{
			dataTable.Clear();
			matrix.Clear();
			matrix2.Clear();

			// calculate the number of columns
			int nDataShown = Util.CountTrue(chkShowARefVoltage.Checked, chkShowBRefVoltage.Checked, chkShowABCurrent.Checked,
			                           chkShowABVoltage.Checked, chkShowImpedence.Checked);
			var nColumns = SetupColumns(nDataShown);

			SortedSet<TestRecord> testRecords =  new SortedSet<TestRecord>();
			foreach (int testRecordId in selectedDataSet)
			{
				testRecords.Add(testRecordDao.FindById(testRecordId));
			}

			int datasetNum = 0;
			foreach (var testRecord in testRecords)
			{
                if (testRecord == null) continue;

				IList<TestData> dataSet = testRecordDao.GetTestData(testRecord.Id, chkIgnoreLeadTime.Checked);
				//TestRecord testRecord = testRecordDao.FindById(testRecordId);
                if (dataTable.Columns.Count >= FIXED_COLS)
                {
                    try
                    {
                        chartTitle = testRecord.DataSet + " " + dataTable.Columns[FIXED_COLS].ColumnName.Substring(dataTable.Columns[FIXED_COLS].ColumnName.LastIndexOf(':') + 1);
                    }
                    catch
                    { }
                }
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
						row[3] = testData.Temperature.ToString();
						matrix.Add(row);

						row = new string[nColumns];
						row[0] = Util.formatTime(testData.ElapsedTime);
						row[1] = testData.TestType;
						row[2] = testData.StepTime.ToString();
						row[3] = testData.Temperature.ToString();
						matrix2.Add(row);
					}

					// if elapsed time is not aligned - skip the rest
					//if (matrix[i][0] != testData.ElapsedTime.ToString())
					//	break;

					int col = datasetNum * nDataShown + FIXED_COLS;
					if (chkShowARefVoltage.Checked)
					{
						matrix2[rowNum][col] = testData.ARefVoltage.ToString();
						matrix[rowNum][col] = Util.formatNumber(testData.ARefVoltage, "V");
						col++;
					}
					if (chkShowBRefVoltage.Checked)
					{
						matrix2[rowNum][col] = testData.BRefVoltage.ToString();
						matrix[rowNum][col] = Util.formatNumber(testData.BRefVoltage, "V");
						col++;
					}
					if (chkShowABVoltage.Checked)
					{
						matrix2[rowNum][col] = testData.ABVoltage.ToString();
						matrix[rowNum][col] = Util.formatNumber(testData.ABVoltage, "V");
						col++;
					}
					if (chkShowABCurrent.Checked)
					{
						matrix2[rowNum][col] = testData.ABCurrent.ToString();
						matrix[rowNum][col] = Util.formatNumber(testData.ABCurrent, "A");
						col++;
					}
					if (chkShowImpedence.Checked)
					{
						if (Math.Abs(testData.ABCurrent - 0) > 1E-9)
						{
							matrix2[rowNum][col] = (testData.ABVoltage/testData.ABCurrent).ToString();
							matrix[rowNum][col] = Util.formatNumber((testData.ABVoltage/testData.ABCurrent), "\u2126");
						}
					}
					rowNum++;
				}
				datasetNum++;
			}
		}

		private int SetupColumns(int nDataShown)
		{
			int nColumns = (nDataShown * selectedDataSet.Count) + FIXED_COLS;
			dataTable.Columns.Clear();

			for (int a = 0; a < nColumns; a++)
			{
				dataTable.Columns.Add(new DataColumn());
			}
			return nColumns;
		}

		private void SetColumnNames(int setNumber, int nDataShown, TestRecord testRecord)
		{
			int col = setNumber * nDataShown + FIXED_COLS;
			if (chkShowARefVoltage.Checked)
				_setColName(dataTable.Columns[col++], testRecord.Sample, ":ARef-V");
			if (chkShowBRefVoltage.Checked)
				_setColName(dataTable.Columns[col++], testRecord.Sample, ":BRef-V");
			if (chkShowABVoltage.Checked)
				_setColName(dataTable.Columns[col++], testRecord.Sample, ":AB-V");
			if (chkShowABCurrent.Checked)
				_setColName(dataTable.Columns[col++], testRecord.Sample, ":AB-A");
			if (chkShowImpedence.Checked)
				_setColName(dataTable.Columns[col], testRecord.Sample, ":AB-\u2126");
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
			dataTable.Columns[3].ColumnName = "Temp";
			foreach (object[] t in matrix)
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
									   chkShowABVoltage.Checked, chkShowImpedence.Checked);
			int col = FIXED_COLS;
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

		readonly Color[] colors = new[] {Color.Gray, Color.SpringGreen, Color.Blue, Color.Orange, Color.Green, Color.Crimson, Color.MediumOrchid, Color.SteelBlue};

		private Color getColor(string sampleName, int seriesIndex)
		{
			if (chkUseCustomColor.Checked)
			{
				if (sampleName.ToUpper().StartsWith("DIW")) return colors[0];
				if (sampleName.ToUpper().StartsWith("CTRL")) return colors[1];
				if (sampleName.ToUpper().StartsWith("B5")) return colors[2];
				if (sampleName.ToUpper().StartsWith("SPRITZ")) return colors[3];
				if (sampleName.ToUpper().StartsWith("KMK")) return colors[4];
				if (sampleName.ToUpper().StartsWith("M3")) return colors[5];

				if (sampleName.ToUpper().StartsWith("1ML")) return colors[1];
				if (sampleName.ToUpper().StartsWith("2ML")) return colors[2];
				if (sampleName.ToUpper().StartsWith("4ML")) return colors[3];
				if (sampleName.ToUpper().StartsWith("8ML")) return colors[4];
			}
			return colors[seriesIndex % colors.Length];
		}

		private void DrawChart()
		{
			chart1.Series.Clear();

			chart1.Titles.Clear();
			chart1.Titles.Add( chartTitle);
			chart1.ChartAreas[0].AxisY.IsLogarithmic = chkLogarithmic.Checked;

			double minY = 0, maxY = 0;

			for (int i = FIXED_COLS; i < dataTable.Columns.Count; i++)
			{
				Series c = new Series
				{
					Name = dataTable.Columns[i].ColumnName,
					ChartType = SeriesChartType.Line,
					XValueType = ChartValueType.Int32,
					Color = getColor(dataTable.Columns[i].ColumnName, i)
				};
				chart1.Series.Add(c);

				Series c2 = null;
				if (chkIntegrate.Checked || chkShowTemperature.Checked)
				{
					c2 = new Series
					{
						Name = c.Name + "-i",
						ChartType = SeriesChartType.Line,
						XValueType = ChartValueType.Int32,
						YAxisType = AxisType.Secondary,
						Color = c.Color
					};
					chart1.Series.Add(c2);
				}

				int lastStepTime = 0;
				double lastY = 0;
				long curX = 0;
				double integral = 0;
				double totalIntegral = 0;

				foreach (string[] row in matrix2)
				{
					int curStepTime = Util.ParseInt(row[2]);
					double curY = Util.ParseDoubleE(row[i]);

					//curY += 200E-9;
					if (chkInvertGraph.Checked) curY = -curY;

					int interval;
					if (curStepTime < lastStepTime)
						interval = curX > 0 ? 2 : 0;
					else
						interval = curStepTime - lastStepTime;

					curX += interval;
					double yValue;

					if (chkIntegrate.Checked)
					{
						if (curStepTime < lastStepTime)
							integral = 0;
						else
						{
							integral += (curY + lastY)/2*interval;
							totalIntegral += (curY + lastY) / 2 * interval;
						}

						yValue = integral;

						c2.Points.AddXY(curX, totalIntegral);
					}
					else
					{
						if (chkShowTemperature.Checked)
							c2.Points.AddXY(curX, Util.ParseDoubleE(row[TEMP_COL]));
						yValue = curY;
					}

					if (!(chkLogarithmic.Checked && yValue < 0))
						c.Points.AddXY(curX, yValue);

					lastStepTime = curStepTime;
					lastY = curY;

					if (yValue < minY) minY = yValue;
					if (yValue > maxY) maxY = yValue;
				}
			}

			chart1.ChartAreas[0].RecalculateAxesScale();
			//Set initial zoom
			chart1.ChartAreas[0].AxisX.ScaleView.Zoom(0, 4000);
			chart1.ChartAreas[0].AxisY.ScaleView.Zoom(0, 3* Util.NextExponent(maxY)/10);
		}

	}
}
