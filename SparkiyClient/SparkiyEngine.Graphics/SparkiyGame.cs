using System;
using System.Text;
using Windows.UI.Xaml.Media.Imaging;
using MetroLog;
using Microsoft.Xna.Framework;
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
			throw new NotImplementedException();
			return new NumberGroup3();
		}

		public void SetStrokeThickness(double thickness)
		{
			this.Game.Canvas.SetStrokeThickness((float)thickness);
		}

		public double GetStrokeThickness()
		{
			throw new NotImplementedException();
			return 0;
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
			throw new NotImplementedException();
			return new NumberGroup3();
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
			throw new NotImplementedException();
		}

		public void DrawTexture(string assetName, double x, double y, double width, double height)
		{
			throw new NotImplementedException();
		}

		public void SetBackground(double red, double green, double blue)
		{
			throw new NotImplementedException();
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
			throw new NotImplementedException();
		}

		public void PopTransform()
		{
			throw new NotImplementedException();
		}

		public void SaveTransform(string key)
		{
			throw new NotImplementedException();
		}

		public void LoadTransform(string key)
		{
			throw new NotImplementedException();
		}

		public void ResetTransform()
		{
			throw new NotImplementedException();
		}

		public void Reset()
		{
			throw new NotImplementedException();
		}

		public void Play()
		{
			//throw new NotImplementedException();
		}

		public void Pause()
		{
			throw new NotImplementedException();
		}

		public void Stop()
		{
			throw new NotImplementedException();
		}

		public void AddImageAsset(string name, WriteableBitmap imageAsset)
		{
			throw new NotImplementedException();
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


	    public SparkiyGame()
	    {
			// Creates a graphics manager. This is mandatory.
			this.graphicsDeviceManager = new GraphicsDeviceManager(this);

			// Setup the relative directory to the executable directory
			// for loading contents with the ContentManager
			this.Content.RootDirectory = "Assets";

			// Add services
			this.Services.AddService(typeof(IGraphicsBindings), new GraphicsBindings(this));

			// Add components 
		    this.Canvas = new SparkiyCanvas(this);
			this.Components.Add(this.Canvas);
		    //this.Components.Add(new TimeDebuger(this));

			// MOuse is visible by default
		    this.IsMouseVisible = true;
	    }
		

	    protected override void Draw(GameTime gameTime)
	    {
			// Clears the screen with the Color.CornflowerBlue
			this.GraphicsDevice.Clear(Color.CornflowerBlue);

		    base.Draw(gameTime);
	    }

		public SparkiyCanvas Canvas { get; private set; }

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
