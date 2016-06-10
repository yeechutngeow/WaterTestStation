using System.Collections.Generic;
using System.Linq;

//
// Multiplexer build using a cascade of relays
//
namespace WaterTestStation.hardware
{
	public class RelayMux
	{
		public UsbRelay usbRelay;
		public int[] relays;

		public RelayMux(UsbRelay usbRelay, int[] relays)
		{
			this.usbRelay = usbRelay;
			this.relays = relays;
		}

		// starts with 0
		public void SelectChannel(int channelNumber)
		{
			List<int> onList = new List<int>();
			List<int> offList = new List<int>();

			int i = 0;
			for (; i < channelNumber; i++)
				onList.Add(relays[i]);

			for (; i < relays.Count(); i++)
				offList.Add(relays[i]);

			usbRelay.SetChannels(onList, offList);
		}

	}
}
