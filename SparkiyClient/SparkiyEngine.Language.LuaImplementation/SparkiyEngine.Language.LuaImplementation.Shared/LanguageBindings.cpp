#include "pch.h"
#include "LanguageBindings.h"

using namespace Platform;
using namespace SparkiyEngine_Language_LuaImplementation;

LanguageBindings::LanguageBindings(LuaImplementation ^impl) :
luaImpl(impl)
{
}

void LanguageBindings::LoadScript(String ^id, String ^content) 
{
	// Convert to C compatible string
	std::string sId = GetString(id);
	const char *cContent = GetCString(content);

	// Create script
	auto script = new LuaScript(sId, cContent);

	// Add to the scripts list
	this->luaImpl->AddScript(sId, script);
}

