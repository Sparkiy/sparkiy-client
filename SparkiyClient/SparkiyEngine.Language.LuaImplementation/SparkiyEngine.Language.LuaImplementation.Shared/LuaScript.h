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

		Platform::Object^ CallMethod(const char *name, SparkiyEngine::Bindings::Common::Component::MethodDeclarationOverloadDetails^ declaration, const Platform::Array<Platform::Object^>^ paramValues);
		void RegisterMethod(SparkiyEngine::Bindings::Common::Component::MethodDeclarationDetails ^declaration);
		void Load();
		void Start();

	protected:
		void RegisterFunction(const char *name, FunctionPointer pt);
		void RegisterLibrary(const char *name, const luaL_Reg *functions);
		const char* GetErrorMessage();
		void HandleException();

		// Script loading

		// Helper methods
		static int UniversalFunction(lua_State* luaState);
		static const char* GetFunctionName(lua_State *luaState);
		static LuaScript* GetCallerScript(lua_State *luaState);
		static int PanicHandler(lua_State *luaState);
		static bool FunctionExist(lua_State *luaState, const char *name);
		static int CallFunction(lua_State *luaState, const char *name, int numParameters, int numResults);

	private:
		// Script info
		SparkiyEngine_Language_LuaImplementation::LuaImplementation				^m_luaImpl;
		const char																*m_id;
		const char																*m_content;

		// Script state
		bool																	 m_isRunning;
		bool																	 m_isValid;

		// The Lua interpreter.
		lua_State																*m_luaState;
	};

}