using SparkiyEngine.Bindings.Component.Common;
using SparkiyEngine.Bindings.Component.Graphics;
using SparkiyEngine.Bindings.Component.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparkiyEngine.Bindings.Component.Engine
{
	public interface IEngineBindings : IBindingsBase
    {
	    event EngineMessagingEventHandler OnMessageCreated;

	    void AddMessage(EngineMessage message);

	    EngineMessage[] GetMessages();

	    void ClearMessages();
		void Reset();

		IGraphicsBindings GraphicsBindings { get; }

		ILanguageBindings LanguageBindings { get; }
	}
}
