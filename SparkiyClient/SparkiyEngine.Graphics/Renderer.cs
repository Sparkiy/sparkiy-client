using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Xna.Framework;
using MonoGame.Framework;
using SparkiyEngine.Bindings.Component.Engine;
using SparkiyEngine.Bindings.Component.Graphics;

namespace SparkiyEngine.Graphics
{
	/// <summary>
	/// Bootstraps the graphics engine and provides access to GraphicsBindings interface
	/// </summary>
	public class Renderer : IDisposable, IGraphicsSettings
	{
		private SparkiyGame game;


		/// <summary>
		/// Initializes a new instance of the <see cref="Renderer"/> class.
		/// </summary>
		public Renderer(IEngineBindings engine, object panel)
		{
			this.Panel = panel;
			this.game = XamlGame<SparkiyGame>.Create(string.Empty, Window.Current.CoreWindow, panel as SwapChainPanel);
			this.game.AssignEngine(engine);
			this.game.Run(GameRunBehavior.Asynchronous);
		}


		#region IDisposable Support

		private bool disposedValue = false;	// To detect redundant calls

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
		private void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// NOTE: Dispose managed objects here

					if (this.game != null)
					{
						//this.game.Exit();
						this.game.Dispose();
					}
				}

				// NOTE: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// NOTE: set large fields to null.

				disposedValue = true;
			}
		}

		// NOTE: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources. 
		// ~Renderer() {
		//   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
		//   Dispose(false);
		// }

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// NOTE: uncomment the following line if the finalizer is overridden above.
			// GC.SuppressFinalize(this);
		}

		#endregion IDisposable Support

		#region Properties

		/// <summary>
		/// Gets or sets the panel.
		/// </summary>
		/// <value>
		/// The panel.
		/// </value>
		public object Panel { get; private set; }

		/// <summary>
		/// Gets the graphics bindings.
		/// </summary>
		/// <value>
		/// The graphics bindings.
		/// </value>
		public IGraphicsBindings GraphicsBindings
		{
			get { return this.game.GraphicsBindings; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is mouse visible.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is mouse visible; otherwise, <c>false</c>.
		/// </value>
		public bool IsMouseVisible
		{
			get { return this.game.IsMouseVisible; }
			set { this.game.IsMouseVisible = value; }
		}

		#endregion Properties
	}
}