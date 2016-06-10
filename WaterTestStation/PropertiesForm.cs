using System;
using System.Windows.Forms;

namespace WaterTestStation
{
	public partial class PropertiesForm : Form
	{
		public PropertiesForm()
		{
			InitializeComponent();

			txtCom1.Text = Config.RelayCom1 + "";
			txtCom2.Text = Config.RelayCom2 + "";

			chkHasMultimeter.Checked = Config.HasMultimeter;
			chkHasRelay.Checked = Config.HasRelay;

			txtMultimeterDelay.Text = Config.MultimeterDelay + "";
			txtTemperatureRefresh.Text = Config.TemperatureRefreshInterval + "";
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			Properties.Settings.Default.RelayCom1 = int.Parse(txtCom1.Text);
			Properties.Settings.Default.RelayCom2 = int.Parse(txtCom2.Text);
			Properties.Settings.Default.HasRelay = chkHasRelay.Checked;
			Properties.Settings.Default.HasMultimeter = chkHasMultimeter.Checked;

			Properties.Settings.Default.MultimeterDelay = int.Parse(txtMultimeterDelay.Text);
			Properties.Settings.Default.TemperatureRefreshInterval = int.Parse(txtTemperatureRefresh.Text);

			Properties.Settings.Default.Save();
			this.Close();
		}

	}
}
