using System.Threading;
using WaterTestStation.model;

namespace WaterTestStation
{
	class LightAndTemperatureSensor
	{
		private static Thread thread;

		public static void StartSensor()
		{
			//if (!Config.HasMultimeter || !Config.HasRelay)
			//	return;
			thread = new Thread(_execute);
			thread.Start();
		}

		private static void _execute()
		{
			while (true)
			{
				Main.MultimeterQueue.Enqueue(new MeterRequest(null, null, TestType.ForwardCharge, 0, 0, 0, false));
				Thread.Sleep(Config.TemperatureRefreshInterval * 1000);
			}
		}

		public static void Stop()
		{
			thread.Abort();
		}
	}

}
