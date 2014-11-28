#pragma once

#include "pch.h"
#include "LuaImplementation.h"

using namespace Platform;
using namespace Windows::Foundation::Collections;

namespace SparkiyEngine_Language_LuaImplementation
{
	public ref class LanguageBindings sealed : SparkiyEngine::Bindings::Component::Language::ILanguageBindings
	{
	public:
		virtual void MapToGraphicsMethods(IMapView<String ^, SparkiyEngine::Bindings::Component::Common::MethodDeclarationDetails ^> ^declarations);
		virtual void LoadScript(String ^id, String ^content);
		virtual void StartScript(String ^id);

		// Methods
		virtual Object^ CallMethod(String^ script, String^ name, SparkiyEngine::Bindings::Component::Common::MethodDeclarationOverloadDetails^ declaration, const Array<Object^>^ paramValues);
		virtual Object^ CallMethod(String^ name, SparkiyEngine::Bindings::Component::Common::MethodDeclarationOverloadDetails^ declaration, const Array<Object^>^ paramValues);

		// Variables and Constants
		virtual void SetConstant(String^ name, Object^ value, SparkiyEngine::Bindings::Component::Common::DataTypes dataType);
		virtual void SetVariable(String^ name, Object^ value, SparkiyEngine::Bindings::Component::Common::DataTypes dataType);

		// Settings
		virtual void Reset();

		virtual property SparkiyEngine::Bindings::Component::Common::SupportedLanguages Language
		{
			SparkiyEngine::Bindings::Component::Common::SupportedLanguages get();
		};

	internal:
		LanguageBindings(LuaImplementation ^impl);

	private:
		LuaImplementation																					^m_luaImpl;

		// State
		bool																								 m_didLoadScript;
	};
}
