using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace SparkiyEngine.Bindings.Common.Test.Windows
{
	[TestClass]
	public class LatestBindingVersionTest
	{
		[TestMethod]
		public void VersionNotNull()
		{
			LatestBindingsVersion.Version.Should().NotBeNull();
		}

		[TestMethod]
		public void VersionNotEmpty()
		{
			// Major version cant be 0
			LatestBindingsVersion.Version.Major.Should().NotBe(0);

			// Date must be filled
			LatestBindingsVersion.Version.Date.Should().NotBe(new DateTime());
		}
	}
}