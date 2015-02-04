using System.Collections.Generic;

namespace SparkiyEngine.Bindings.Component.Common
{
    public sealed class MethodDeclarationDocumentationDetails
    {
        public MethodDeclarationDetails Declaration { get; set; }

        public string Category { get; set; }

        public string Summary { get; set; }

        public IEnumerable<MethodDeclarationDocumentationDetails> SeeAlso { get; set; }

        public IEnumerable<MethodDeclarationDocumentationDetailsParam> Params { get; set; }

        public IEnumerable<MethodDeclarationDocumentationDetailsExample> Examples { get; set; } 
    }
}