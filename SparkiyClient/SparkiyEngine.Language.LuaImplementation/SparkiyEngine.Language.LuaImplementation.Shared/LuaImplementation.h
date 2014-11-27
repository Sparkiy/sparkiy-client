#pragma once

#include "pch.h"
#include <lua.hpp>
#include "LuaScript.h"

namespace SparkiyEngine_Language_LuaImplementation
{
	public ref class LuaImplementation sealed
	{
	public:
		LuaImplementation(SparkiyEngine::Bindings::Component::Engine::IEngineBindings^ engine);
		
		SparkiyEngine::Bindings::Component::Language::ILanguageBindings^ GetLanguageBindings();

	internal:
		void AddScript(const char *id, LuaScript *script);
		LuaScript* GetScript(const char *id);

		void MethodRequest(SparkiyEngine::Bindings::Component::Common::MethodDeclarationDetails^ declaration, SparkiyEngine::Bindings::Component::Common::MethodDeclarationOverloadDetails^ overload, const Platform::Array<Platform::Object^>^ inputValues);
		void CreateMessage(std::string message);

		Platform::Object^ CallMethod(const char *script, const char *name, SparkiyEngine::Bindings::Component::Common::MethodDeclarationOverloadDetails^ declaration, const Platform::Array<Platform::Object^>^ paramValues);

		void SetConstant(const char *name, Platform::Object^ value, SparkiyEngine::Bindings::Component::Common::DataTypes dataType);
		void SetVariable(const char *name, Platform::Object^ value, SparkiyEngine::Bindings::Component::Common::DataTypes dataType);

		std::map<const char *, SparkiyEngine::Bindings::Component::Common::MethodDeclarationDetails ^, StrCompare>				 m_declarations;

	private:
		void Initialize();

	private:
		SparkiyEngine::Bindings::Component::Engine::IEngineBindings													^m_engine;
		SparkiyEngine::Bindings::Component::Language::ILanguageBindings												^m_languageBindings;
		std::map<const char *, LuaScript *, StrCompare>																 m_scripts;
	};
}
