using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaterTestStation
{
	internal class Config
	{
		public static bool HasRelay
		{
			get { return Properties.Settings.Default.HasRelay; }
		}

		public static bool HasMultimeter
		{
			get { return Properties.Settings.Default.HasMultimeter; }
		}

		public static int RelayCom1
		{
			get { return Properties.Settings.Default.RelayCom1; }
		}

		public static int RelayCom2
		{
			get { return Properties.Settings.Default.RelayCom2; }
		}

		public static int MultimeterDelay
		{
			get { return Properties.Settings.Default.MultimeterDelay; }
		}

		public static int TemperatureRefreshInterval
		{
			get { return Properties.Settings.Default.TemperatureRefreshInterval; }
		}
	}
}
