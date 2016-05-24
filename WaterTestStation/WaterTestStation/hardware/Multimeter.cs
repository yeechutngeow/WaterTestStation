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

		private double ReadCurrent()
		{
			return _ReadMeter(":measure:current:DC?", Config.MultimeterDelay);
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
		public double ReadABVoltage(TestStation station)
		{
			usbRelay.SetChannels(
				new[] { readingSelector[0] },
				new[] { readingSelector[1], readingSelector[2], readingSelector[3]});
			double result = -ReadVoltage();
			TurnOffMeter();
			return result;
		}

		/* 
		 * Reads charge & discharge current from A to B
		 * This will require breaking the circuit to route the current into the multimeter
		 */
		public double ReadABCurrent(TestStation station)
		{
			station.currentSwitch.ToggleOn();
			usbRelay.SetChannels(
				new[] { readingSelector[0], readingSelector[3] },
				new[] { readingSelector[1], readingSelector[2]});
			
			double result = ReadCurrent();
			station.currentSwitch.ToggleOff();
			TurnOffMeter();
			return result;
		}

		/*
		 * Breaks the circuit and route the current into the multimeter,
		 * And reads current and voltage together
		 */
		public void ReadABCurrentAndVoltage(TestStation station, out double ABCurrent, out double ABVoltage)
		{
			station.currentSwitch.ToggleOn();
			usbRelay.SetChannels(
				new[] { readingSelector[0], readingSelector[3] },
				new[] { readingSelector[1], readingSelector[2] });

			ABCurrent = ReadCurrent();
			ABVoltage = -ReadVoltage();

			station.currentSwitch.ToggleOff();
			TurnOffMeter();
		}

		/*
		 * Reads ARef voltage
		 */
		public double ReadARefVoltage(TestStation station)
		{
			usbRelay.SetChannels(
				new[] {readingSelector[0], readingSelector[2]},
				new[] { readingSelector[1], readingSelector[3]});
			double result = - ReadVoltage();
			TurnOffMeter();
			return result;
		}

		/*
		 * Reads BRef voltage
		 */
		public double ReadBRefVoltage(TestStation station)
		{
			usbRelay.SetChannels(
				new[] {readingSelector[0], readingSelector[1]},
				new[] { readingSelector[2], readingSelector[3]});
			double result = ReadVoltage();
			TurnOffMeter();
			return result;
		}

		/* 
		 * reads charge & discharge current from A to B
		 */
		public double ReadABCapacitance(TestStation station)
		{
			usbRelay.SetChannels(
				new[] { readingSelector[0] },
				new[] { readingSelector[1], readingSelector[2], readingSelector[3]});
			double result = ReadCapacitance();
			TurnOffMeter();
			return result;
		}

		public void TurnOffMeter()
		{
			usbRelay.SetChannels(new int [] {}, readingSelector );
		}

	}
}
