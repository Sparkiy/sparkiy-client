#pragma once

#include "pch.h"
#include "LuaImplementation.h"

using namespace Platform;
using namespace Windows::Foundation::Collections;

namespace SparkiyEngine_Language_LuaImplementation
{
	private ref class LanguageBindings : SparkiyEngine::Bindings::Language::ILanguageBindings
	{
	public:
		virtual void MapToGraphicsMethods(IMapView<String ^, SparkiyEngine::Bindings::Common::Component::MethodDeclarationDetails ^> ^declarations);
		virtual void LoadScript(String ^id, String ^content);
		virtual void StartScript(String ^id);

		virtual event SparkiyEngine::Bindings::Language::Component::MethodRequestEventHandler^ OnMethodRequested;
		virtual void RaiseMethodRequestedEvent(SparkiyEngine::Bindings::Language::Component::MethodRequestEventArguments^ args);

	internal:
		LanguageBindings(LuaImplementation ^impl);

	private:
		LuaImplementation																					^m_luaImpl;

		// State
		bool																								 m_didLoadScript;
	};
}
