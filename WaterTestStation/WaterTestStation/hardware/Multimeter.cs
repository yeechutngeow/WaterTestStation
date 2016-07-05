using System;
using System.Collections.Generic;
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

		private readonly Pt100 pt100 = new Pt100();

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
				mbSession.Write(":MEASure:CURRent:DC MAX");
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
			return _ReadMeter("voltage:DC", Config.MultimeterDelay);
		}

		private double ReadCurrent()
		{
			return -1 * _ReadMeter("current:DC", Config.MultimeterDelay);
		}

		private double ReadResistance()
		{
			return _ReadMeter("resistance", Config.MultimeterDelay + 200);
		}

		private double ReadCapacitance()
		{
			return _ReadMeter("capacitance", 32000);
		}

		private double _ReadMeter(string command, int delay)
		{
			if (!Config.HasMultimeter)
			{
				Thread.Sleep(delay+ 20);
				return random.NextDouble();
			}

			mbSession.Write(":function:" + command);
			Thread.Sleep(delay);
			mbSession.Write(":measure:" + command + "?");
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
			_setChannels("1000");
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
			_setChannels("1001");
			
			double result = ReadCurrent();
			station.currentSwitch.ToggleOff();
			if (Config.HasMultimeter)
				mbSession.Write(":function:voltage:DC");

			//TurnOffMeter();
			return result;
		}

		/*
		 * Breaks the circuit and route the current into the multimeter,
		 * And reads current and voltage together
		 */
		public void ReadABCurrentAndVoltage(TestStation station, out double ABCurrent, out double ABVoltage)
		{
			station.currentSwitch.ToggleOn();
			_setChannels("1001");

			ABCurrent = ReadCurrent();
			ABVoltage = -ReadVoltage();

			station.currentSwitch.ToggleOff();
			//TurnOffMeter();
		}

		/*
		 * Reads ARef voltage
		 */
		public double ReadARefVoltage(TestStation station)
		{
			_setChannels("1010");
			double result = - ReadVoltage();
			//TurnOffMeter();
			return result;
		}

		/*
		 * Reads BRef voltage
		 */
		public double ReadBRefVoltage(TestStation station)
		{
			_setChannels("1100");
			double result = ReadVoltage();
			//TurnOffMeter();
			return result;
		}

		/* 
		 * reads charge & discharge current from A to B
		 */
		public double ReadABCapacitance(TestStation station)
		{
			_setChannels("1000");
			double result = ReadCapacitance();
			//TurnOffMeter();
			return result;
		}

		/* 
		 * reads temperature sensor
		 */
		public double ReadTemperature()
		{
			TurnOffMeter();

			double result = pt100.Convert(ReadResistance());
			if (Config.HasMultimeter)
				mbSession.Write(":function:voltage:DC");
	
			return result;
		}

		private void _setChannels(String pattern)
		{
			IList<int> onList = new List<int>();
			IList<int> offList = new List<int>();
			int i = 0;
			foreach (char c in pattern)
			{
				if (c == '1') onList.Add(readingSelector[i]);
				else offList.Add(readingSelector[i]);
				i++;
			}
			usbRelay.SetChannels(onList, offList);
		}

		public void TurnOffMeter()
		{
			usbRelay.SetChannels(new int [] {}, readingSelector );
		}


		/**
		 * 
		 * Parameter Range		Resolution	Change
		 * 	0		200 μA		1 nA		150 uA
		 * 	1		2 mA		10 nA		1.5 mA	
		 * 	2		20 mA		100 nA		15 mA
		 * 	3		200 mA		1 μA		150 mA
		 * 	4		2 A			10 μA		1.5 A
		 *	5		10 A		100 μA
		 *	MIN		200 μA		1 nA
		 *	MAX		10 A		100 μA
		 *	DEF		200 mA		1 μA
		 * 
		 **/
		public static string DetermineCurrentRange(double c)
		{
			string range = "DEF";
			if (c < 150E-6)
				range = "0";
			else if (c < 1.5E-3)
				range = "1";
			else if (c < 15E-3)
				range = "2";
			else if (c < 150E-3)
				range = "3";
			else if (c < 1.5)
				range = "4";
			else
				range = "5";
			return range;
		}
	}
}
