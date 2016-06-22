using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using WaterTestStation.dao;
using WaterTestStation.model;

namespace WaterTestStation
{
	public partial class ViewResultsForm : Form
	{
		readonly SortedSet<int> selectedDataSet = new SortedSet<int>();
		readonly SortedSet<TestRecord> testRecords = new SortedSet<TestRecord>();

		private const int FIXED_COLS = 4;
		private const int TEMP_COL = 3;

		// data matrix - rows of data, each row corresponds to an elapsed time
		readonly IList<string[]> matrix = new List<string[]>();
		// as above, storing un-formatted raw data
		readonly IList<double[]> matrix2 = new List<double[]>();
		// for converting data matrix into data source for grid - columns of data, each column is a data series.
		readonly DataTable dataTable = new DataTable();

		readonly TestRecordDao testRecordDao = new TestRecordDao();

		// track user setting of column width
		readonly int[] colWidth = new int[30];

		private string chartTitle;

		public ViewResultsForm()
		{
			InitializeComponent();
			cboYaxis1.SelectedIndex = 0;
			cboYaxis2.SelectedIndex = 3;

			this.ResizeRedraw = true;

			colWidth[0] = 55;
			colWidth[1] = 90;
			colWidth[2] = 35;
			colWidth[3] = 50;
			for (int i = FIXED_COLS; i < colWidth.Length; i++)
				colWidth[i] = 70;

			//--- chart properties
			ChartArea ca = chart1.ChartAreas[0];

			ca.AxisX.ScrollBar.Enabled = true;
			ca.AxisY.ScrollBar.Enabled = true;
			ca.AxisY2.ScrollBar.Enabled = true;

			//Enable range selection and zooming end user interface
			ca.CursorX.IsUserEnabled = true;
			ca.CursorX.IsUserSelectionEnabled = true;
			ca.AxisX.ScaleView.Zoomable = true;
			ca.AxisX.ScrollBar.IsPositionedInside = true;

			ca.CursorY.IsUserEnabled = true;
			ca.CursorY.IsUserSelectionEnabled = true;
			ca.AxisY.ScaleView.Zoomable = true;
			ca.AxisY.ScrollBar.IsPositionedInside = true;

			ca.AxisY2.ScaleView.Zoomable = true;
			ca.AxisY2.ScrollBar.IsPositionedInside = true;
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
			int nColumns = SetupColumns();

			testRecords.Clear();
			foreach (int testRecordId in selectedDataSet)
				testRecords.Add(testRecordDao.FindById(testRecordId));

			int datasetNum = 0;
			foreach (var testRecord in testRecords)
			{
				if (testRecord == null) continue;

				IList<TestData> dataSet = testRecordDao.GetTestData(testRecord.Id, chkIgnoreLeadTime.Checked);
				//TestRecord testRecord = testRecordDao.FindById(testRecordId);
				chartTitle = cboYaxis1.SelectedText;
				_setColumnNames(datasetNum, testRecord);

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
						row[3] = testData.Temperature.ToString("0.000");
						matrix.Add(row);

						var row2 = new double[nColumns];
						row2[0] = testData.ElapsedTime;
						//row2[1] = testData.TestType;
						row2[2] = testData.StepTime;
						row2[3] = testData.Temperature;
						matrix2.Add(row2);
					}

					// if elapsed time is not aligned - skip the rest
					//if (matrix[i][0] != testData.ElapsedTime.ToString())
					//	break;

					int col = datasetNum*nDataShown() + FIXED_COLS;

					matrix2[rowNum][col] = testData.ABVoltage;
					matrix[rowNum][col] = Util.formatNumber(testData.ABVoltage, "V");
					col++;

					matrix2[rowNum][col] = testData.ABCurrent;
					matrix[rowNum][col] = Util.formatNumber(testData.ABCurrent, "A");
					col++;

					double impedance = 0;
					if (Math.Abs(testData.ABCurrent - 0) > 1E-12)
						impedance = Math.Abs(testData.ABVoltage/testData.ABCurrent);
					matrix2[rowNum][col] = impedance;
					matrix[rowNum][col] = Util.formatNumber(impedance, "\u2126");
					col++;

					if (chkShowReferenceVoltage.Checked)
					{
						matrix2[rowNum][col] = testData.ARefVoltage;
						matrix[rowNum][col] = Util.formatNumber(testData.ARefVoltage, "V");
						col++;

						matrix2[rowNum][col] = testData.BRefVoltage;
						matrix[rowNum][col] = Util.formatNumber(testData.BRefVoltage, "V");
					}
					rowNum++;
				}
				datasetNum++;
			}

