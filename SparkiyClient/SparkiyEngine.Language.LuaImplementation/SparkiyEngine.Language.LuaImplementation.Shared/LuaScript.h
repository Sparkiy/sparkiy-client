#pragma once

#include "pch.h"
#include "LuaImplementation.h"

namespace SparkiyEngine_Language_LuaImplementation
{

	// 
	// Script global values
	//
	static const char* ScriptPointerVariableName = "___ScriptPointer";


	typedef int(*FunctionPointer) (lua_State *);

	class LuaScript
	{
	public:
		LuaScript(SparkiyEngine_Language_LuaImplementation::LuaImplementation^ luaImpl, const char *id, const char *content);
		~LuaScript();

		void RegisterMethod(SparkiyEngine::Bindings::Common::Component::MethodDeclarationDetails ^declaration);
		void Load();
		void Start();

	protected:
		void RegisterFunction(const char *name, FunctionPointer pt);
		void RegisterLibrary(const char *name, const luaL_Reg *functions);

		// Script loading
		bool HandleResult(int status);

		// Helper methods
		static int UniversalFunction(lua_State* luaState);
		static const char* GetFunctionName(lua_State *luaState);
		static LuaScript* GetCallerScript(lua_State *luaState);
		static int PanicHandler(lua_State *luaState);

	private:
		// Script info
		SparkiyEngine_Language_LuaImplementation::LuaImplementation				^m_luaImpl;
		const char																*m_id;
		const char																*m_content;

		// Script state
		bool																	 m_isRunning;
		bool																	 m_isValid;

		// The Lua interpreter.
		lua_State *m_luaState;
	};

}