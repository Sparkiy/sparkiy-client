using System;

namespace SparkiyEngine.Bindings.Common.Component
{
	public sealed class MethodDeclarationOverloadDetails
	{
		public MethodTypes Type { get; set; }

		public DataTypes[] Input { get; set; }

		public DataTypes[] Return { get; set; }

		public string Uid { get; set; }

		public Object Method { get; set; }


		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj is MethodDeclarationOverloadDetails && Equals((MethodDeclarationOverloadDetails) obj);
		}

		private bool Equals(MethodDeclarationOverloadDetails other)
		{
			return string.Equals(Uid, other.Uid);
		}

		public override int GetHashCode()
		{
			return (Uid != null ? Uid.GetHashCode() : 0);
		}
	}
}