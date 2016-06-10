using System;
using System.Collections.Generic;

namespace WaterTestStation.model
{
	[Serializable]
	public class TestProgram
	{
		virtual public int Id { get; set; }
		virtual public String Description { get; set; }
		virtual public String Title { get; set; }
		virtual public String Name { get; set; }
		virtual public int Cycles { get; set; }	// number of cycles to run the program
		virtual public bool Active { get; set; }
		virtual public DateTime DateCreated { get; set; }
		virtual public DateTime LastUpdated { get; set; }

		private IList<TestProgramStep> _testProgramSteps = new List<TestProgramStep>();

		virtual public IList<TestProgramStep> TestProgramSteps
		{
			get { return _testProgramSteps; }
			set { _testProgramSteps = value; }
		}

		public virtual String TestSummary()
		{
			String testSummary = "";
			foreach (var s in TestProgramSteps)
				testSummary += s.TestType + "(" + s.Duration + ");";
			return testSummary;
		}
	}
}
