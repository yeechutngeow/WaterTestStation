using System;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg;

namespace WaterTestStation.model
{
	public sealed class SessionFactory
	{
		private static volatile ISessionFactory iSessionFactory;
		private static readonly object syncRoot = new Object();

		public static ISession OpenSession
		{
			get
			{
				if (iSessionFactory == null)
				{
					lock (syncRoot)
					{
						if (iSessionFactory == null)
						{
							Configuration configuration = new Configuration();
							configuration.Configure();
							configuration.AddAssembly(typeof (TestProgram).Assembly);
							//configuration.AddAssembly(Assembly.GetCallingAssembly());
							iSessionFactory = configuration.BuildSessionFactory();
						}
					}
				}
				return iSessionFactory.OpenSession();
			}
		}
	} 
}
