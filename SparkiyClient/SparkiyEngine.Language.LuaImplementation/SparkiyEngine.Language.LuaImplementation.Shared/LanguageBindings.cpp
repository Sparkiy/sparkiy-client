#include "pch.h"
#include "LanguageBindings.h"

using namespace Platform;
using namespace Windows::Foundation::Collections;
using namespace SparkiyEngine_Language_LuaImplementation;

LanguageBindings::LanguageBindings(LuaImplementation ^impl) :
m_luaImpl(impl),
m_didLoadScript(false)
{
}

void LanguageBindings::MapToGraphicsMethods(IMapView<String ^, SparkiyEngine::Bindings::Common::Component::MethodDeclarationDetails ^> ^declarations)
{
	// Check if we didnt already loaded some scripts, in case we did, throw exception
	if (this->m_didLoadScript) 
	{
		throw ref new Exception(-1, "Can't map methods, already loaded at least one script. Map methods first before loading scripts.");
	}

	this->m_declarations = declarations;
}

void LanguageBindings::LoadScript(String ^id, String ^content) 
{
	// Convert to C and C++ compatible strings
	std::string sId = GetString(id);
	const char *cContent = GetCString(content);

	// Create script
	auto script = new LuaScript(sId, cContent);

	// Map methods
	std::for_each(begin(this->m_declarations), end(this->m_declarations), [=](IKeyValuePair<Platform::String^, SparkiyEngine::Bindings::Common::Component::MethodDeclarationDetails ^>^ decl) 
	{
		auto details = decl->Value;

		script->RegisterMethod(details);
	});

	// Add to the scripts list
	this->m_luaImpl->AddScript(sId, script);

	// mark that binding loaded at least one script
	this->m_didLoadScript = true;
}

void LanguageBindings::StartScript(Platform::String ^id)
{
	// Convert to C and C++ compatible strings
	std::string sId = GetString(id);

	// Retrieve script
	auto script = this->m_luaImpl->GetScript(sId);

	// Start script
	script->Start();
}

