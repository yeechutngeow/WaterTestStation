using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaterTestStation
{
/**
 * Synchronized multipole switch simulated with multiple relays
 */
	public class MultiPoleSwitch
	{
		public UsbRelay usbRelay;
		public int[] switches;

		public MultiPoleSwitch(UsbRelay usbRelay, int[] switches)
		{
			this.usbRelay = usbRelay;
			this.switches = switches;
		}

		public void ToggleOn()
		{
			usbRelay.SetChannels(switches, new int[] {});
		}

		public void ToggleOff()
		{
			usbRelay.SetChannels(new int[] {}, switches);
		}
	}
}
