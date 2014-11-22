using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparkiyEngine.Bindings.Common;
using SparkiyEngine.Bindings.Common.Component;
using SparkiyEngine.Bindings.Graphics;
using SparkiyEngine.Bindings.Language;

namespace SparkiyEngine.Bindings.Engine
{
	public interface IEngineBindings : IBindingsBase
    {
	    event EngineMessagingEventHandler OnMessageCreated;

	    void HandleMessageCreated(EngineMessage message);

	    EngineMessage[] GetMessages();

	    void ClearMessages();
		void Reset();

		IGraphicsBindings GraphicsBindings { get; }

		ILanguageBindings LanguageBindings { get; }
	}
}
