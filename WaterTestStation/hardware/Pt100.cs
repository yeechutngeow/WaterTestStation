﻿using System.Linq;

namespace WaterTestStation.hardware
{
	/**
	 * converts Pt100 resistance value to temperature
	 */
	class Pt100
	{
		private const double firstTemp = 0;

		// conversion table one degree increment
		// only handles 0-109C range
		private readonly double[] conversionTable =
			{
				100.00, 100.39, 100.78, 101.17, 101.56, 101.95, 102.34, 102.73, 103.12, 103.51,
				103.90, 104.29, 104.68, 105.07, 105.46, 105.85, 106.24, 106.63, 107.02, 107.40,
				107.79, 108.18, 108.57, 108.96, 109.35, 109.73, 110.12, 110.51, 110.90, 111.29,
				111.67, 112.06, 112.45, 112.83, 113.22, 113.61, 114.00, 114.38, 114.77, 115.15,
				115.54, 115.93, 116.31, 116.70, 117.08, 117.47, 117.86, 118.24, 118.63, 119.01,
				119.40, 119.78, 120.17, 120.55, 120.94, 121.32, 121.71, 122.09, 122.47, 122.86,
				123.24, 123.63, 124.01, 124.39, 124.78, 125.16, 125.54, 125.93, 126.31, 126.69,
				127.08, 127.46, 127.84, 128.22, 128.61, 128.99, 129.37, 129.75, 130.13, 130.52,
				130.90, 131.28, 131.66, 132.04, 132.42, 132.80, 133.18, 133.57, 133.95, 134.33,
				134.71, 135.09, 135.47, 135.85, 136.23, 136.61, 136.99, 137.37, 137.75, 138.13,
				138.51, 138.88, 139.26, 139.64, 140.02, 140.40, 140.78, 141.16, 141.54, 141.91
			};

		
		public double Convert(double r)
		{
			int index = 0;
			while (index < conversionTable.Count() && conversionTable[index] < r)
			{
				index++;
			}
			if (index == 0) return firstTemp;
			if (index == conversionTable.Count()) return firstTemp + index;

			double result = firstTemp + index + (r - conversionTable[index-1])/(conversionTable[index] - conversionTable[index-1]);
			return result;
		}
	}

}
