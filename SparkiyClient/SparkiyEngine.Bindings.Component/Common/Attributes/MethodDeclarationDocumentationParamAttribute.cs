using System;

namespace SparkiyEngine.Bindings.Component.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    internal class MethodDeclarationDocumentationParamAttribute : Attribute
    {
        public MethodDeclarationDocumentationParamAttribute(string name, DataTypes type, string description)
        {
            this.Name = name;
            this.Type = type;
            this.Description = description;
        }


        public string Name { get; }

        public DataTypes Type { get; }

        public string Description { get; }


        protected bool Equals(MethodDeclarationDocumentationParamAttribute other)
        {
            return base.Equals(other) && string.Equals(Name, other.Name);
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MethodDeclarationDocumentationParamAttribute) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ (Name?.GetHashCode() ?? 0);
            }
        }
    }
}