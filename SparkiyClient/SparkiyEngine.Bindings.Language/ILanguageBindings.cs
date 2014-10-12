using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparkiyEngine.Bindings.Language
{
    public interface ILanguageBindings
    {
	    void LoadScript(string id, string content);
    }
}
