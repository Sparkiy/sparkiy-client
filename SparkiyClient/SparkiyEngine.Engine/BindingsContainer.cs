using Microsoft.Practices.Unity;
using SparkiyEngine.Bindings.Engine;
using SparkiyEngine.Bindings.Graphics;
using SparkiyEngine.Bindings.Language;

namespace SparkiyEngine.Core
{
	public sealed class BindingsContainer
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BindingsContainer"/> class.
		/// </summary>
		public BindingsContainer()
		{
			this.Container = new UnityContainer();
		}

		/// <summary>
		/// Gets the container.
		/// </summary>
		/// <value>
		/// The container.
		/// </value>
		internal UnityContainer Container { get; private set; }

		/// <summary>
		/// Gets the language bindings.
		/// </summary>
		/// <value>
		/// The language bindings.
		/// </value>
		public ILanguageBindings Language
		{
			get { return this.Container.Resolve<ILanguageBindings>(); }
		}

		/// <summary>
		/// Gets the graphics bindings.
		/// </summary>
		/// <value>
		/// The graphics bindings.
		/// </value>
		public IGraphicsBindings Graphics
		{
			get { return this.Container.Resolve<IGraphicsBindings>(); }
		}

		/// <summary>
		/// Gets the engine bindings.
		/// </summary>
		/// <value>
		/// The engine bindings.
		/// </value>
		public IEngineBindings Engine
		{
			get { return this.Container.Resolve<IEngineBindings>(); }
		}
	}
}