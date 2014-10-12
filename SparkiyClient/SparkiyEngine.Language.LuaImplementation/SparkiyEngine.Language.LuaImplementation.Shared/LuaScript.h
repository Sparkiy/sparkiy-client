#pragma once

#include "pch.h"


class LuaScript
{
public:
	LuaScript(std::string id, const char *content);
	~LuaScript();

private:
	std::string						 m_id;
	const char						*m_content;
};

