using System;
using System.IO.Compression;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Direct2D;
using SharpDX.Toolkit.Graphics;
using SparkiyEngine.Bindings.Common.Component;
using SparkiyEngine.Bindings.Graphics;
using SparkiyEngine.Bindings.Graphics.Component;

namespace SparkiyEngine.Graphics.DirectX
{
	public class GraphicsBindings : Component, IGraphicsBindings
	{
		private readonly SparkiyGame game;


		public GraphicsBindings(SparkiyGame game)
		{
			this.game = game;
		}


		public void SetBackground(double red, double green, double blue)
		{
			this.game.BackgroundColor = new Color4((float)red, (float)green, (float)blue, 1f);
		}

		public void DrawRectangle(double x, double y, double width, double height)
		{
			this.game.DrawRectangle((float)x, (float)y, (float)width, (float)height);
		}

		#region StrokeColor

		public NumberGroup3 GetStrokeColor()
		{
			return new NumberGroup3()
			{
				First = this.game.StrokeColor.Red,
				Second = this.game.StrokeColor.Green,
				Third = this.game.StrokeColor.Blue
			};
		}

		public void SetStrokeColor(double red, double green, double blue)
		{
			this.game.StrokeColor = new Color4((float)red, (float)green, (float)blue, 1f);
		}

		#endregion StrokeColor

		#region StrokeThickness

		public float GetStrokeThickness()
		{
			return this.game.StrokeThickness;
		}

		public void SetStrokeThickness(double thickness)
		{
			this.game.StrokeThickness = (float)thickness;
		}

		#endregion StrokeThickness

		public void StrokeDisable()
		{
			this.game.IsStrokeEnabled = false;
		}

		public void Reset()
		{
			this.game.Reset();
		}


		public event MethodCallRequestEventHandler Pre2DDraw;

		public void TriggerPre2dDraw()
		{
			if (this.Pre2DDraw != null)
				this.Pre2DDraw(this);
		}
	}

	public class SparkiyGame : Game
	{
		private GraphicsDeviceManager graphicsDeviceManager;
		private SpriteBatch spriteBatch;
		private Texture2D logoTexture;

		private Color4 backgroundColor;

		// Styles
		private bool isStrokeEnabled;
		private Color4 strokeColor;
		private float strokeThickness;


		/// <summary>
		/// Initializes a new instance of the <see cref="SparkiyGame" /> class.
		/// </summary>
		public SparkiyGame()
		{
			// Creates a graphics manager. This is mandatory.
			this.graphicsDeviceManager = new GraphicsDeviceManager(this);

			// This flag is mandatory to support Direct2D
			this.graphicsDeviceManager.DeviceCreationFlags |= SharpDX.Direct3D11.DeviceCreationFlags.BgraSupport;

			// Setup the relative directory to the executable directory
			// for loading contents with the ContentManager
			this.Content.RootDirectory = "Assets";

			// Add Direct2D service
			this.Services.AddService(typeof(IDirect2DService), new Direct2DService(this.graphicsDeviceManager));

			// Add Bindings service
			this.Services.AddService(typeof(IGraphicsBindings), new GraphicsBindings(this));
		}

		protected override void Initialize()
		{
			base.Initialize();
		}

		protected override void LoadContent()
		{
			// Loads the balls texture (32 textures (32x32) stored vertically => 32 x 1024 ).
			this.logoTexture = Content.Load<Texture2D>("sparkiy.png");

			// SpriteFont supports the following font file format:
			// - DirectX Toolkit MakeSpriteFont or SharpDX Toolkit tkfont
			// - BMFont from Angelcode http://www.angelcode.com/products/bmfont/
			//this.debugFont = Content.Load<SpriteFont>("DebugFont");

			// Instantiate a SpriteBatch
			this.spriteBatch = new SpriteBatch(this.GraphicsDevice);

			// Create 2D Canvas for caching 
			this.Canvas = new SharpDX.Toolkit.Direct2D.Canvas(this);
			Brushes.Initialize(this.Canvas.DeviceContext);
			this.Canvas.Clear();

			base.LoadContent();
		}

		protected override void UnloadContent()
		{
			this.spriteBatch.Dispose();

			base.UnloadContent();
		}

		public void DrawRectangle(float x, float y, float width, float height)
		{
			this.Canvas.PushObject(
				new CanvasRectangle(
					new RectangleF((float)x, (float)y, (float)width, (float)height),
					new SolidColorBrush(this.Canvas.DeviceContext, this.StrokeColor),
					false,
					this.StrokeThickness));
		}

		protected override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);

			// Clears the screen with the Color.CornflowerBlue
			this.GraphicsDevice.Clear(this.BackgroundColor);

			// Draw 2D
			this.Canvas.Clear();
			this.Canvas.Render();
			this.GraphicsBindings.TriggerPre2dDraw();
			this.Canvas.Render();
		}

		public void Reset()
		{
			// Clear the canvas
			this.Canvas.Clear();

			// Clear all properties
			this.BackgroundColor = new Color4();
			this.StrokeColor = new Color4();
			this.StrokeThickness = 2f;
		}

		public SharpDX.Toolkit.Direct2D.Canvas Canvas { get; set; }

		public Color4 BackgroundColor
		{
			get { return this.backgroundColor; }
			set { this.backgroundColor = value; }
		}

		public bool IsStrokeEnabled
		{
			get { return this.isStrokeEnabled; }
			set { this.isStrokeEnabled = value; }
		}

		public Color4 StrokeColor
		{
			get { return this.strokeColor; }
			set
			{
				this.strokeColor = value;

				// Enable stroke if possible
				if (this.StrokeThickness != 0)
					this.IsStrokeEnabled = true;
			}
		}

		public float StrokeThickness
		{
			get { return this.strokeThickness; }
			set
			{
				this.strokeThickness = Math.Max(0, value);

				// Disable/Enable stroke depending on thickness
				if (this.StrokeThickness == 0)
					this.IsStrokeEnabled = false;
				else this.IsStrokeEnabled = true;
			}
		}

		public IGraphicsBindings GraphicsBindings
		{
			get { return this.Services.GetService<IGraphicsBindings>(); }
		}
	}
}