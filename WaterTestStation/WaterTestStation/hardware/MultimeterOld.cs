using System;
using System.Collections.Generic;
using System.Threading;
using NationalInstruments.VisaNS;

namespace WaterTestStation.hardware
{
	// using multiplexer
	public class MultimeterOld
	{
		private MessageBasedSession mbSession; //Create Message based session

		private readonly int[] channelSelector;
		private readonly int[] readingSelector;
		private readonly UsbRelay usbRelay;

		public MultimeterOld(UsbRelay usbRelay, int[] channelSelector, int[] readingSelector)
		{
			this.channelSelector = channelSelector;
			this.readingSelector = readingSelector;
			this.usbRelay = usbRelay;
		}

		public void OpenSession()
		{
			const string strVISARsrc = "USB0::0x1AB1::0x0C94::DM3O161550090::INSTR";

			if (!Main.HasMultimeter)
				return;

			mbSession = (MessageBasedSession)ResourceManager.GetLocalManager().Open(strVISARsrc);

			mbSession.Write(":function:voltage:DC");
		}

		Random random = new Random();

		private float ReadMeter()
		{
			if (!Main.HasMultimeter)
			{
				Thread.Sleep(30); 
				return (float) random.NextDouble();
			}

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
		public float ReadABVoltage(int channelNumber)
		{
			SelectChannel(channelNumber);
			usbRelay.SetChannels(new int[]{}, readingSelector);
			return ReadMeter();
		}

		/* 
		 * reads charge & discharge current from A to B
		 */
		public float ReadABCurrent(int channelNumber)
		{
			SelectChannel(channelNumber);
			usbRelay.SetChannels(new[] {readingSelector[2]}, new int[] {readingSelector[0],readingSelector[1]});
			return ReadMeter();
		}

		/*
		 * Reads A voltage
		 */
		public float ReadARefVoltage(int channelNumber)
		{
			SelectChannel(channelNumber);
			usbRelay.SetChannels(new[] {readingSelector[1]}, new[] {readingSelector[0], readingSelector[2]});
			return ReadMeter();
		}

		public float ReadBRefVoltage(int channelNumber)
		{
			SelectChannel(channelNumber);
			usbRelay.SetChannels(new[] {readingSelector[0]}, new int[] {readingSelector[1], readingSelector[2]});
			return ReadMeter();
		}


		private void SelectChannel(int channelNumber)
		{
			/*
			 * Selector bits for multiplexer
			 *  0 - (0,0,0)
			 *  1 - (0,0,1)
			 *  2 - (0,1,0)
			 *  3 - (0,1,1) 
			 *  4 - etc
			 */
			List<int> onList = new List<int>();
			List<int> offList = new List<int>();
			// first bit
			if ((channelNumber & 1) > 0) onList.Add(channelSelector[2]);
			else offList.Add(channelSelector[2]);
			// second bit
			if ((channelNumber & 2) > 0) onList.Add(channelSelector[1]);
			else offList.Add(channelSelector[1]);
			// third bit
			if ((channelNumber & 4) > 0) onList.Add(channelSelector[0]);
			else offList.Add(channelSelector[0]);

			usbRelay.SetChannels(onList, offList);
		}
	}
}
