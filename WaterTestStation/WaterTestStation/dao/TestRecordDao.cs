using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using WaterTestStation.model;

namespace WaterTestStation.dao
{
	class TestRecordDao
	{
		public TestRecord CreateTestRecord(int testProgramId, string sample, string description, string vesselId, 
				int StationNumber, string leadTime, string testDataSet)
		{

			TestRecord r = new TestRecord
				{
					LeadTime = Util.ParseInt(leadTime),
					Description = description,
					VesselId = vesselId,
					Sample = sample,
					StationNumber = StationNumber,
					TestStart = DateTime.Now,
					DataSet = testDataSet
				};

			TestProgram testProgram = new TestProgramDao().FindById(testProgramId);
			if (testProgram != null)
			{
				r.TestProgramId = testProgramId;
				r.TestSummary = testProgram.TestSummary();
			}

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
		                          double ARefVoltage, double BRefVoltage, double ABVoltage, double ABCurrent,
		                          double temperature, double lightLevel)
		{
			LogTestData(testRecordId, testType, cycle, elapsedTime, stepTime, ARefVoltage, BRefVoltage, ABVoltage, ABCurrent,
		                          temperature, lightLevel, "");
		}

		internal void LogTestData(int testRecordId, TestType testType, int cycle, int elapsedTime, int stepTime, 
			double ARefVoltage, double BRefVoltage, double ABVoltage, double ABCurrent, double temperature, double lightLevel, string notes)
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
					ABCurrent = ABCurrent,
					Temperature = temperature,
					LightLevel = lightLevel,
					Notes = notes
				};
			using (ISession session = SessionFactory.OpenSession)
			{
				session.SaveOrUpdate(testData);
			}
		}

		internal IList<TestData> GetTestData(int testRecordId, bool ignoreLeadTime)
		{
			using (ISession session = SessionFactory.OpenSession)
			{
				string sql = "from TestData where TestRecordId = :testRecordId ";
				if (ignoreLeadTime) sql += " and cycle > 0 ";
				sql += "order by Id ";
				return session.CreateQuery(sql)
				       .SetParameter("testRecordId", testRecordId)
				       .List().Cast<TestData>().ToList();
			}
		}
	}
}
