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
		
		SparkiyEngine::Bindings::Component::Language::ILanguageBindings^ GetLanguageBindings();

	internal:
		void AddScript(const char *id, LuaScript *script);
		LuaScript* GetScript(const char *id);

		void RaiseMethodRequestedEvent(SparkiyEngine::Bindings::Component::Common::MethodDeclarationDetails^ declaration, SparkiyEngine::Bindings::Component::Common::MethodDeclarationOverloadDetails^ overload, Platform::Array<Platform::Object^>^ inputValues);
		void RaiseMessageCreatedEvent(std::string message);

		Platform::Object^ CallMethod(const char *script, const char *name, SparkiyEngine::Bindings::Component::Common::MethodDeclarationOverloadDetails^ declaration, const Platform::Array<Platform::Object^>^ paramValues);

		std::map<const char *, SparkiyEngine::Bindings::Component::Common::MethodDeclarationDetails ^, StrCompare>				 m_declarations;

	private:
		void Initialize();

	private:
		SparkiyEngine::Bindings::Component::Language::ILanguageBindings												^m_languageBindings;
		std::map<const char *, LuaScript *, StrCompare>																 m_scripts;
	};
}
