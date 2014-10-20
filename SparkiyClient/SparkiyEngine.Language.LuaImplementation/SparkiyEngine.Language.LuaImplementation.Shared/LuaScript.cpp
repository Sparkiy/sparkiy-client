#include "pch.h"
#include "LuaScript.h"

using namespace SparkiyEngine_Language_LuaImplementation;
using namespace SparkiyEngine::Bindings::Common::Component;


// Constructor
LuaScript::LuaScript(LuaImplementation^ luaImpl, const char *id, const char *content) :
m_luaImpl(luaImpl),
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

//
// RegisterMethod
//
void LuaScript::RegisterMethod(MethodDeclarationDetails ^declaration)
{
	OutputDebugStringW(GetWString("Registering method \"" + GetString(declaration->Name) + "\"\n").c_str());

	// Register function with lua VM
	auto cName = GetCString(declaration->Name);
	this->RegisterFunction(cName, UniversalFunction);
}

//
// Start
//
void LuaScript::Start()
{
	OutputDebugStringW(GetWString("Starting script with id(" + GetString(this->m_id) + ")\n").c_str());

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

const char* LuaScript::GetErrorMessage() 
{
	const char* estr = lua_tostring(this->m_luaState, -1);
	lua_pop(this->m_luaState, 1);
	return estr;
}

void LuaScript::HandleException() 
{
	auto errorMessage = this->GetErrorMessage();
	OutputDebugStringW(GetWString("Error: " + GetString(errorMessage) + "\n").c_str());
	//Print("Error: " + err);
}

//
// Load
//
void LuaScript::Load()
{
	try 
	{
		// Run the script
		this->m_isValid = luaL_dostring(m_luaState, this->m_content) == LUA_OK;

		if (!this->m_isValid)
			this->HandleException();
	}
	catch (...) 
	{
		this->HandleException();
	}

	// Call loaded function
	//if (FunctionExist(this->m_luaState, LoadedFunctionName))
	//	CallFunction(this->m_luaState, LoadedFunctionName, 0, 0);
}

// static 
// UniversalFunction
//
int LuaScript::UniversalFunction(lua_State* luaState)
{
	auto invalidFunctionNameErrorMessage = "Invalid function name. Requested function name was registered, but not mapped properly.";
	auto invalidArgTypeErrorMessage = "Invalid argument type passed to function \"%s\".";
	auto invalidOverloadErrorMessage = "Invalid argument arangement or unknown function \"%s\".";

	auto callerScript = GetCallerScript(luaState);
	auto functionName = GetFunctionName(luaState);
	auto declaration = callerScript->m_luaImpl->m_declarations[functionName];

	// Check if declaration found
	if (declaration == nullptr)
		luaL_error(luaState, invalidFunctionNameErrorMessage);

	// Match number of arguments with overload declaration
	MethodDeclarationOverloadDetails^ matchedOverload;
	int numberOfArguments = lua_gettop(luaState);
	for (auto index = begin(declaration->Overloads); index != end(declaration->Overloads); index++)
	{
		// Check if current overload accepts passed number of arguments
		if ((*index)->Input->Length == numberOfArguments)
		{
			matchedOverload = *index;
			break;
		}
	}

	// Check if declaration was matched
	if (matchedOverload == nullptr)
		luaL_error(luaState, invalidOverloadErrorMessage, functionName);

	// Create input values array
	auto inputValues = ref new Platform::Array<Platform::Object^>(numberOfArguments);
	
	// Retrieve all arguments
	for (int index = 0, argIndex = -numberOfArguments; index < numberOfArguments; index++, argIndex++)
	{
		auto requiredType = matchedOverload->Input[index];
		switch (requiredType)
		{
		case DataTypes::Number:
			if (!lua_isnumber(luaState, argIndex))
				luaL_error(luaState, invalidArgTypeErrorMessage, functionName);
			inputValues[index] = lua_tonumber(luaState, argIndex);
			break;
		case DataTypes::String:
			if (!lua_isstring(luaState, argIndex))
				luaL_error(luaState, invalidArgTypeErrorMessage, functionName);
			inputValues[index] = GetPString(lua_tostring(luaState, argIndex));
			break;
		default:
			luaL_error(luaState, invalidArgTypeErrorMessage, functionName);
			break;
		}
	}

	callerScript->m_luaImpl->RaiseMethodRequestedEvent(declaration, matchedOverload, inputValues);

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
	throw;

	// This should not be reached
	return 0;
}
