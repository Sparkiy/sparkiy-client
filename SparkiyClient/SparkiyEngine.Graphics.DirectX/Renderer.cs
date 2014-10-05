using SharpDX;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;
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

namespace SparkiyEngine.Graphics.DirectX
{
	public class SparkiyGame : Game
	{
		private GraphicsDeviceManager graphicsDeviceManager;
		private SpriteBatch spriteBatch;
		private Texture2D logoTexture;

		/// <summary>
		/// Initializes a new instance of the <see cref="SparkiyGame" /> class.
		/// </summary>
		public SparkiyGame()
		{
			// Creates a graphics manager. This is mandatory.
			graphicsDeviceManager = new GraphicsDeviceManager(this);

			// Setup the relative directory to the executable directory
			// for loading contents with the ContentManager
			Content.RootDirectory = "Assets";
		}

		protected override void Initialize()
		{
			Window.Title = "HelloWorld!";
			base.Initialize();
		}

		protected override void LoadContent()
		{
			// Loads the balls texture (32 textures (32x32) stored vertically => 32 x 1024 ).
			logoTexture = Content.Load<Texture2D>("sparkiy");

			// SpriteFont supports the following font file format:
			// - DirectX Toolkit MakeSpriteFont or SharpDX Toolkit tkfont
			// - BMFont from Angelcode http://www.angelcode.com/products/bmfont/
			//arial16BMFont = Content.Load<SpriteFont>("Arial16");

			// Instantiate a SpriteBatch
			spriteBatch = new SpriteBatch(GraphicsDevice);

			base.LoadContent();
		}

		protected override void UnloadContent()
		{
			spriteBatch.Dispose();

			base.UnloadContent();
		}

		protected override void Draw(GameTime gameTime)
		{
			// Clears the screen with the Color.CornflowerBlue
			GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin(SpriteSortMode.Deferred, GraphicsDevice.BlendStates.NonPremultiplied);
			spriteBatch.Draw(logoTexture, new Vector2(100, 100), Color.White);
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}

	public class Renderer : IDisposable
	{
		private Game game;


		public Renderer(SwapChainPanel panel)
		{
			this.game = new SparkiyGame();
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
	}
}
