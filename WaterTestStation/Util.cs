using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace WaterTestStation
{
	public class Util
	{
		public static string formatNumber(double n, string unit)
		{
			if (Math.Abs(n) < 1E-9)
				return (n * 1E12).ToString(4) + " p" + unit;
			if (Math.Abs(n) < 1E-6)
				return (n * 1E9).ToString(4) + " n" + unit;
			if (Math.Abs(n) < 1E-3)
				return (n * 1E6).ToString(4) + " u" + unit;
			if (Math.Abs(n) < 1)
				return (n * 1E3).ToString(4) + " m" + unit;
			return n.ToString(4) + unit;
		}

		public static string formatTime(int seconds)
		{
			TimeSpan ts = TimeSpan.FromSeconds(seconds);
			return ts.ToString(@"hh\:mm\:ss");
		}

		public static int CountTrue(params bool[] args)
		{
			return args.Count(t => t);
		}

		public static int ParseInt(string s)
		{
			int v = 0;
			Int32.TryParse(s, out v);
			return v;
		}

		public static double ParseDouble(string s)
		{
			double v = 0;
			Double.TryParse(Regex.Replace(s, "[^0-9.]", ""), out v);
			return v;
		}

		public static double ParseDoubleE(string s)
		{
			double v = 0;
			Double.TryParse(s, out v);
			return v;
		}

		public static double NextExponent(double v)
		{
			double result = v;

			if (Math.Abs(v) < 1E-7) result = 1E-7;
			else if (Math.Abs(v) < 1E-6) result = 1E-6;
			else if (Math.Abs(v) < 1E-5) result = 1E-5;
			else if (Math.Abs(v) < 1E-4) result = 1E-4;
			else if (Math.Abs(v) < 1E-3) result = 1E-3;
			else if (Math.Abs(v) < 1E-2) result = 1E-2;
			else if (Math.Abs(v) < 1E-1) result = 1E-1;
			else if (Math.Abs(v) < 1E0) result = 1E-0;
			else if (Math.Abs(v) < 1E1) result = 1E1;
			else if (Math.Abs(v) < 1E2) result = 1E2;

			if (v > 0)
				return result;
			else
				return -result;
		}
	}
}
