using System;

namespace SparkiyEngine.Bindings.Common
{
	public static class LatestBindingsVersion
	{
		static LatestBindingsVersion()
		{
			LatestBindingsVersion.Version = new BindingsVersion()
			{
				Major = 1,
				Minor = 0,
				Revision = 0
			};
		}


		public static BindingsVersion Version { get; private set; }
	}
}