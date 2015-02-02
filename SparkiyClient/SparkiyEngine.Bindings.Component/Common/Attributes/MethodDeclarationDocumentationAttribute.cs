using System;

namespace SparkiyEngine.Bindings.Component.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    internal class MethodDeclarationDocumentationAttribute : Attribute
    {
        public MethodDeclarationDocumentationAttribute(string category, string summary)
        {
            this.Category = category;
            this.Summary = summary;
        }

        public MethodDeclarationDocumentationAttribute(string category, string summary, string[] seeAlso)
            : this(category, summary)
        {
            this.SeeAlso = seeAlso;
        }

        public string Category { get; set; }

        public string Summary { get; set; }

        public string[] SeeAlso { get; set; }
    }
}