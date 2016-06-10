using System.Collections.Generic;
using NHibernate;
using WaterTestStation.model;

namespace WaterTestStation.dao
{
	class TestProgramDao
	{
		public IList<TestProgram> GetProgramsList()
		{
			IList<TestProgram> list;
			using (ISession session = SessionFactory.OpenSession)
			{
				IQuery query = session.CreateQuery("FROM TestProgram where Active = 1");
				list = query.List<TestProgram>();
			}
			return list;
		}

		public TestProgram FindById(int id)
		{
			using (ISession session = SessionFactory.OpenSession)
			{
				return session.Get<TestProgram>(id);
			}
		}

	}
}
