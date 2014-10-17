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
	public class Renderer : IDisposable
	{
		private SparkiyGame game;


		public Renderer()
		{
			this.game = new SparkiyGame();
		}

		public Renderer(SwapChainPanel panel) : this()
		{
			this.AssignPanel(panel);
		}

		public void AssignPanel(Panel panel)
		{
			this.game.Run(panel);
		}


		#region IDisposable Support
		private bool disposedValue = false;	// To detect redundant calls

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

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// NOTE: uncomment the following line if the finalizer is overridden above.
			// GC.SuppressFinalize(this);
		}
		#endregion

		public IGraphicsBindings GraphicsBindings
		{
			get { return this.game.GraphicsBindings; }
		}
	}
}
