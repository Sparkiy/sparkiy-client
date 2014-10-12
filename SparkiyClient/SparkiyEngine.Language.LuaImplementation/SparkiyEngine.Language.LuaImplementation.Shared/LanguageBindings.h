#pragma once

#include "pch.h"
#include <LuaImplementation.h>

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

	internal:
		LanguageBindings(LuaImplementation ^impl);

	private:
		LuaImplementation																					^m_luaImpl;
		IMapView<String ^, SparkiyEngine::Bindings::Common::Component::MethodDeclarationDetails ^>			^m_declarations;

		// State
		bool																								 m_didLoadScript;
	};
}
