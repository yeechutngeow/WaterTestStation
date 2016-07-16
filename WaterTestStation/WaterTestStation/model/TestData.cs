using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaterTestStation.model
{
	class TestData
	{
		public virtual int Id { get; set; }
		public virtual int TestRecordId { get; set; }
		public virtual DateTime TimeStamp { get; set; }
		public virtual String TestType { get; set; }
		public virtual int Cycle { get; set; }
		public virtual int ElapsedTime { get; set; }
		public virtual int StepTime { get; set; }
		public virtual double ARefVoltage { get; set; }
		public virtual double BRefVoltage { get; set; }
		public virtual double ABVoltage { get; set; }
		public virtual double ABCurrent { get; set; }
		public virtual double Temperature { get; set; }
		public virtual double LightLevel { get; set; }
	}
}
