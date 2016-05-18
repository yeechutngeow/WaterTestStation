﻿using System;
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

		private string chartTitle;

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
			                           chkShowABVoltage.Checked);
			var nColumns = SetupColumns(nDataShown);

			SortedSet<TestRecord> testRecords =  new SortedSet<TestRecord>();
			foreach (int testRecordId in selectedDataSet)
			{
				testRecords.Add(testRecordDao.FindById(testRecordId));
			}

			int datasetNum = 0;
			foreach (var testRecord in testRecords)
			{
				IList<TestData> dataSet = testRecordDao.GetTestData(testRecord.Id, chkIgnoreLeadTime.Checked);
				//TestRecord testRecord = testRecordDao.FindById(testRecordId);
				chartTitle = testRecord.DataSet;

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
						row[2] = testData.StepTime.ToString();
						matrix2.Add(row);
					}

					// if elapsed time is not aligned - skip the rest
					//if (matrix[i][0] != testData.ElapsedTime.ToString())
					//	break;

					int col = datasetNum*nDataShown + 3;
					if (chkShowARefVoltage.Checked)
					{
						if (Math.Abs(testData.ARefVoltage) >= Config.VoltageThreshold)
						{
							matrix2[rowNum][col] = testData.ARefVoltage.ToString();
							matrix[rowNum][col] = Util.formatNumber(testData.ARefVoltage, "V");
						}
						col++;
					}
					if (chkShowBRefVoltage.Checked)
					{
						if (Math.Abs(testData.BRefVoltage) >= Config.VoltageThreshold)
						{
							matrix2[rowNum][col] = testData.BRefVoltage.ToString();
							matrix[rowNum][col] = Util.formatNumber(testData.BRefVoltage, "V");
						}
						col++;
					}
					if (chkShowABVoltage.Checked)
					{
						if (Math.Abs(testData.ABVoltage) >= Config.VoltageThreshold)
						{
							matrix2[rowNum][col] = testData.ABVoltage.ToString();
							matrix[rowNum][col] = Util.formatNumber(testData.ABVoltage, "V");
						}
						col++;
					}
					if (chkShowABCurrent.Checked)
					{
						if (Math.Abs(testData.ABCurrent) >= Config.CurrentThreshold)
						{
							matrix2[rowNum][col] = testData.ABCurrent.ToString();
							matrix[rowNum][col] = Util.formatNumber(testData.ABCurrent, "A");
						}
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

		Color[] colors = new Color[] {Color.Gray, Color.SpringGreen, Color.Blue, Color.Orange, Color.Green, Color.Crimson, Color.MediumOrchid, Color.SteelBlue};
		bool[] colorUsed;

		private Color _useColor(int index)
		{
			colorUsed[index] = true;
			return colors[index];
		}

		private Color getColor(string sampleName)
		{
			if (sampleName.ToUpper().StartsWith("DIW")) return _useColor(0);
			if (sampleName.ToUpper().StartsWith("CTRL")) return _useColor(1);
			if (sampleName.ToUpper().StartsWith("B5")) return _useColor(2);
			if (sampleName.ToUpper().StartsWith("SPRITZ")) return _useColor(3);
			if (sampleName.ToUpper().StartsWith("KMK")) return _useColor(4);
			if (sampleName.ToUpper().StartsWith("M3")) return _useColor(5);

			if (sampleName.ToUpper().StartsWith("1ML")) return _useColor(1);
			if (sampleName.ToUpper().StartsWith("2ML")) return _useColor(2);
			if (sampleName.ToUpper().StartsWith("4ML")) return _useColor(3);
			if (sampleName.ToUpper().StartsWith("8ML")) return _useColor(4);
			return Color.White;
		}

		private void DrawChart()
		{
			colorUsed = new bool[8];
			chart1.Series.Clear();

			chart1.Titles.Clear();
			chart1.Titles.Add("DataSet: " + chartTitle);
			chart1.ChartAreas[0].AxisY.IsLogarithmic = chkLogarithmic.Checked;

			double minY = 0, maxY = 0;

			for (int i = 3; i < dataTable.Columns.Count; i++)
			{
				Series c = new Series
				{
					Name = dataTable.Columns[i].ColumnName,
					ChartType = SeriesChartType.Line,
					XValueType = ChartValueType.Int32,
				};
				Color color = getColor(dataTable.Columns[i].ColumnName);
				if (color != Color.White) c.Color = color;

				chart1.Series.Add(c);

				int lastStepTime = 0;
				double lastY = 0;
				int curX = 0;
				double integral = 0;

				foreach (string[] row in matrix2)
				{
					int curStepTime = Util.ParseInt(row[2]);
					double curY = Util.ParseDoubleE(row[i]);

					//curY += 200E-9;
					if (chkInvertGraph.Checked) curY = -curY;

					int interval;
					if (curStepTime == 0)
						interval = curX > 0 ? 2 : 0;
					else
						interval = curStepTime - lastStepTime;

					curX += interval;
					double yValue = 0;

					if (chkIntegrate.Checked)
					{
						if (curStepTime == 0)
							integral = 0;
						else
							integral += (curY + lastY)/2*interval;

						yValue = integral;
					}
					else
						yValue = curY;

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
			chart1.ChartAreas[0].AxisY.ScaleView.Zoom(Util.NextExponent(maxY)/10, 2* Util.NextExponent(maxY)/10);
		}

	}
}
