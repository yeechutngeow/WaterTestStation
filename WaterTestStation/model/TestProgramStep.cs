
using System;

namespace WaterTestStation.model
{
	public class TestProgramStep
	{
		virtual public int? Id { get; set; }
		virtual public int? TestProgramId { get; set; }
		virtual public string TestType { get; set; }
		virtual public int Duration { get; set; }
		virtual public int RowOrder { get; set; }

		virtual public TestType GetTestType()
		{
			return (TestType) Enum.Parse(typeof(TestType), TestType);
		}
	}
}
