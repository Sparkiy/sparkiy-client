using System.IO.Compression;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Direct2D;
using SharpDX.Toolkit.Graphics;
using SparkiyEngine.Bindings.Common.Component;
using SparkiyEngine.Bindings.Graphics;

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
			this.game.Canvas.PushObject(
				new CanvasRectangle(
					new RectangleF((float)x, (float)y, (float)width, (float)height),
					new SolidColorBrush(this.game.Canvas.DeviceContext, this.game.StrokeColor),
					false,
					4f));
		}

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

		public void Reset()
		{
			this.game.Reset();
		}
	}

	public class SparkiyGame : Game
	{
		private GraphicsDeviceManager graphicsDeviceManager;
		private SpriteBatch spriteBatch;
		private SpriteFont debugFont;
		private Texture2D logoTexture;

		private Color4 backgroundColor; 

		// Styles
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
			this.Canvas.Clear();
			Brushes.Initialize(this.Canvas.DeviceContext);

			base.LoadContent();
		}

		protected override void UnloadContent()
		{
			this.spriteBatch.Dispose();

			base.UnloadContent();
		}

		protected override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);

			// Clears the screen with the Color.CornflowerBlue
			this.GraphicsDevice.Clear(this.BackgroundColor);

			this.Canvas.Render();
		}

		public void Reset()
		{
			// Clear the canvas
			this.Canvas.Clear();

			// Clear all properties
			this.BackgroundColor = new Color4();
			this.StrokeColor = new Color4();
		}

		public SharpDX.Toolkit.Direct2D.Canvas Canvas { get; set; }

		public Color4 BackgroundColor
		{
			get { return this.backgroundColor; }
			set { this.backgroundColor = value; }
		}

		public Color4 StrokeColor
		{
			get { return this.strokeColor; }
			set { this.strokeColor = value; }
		}

		public IGraphicsBindings GraphicsBindings
		{
			get { return this.Services.GetService<IGraphicsBindings>(); }
		}
	}
}