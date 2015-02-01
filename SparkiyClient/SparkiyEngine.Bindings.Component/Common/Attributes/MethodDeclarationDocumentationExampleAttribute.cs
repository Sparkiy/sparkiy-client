using System;

namespace SparkiyEngine.Bindings.Component.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    internal class MethodDeclarationDocumentationExampleAttribute : Attribute
    {
        public MethodDeclarationDocumentationExampleAttribute(string code)
        {
            this.Code = code;
        }

        public MethodDeclarationDocumentationExampleAttribute(string imageLink, string code)
            : this(code)
        {
            this.ImageLink = imageLink;
        }


        public string ImageLink { get; set; }

        public string Code { get; set; }
    }
}