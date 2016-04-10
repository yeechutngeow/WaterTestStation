using System;
using System.Collections.Generic;
using System.IO.Ports;

namespace WaterTestStation
{
	public class UsbRelay
	{
		private static readonly SerialPort SerialPort = new SerialPort();
		private static Int32 bitmap = 0;

		public UsbRelay(String comPort)
		{
			SerialPort.PortName = "COM" + comPort;
			SerialPort.BaudRate = 9600;
			//SerialPort.Open();
		}

		public void OpenComPort(int portNumber)
		{
			if (!Main.HasRelay) return;

			if (!SerialPort.IsOpen)
			{
				SerialPort.PortName = "COM" + portNumber;
				SerialPort.Open();
			}
		}

		private String SendCommand(String command)
		{
			if (!Main.HasRelay) return "";

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
			SetChannels(new int[] {channelNumber}, new int[] {});
		}

		public void OffChannel(int channelNumber)
		{
			SetChannels(new int[] { }, new int[] { channelNumber });
		}
		                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             
		public void SetChannels(IEnumerable<int> onList, IEnumerable<int> offList)
		{
			Int32 onMask = 0;
			foreach (var channel in onList)
			{
				onMask = onMask | (1 << channel);
			}
			Int32 offMask = 0;
			foreach (var channel in offList)
			{
				offMask = offMask | (1 << channel);
			}
			bitmap = bitmap | onMask;
			bitmap = bitmap & ~offMask;

			String command = "relay writeall " + bitmap.ToString("X8").ToLower();
			SendCommand(command);
		}
	}
}
