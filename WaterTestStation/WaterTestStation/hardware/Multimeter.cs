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

			if (Config.HasMultimeter)
				return;

			if (mbSession == null)
			{
				mbSession = (MessageBasedSession) ResourceManager.GetLocalManager().Open(strVISARsrc);
				mbSession.Write(":function:voltage:DC");
			}
		}

		public void CloseSession()
		{
			if (Config.HasMultimeter)
				mbSession.Dispose();
		}

		readonly Random random = new Random();

		private double ReadMeter()
		{
			if (Config.HasMultimeter)
			{
				Thread.Sleep(410); 
				return (double) random.NextDouble();
			}

			Thread.Sleep(400);
			mbSession.Write(":measure:voltage:DC?");
			Thread.Sleep(10);
			string result = mbSession.ReadString();

			double value;
			double.TryParse(result, out value);
			return value; 
		}

		/*
		 * Reads open circuit voltage across A and B
		 */
		public double ReadABVoltage(int stationNumber)
		{
			usbRelay.SetChannels(
				new[] { readingSelector[0] },
				new[] { readingSelector[1], readingSelector[2], readingSelector[3] });
			return ReadMeter();
		}

		/* 
		 * reads charge & discharge current from A to B
		 */
		public double ReadABCurrent(int stationNumber)
		{
			usbRelay.SetChannels(
				new[] {readingSelector[0], readingSelector[3]}, 
				new[] {readingSelector[1],readingSelector[2]});
			return ReadMeter() / Config.GetResistorValue(stationNumber);
		}

		/*
		 * Reads ARef voltage
		 */
		public double ReadARefVoltage(int stationNumber)
		{
			usbRelay.SetChannels(
				new[] {readingSelector[0], readingSelector[2]}, 
				new[] {readingSelector[1], readingSelector[3]});
			return ReadMeter();
		}

		/*
		 * Reads BRef voltage
		 */
		public double ReadBRefVoltage(int stationNumber)
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
