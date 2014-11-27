#include "pch.h"
#include "LuaScript.h"

using namespace Platform;
using namespace SparkiyEngine_Language_LuaImplementation;
using namespace SparkiyEngine::Bindings::Component::Common;


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
// SetConstant
//
void SetConstant(const char *name, Object^ value, DataTypes dataType)
{

}

// 
// SetVariable
//
void SetVariable(const char *name, Object^ value, DataTypes dataType)
{

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
// GetErrorMessage
//
const char* LuaScript::GetErrorMessage() 
{
	const char* estr = lua_tostring(this->m_luaState, -1);
	lua_pop(this->m_luaState, 1);
	return estr;
}

//
// HandleException
//
void LuaScript::HandleException() 
{
	auto errorMessage = this->GetErrorMessage();
	this->m_luaImpl->CreateMessage("Error: " + GetString(errorMessage) + "\n");
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

//
// CallMethod
//
Object^ LuaScript::CallMethod(const char *name, MethodDeclarationOverloadDetails^ declaration, const Array<Object^>^ paramValues)
{
	auto invalidFunctionNameErrorMessage = "Invalid function name \"%s\". Requeste function name does not exist in users code.";
	auto invalidNumberOfParametersErrorMessage = "Invalid number of parameters were passed to the function call. Declaration does not match passed values count.";
	auto invalidArgTypeErrorMessage = "Invalid argument type passed to function \"%s\".";
	auto invalidArgTypeReturnedErrorMessage = "Invalid argument type returned from function \"%s\".";

	// Check if function with given name exists
	if (!LuaScript::FunctionExist(this->m_luaState, name))
		return NULL;

	// Check if correct number of parameters were passed
	if (paramValues->Length != declaration->Input->Length)
		luaL_error(this->m_luaState, invalidNumberOfParametersErrorMessage);
	
	// Check if there are any parameters to push to the stack
	if (declaration->Input->Length != 0)
	{
		// TODO Implement
		// TODO Cast parameter and push to stack
		throw;
	}
	
	// Call the function
	LuaScript::CallFunction(this->m_luaState, name, declaration->Input->Length, declaration->Return->Length);

	// Check if there are any returned values
	if (declaration->Return->Length != 0) 
	{
		// TODO Implement
		// TODO Cast returned parameter
		throw;
	}
	else 
	{
		return NULL;
	}
}

// 
// SetConstant
//
void LuaScript::SetConstant(const char *name, Platform::Object^ value, SparkiyEngine::Bindings::Component::Common::DataTypes dataType)
{
	LuaScript::PushLuaStack(this->m_luaState, value, dataType);
	lua_setglobal(this->m_luaState, name);
}

//
// SetVariable
//
void LuaScript::SetVariable(const char *name, Platform::Object^ value, SparkiyEngine::Bindings::Component::Common::DataTypes dataType)
{
	LuaScript::PushLuaStack(this->m_luaState, value, dataType);
	lua_setglobal(this->m_luaState, name);
}

// static 
// UniversalFunction
//
int LuaScript::UniversalFunction(lua_State* luaState)
{
	auto invalidFunctionNameErrorMessage = "Invalid function name. Requested function name was registered, but not mapped properly.";
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
	auto inputValues = ref new Array<Object^>(numberOfArguments);
	
	// Retrieve all arguments
	for (int index = 0, argIndex = -numberOfArguments; index < numberOfArguments; index++, argIndex++)
	{
		auto requiredType = matchedOverload->Input[index];
		inputValues[index] = LuaScript::PopLuaStack(luaState, requiredType, argIndex);
	}

	callerScript->m_luaImpl->MethodRequest(declaration, matchedOverload, inputValues);

	return 0;
}

// static
// PopLuaStack
//
Object^ LuaScript::PopLuaStack(lua_State* luaState, DataTypes dataType, int index)
{
	auto invalidArgTypeErrorMessage = "Invalid argument type passed. Can't parse to \"%s\"";

	switch (dataType)
	{
	case DataTypes::Number:
		if (!lua_isnumber(luaState, index))
			luaL_error(luaState, invalidArgTypeErrorMessage, dataType);
		return lua_tonumber(luaState, index);
		break;
	case DataTypes::String:
		if (!lua_isstring(luaState, index))
			luaL_error(luaState, invalidArgTypeErrorMessage, dataType);
		return GetPString(lua_tostring(luaState, index));
		break;
	default:
		luaL_error(luaState, invalidArgTypeErrorMessage, dataType);
		break;
	}

	return nullptr;
}

// static
// PushLuaStack
//
void LuaScript::PushLuaStack(lua_State* luaState, Object^ value, DataTypes dataType)
{
	auto invalidArgTypeErrorMessage = "Invalid argument type passed. Can't parse to \"%s\"";

	switch (dataType)
	{
	case DataTypes::Number:
		{
			auto numValue = static_cast<lua_Number>(value);
			lua_pushnumber(luaState, numValue);
			break; 
		}
	case DataTypes::String:
		{
			lua_pushstring(luaState, GetCString(static_cast<String^>(value)));
			break;
		}
	default:
		luaL_error(luaState, invalidArgTypeErrorMessage, dataType);
		break;
	}
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

// static
// FunctionExist
//
bool LuaScript::FunctionExist(lua_State *luaState, const char *name) {
	lua_getglobal(luaState, name);
	return lua_isfunction(luaState, -1);
}

// static
// CallFunction
//
int LuaScript::CallFunction(lua_State *luaState, const char *name, int numParameters, int numResults) {
	const char *functionExecutionError = "An error occured while executing function \"%s\"";

	lua_getglobal(luaState, name);
	int errorCode = lua_pcall(luaState, numParameters, numResults, 0);
	if (errorCode)
		luaL_error(luaState, functionExecutionError, name);

	return errorCode;
}

