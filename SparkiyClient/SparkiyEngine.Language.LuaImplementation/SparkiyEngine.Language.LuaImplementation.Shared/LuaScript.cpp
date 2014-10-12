#include "pch.h"
#include "LuaScript.h"


LuaScript::LuaScript(std::string id, const char *content) :
m_id(id),
m_content(content)
{
}

LuaScript::~LuaScript()
{
}

void LuaScript::Start()
{
	OutputDebugStringW(GetWString("Starting script with id(" + this->m_id + ")").c_str());

	OutputDebugStringW(L"Warning: Not implemented function");
}
