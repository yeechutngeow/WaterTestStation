using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;

namespace WaterTestStation.hardware
{
	public class UsbRelay
	{
		private readonly SerialPort SerialPort = new SerialPort();
		private static Int32 bitmap;

		public UsbRelay(int comPort)
		{
			SerialPort.PortName = "COM" + comPort;
			SerialPort.BaudRate = 9600;
			//SerialPort.Open();
		}

		public void OpenComPort(int portNumber)
		{
			if (Config.HasRelay) return;

			if (!SerialPort.IsOpen)
			{
				SerialPort.PortName = "COM" + portNumber;
				SerialPort.Open();
			}
		}

		public void Close()
		{
			if (Config.HasRelay)
				if (SerialPort.IsOpen)
					SerialPort.Close();
		}

		private String SendCommand(String command)
		{
			if (Config.HasRelay) return "";

			SerialPort.DiscardInBuffer();
			SerialPort.Write(command + "\r");
			System.Threading.Thread.Sleep(10);
			String v = null;
			try
			{
				v = SerialPort.ReadExisting();
			}
// ReSharper disable EmptyGeneralCatchClause
			catch
// ReSharper restore EmptyGeneralCatchClause
			{
			}
			return v;
		}

		public void OnChannel(int channelNumber)
		{
			SetChannels(new[] {channelNumber}, new int[] {});
		}

		public void OffChannel(int channelNumber)
		{
			SetChannels(new int[] { }, new[] { channelNumber });
		}
		                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             
		public void SetChannels(IEnumerable<int> onList, IEnumerable<int> offList)
		{
			Int32 onMask = onList.Aggregate(0, (current, channel) => current | (1 << channel));
			Int32 offMask = offList.Aggregate(0, (current, channel) => current | (1 << channel));
			bitmap = bitmap | onMask;
			bitmap = bitmap & ~offMask;

			String command = "relay writeall " + bitmap.ToString("X8").ToLower();
			SendCommand(command);
		}

	}
}
