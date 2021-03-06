﻿
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using WaterTestStation.hardware;
using WaterTestStation.model;

namespace WaterTestStation
{
	/**
	 * A request to the multimeter to do a measurement
	 */
	public class MeterRequest
	{
		private readonly TestStation TestStation;
		private readonly int cycle;
		private readonly int _stepStartTime;
		private readonly int _stepTime;
		private readonly TestProgramStep TestStep;
		private readonly bool logFlag;
		private static Thread thread;

		readonly static Stopwatch stopwatch = new Stopwatch();

		public MeterRequest(TestStation testStation, TestProgramStep pTestStep, int pCycle, int pStepStartTime, int pStepTime, bool flag)
		{
			this.TestStation = testStation;
			this.cycle = pCycle;
			this._stepStartTime = pStepStartTime;
			this._stepTime = pStepTime;
			this.TestStep = pTestStep;
			logFlag = flag;
		}

		public static void StartServiceQueue()
		{
			thread = new Thread(_serviceQueue);
			thread.Start();
		}

		public static void AbortThread()
		{
			thread.Abort();
		}

		public static void _serviceQueue()
		{
			while (true)
			{
				MeterRequest m;
				if (Main.MultimeterQueue.TryDequeue(out m))
				{
					TestType testType = (TestType) Enum.Parse(typeof(TestType), m.TestStep.TestType);
					m.TestStation.SwitchTestType(testType);

					stopwatch.Start();
					Debug.WriteLine("Begin meter readings:" + DateTime.Now);
					SelectStation(Main.mux, m.TestStation.StationNumber);

					// measures everything - will serves to cross check the circuit is correct initially.
					double ARefVoltage = Main.Multimeter.ReadARefVoltage(m.TestStation.StationNumber);
					double BRefVoltage = Main.Multimeter.ReadBRefVoltage(m.TestStation.StationNumber);
					double ABVoltage = Main.Multimeter.ReadABVoltage(m.TestStation.StationNumber);
					double ABCurrent = Main.Multimeter.ReadABCurrent(m.TestStation.StationNumber);
					Debug.WriteLine("End meter readings:" + DateTime.Now + "  Elapsed time:" + stopwatch.Elapsed);
					stopwatch.Stop();

					switch (m.TestStep.GetTestType())
					{
						case TestType.OpenCircuit:
							break;
						case TestType.ForwardCharge:
							break;
						case TestType.ReverseCharge:
							break;
						case TestType.Discharge:
							break;
					}
					Main.Multimeter.TurnOffMeter();
					m.TestStation.LogMeterReadings(m.TestStep, m.cycle, m._stepStartTime, m._stepTime, ARefVoltage, BRefVoltage, ABVoltage, ABCurrent, m.logFlag);
				}
				else
				{
					Thread.Sleep(20);
				}
			}
		}

		private static void _selectChannel(RelayMux mux, int channelNumber, ref List<int> onList, ref List<int> offList)
		{
			int i = 0;

			for (; i < channelNumber; i++)
				onList.Add(mux.relays[i]);

			for (; i < mux.relays.Count(); i++)
				offList.Add(mux.relays[i]);
		}

		// Select multiple multiplexers (on same relay module) all at once 
		public static void SelectStation(RelayMux[] muxList, int channelNumber)
		{
			var onList = new List<int>();
			var offList = new List<int>();

			foreach (var mux in muxList)
			{
				_selectChannel(mux, channelNumber, ref onList, ref offList);
			}

			muxList[0].usbRelay.SetChannels(onList, offList);
		}

	}
}
