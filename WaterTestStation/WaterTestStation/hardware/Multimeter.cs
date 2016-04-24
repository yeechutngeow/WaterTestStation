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

			if (!Config.HasMultimeter)
				return;

			if (mbSession == null)
			{
				mbSession = (MessageBasedSession) ResourceManager.GetLocalManager().Open(strVISARsrc);
				mbSession.Write(":function:voltage:DC");
			}
		}

		public void CloseSession()
		{
			if (Config.HasMultimeter && mbSession != null)
				mbSession.Dispose();
		}

		readonly Random random = new Random();

		private double ReadVoltage()
		{
			return _ReadMeter(":measure:voltage:DC?", Config.MultimeterDelay);
		}

		private double ReadCapacitance()
		{
			return _ReadMeter(":measure:capacitance?", 32000);
		}

		private double _ReadMeter(string command, int delay)
		{
			if (!Config.HasMultimeter)
			{
				Thread.Sleep(delay+ 20);
				return random.NextDouble();
			}

			Thread.Sleep(delay);
			mbSession.Write(command);
			Thread.Sleep(20);
			string result;
			try
			{
				result = mbSession.ReadString();
			}
			catch
			{
				result = "0";
			}

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
			return ReadVoltage();
		}

		/* 
		 * reads charge & discharge current from A to B
		 */
		public double ReadABCurrent(int stationNumber)
		{
			usbRelay.SetChannels(
				new[] {readingSelector[0], readingSelector[3]}, 
				new[] {readingSelector[1],readingSelector[2]});
			return ReadVoltage() / Config.GetResistorValue(stationNumber);
		}

		/* 
		 * reads charge & discharge current from A to B
		 */
		public double ReadABCapacitance(int stationNumber)
		{
			usbRelay.SetChannels(
				new[] { readingSelector[0], readingSelector[3] },
				new[] { readingSelector[1], readingSelector[2] });
			return ReadCapacitance() / Config.GetResistorValue(stationNumber);
		}
		/*
		 * Reads ARef voltage
		 */
		public double ReadARefVoltage(int stationNumber)
		{
			usbRelay.SetChannels(
				new[] {readingSelector[0], readingSelector[2]}, 
				new[] {readingSelector[1], readingSelector[3]});
			return ReadVoltage();
		}

		/*
		 * Reads BRef voltage
		 */
		public double ReadBRefVoltage(int stationNumber)
		{
			usbRelay.SetChannels(
				new[] {readingSelector[0], readingSelector[1]}, 
				new[] {readingSelector[2], readingSelector[3]});
			return -1 * ReadVoltage();
		}

		public void TurnOffMeter()
		{
			usbRelay.SetChannels(new int [] {}, readingSelector );
		}

	}
}
