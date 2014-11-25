#include "pch.h"
#include "LanguageBindings.h"

using namespace Platform;
using namespace Windows::Foundation::Collections;
using namespace SparkiyEngine_Language_LuaImplementation;
using namespace SparkiyEngine::Bindings::Component::Common;
using namespace SparkiyEngine::Bindings::Component::Language;

LanguageBindings::LanguageBindings(LuaImplementation ^impl) :
m_luaImpl(impl),
m_didLoadScript(false)
{
}

//
// MapToGraphicsMethods
//
void LanguageBindings::MapToGraphicsMethods(IMapView<String ^, MethodDeclarationDetails ^> ^declarations)
{
	// Check if we did already load some scripts throw an exception
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

Object^ LanguageBindings::CallMethod(String^ name, MethodDeclarationOverloadDetails^ declaration, const Array<Object^> ^paramValues) 
{
	const char *cName = GetCString(name);

	return this->m_luaImpl->CallMethod(nullptr, cName, declaration, paramValues);
}

Object^ LanguageBindings::CallMethod(String^ script, String^ name, MethodDeclarationOverloadDetails^ declaration, const Array<Object^> ^paramValues)
{
	const char *cScript = GetCString(script);
	const char *cName = GetCString(name);

	return this->m_luaImpl->CallMethod(cScript, cName, declaration, paramValues);
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
// Reset
//
void LanguageBindings::Reset()
{
	// NOTE nothing to do here right now...
}
