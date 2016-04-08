
namespace WaterTestStation.model
{
	public enum TestType
	{
		ForwardCharge = 0,  // readings: 0,1,2,3,4,5,10,20,30,60,90,... (+30)
		ReverseCharge = 1,	// readings: Ditto
		Discharge = 2,		// readings: Ditto
		OpenCircuit = 3		// readings: 0,5,30,60,120,... (+60)
	}
}
