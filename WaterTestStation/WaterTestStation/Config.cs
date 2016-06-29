using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaterTestStation
{
	static class Config
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

		public static double ChartChargingCurrentMax
		{
			get { return Properties.Settings.Default.ChartChargingCurrentMax; }
			set { Properties.Settings.Default.ChartChargingCurrentMax = value; }
		}

		public static double ChartChargingCurrentMin
		{
			get { return Properties.Settings.Default.ChartChargingCurrentMin; }
			set { Properties.Settings.Default.ChartChargingCurrentMin = value; }
		}

		public static double ChartChargingVoltageMax
		{
			get { return Properties.Settings.Default.ChartChargingVoltageMax; }
			set { Properties.Settings.Default.ChartChargingVoltageMax = value; }
		}
		public static double ChartChargingVoltageMin
		{
			get { return Properties.Settings.Default.ChartChargingVoltageMin; }
			set { Properties.Settings.Default.ChartChargingVoltageMin = value; }
		}
		public static double ChartCumulativeIntegralMax
		{
			get { return Properties.Settings.Default.ChartCumulativeIntegralMax; }
			set { Properties.Settings.Default.ChartCumulativeIntegralMax = value; }
		}
		public static double ChartCumulativeIntegralMin
		{
			get { return Properties.Settings.Default.ChartCumulativeIntegralMin; }
			set { Properties.Settings.Default.ChartCumulativeIntegralMin = value; }
		}
		public static double ChartDischargeCurrentMax
		{
			get { return Properties.Settings.Default.ChartDischargeCurrentMax; }
			set { Properties.Settings.Default.ChartDischargeCurrentMax = value; }
		}
		public static double ChartDischargeCurrentMin
		{
			get { return Properties.Settings.Default.ChartDischargeCurrentMin; }
			set { Properties.Settings.Default.ChartDischargeCurrentMin = value; }
		}
		public static double ChartDischargeVoltageMax
		{
			get { return Properties.Settings.Default.ChartDischargeVoltageMax; }
			set { Properties.Settings.Default.ChartDischargeVoltageMax = value; }
		}
		public static double ChartDischargeVoltageMin
		{
			get { return Properties.Settings.Default.ChartDischargeVoltageMin; }
			set { Properties.Settings.Default.ChartDischargeVoltageMin = value; }
		}
		public static double ChartOpenCircuitVoltageMax
		{
			get { return Properties.Settings.Default.ChartOpenCircuitVoltageMax; }
			set { Properties.Settings.Default.ChartOpenCircuitVoltageMax = value; }
		}
		public static double ChartOpenCircuitVoltageMin
		{
			get { return Properties.Settings.Default.ChartOpenCircuitVoltageMin; }
			set { Properties.Settings.Default.ChartOpenCircuitVoltageMin = value; }
		}
		public static double ChartTemperatureMax
		{
			get { return Properties.Settings.Default.ChartTemperatureMax; }
			set { Properties.Settings.Default.ChartTemperatureMax = value; }
		}
		public static double ChartTemperatureMin
		{
			get { return Properties.Settings.Default.ChartTemperatureMin; }
			set { Properties.Settings.Default.ChartTemperatureMin = value; }
		}
		public static double ChartCurrentIntegralMax
		{
			get { return Properties.Settings.Default.ChartCurrentIntegralMax; }
			set { Properties.Settings.Default.ChartCurrentIntegralMax = value; }
		}
		public static double ChartCurrentIntegralMin
		{
			get { return Properties.Settings.Default.ChartCurrentIntegralMin; }
			set { Properties.Settings.Default.ChartCurrentIntegralMin = value; }
		}
		public static double ChartImpedanceMax
		{
			get { return Properties.Settings.Default.ChartImpedanceMax; }
			set { Properties.Settings.Default.ChartImpedanceMax = value; }
		}
		public static double ChartImpedanceMin
		{
			get { return Properties.Settings.Default.ChartImpedanceMin; }
			set { Properties.Settings.Default.ChartImpedanceMin = value; }
		}
	}
}
