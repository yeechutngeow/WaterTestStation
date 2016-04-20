﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			Properties.Settings.Default.RelayCom1 = int.Parse(txtCom1.Text);
			Properties.Settings.Default.RelayCom2 = int.Parse(txtCom2.Text);
			Properties.Settings.Default.HasRelay = chkHasRelay.Checked;
			Properties.Settings.Default.HasMultimeter = chkHasMultimeter.Checked;

			Properties.Settings.Default.Save();
			this.Close();
		}
	}
}