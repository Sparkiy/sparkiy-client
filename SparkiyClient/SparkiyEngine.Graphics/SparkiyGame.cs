using System;
using System.Text;
using Windows.UI.Xaml.Media.Imaging;
using MetroLog;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SparkiyEngine.Bindings.Component.Common;
using SparkiyEngine.Bindings.Component.Engine;
using SparkiyEngine.Bindings.Component.Graphics;
using SparkiyEngine.Graphics.Canvas;

namespace SparkiyEngine.Graphics
{
	public class GraphicsBindings : GameComponent, IGraphicsBindings
	{
		public GraphicsBindings(SparkiyGame game) : base(game)
		{
			this.Game = game;
		}

		public void SetStrokeColor(double red, double green, double blue)
		{
			this.Game.Canvas.SetStrokeColor((float)red, (float)green, (float)blue);
		}

		public NumberGroup3 GetStrokeColor()
		{
			var strokeColor = this.Game.Canvas.GetStrokeColor();
			return new NumberGroup3
			{
				First = strokeColor.R / 255.0,
				Second = strokeColor.G / 255.0,
				Third = strokeColor.B / 255.0
			};
		}

		public void SetStrokeThickness(double thickness)
		{
			this.Game.Canvas.SetStrokeThickness((float)thickness);
		}

		public double GetStrokeThickness()
		{
			return this.Game.Canvas.GetStrokeThickness();
		}

		public void StrokeDisable()
		{
			this.Game.Canvas.StrokeDisable();
		}

		public void SetFill(double red, double green, double blue)
		{
			this.Game.Canvas.SetFill((float)red, (float)green, (float)blue);
		}

		public NumberGroup3 GetFill()
		{
			var fillColor = this.Game.Canvas.GetFill();
			return new NumberGroup3
			{
				First = fillColor.R / 255.0,
				Second = fillColor.G / 255.0,
				Third = fillColor.B / 255.0
			};
		}

		public void FillDisable()
		{
			this.Game.Canvas.FillDisable();
		}

		public void PushStyle()
		{
			this.Game.Canvas.PushStyle();
		}

		public void PopStyle()
		{
			this.Game.Canvas.PopStyle();
		}

		public void SaveStyle(string key)
		{
			this.Game.Canvas.SaveStyle(key);
		}

		public void LoadStyle(string key)
		{
			this.Game.Canvas.LoadStyle(key);
		}

		public void ResetStyle()
		{
			this.Game.Canvas.ResetStyle();
		}

		public void DrawRectangle(double x, double y, double width, double height)
		{
			this.Game.Canvas.DrawRect((float)x, (float)y, (float)width, (float)height);
		}

		public void DrawSquare(double x, double y, double size)
		{
			this.Game.Canvas.DrawSquare((float)x, (float)y, (float)size);
		}

		public void DrawEllipse(double x, double y, double majorRadius, double minorRadius)
		{
			this.Game.Canvas.DrawEllipse((float)x, (float)y, (float)majorRadius * 2f, (float)minorRadius * 2f);
		}

		public void DrawCircle(double x, double y, double radius)
		{
			this.Game.Canvas.DrawEllipse((float)x, (float)y, (float)radius * 2f, (float)radius * 2f);
		}

		public void DrawLine(double x1, double y1, double x2, double y2)
		{
			this.Game.Canvas.DrawLine((float)x1, (float)y1, (float)x2, (float)y2);
		}

		public void DrawTexture(string assetName, double x, double y)
		{
			// Retrieve texture
			var texture = this.Game.ResolveTexture(assetName);

			// Call canvas method to draw texture
			this.Game.Canvas.DrawTexture((float) x, (float) y, texture.Width, texture.Height, texture);
		}

		public void DrawTexture(string assetName, double x, double y, double width, double height)
		{
			// Retrieve texture
			var texture = this.Game.ResolveTexture(assetName);

			// Call canvas method to draw texture
			this.Game.Canvas.DrawTexture((float)x, (float)y, (float)width, (float)height, texture);
		}

		public void SetBackground(double red, double green, double blue)
		{
			this.Game.Canvas.SetBackgroundColor((float)red, (float)green, (float)blue);
			this.Game.Canvas.ClearBackground();
		}

