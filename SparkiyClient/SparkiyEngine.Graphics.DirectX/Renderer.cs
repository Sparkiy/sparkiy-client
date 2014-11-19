using SharpDX.Toolkit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Graphics.Imaging;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using SparkiyEngine.Bindings.Graphics;

namespace SparkiyEngine.Graphics.DirectX
{
	/// <summary>
	/// Renderer handles Graphics Settings and manages Graphics Bindings implementation
	/// </summary>
	public class Renderer : IDisposable, IGraphicsSettings
	{
		private SparkiyGame game;


		/// <summary>
		/// Initializes a new instance of the <see cref="Renderer"/> class.
		/// </summary>
		public Renderer()
		{
			this.game = new SparkiyGame();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Renderer"/> class.
		/// </summary>
		/// <param name="panel">The panel to assign as drawing surface.</param>
		public Renderer(SwapChainPanel panel) : this()
		{
			this.AssignPanel(panel);
		}

		/// <summary>
		/// Assigns the panel as drawing surface.
		/// </summary>
		/// <param name="panel">The panel to assign.</param>
		public void AssignPanel(object panel)
		{
			this.game.Run(panel);
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
						this.game.Exit();
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
