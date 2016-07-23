using System;
using System.Collections.Generic;
using System.Threading;
using NationalInstruments.VisaNS;

namespace WaterTestStation.hardware
{
	/*
			Old unit: "USB0::0x1AB1::0x0C94::DM3O161550090::INSTR";
			New Unit: "USB0::0x1AB1::0x0C94::DM3O175000896::INSTR";
	 */

	public class Multimeter
	{
		private MessageBasedSession mbSession; //Create Message based session

		private readonly int[] readingSelector1, readingSelector2;
		private readonly UsbRelay usbRelay1, usbRelay2;

		private readonly Pt100 pt100 = new Pt100();

		public Multimeter(UsbRelay usbRelay1, int[] readingSelector1, UsbRelay usbRelay2, int[] readingSelector2)
		{
			this.usbRelay1 = usbRelay1;
			this.readingSelector1 = readingSelector1;
			this.usbRelay2 = usbRelay2;
			this.readingSelector2 = readingSelector2;
		}

		public void OpenSession()
		{
			if (mbSession != null) return;

			try
			{
				string[] visaResource = ResourceManager.GetLocalManager().FindResources("?*");
				Config.DM3068VisaResourceStr = visaResource[0];
				mbSession = (MessageBasedSession)ResourceManager.GetLocalManager().Open(Config.DM3068VisaResourceStr);
				mbSession.Write(":function:voltage:DC");

				Config.HasMultimeter = true;
			}
			catch
			{
				Config.HasMultimeter = false;
			}
		}

		private void WriteSession(string msg)
		{
			if (Config.HasMultimeter)
				mbSession.Write(msg);
		}

		public void CloseSession()
		{
			if (Config.HasMultimeter && mbSession != null)
				mbSession.Dispose();
		}

		readonly Random random = new Random();

		private double _ReadVoltage()
		{
			return _readMeter("voltage:DC", Config.MultimeterDelay);
		}

		private double _readCurrent(double lastReading)
		{
			char range = _determineCurrentRange(lastReading);
		again:
			if (range == '4' || range == '5')
				usbRelay2.OnChannel(readingSelector2[1]);
			else
				usbRelay2.OffChannel(readingSelector2[1]);

			WriteSession(":measure:current:dc " + range);

			int sign = -1;

			// the old unit has 200mA polarity reversed
			if (Config.DM3068VisaResourceStr.StartsWith("USB0::0x1AB1::0x0C94::DM3O161550090"))
				sign = sign * (range == '4' || range == '5' ? 1 : -1);

			double result = sign * _readMeter("current:DC", Config.MultimeterDelay);

			if (Math.Abs(result) > 1E10)	/* overload */
			{
				if (range < '5')
				{
					range = (char)(range + 1);
					goto again;
				}
			}
			return result;
		}

		private double ReadResistance()
		{
			return _readMeter("resistance", Config.MultimeterDelay + 200);
		}

		private double ReadCapacitance()
		{
			return _readMeter("capacitance", 32000);
		}

		private double _readMeter(string command, int delay)
		{
			if (!Config.HasMultimeter)
			{
				Thread.Sleep(delay + 20);
				return random.NextDouble();
			}

			WriteSession(":function:" + command);
			Thread.Sleep(delay);
			WriteSession(":measure:" + command + "?");
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
			WriteSession(":measure auto");
			_setChannels(usbRelay1, "1001");
			usbRelay2.OffChannel(readingSelector2[0]);
			double result = -1 * _ReadVoltage();
			TurnOffMeter();
			return result;
		}

		/* 
		 * Reads charge & discharge current from A to B
		 * This will require breaking the circuit to route the current into the multimeter
		 */
		public double ReadABCurrent(TestStation station, double lastCurrent)
		{
			station.currentSwitch.ToggleOn();
			_setChannels(usbRelay1, "1011");
			usbRelay2.OnChannel(readingSelector2[0]);

			double result = -1 * _readCurrent(lastCurrent);
			station.currentSwitch.ToggleOff();
			WriteSession(":function:voltage:DC");

			//TurnOffMeter();
			return result;
		}

		/*
		 * Breaks the circuit and route the current into the multimeter,
		 * And reads current and voltage together
		 */
		public void ReadABCurrentAndVoltage(TestStation station, out double ABCurrent, out double ABVoltage, double lastCurrent)
		{
			station.currentSwitch.ToggleOn();
			_setChannels(usbRelay1, "1011");
			usbRelay2.OnChannel(readingSelector2[0]);

			ABCurrent = _readCurrent(lastCurrent);
			_setChannels(usbRelay1, "1001");
			WriteSession(":measure auto");
			ABVoltage = -_ReadVoltage();

			station.currentSwitch.ToggleOff();
		}

		/*
		 * Reads ARef voltage
		 */
		public double ReadARefVoltage()
		{
			WriteSession(":measure auto");
			_setChannels(usbRelay1, "1011");
			usbRelay2.OffChannel(readingSelector2[0]);
			double result = -_ReadVoltage();
			return result;
		}

		/*
		 * Reads BRef voltage
		 */
		public double ReadBRefVoltage()
		{
			WriteSession(":measure auto");
			_setChannels(usbRelay1, "1101");
			usbRelay2.OffChannel(readingSelector2[0]);
			double result = _ReadVoltage();
			return result;
		}

		/* 
		 * reads charge & discharge current from A to B
		 */
		public double ReadABCapacitance()
		{
			WriteSession(":measure auto");
			_setChannels(usbRelay1, "1001");
			double result = ReadCapacitance();
			//TurnOffMeter();
			return result;
		}

		/* 
		 * reads temperature sensor
		 */
		public double ReadTemperature()
		{
			WriteSession(":measure auto");
			_setChannels(usbRelay1, "0001");
			usbRelay2.OffChannel(readingSelector2[0]);

			double result = pt100.Convert(ReadResistance());
			WriteSession(":function:voltage:DC");

			return result;
		}

		readonly LightSensor lightMeter = new LightSensor();
		public double ReadLightLevel(ref double lastCurrent)
		{
			_setChannels(usbRelay1, "0000");
			usbRelay2.OffChannel(readingSelector2[0]);
			lastCurrent = _readCurrent(lastCurrent);
			return lightMeter.Convert(lastCurrent);
		}

		private void _setChannels(UsbRelay relay, String pattern)
		{
			IList<int> onList = new List<int>();
			IList<int> offList = new List<int>();
			int i = 0;
			foreach (char c in pattern)
			{
				if (c == '1') onList.Add(readingSelector1[i]);
				else offList.Add(readingSelector1[i]);
				i++;
			}
			relay.SetChannels(onList, offList);
		}

		public void TurnOffMeter()
		{
			usbRelay1.SetChannels(new int[] { }, readingSelector1);
			usbRelay2.OffChannel(readingSelector2[0]);
		}

		/**
		 * Voltage Parameter Range Resolution
		 * 0	200 mV		100 nV
		 * 1	2 V			1 μV
		 * 2	20 V		10 μV
		 * 3	200 V		100 μV
		 * 4	1000 V		1 mV
		 * MIN	200 mV		100 nV
		 * MAX	1000 V		1 mV
		 * DEF	20 V		10 μV
		 **/

		/**
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
		 **/
		private char _determineCurrentRange(double lastReading)
		{
			lastReading = Math.Abs(lastReading);
			if (lastReading < 150E-6)
				return '0';
			if (lastReading < 1.5E-3)
				return '1';
			if (lastReading < 15E-3)
				return '2';

			if (lastReading < 150E-3)
				return '3';
			if (lastReading < 1.5)
				return '4';

			return '5';
		}
	}
}
