using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaterTestStation.model
{
	class TestRecord
	{
		public virtual int Id { get; set; }
		public virtual int TestProgramId { get; set; }
		public virtual string DataSet { get; set; }
		public virtual string TestSummary { get; set; }
		public virtual string Description { get; set; }
		public virtual string VesselId { get; set; }
		public virtual string Sample { get; set; }
		public virtual int StationNumber { get; set; }
		public virtual DateTime TestStart { get; set; }
		public virtual DateTime? TestEnd { get; set; }
		public virtual int Cycles { get; set; }
		public virtual int LeadTime { get; set; }
		public virtual int TotalDuration { get; set; }

	}
}
