using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using WaterTestStation.model;

namespace WaterTestStation.dao
{
	class TestRecordDao
	{
		public TestRecord CreateTestRecord(int testProgramId, string sample, string description, string vesselId, int StationNumber)
		{
			TestProgram testProgram = new TestProgramDao().FindById(testProgramId);

			TestRecord r = new TestRecord
				{
					Cycles = testProgram.Cycles,
					Description = description,
					VesselId = vesselId,
					Sample = sample,
					StationNumber = StationNumber,
					TestStart = DateTime.Now,
					TestProgramId = testProgramId,
					TestSummary = testProgram.TestSummary()
				};

			using (ISession session = SessionFactory.OpenSession)
			{
				session.SaveOrUpdate(r);
			}

			return r;
		}

		public void saveOrUpdate(TestRecord testRecord)
		{
			using (ISession session = SessionFactory.OpenSession)
			{
				//session.Evict(testRecord);
				session.SaveOrUpdate(testRecord);
				session.Flush();
			}
		}


		public TestRecord FindById(int id)
		{
			using (ISession session = SessionFactory.OpenSession)
			{
				return session.Get<TestRecord>(id);
			}
		}

		public IList<TestRecord> FindAll()
		{
			using (ISession session = SessionFactory.OpenSession)
			{
				return session.CreateQuery("from TestRecord order by TestStart desc")
				              .List().Cast<TestRecord>().ToList();
			}
			
		}

		internal void LogTestData(int testRecordId, TestType testType, int cycle, int elapsedTime, int stepTime, 
			double ARefVoltage, double BRefVoltage, double ABVoltage, double ABCurrent)
		{
			TestData testData = new TestData
				{
					TestRecordId = testRecordId,
					TimeStamp = DateTime.Now,
					TestType = testType.ToString(),
					Cycle = cycle,
					ElapsedTime = elapsedTime,
					StepTime = stepTime,
					ARefVoltage = ARefVoltage,
					BRefVoltage = BRefVoltage,
					ABVoltage = ABVoltage,
					ABCurrent = ABCurrent
				};
			using (ISession session = SessionFactory.OpenSession)
			{
				session.SaveOrUpdate(testData);
			}
		}

		internal IList<TestData> GetTestData(int testRecordId)
		{
			using (ISession session = SessionFactory.OpenSession)
			{
				return session.CreateQuery("from TestData where TestRecordId = :testRecordId order by Id")
				       .SetParameter("testRecordId", testRecordId)
				       .List().Cast<TestData>().ToList();
			}
		}
	}
}
