using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkiyEngine.Bindings.Common
{
	public struct BindingsVersion
	{
		public int Major;
		public int Minor;
		public int Revision;

		public DateTime Date;

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}
			return obj is BindingsVersion && this.Equals((BindingsVersion) obj);
		}

		public bool Equals(BindingsVersion other)
		{
			return
				(other.Major == 0 && other.Minor == 0 && other.Revision == 0) ||
				(this.Major == 0 && this.Minor == 0 && this.Revision == 0) ||
				(other.Major == this.Major && other.Minor == this.Minor && other.Revision == this.Revision);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int hashCode = Major;
				hashCode = (hashCode * 397) ^ Minor;
				hashCode = (hashCode * 397) ^ Revision;
				return hashCode;
			}
		}
	}
}
