using System;
using System.Threading;
using NationalInstruments.VisaNS;

namespace WaterTestStation.hardware
{
	// using multiplexer
	public class Multimeter
	{
		private MessageBasedSession mbSession; //Create Message based session

		private readonly int[] readingSelector;
		private readonly UsbRelay usbRelay;

		public Multimeter(UsbRelay usbRelay, int[] readingSelector)
		{
			this.usbRelay = usbRelay;
			this.readingSelector = readingSelector;
		}

		public void OpenSession()
		{
			const string strVISARsrc = "USB0::0x1AB1::0x0C94::DM3O161550090::INSTR";

			if (!Main.HasMultimeter)
				return;

			mbSession = (MessageBasedSession)ResourceManager.GetLocalManager().Open(strVISARsrc);

			mbSession.Write(":function:voltage:DC");
		}

		readonly Random random = new Random();

		private float ReadMeter()
		{
			if (!Main.HasMultimeter)
			{
				Thread.Sleep(30); 
				return (float) random.NextDouble();
			}

			Thread.Sleep(400);
			mbSession.Write(":measure:voltage:DC?");
			Thread.Sleep(10);
			string result = mbSession.ReadString();

			float value;
			float.TryParse(result, out value);
			return value; 
		}

		/*
		 * Reads open circuit voltage across A and B
		 */
		public float ReadABVoltage(int stationNumber)
		{
			usbRelay.SetChannels(
				new[] { readingSelector[0] },
				new[] { readingSelector[1], readingSelector[2], readingSelector[3] });
			return ReadMeter();
		}

		/* 
		 * reads charge & discharge current from A to B
		 */
		public float ReadABCurrent(int stationNumber)
		{
			usbRelay.SetChannels(
				new[] {readingSelector[0], readingSelector[3]}, 
				new[] {readingSelector[1],readingSelector[2]});
			return ReadMeter() / Config.GetResistorValue(stationNumber);
		}

		/*
		 * Reads ARef voltage
		 */
		public float ReadARefVoltage(int stationNumber)
		{
			usbRelay.SetChannels(
				new[] {readingSelector[0], readingSelector[2]}, 
				new[] {readingSelector[1], readingSelector[3]});
			return ReadMeter();
		}

		/*
		 * Reads BRef voltage
		 */
		public float ReadBRefVoltage(int stationNumber)
		{
			usbRelay.SetChannels(
				new[] {readingSelector[0], readingSelector[1]}, 
				new[] {readingSelector[2], readingSelector[3]});
			return -1 * ReadMeter();
		}

		public void TurnOffMeter()
		{
			usbRelay.SetChannels(new int [] {}, readingSelector );
		}
	}
}
