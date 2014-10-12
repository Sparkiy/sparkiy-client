#include "pch.h"
#include "LuaScript.h"

// Constructor
LuaScript::LuaScript(std::string id, const char *content) :
m_id(id),
m_content(content),
m_isRunning(false),
m_isValid(false)
{
	// Initialize Lua
	m_luaState = luaL_newstate();
	luaL_openlibs(m_luaState);

	// Register custom functions
	//this->RegisterCustomFunctions();
	//this->RegisterCustomLibraries();

	// Panic handler
	lua_atpanic(m_luaState, PanicHandler);

	// Save pointer to this object
	lua_pushlightuserdata(m_luaState, this);
	lua_setglobal(m_luaState, ScriptPointerVariableName);

	// Initialize variable monitors
	//this->m_monitors = ref new Platform::Collections::Vector<IMonitorItem^>();
}

// Destructor
LuaScript::~LuaScript()
{
	// Cleanup Lua vm
	lua_close(m_luaState);
}

void LuaScript::RegisterMethod(SparkiyEngine::Bindings::Common::Component::MethodDeclarationDetails ^declaration)
{
	OutputDebugStringW(GetWString("Registering method \"" + declaration->Name + "\"\n").c_str());
	OutputDebugStringW(L"Warning: Not implemented function\n");
}

//
// Start
//
void LuaScript::Start()
{
	OutputDebugStringW(GetWString("Starting script with id(" + this->m_id + ")\n").c_str());
	OutputDebugStringW(L"Warning: Not implemented function\n");
}

// 
// RegisterFunction
//
void LuaScript::RegisterFunction(const char *name, FunctionPointer pt)
{
	lua_register(m_luaState, name, pt);
}

// 
// RegisterLibrary
//
void LuaScript::RegisterLibrary(const char *name, const luaL_Reg *functions) {
	luaL_newlib(this->m_luaState, functions);
	lua_setglobal(this->m_luaState, name);
}

//
// GetCallerScript
//
LuaScript* LuaScript::GetCallerScript(lua_State *luaState) {
	lua_getglobal(luaState, ScriptPointerVariableName); 
	const LuaScript *pt = static_cast<const LuaScript *>(lua_topointer(luaState, -1));
	lua_pop(luaState, 1); 
	return const_cast<LuaScript *>(pt);
}

//
// PanicHandler
//
int LuaScript::PanicHandler(lua_State *luaState)
{
	LuaScript* callerScript = LuaScript::GetCallerScript(luaState);

	// Construct error message
	//Platform::String^ message = "Panic in '" + GetPString(callerScript->m_name) + "': " + GetPString(lua_tostring(luaState, -1));

	// Write error message to output
	//if (callerScript->m_engineValues->m_userService != nullptr)
	//	callerScript->m_engineValues->m_userService->WriteMessage(message, MessageTypes::Error);
	OutputDebugStringW(L"Warning: Not implemented function");

	callerScript->m_isValid = false;
	return 0;
}
