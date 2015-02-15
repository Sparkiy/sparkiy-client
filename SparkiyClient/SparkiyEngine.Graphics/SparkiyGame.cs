using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using MetroLog;
using Microsoft.Xna.Framework;
using MonoGame.Framework;
using SparkiyEngine.Bindings.Component.Common;
using SparkiyEngine.Bindings.Component.Engine;
using SparkiyEngine.Bindings.Component.Graphics;

namespace SparkiyEngine.Graphics
{
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

	public class GraphicsBindings : GameComponent, IGraphicsBindings
	{
		public GraphicsBindings(Game game) : base(game)
		{
		}

		public void SetStrokeColor(double red, double green, double blue)
		{
			//throw new NotImplementedException();
		}

		public NumberGroup3 GetStrokeColor()
		{
			//throw new NotImplementedException();
			return new NumberGroup3();
		}

		public void SetStrokeThickness(double thickness)
		{
			//throw new NotImplementedException();
		}

		public double GetStrokeThickness()
		{
			//throw new NotImplementedException();
			return 0;
		}

		public void StrokeDisable()
		{
			//throw new NotImplementedException();
		}

		public void SetFill(double red, double green, double blue)
		{
			//throw new NotImplementedException();
		}

		public NumberGroup3 GetFill()
		{
			//throw new NotImplementedException();
			return new NumberGroup3();
		}

		public void FillDisable()
		{
			//throw new NotImplementedException();
		}

		public void PushStyle()
		{
			//throw new NotImplementedException();
		}

		public void PopStyle()
		{
			//throw new NotImplementedException();
		}

		public void SaveStyle(string key)
		{
			//throw new NotImplementedException();
		}

		public void LoadStyle(string key)
		{
			//throw new NotImplementedException();
		}

		public void ResetStyle()
		{
			//throw new NotImplementedException();
		}

		public void DrawRectangle(double x, double y, double width, double height)
		{
			//throw new NotImplementedException();
		}

		public void DrawSquare(double x, double y, double size)
		{
			//throw new NotImplementedException();
		}

		public void DrawEllipse(double x, double y, double majorRadius, double minorRadius)
		{
			//throw new NotImplementedException();
		}

		public void DrawCircle(double x, double y, double radius)
		{
			//throw new NotImplementedException();
		}

		public void DrawLine(double x1, double y1, double x2, double y2)
		{
			//throw new NotImplementedException();
		}

		public void DrawTexture(string assetName, double x, double y)
		{
			//throw new NotImplementedException();
		}

		public void DrawTexture(string assetName, double x, double y, double width, double height)
		{
			//throw new NotImplementedException();
		}

		public void SetBackground(double red, double green, double blue)
		{
			//throw new NotImplementedException();
		}

		public void SetTranslation(double x, double y)
		{
			//throw new NotImplementedException();
		}

		public void SetRotation(double angle)
		{
			//throw new NotImplementedException();
		}

		public void SetScale(double scale)
		{
			//throw new NotImplementedException();
		}

		public void PushTransform()
		{
			//throw new NotImplementedException();
		}

		public void PopTransform()
		{
			//throw new NotImplementedException();
		}

		public void SaveTransform(string key)
		{
			//throw new NotImplementedException();
		}

		public void LoadTransform(string key)
		{
			//throw new NotImplementedException();
		}

		public void ResetTransform()
		{
			//throw new NotImplementedException();
		}

		public void Reset()
		{
			//throw new NotImplementedException();
		}

		public void Play()
		{
			//throw new NotImplementedException();
		}

		public void Pause()
		{
			//throw new NotImplementedException();
		}

		public void Stop()
		{
			//throw new NotImplementedException();
		}

		public void AddImageAsset(string name, WriteableBitmap imageAsset)
		{
			//throw new NotImplementedException();
		}
	}

    public class SparkiyGame : Game
    {
		private static readonly ILogger Log = LogManagerFactory.DefaultLogManager.GetLogger<SparkiyGame>();
		private readonly GraphicsDeviceManager graphicsDeviceManager;
	    private IEngineBindings engine;

	    public SparkiyGame()
	    {
			// Creates a graphics manager. This is mandatory.
			this.graphicsDeviceManager = new GraphicsDeviceManager(this);

			// Setup the relative directory to the executable directory
			// for loading contents with the ContentManager
			this.Content.RootDirectory = "Assets";

			// Add Bindings service
			this.Services.AddService(typeof(IGraphicsBindings), new GraphicsBindings(this));
	    }


	    protected override void Draw(GameTime gameTime)
	    {
		    base.Draw(gameTime);

		    // Clears the screen with the Color.CornflowerBlue
		    this.GraphicsDevice.Clear(Color.CornflowerBlue);
	    }

		public IGraphicsBindings GraphicsBindings
		{
			get { return this.Services.GetService<IGraphicsBindings>(); }
		}

	    public void AssignEngine(IEngineBindings engine)
	    {
		    this.engine = engine;
	    }
    }
}
