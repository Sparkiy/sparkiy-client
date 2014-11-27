#include "pch.h"
#include "LuaImplementation.h"
#include "LanguageBindings.h"

using namespace Platform;
using namespace SparkiyEngine_Language_LuaImplementation;
using namespace SparkiyEngine::Bindings::Component::Common;
using namespace SparkiyEngine::Bindings::Component::Language;
using namespace SparkiyEngine::Bindings::Component::Engine;

// Constructor
LuaImplementation::LuaImplementation(IEngineBindings^ engine) 
	: m_engine(engine)
{
	this->Initialize();
}

//
// Initialize
//
void LuaImplementation::Initialize()
{
	// Create language bindings 
	this->m_languageBindings = ref new LanguageBindings(this);
}

// 
// GetLanguageBindings
//
ILanguageBindings^ LuaImplementation::GetLanguageBindings()
{
	return this->m_languageBindings;
}

//
// AddScript
//
void LuaImplementation::AddScript(const char * id, LuaScript *script)
{
	this->m_scripts[id] = script;

	OutputDebugStringW(GetWString("Added script with id(" + GetString(id) + ")\n").c_str());
}

// 
// GetScript
//
LuaScript* LuaImplementation::GetScript(const char * id)
{
	return this->m_scripts[id];
}

//
// MethodRequest
//
void LuaImplementation::MethodRequest(MethodDeclarationDetails^ declaration, MethodDeclarationOverloadDetails^ overload, const Array<Object^>^ inputValues)
{
	Object^ returnValue = this->m_engine->MethodRequested(declaration, overload, inputValues);
	// TODO Implement return value
}

//
// RaiseMessageCreatedEvent
//
void LuaImplementation::CreateMessage(std::string message)
{
	auto messageInstance = ref new EngineMessage();
	messageInstance->Message = GetPString(message);
	messageInstance->Source = this->GetLanguageBindings();
	messageInstance->SourceType = BindingTypes::Language;

	this->m_engine->AddMessage(messageInstance);
}

// 
// CallMethod
// 
Object^ LuaImplementation::CallMethod(const char *script, const char *name, MethodDeclarationOverloadDetails^ declaration, const Array<Object^>^ paramValues)
{
	// Check if this call is script wildcard
	if (script == nullptr) 
	{
		for (std::map<const char *, LuaScript *, StrCompare>::iterator iter = this->m_scripts.begin(); iter != this->m_scripts.end(); ++iter)
		{
			iter->second->CallMethod(name, declaration, paramValues);
		}
		return NULL;
	}
	else 
	{
		// TODO Check if script exists
		return this->m_scripts[script]->CallMethod(name, declaration, paramValues);
	}
}

//
// SetConstant
//
void LuaImplementation::SetConstant(const char *name, Object^ value, DataTypes dataType)
{
	// Set constant to all scripts
	for (std::map<const char *, LuaScript *, StrCompare>::iterator iter = this->m_scripts.begin(); iter != this->m_scripts.end(); ++iter)
	{
		iter->second->SetConstant(name, value, dataType);
	}
}

//
// SetVariable
//
void LuaImplementation::SetVariable(const char *name, Object^ value, DataTypes dataType)
{
	// Set variables to all scripts
	for (std::map<const char *, LuaScript *, StrCompare>::iterator iter = this->m_scripts.begin(); iter != this->m_scripts.end(); ++iter)
	{
		iter->second->SetVariable(name, value, dataType);
	}
}
