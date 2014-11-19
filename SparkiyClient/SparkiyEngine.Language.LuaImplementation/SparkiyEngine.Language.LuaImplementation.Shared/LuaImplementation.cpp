#include "pch.h"
#include "LuaImplementation.h"
#include "LanguageBindings.h"

using namespace Platform;
using namespace SparkiyEngine_Language_LuaImplementation;
using namespace SparkiyEngine::Bindings::Language;
using namespace SparkiyEngine::Bindings::Language::Component;
using namespace SparkiyEngine::Bindings::Common::Component;

// Constructor
LuaImplementation::LuaImplementation()
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
// RaiseMethodRequestedEvent
//
void LuaImplementation::RaiseMethodRequestedEvent(MethodDeclarationDetails^ declaration, MethodDeclarationOverloadDetails^ overload, Platform::Array<Platform::Object^>^ inputValues)
{
	auto args = ref new MethodRequestEventArguments();
	args->Declaration = declaration;
	args->Overload = overload;
	args->InputValues = inputValues;

	this->m_languageBindings->RaiseMethodRequestedEvent(args);
}

//
// RaiseMessageCreatedEvent
//
void LuaImplementation::RaiseMessageCreatedEvent(std::string message) 
{
	auto args = ref new MessagingRequestEventArgs();
	args->Message = GetPString(message);

	this->m_languageBindings->RaiseMessageCreatedEvent(args);
}

// 
// CallMethod
// 
Object^ LuaImplementation::CallMethod(const char *script, const char *name, SparkiyEngine::Bindings::Common::Component::MethodDeclarationOverloadDetails^ declaration, const Array<Object^>^ paramValues)
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
