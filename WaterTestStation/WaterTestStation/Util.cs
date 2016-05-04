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
			try
			{
				v = int.Parse(Regex.Replace(s, "[^0-9.]", ""));
			} catch
			{}

			return v;
		}

		public static double ParseDouble(string s)
		{
			double v = 0;
			try
			{
				v = double.Parse(Regex.Replace(s, "[^0-9.]", ""));
			}
			catch
			{
			}
			return v;
		}

		public static double ParseDoubleE(string s)
		{
			double v = 0;
			try
			{
				v = double.Parse(s);
			}
			catch
			{
			}
			return v;
		}
	}
}