			// Calculate the average
			_calculateAverage();
		}

		private void _calculateAverage()
		{
			int col;
			for (int i = 0; i < matrix.Count; i++)
			{
				var row = matrix[i];
				var row2 = matrix2[i];

				double sumVoltage = 0;
				double sumCurrent = 0;
				double sumARefVoltage = 0;
				double sumBRefVoltage = 0;
				for (int j = FIXED_COLS; j < row.Length; j += nDataShown())
				{
					sumVoltage += row2[j];
					sumCurrent += row2[j + 1];
					if (chkShowReferenceVoltage.Checked)
					{
						sumARefVoltage += row2[j + 3];
						sumBRefVoltage += row2[j + 4];
					}
				}

				col = row.Length - nDataShown();
				row[col++] = Util.formatNumber(sumVoltage/testRecords.Count, "V");
				row[col++] = Util.formatNumber(sumCurrent/testRecords.Count, "V");
				row[col++] = Util.formatNumber(sumVoltage/sumCurrent, "\u2126");
				if (chkShowReferenceVoltage.Checked)
				{
					row[col++] = Util.formatNumber(sumARefVoltage, "V");
					row[col] = Util.formatNumber(sumBRefVoltage, "V");
				}

				col = row.Length - nDataShown();
				row2[col++] = sumVoltage/testRecords.Count;
				row2[col++] = sumCurrent/testRecords.Count;
				row2[col++] = Math.Abs(sumVoltage/sumCurrent);
				if (chkShowReferenceVoltage.Checked)
				{
					row2[col++] = sumARefVoltage;
					row2[col] = sumBRefVoltage;
				}
			}
			col = matrix[0].Length - nDataShown();
			_setColName(dataTable.Columns[col++], "Avg", ":AB-V");
			_setColName(dataTable.Columns[col++], "Avg", ":AB-A");
			_setColName(dataTable.Columns[col++], "Avg", ":Imp");
			if (chkShowReferenceVoltage.Checked)
			{
				_setColName(dataTable.Columns[col++], "Avg", ":Imp");
				_setColName(dataTable.Columns[col++], "Avg", ":Imp");
			}
		}

		private int SetupColumns()
		{
			dataTable.Columns.Clear();

			int nColumns = (nDataShown() * (selectedDataSet.Count + 1)) + FIXED_COLS;

			for (int a = 0; a < nColumns; a++)
				dataTable.Columns.Add(new DataColumn());

			return nColumns;
		}

		private void _setColumnNames(int setNumber, TestRecord testRecord)
		{
			int col = setNumber * nDataShown() + FIXED_COLS;
			_setColName(dataTable.Columns[col++], testRecord.Sample, ":V");
			_setColName(dataTable.Columns[col++], testRecord.Sample, ":A");
			_setColName(dataTable.Columns[col++], testRecord.Sample, ":Imp");

			if (chkShowReferenceVoltage.Checked)
			{
				_setColName(dataTable.Columns[col++], testRecord.Sample, ":ARef");
				_setColName(dataTable.Columns[col], testRecord.Sample, ":BRef");
			}
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
			int nDataShown = 3 + (chkShowReferenceVoltage.Checked ? 2 : 0);

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

		private int nDataShown()
		{
			return 3 + (chkShowReferenceVoltage.Checked ? 2 : 0);
		}

		private void DrawChart()
		{
			chart1.Series.Clear();
			chart1.Titles.Clear();
			chart1.Titles.Add(chartTitle);

			int dataSets = testRecords.Count;
			if (chkPlotAverage.Checked) dataSets++;

			for (int i = 0; i < dataSets; i++)
			{
				Series series1, series2;
				int col1, col2;

				_determneDataColumns(i, out col1, out col2, out series1, out series2);
				chart1.Series.Add(series1);
				if (series2 != null)
					chart1.Series.Add(series2);

				int lastStepTime = 0;
				double lastY1 = 0;
				long curX = 0;
				double integral = 0;
				double totalIntegral = 0;

				for (int j = 0; j < matrix.Count; j++ )
				{
					int curStepTime = Util.ParseInt(matrix[j][2]);

					double curY1 = matrix2[j][col1];

					if (chkInvertGraph1.Checked) curY1 = -curY1;

					int interval;
					if (curStepTime < lastStepTime)
						interval = curX > 0 ? 2 : 0;
					else
						interval = curStepTime - lastStepTime;

					curX += interval;

					// calculate integral (only current integral is meanigful, but calculate nevertheless to simplify program logic
					if (curStepTime < lastStepTime)
						integral = 0;
					else
					{
						integral += (curY1 + lastY1)/2*interval;
						totalIntegral += (curY1 + lastY1)/2*interval;
					}

					double yValue = 0;
					if ((string) cboYaxis1.SelectedItem == "Current Integral")
						yValue = integral;
					else
						yValue = curY1;

					if (chkAxis1Logarithmic.Checked)
					{
						if (yValue > 0)
							series1.Points.AddXY(curX, yValue);
					}
					else
						series1.Points.AddXY(curX, yValue);


					if (series2 != null)
					{
						if ((string)cboYaxis2.SelectedItem == "Cumulative Integral")
							yValue = totalIntegral;
						else
							yValue = matrix2[j][col2];

						if (chkAxis2Logarithmic.Checked)
						{
							if (yValue > 0)
								series2.Points.AddXY(curX, yValue);
						}
						else
							series2.Points.AddXY(curX, yValue);
					}

					lastStepTime = curStepTime;
					lastY1 = curY1;
				}
			}

			chart1.ChartAreas[0].RecalculateAxesScale();
			//Set initial zoom
			chart1.ChartAreas[0].AxisX.ScaleView.Zoom(0, 4000);
			//chart1.ChartAreas[0].AxisX.Interval = 600;

			double y1Min = Util.ParseDoubleE(txtY1Min.Text);
			double y1Max = Util.ParseDoubleE(txtY1Max.Text);
			double y2Min = Util.ParseDoubleE(txtY2Min.Text);
			double y2Max = Util.ParseDoubleE(txtY2Max.Text);
			chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
			chart1.ChartAreas[0].AxisY2.ScaleView.Zoomable = true;
			chart1.ChartAreas[0].AxisY.ScaleView.Zoom(y1Min, y1Max);
			chart1.ChartAreas[0].AxisY2.ScaleView.Zoom(y2Min, y2Max);
		}

		private void _determneDataColumns(int dataSetNumber, out int col1, out int col2, out Series series1, out Series series2)
		{
			string axis1 = (string) cboYaxis1.SelectedItem;
			col1 = _determineDataColumn(dataSetNumber, axis1);

			series1 = new Series
			{
				Name = dataTable.Columns[col1].ColumnName + _chartAbbreviation(axis1),
				ChartType = SeriesChartType.Line,
				XValueType = ChartValueType.Int32,
				Color = getColor(dataTable.Columns[col1].ColumnName, dataSetNumber)
			};

			series2 = null;
			string axis2 = (string) cboYaxis2.SelectedItem;
			col2 = _determineDataColumn(dataSetNumber, axis2);

			if (axis2 == "")
				;
			else if (axis2 == "Temperature" )
			{
				if (dataSetNumber == 0)
				{
					col2 = TEMP_COL;
					series2 = new Series
					{
						Name = "Temp",
						ChartType = SeriesChartType.Line,
						XValueType = ChartValueType.Int32,
						YAxisType = AxisType.Secondary,
						Color = getColor(dataTable.Columns[col1].ColumnName, dataSetNumber)
					};
				}
			}
			else
			{
				series2 = new Series
				{
					Name = dataTable.Columns[col2].ColumnName + _chartAbbreviation(axis2),
					ChartType = SeriesChartType.Line,
					XValueType = ChartValueType.Int32,
					YAxisType = AxisType.Secondary,
					Color = getColor(dataTable.Columns[col2].ColumnName, dataSetNumber)
				};
			}
		}

		private int _determineDataColumn(int dataSetNumber, string chartType)
		{
			int dataCol = 0;
			if (chartType == "Temperature")
				dataCol = TEMP_COL;
			if (chartType == "Voltage")
				dataCol = FIXED_COLS + nDataShown()*dataSetNumber + 0;
			else if (chartType == "Current" || chartType == "Current Integral" || chartType == "Cumulative Integral")
				dataCol = FIXED_COLS + nDataShown()*dataSetNumber + 1;
			else if (chartType == "Impedance" )
				dataCol = FIXED_COLS + nDataShown()*dataSetNumber + 2;

			return dataCol;
		}

		private string _chartAbbreviation(string chartType)
		{
			string abbr = "";
			if (chartType == "Temperature")
				abbr = "Temp";
			if (chartType == "Voltage")
				abbr = "";
			else if (chartType == "Current")
				abbr = "";
			else if (chartType == "Current Integral")
				abbr = "S";
			else if (chartType == "Cumulative Integral")
				abbr = "S\u03A3";
			else if (chartType == "Impedance")
				abbr = "";
			return abbr;
		}

	}

}
