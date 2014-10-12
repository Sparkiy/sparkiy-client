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

	auto cName = GetCString(declaration->Name);

	this->RegisterFunction(cName, UniversalFunction);
}

//
// Start
//
void LuaScript::Start()
{
	OutputDebugStringW(GetWString("Starting script with id(" + this->m_id + ")\n").c_str());
	OutputDebugStringW(L"Warning: Not implemented function\n");

	this->m_isRunning = true;

	// Check if receiving function exists
	//if (LuaScript::FunctionExist(this->m_luaState, StartFunctionName))
	//{
	//	// Push all arguments
	//	LuaScript::AddArgumentStringArray(this->m_luaState, parameters);

	//	// Call the function
	//	int callError = LuaScript::CallFunction(this->m_luaState, StartFunctionName, parameters.size(), 0);
	//}
}

// 
// RegisterFunction
//
void LuaScript::RegisterFunction(const char *name, FunctionPointer pt)
{
	lua_pushstring(this->m_luaState, name);
	lua_pushcclosure(this->m_luaState, pt, 1);
	lua_setglobal(this->m_luaState, name);
}

// 
// RegisterLibrary
//
void LuaScript::RegisterLibrary(const char *name, const luaL_Reg *functions) {
	luaL_newlib(this->m_luaState, functions);
	lua_setglobal(this->m_luaState, name);
}

//
// Load
//
void LuaScript::Load()
{
	// Run the script
	this->m_isValid = HandleResult(luaL_dostring(m_luaState, this->m_content));

	// Call loaded function
	//if (FunctionExist(this->m_luaState, LoadedFunctionName))
	//	CallFunction(this->m_luaState, LoadedFunctionName, 0, 0);
}

// 
// HandleResult
//
bool LuaScript::HandleResult(int status)
{
	// Check if error occurred
	if (status != LUA_OK)
	{
		// Retrieve error message from native Lua interface
		int type = lua_type(m_luaState, -1);
		const char *msg;
		if (type == LUA_TSTRING)
			msg = lua_tostring(m_luaState, -1);
		else msg = "(error object is not a string)";

		// Build final message
		Platform::String^ message = GetPString(this->m_id) + ": " + GetPString(msg);

		// Display the error message
		//if (m_engineValues->m_userService != nullptr)
		//	m_engineValues->m_userService->WriteMessage(message, MessageTypes::Debug);

		// Failed
		return false;
	}

	// Confirm successful
	return true;
}

// static 
// UniversalFunction
//
int LuaScript::UniversalFunction(lua_State* luaState)
{
	auto functionName = GetFunctionName(luaState);

	OutputDebugStringW(GetWString("Called function \"" + GetString(functionName) + "\"\n").c_str());

	return 0;
}

// static
// GetFunctionName
//
const char* LuaScript::GetFunctionName(lua_State* luaState)
{
	return lua_tostring(luaState, lua_upvalueindex(1));
}

// static
// GetCallerScript
//
LuaScript* LuaScript::GetCallerScript(lua_State *luaState) {
	lua_getglobal(luaState, ScriptPointerVariableName); 
	const LuaScript *pt = static_cast<const LuaScript *>(lua_topointer(luaState, -1));
	lua_pop(luaState, 1); 
	return const_cast<LuaScript *>(pt);
}

// static
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
