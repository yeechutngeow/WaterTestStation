using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaterTestStation.hardware
{
	class LightMeter
	{
		private const double minA = 1E-6;
		private const double maxA = 1000E-6;
		private const double minLux = 1;
		private const double maxLux = 10000;

		public double Convert(double current)
		{
			double lux = (current - minA) * (maxLux - minLux) / (maxA-minA) + minLux;
			return lux;
		}
	}
}
