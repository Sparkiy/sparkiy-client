using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparkiyEngine.Bindings.Common;
using SparkiyEngine.Bindings.Common.Component;
using SparkiyEngine.Bindings.Language.Component;

namespace SparkiyEngine.Bindings.Language
{
	public interface ILanguageBindings : IBindingsBase
	{
		//
		// Methods
		//
		event MethodRequestEventHandler OnMethodRequested;
		void RaiseMethodRequestedEvent(MethodRequestEventArguments args);
		void MapToGraphicsMethods(IReadOnlyDictionary<string, MethodDeclarationDetails> declarations);

		//
		// Messaging 
		//
		event MessagingRequestEventHandler OnMessageCreated;
		void RaiseMessageCreatedEvent(MessagingRequestEventArgs args);

		//
		// Scripts
		// 
	    void LoadScript(string id, string content);
	    void StartScript(string id);

		// 
		// Settings
		//
		void Reset();
	}
}
