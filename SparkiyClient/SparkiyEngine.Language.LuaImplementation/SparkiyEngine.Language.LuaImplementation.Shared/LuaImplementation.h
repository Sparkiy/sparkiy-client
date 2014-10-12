#pragma once

#include "pch.h"
#include <lua.hpp>
#include "LuaScript.h"


namespace SparkiyEngine_Language_LuaImplementation
{
	public ref class LuaImplementation sealed
	{
	public:
		LuaImplementation();

		SparkiyEngine::Bindings::Language::ILanguageBindings^ GetLanguageBindings();

	internal:
		void AddScript(std::string id, LuaScript *script);
		LuaScript* GetScript(std::string id);

	private:
		void Initialize();

	private:
		SparkiyEngine::Bindings::Language::ILanguageBindings									^m_languageBindings;
		std::map<std::string, LuaScript *>                   m_scripts;
	};
}
