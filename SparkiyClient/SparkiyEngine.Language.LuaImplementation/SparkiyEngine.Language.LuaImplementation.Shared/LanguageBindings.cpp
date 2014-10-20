#include "pch.h"
#include "LanguageBindings.h"

using namespace Platform;
using namespace Windows::Foundation::Collections;
using namespace SparkiyEngine_Language_LuaImplementation;
using namespace SparkiyEngine::Bindings::Common::Component;
using namespace SparkiyEngine::Bindings::Language::Component;

LanguageBindings::LanguageBindings(LuaImplementation ^impl) :
m_luaImpl(impl),
m_didLoadScript(false)
{
}

//
// MapToGraphicsMethods
//
void LanguageBindings::MapToGraphicsMethods(IMapView<String ^,MethodDeclarationDetails ^> ^declarations)
{
	// Check if we didnt already loaded some scripts, in case we did, throw exception
	if (this->m_didLoadScript) 
	{
		throw ref new Exception(-1, "Can't map methods, already loaded at least one script. Map methods first before loading scripts.");
	}

	// Populate map in LuaImplementation instance, IMapView to std::map
	std::for_each(begin(declarations), end(declarations),
		[=](IKeyValuePair<String^, MethodDeclarationDetails ^>^ decl) {
		this->m_luaImpl->m_declarations[GetCString(decl->Key)] = decl->Value;
	});
}

//
// LoadScript
//
void LanguageBindings::LoadScript(String ^id, String ^content)
{
	// Convert to C compatible strings
	const char *cId = GetCString(id);
	const char *cContent = GetCString(content);

	// Create script
	LuaScript *script = new LuaScript(this->m_luaImpl, cId, cContent);

	// Map methods
	std::for_each(this->m_luaImpl->m_declarations.begin(), this->m_luaImpl->m_declarations.end(),
		[=](std::pair<const char *, MethodDeclarationDetails ^> decl)
	{
		auto details = decl.second;

		script->RegisterMethod(details);
	});

	// Load content to the script
	script->Load();

	// Add to the scripts list
	this->m_luaImpl->AddScript(cId, script);

	// mark that binding loaded at least one script
	this->m_didLoadScript = true;
}

// 
// StartScript
//
void LanguageBindings::StartScript(String ^id)
{
	// Convert to C compatible strings
	const char *cId = GetCString(id);

	// Retrieve script
	auto script = this->m_luaImpl->GetScript(cId);

	// Start script
	script->Start();
}

//
// RaiseMethodRequestedEvent
//
void LanguageBindings::RaiseMethodRequestedEvent(MethodRequestEventArguments^ args)
{
	// Raise event with arguments
	this->OnMethodRequested::raise(this, args);
}

//
// RaiseMethodRequestedEvent
//
void LanguageBindings::RaiseMessageCreatedEvent(MessagingRequestEventArgs^ args)
{
	// Raise event with arguments
	this->OnMessageCreated::raise(this, args);
}