		public void SetTranslation(double x, double y)
		{
			throw new NotImplementedException();
		}

		public void SetRotation(double angle)
		{
			throw new NotImplementedException();
		}

		public void SetScale(double scale)
		{
			throw new NotImplementedException();
		}

		public void PushTransform()
		{
			this.Game.Canvas.PushTransform();
		}

		public void PopTransform()
		{
			this.Game.Canvas.PopStyle();
		}

		public void SaveTransform(string key)
		{
			this.Game.Canvas.SaveTransform(key);
		}

		public void LoadTransform(string key)
		{
			this.Game.Canvas.LoadTransform(key);
		}

		public void ResetTransform()
		{
			this.Game.Canvas.ResetTransform();
		}

		public void Reset()
		{
			this.Game.Reset();
		}

		public void Play()
		{
			this.Game.Play();
		}

		public void Pause()
		{
			this.Game.Pause();
		}

		public void Stop()
		{
			this.Game.Pause();
		}

		public void AddImageAsset(string name, WriteableBitmap imageAsset)
		{
			this.Game.AddImageAsset(name, imageAsset);
		}


		#region Properties

		/// <summary>
		/// Gets the game.
		/// </summary>
		/// <value>
		/// The game.
		/// </value>
		public new SparkiyGame Game { get; private set; }

		#endregion /Properties
	}

    public class SparkiyGame : Game
    {
		private static readonly ILogger Log = LogManagerFactory.DefaultLogManager.GetLogger<SparkiyGame>();
		private readonly GraphicsDeviceManager graphicsDeviceManager;
	    private IEngineBindings engine;
	    private ITextureProvider textureProvider;

	    private bool isRunning;


	    public SparkiyGame()
	    {
			// Creates a graphics manager. This is mandatory.
			this.graphicsDeviceManager = new GraphicsDeviceManager(this);

			// Setup the relative directory to the executable directory
			// for loading contents with the ContentManager
			this.Content.RootDirectory = "Assets";

			// Add services
			this.Services.AddService(typeof(IGraphicsBindings), new GraphicsBindings(this));

			// Initialize
			this.InitializeResources();
	    }

	    private void InitializeResources()
	    {
		    this.isRunning = false;

			// Initialize canvas
			this.Canvas = new SparkiyCanvas(this);
			this.Canvas.DrawReady += CanvasOnDrawReady;
			this.Components.Add(this.Canvas);

			// Instantiate texture provider
			this.textureProvider = new BitmapImageTextureProvider(this);

			// Mouse is visible by default
			this.IsMouseVisible = true;
	    }


	    protected override void Draw(GameTime gameTime)
	    {
			// Do not call components if game is not running
			if (this.isRunning)
				base.Draw(gameTime);
	    }

	    private void CanvasOnDrawReady(object sender)
	    {
		    this.engine.CallDrawFunction();
	    }

	    internal Texture2D ResolveTexture(string assetName)
	    {
		    return this.textureProvider.GetTexture(assetName);
	    }

	    public SparkiyCanvas Canvas { get; private set; }

	    public IGraphicsBindings GraphicsBindings
		{
			get { return this.Services.GetService<IGraphicsBindings>(); }
		}

	    internal ITextureProvider TextureProvider
	    {
			get { return this.textureProvider; }
	    }

	    public void Play()
	    {
		    this.isRunning = true;
	    }

	    public void Pause()
	    {
			this.isRunning = false;
	    }

	    public void Reset()
	    {
			// Stop the game
		    this.Pause();

			// Reset canvas
			this.Canvas.Reset();

		    // Initialize game
		    this.InitializeResources();
	    }

	    public void AssignEngine(IEngineBindings engine)
	    {
		    this.engine = engine;
	    }

		public void AddImageAsset(string name, WriteableBitmap imageAsset)
		{
			// Check if current instance of texture provider is supported and not null
			var provider = textureProvider as BitmapImageTextureProvider;
			if (provider == null)
				throw new InvalidOperationException("Texture provider not supported.");

			// Add image asset to texture provider
			provider.AddImageAsset(name, imageAsset);
		}
    }
}
