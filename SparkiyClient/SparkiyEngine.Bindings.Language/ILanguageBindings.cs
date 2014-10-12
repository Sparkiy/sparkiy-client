using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparkiyEngine.Bindings.Common;
using SparkiyEngine.Bindings.Common.Attributes;
using SparkiyEngine.Bindings.Common.Component;

namespace SparkiyEngine.Bindings.Language
{
    public interface ILanguageBindings
    {
	    void MapToGraphicsMethods(IReadOnlyDictionary<string, MethodDeclarationDetails> declarations);

	    void LoadScript(string id, string content);

	    void StartScript(string id);
    }
}
