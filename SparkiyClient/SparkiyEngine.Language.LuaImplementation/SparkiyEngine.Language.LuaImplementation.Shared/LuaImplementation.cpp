#include "pch.h"
#include "LuaImplementation.h"
#include "LanguageBindings.h"

using namespace SparkiyEngine_Language_LuaImplementation;


LuaImplementation::LuaImplementation()
{
	this->Initialize();
}

void LuaImplementation::Initialize()
{
	// Create language bindings 
	this->m_languageBindings = ref new LanguageBindings(this);
}

SparkiyEngine::Bindings::Language::ILanguageBindings^ LuaImplementation::GetLanguageBindings()
{
	return this->m_languageBindings;
}

void LuaImplementation::AddScript(std::string id, LuaScript *script)
{
	this->m_scripts[id] = script;

	OutputDebugStringW(GetWString("Added script with id(" + id + ")\n").c_str());
}

LuaScript* LuaImplementation::GetScript(std::string id)
{
	return this->m_scripts[id];
}
