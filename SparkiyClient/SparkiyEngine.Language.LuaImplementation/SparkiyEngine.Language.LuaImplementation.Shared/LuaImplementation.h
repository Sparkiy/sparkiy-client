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
		void AddScript(const char *id, LuaScript *script);
		LuaScript* GetScript(const char *id);

		std::map<const char *, SparkiyEngine::Bindings::Common::Component::MethodDeclarationDetails ^, StrCompare>				 m_declarations;

	private:
		void Initialize();

	private:
		SparkiyEngine::Bindings::Language::ILanguageBindings														^m_languageBindings;
		std::map<const char *, LuaScript *, StrCompare>																 m_scripts;
	};
}
