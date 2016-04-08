using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaterTestStation
{
	class Config
	{
		public static String RelayComPort()
		{
			return System.Configuration.ConfigurationManager.AppSettings["UsbComPort"];
		}

		public static float GetResistorValue(int stationNumber)
		{
			return float.Parse(System.Configuration.ConfigurationManager.AppSettings["Resistor" + stationNumber]);
		}
	}
}
