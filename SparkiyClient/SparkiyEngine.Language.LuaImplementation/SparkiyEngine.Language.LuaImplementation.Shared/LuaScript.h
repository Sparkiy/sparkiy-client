#pragma once

#include "pch.h"

// 
// Script global values
//
static const char* ScriptPointerVariableName = "___ScriptPointer";


typedef int(*FunctionPointer) (lua_State *);

class LuaScript
{
public:
	LuaScript(std::string id, const char *content);
	~LuaScript();

	void RegisterMethod(SparkiyEngine::Bindings::Common::Component::MethodDeclarationDetails ^declaration);
	void Start();

protected:
	void RegisterFunction(const char *name, FunctionPointer pt);
	void RegisterLibrary(const char *name, const luaL_Reg *functions);

	// Helper methods
	static LuaScript* GetCallerScript(lua_State *luaState);
	static int PanicHandler(lua_State *luaState);

private:
	// Script info
	std::string						 m_id;
	const char						*m_content;

	// Script state
	bool							 m_isRunning;
	bool							 m_isValid;

	// The Lua interpreter.
	lua_State *m_luaState;
};

