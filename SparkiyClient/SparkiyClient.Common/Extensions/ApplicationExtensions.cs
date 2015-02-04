using System;
using Windows.ApplicationModel;
using Windows.UI.Xaml;

namespace SparkiyClient.Common.Extensions
{
	public static class ApplicationExtensions
	{
		public static string GetVersion(this Application @this, bool includeRevision = false)
		{
			var version = Package.Current.Id.Version;
			return String.Format("{0}.{1}.{2}{3}",
				version.Major,
				version.Minor,
				version.Build,
				includeRevision ? "." + version.Revision : String.Empty);
		}
	}
}