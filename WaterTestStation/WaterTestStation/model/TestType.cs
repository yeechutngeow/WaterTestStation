
namespace WaterTestStation.model
{
	public enum TestType
	{
		OpenCircuit,	// readings: 0,5,30,60,120,... (+60)
		Discharge,		// readings: Ditto
		ForwardCharge,  // readings: 0,1,2,3,4,5,10,20,30,60,90,... (+30)
		ReverseCharge,	// readings: Ditto
	}
}
