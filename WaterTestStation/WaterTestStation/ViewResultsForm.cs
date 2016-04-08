using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WaterTestStation.dao;
using WaterTestStation.model;

namespace WaterTestStation
{
	public partial class ViewResultsForm : Form
	{
		SortedSet<int> selectedDataSet = new SortedSet<int>();

		TestRecordDao testRecordDao = new TestRecordDao();
		public ViewResultsForm()
		{
			InitializeComponent();
			this.ResizeRedraw = true;
		}


		private void lbtnAddDataSet_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			var f = new SelectDataForm();
			f.ShowDialog();

			foreach (int dataSetId in f.selectedData)
			{
				selectedDataSet.Add(dataSetId);
			}
			RefreshData();
		}

		private void _compileData()
		{
			foreach (int testRecordId in selectedDataSet)
			{
				IList<TestData> dataSet = testRecordDao.GetTestData(testRecordId);
			}
		}

		private void RefreshData()
		{
			_compileData();
		}
	}
}
