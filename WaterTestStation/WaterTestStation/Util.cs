using System;
using System.Linq;

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

		public static int CountTrue(params bool[] args)
		{
			return args.Count(t => t);
		}

		public static int ParseInt(string s)
		{
			int v = 0;
			try
			{
				v = int.Parse(s);
			} catch
			{}

			return v;
		}


	}
}
