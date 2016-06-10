using System.Threading;

namespace WaterTestStation
{
	class TemperatureSensor
	{
		private static Thread thread;

		public static void StartSensor()
		{
			if (!Config.HasMultimeter || !Config.HasRelay)
				return;
			thread = new Thread(_execute);
			thread.Start();
		}

		private static void _execute()
		{
			while (true)
			{
				Main.MultimeterQueue.Enqueue(new MeterRequest(null, null, 0, 0, 0, false));
				Thread.Sleep(Config.TemperatureRefreshInterval * 1000);
			}
			thread.Abort();
		}

		public static void Stop()
		{
			thread.Abort();
		}
	}

}
