#pragma once

#include "pch.h"
#include "LuaImplementation.h"

using namespace Platform;
using namespace Windows::Foundation::Collections;

namespace SparkiyEngine_Language_LuaImplementation
{
	public ref class LanguageBindings sealed : SparkiyEngine::Bindings::Language::ILanguageBindings
	{
	public:
		virtual void MapToGraphicsMethods(IMapView<String ^, SparkiyEngine::Bindings::Common::Component::MethodDeclarationDetails ^> ^declarations);
		virtual void LoadScript(String ^id, String ^content);
		virtual void StartScript(String ^id);

		// Methods
		virtual event SparkiyEngine::Bindings::Language::Component::MethodRequestEventHandler^ OnMethodRequested;
		virtual void RaiseMethodRequestedEvent(SparkiyEngine::Bindings::Language::Component::MethodRequestEventArguments^ args);
		virtual Object^ CallMethod(String^ script, String^ name, SparkiyEngine::Bindings::Common::Component::MethodDeclarationOverloadDetails^ declaration, const Array<Object^>^ paramValues);
		virtual Object^ CallMethod(String^ name, SparkiyEngine::Bindings::Common::Component::MethodDeclarationOverloadDetails^ declaration, const Array<Object^>^ paramValues);

		// Messaging
		virtual event SparkiyEngine::Bindings::Language::Component::MessagingRequestEventHandler^ OnMessageCreated;
		virtual void RaiseMessageCreatedEvent(SparkiyEngine::Bindings::Language::Component::MessagingRequestEventArgs^ args);

		// Settings
		virtual void Reset();

	internal:
		LanguageBindings(LuaImplementation ^impl);

	private:
		LuaImplementation																					^m_luaImpl;

		// State
		bool																								 m_didLoadScript;
	};
}
