using System;
using System.Reflection;
using Microsoft.Practices.Unity;
using SparkiyEngine.Bindings.Common;
using SparkiyEngine.Bindings.Common.Attributes;
using SparkiyEngine.Bindings.Engine;
using SparkiyEngine.Bindings.Graphics;
using SparkiyEngine.Bindings.Language;

namespace SparkiyEngine.Core
{
	public sealed class SparkiyBootstrap
	{
		// Engine global state
		private bool isInitialized;
		private SupportedLanguages initializationLanguage;

		/// <summary>
		/// Initializes the lua language engine.
		/// </summary>
		public void InitializeLua(ILanguageBindings language, IGraphicsBindings graphics, IEngineBindings engine)
		{
			// Ensure engine is initialized only once
			this.CheckIfInitialized();

			// Set engine in initial state
			this.initializationLanguage = SupportedLanguages.Lua;
			this.isInitialized = true;

			// Initialize bindings container
			this.Bindings = new BindingsContainer();
			this.Bindings.Language = language;
			this.Bindings.Graphics = graphics;
			this.Bindings.Engine = engine;

			// Map methods Graphics > Language
			this.Bindings.Language.MapToGraphicsMethods(
				MethodDeclarationResolver.ResolveAll(
					this.Bindings.Graphics.GetType(), 
					this.initializationLanguage));

			// Map methods Graphics < Language
			this.Bindings.Language.OnMethodRequested += (sender, args) =>
			{
				var method = (MethodInfo) args.Overload.Method;
				method.Invoke(this.Bindings.Graphics, args.InputValues);
			};
		}

		/// <summary>
		/// Checks if engine was already initialized. 
		/// </summary>
		/// <exception cref="System.InvalidOperationException">Throws Invalid Operation exception when engine is being initilized for second time.</exception>
		/// <remarks>Only one initialization is possible in one instance of engine. Create another engine instance and initilize it with required langauge.</remarks>
		private void CheckIfInitialized()
		{
			if (this.isInitialized)
				throw new InvalidOperationException(String.Format(
					"Engine was already initialized with {0}. Create new instance and reinitilize it with required language.",
					this.initializationLanguage));
		}

		/// <summary>
		/// Gets the bindings.
		/// </summary>
		/// <value>
		/// The bindings.
		/// </value>
		public BindingsContainer Bindings { get; private set; }
	}
}