#pragma once

#include "pch.h"
#include <LuaImplementation.h>


namespace SparkiyEngine_Language_LuaImplementation
{
	private ref class LanguageBindings : SparkiyEngine::Bindings::Language::ILanguageBindings
	{
	public:
		virtual void LoadScript(Platform::String ^id, Platform::String ^content);
		virtual void StartScript(Platform::String ^id);

	internal:
		LanguageBindings(LuaImplementation ^impl);

	private:
		LuaImplementation							^luaImpl;
	};
}
