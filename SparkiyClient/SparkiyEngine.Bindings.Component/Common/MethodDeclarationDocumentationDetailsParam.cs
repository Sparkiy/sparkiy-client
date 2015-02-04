namespace SparkiyEngine.Bindings.Component.Common
{
    public sealed class MethodDeclarationDocumentationDetailsParam
    {
        public string Name { get; set; }

        public DataTypes Type { get; set; }

        public string Description { get; set; }


        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is MethodDeclarationDocumentationDetailsParam && Equals((MethodDeclarationDocumentationDetailsParam) obj);
        }

        private bool Equals(MethodDeclarationDocumentationDetailsParam other)
        {
            return string.Equals(Name, other.Name);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }
}