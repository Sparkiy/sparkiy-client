#pragma once

#include "pch.h"

// String comparer
// Note: source http://stackoverflow.com/questions/4157687/using-char-as-a-key-in-stdmap
struct StrCompare : public std::binary_function<const char*, const char*, bool> {
public:
	bool operator() (const char* str1, const char* str2) const
	{
		return std::strcmp(str1, str2) < 0;
	}
};

// String helpers
static const char* GetCString(std::string str);
static const char* GetCString(std::wstring str);
static const char* GetCString(Platform::String^ str);
static std::string GetString(const char *str);
static std::string GetString(std::wstring str);
static std::string GetString(Platform::String^ str);
static std::wstring GetWString(const char *str);
static std::wstring GetWString(std::string str);
static std::wstring GetWString(Platform::String^ str);
static Platform::String^ GetPString(const char *str);
static Platform::String^ GetPString(std::string str);
static Platform::String^ GetPString(std::wstring str);

//
// String helpers
//
static const char* GetCString(std::string str) {
	return GetCString(GetWString(str));
}
static const char* GetCString(std::wstring str) {
	char *szTo = new char[str.length() + 1];
	szTo[str.length()] = '\0';
	WideCharToMultiByte(CP_ACP, 0, str.c_str(), -1, szTo, (int) str.length(), NULL, NULL);
	return szTo;
}
static const char* GetCString(Platform::String^ str) {
	return GetCString(GetWString(str));
}
static std::string GetString(const char *str){
	std::string s(str);
	return s;
}
static std::string GetString(std::wstring str) {
	std::string s(str.begin(), str.end());
	return s;
}
static std::string GetString(Platform::String^ str) {
	return GetString(GetWString(str));
}
static std::wstring GetWString(const char *str) {
	return GetWString(GetString(str));
}
static std::wstring GetWString(std::string str) {
	std::wstring ws(str.begin(), str.end());
	return ws;
}
static std::wstring GetWString(Platform::String^ str) {
	return str->Data();
}
static Platform::String^ GetPString(const char *str) {
	return GetPString(GetWString(str));
}
static Platform::String^ GetPString(std::string str) {
	return GetPString(GetWString(str));
}
static Platform::String^ GetPString(std::wstring str) {
	return ref new Platform::String(&str[0]);
}

