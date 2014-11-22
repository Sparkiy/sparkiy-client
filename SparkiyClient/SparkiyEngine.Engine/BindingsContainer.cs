using Microsoft.Practices.Unity;
using SparkiyEngine.Bindings.Component.Engine;
using SparkiyEngine.Bindings.Component.Graphics;
using SparkiyEngine.Bindings.Component.Language;

namespace SparkiyEngine.Core
{
	public sealed class BindingsContainer
	{
		/// <summary>
		/// Gets the language bindings.
		/// </summary>
		/// <value>
		/// The language bindings.
		/// </value>
		public ILanguageBindings Language { get; set; }

		/// <summary>
		/// Gets the graphics bindings.
		/// </summary>
		/// <value>
		/// The graphics bindings.
		/// </value>
		public IGraphicsBindings Graphics { get; set; }

		/// <summary>
		/// Gets the engine bindings.
		/// </summary>
		/// <value>
		/// The engine bindings.
		/// </value>
		public IEngineBindings Engine { get; set; }
	}
}