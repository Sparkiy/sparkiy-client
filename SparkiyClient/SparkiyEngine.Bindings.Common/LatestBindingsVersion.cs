using System;

namespace SparkiyEngine.Bindings.Common
{
	public static class LatestBindingsVersion
	{
		public static readonly BindingsVersion Version = new BindingsVersion()
		{
			Date = new DateTime(2014, 10, 10),

			Major = 1,
			Minor = 0,
			Revision = 0
		};
	}
}