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

		void Initialize();

		Platform::Object^ CallMethod(const char *name, SparkiyEngine::Bindings::Component::Common::MethodDeclarationOverloadDetails^ declaration, const Platform::Array<Platform::Object^>^ paramValues);
		void RegisterMethod(SparkiyEngine::Bindings::Component::Common::MethodDeclarationDetails ^declaration);
		bool Load();
		void Start();

		// Variables and Constants
		void SetConstant(const char *name, Platform::Object^ value, SparkiyEngine::Bindings::Component::Common::DataTypes dataType);
		void SetVariable(const char *name, Platform::Object^ value, SparkiyEngine::Bindings::Component::Common::DataTypes dataType);

	protected:
		void RegisterFunction(const char *name, FunctionPointer pt);
		const char* GetErrorMessage();
		void HandleError();
		void HandleException();

		// Helper methods
		static Platform::Object^ PopLuaStack(lua_State* luaState, SparkiyEngine::Bindings::Component::Common::DataTypes dataType, int index);
		static void PushLuaStack(lua_State* luaState, Platform::Object^ value, SparkiyEngine::Bindings::Component::Common::DataTypes dataType);
		static int UniversalFunction(lua_State* luaState);
		static const char* GetFunctionName(lua_State *luaState);
		static LuaScript* GetCallerScript(lua_State *luaState);
		static int PanicHandler(lua_State *luaState);
		static bool FunctionExist(lua_State *luaState, const char *name);
		static bool CallFunction(lua_State *luaState, const char *name, int numParameters, int numResults);
		static void HandleProtectedCallError(lua_State *luaState, const char *name);

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