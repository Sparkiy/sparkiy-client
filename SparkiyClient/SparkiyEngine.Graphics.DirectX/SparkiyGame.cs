using System.IO.Compression;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Direct2D;
using SharpDX.Toolkit.Graphics;
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


		public void SetBackground(float r, float g, float b)
		{
			this.game.BackgroundColor = new Color4(r, g, b, 1f);
		}

		public void DrawRectangle(int x, int y, int w, int h)
		{
			this.game.Canvas.PushObject(
				new CanvasRectangle(
					new RectangleF(x, y, w, h),
					Brushes.Purple,
					false, 
					2f));
		}
	}

	public class SparkiyGame : Game
	{
		private GraphicsDeviceManager graphicsDeviceManager;
		private SpriteBatch spriteBatch;
		private SpriteFont debugFont;
		private Texture2D logoTexture;

		private Color4 backgroundColor; 


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
			this.Canvas.PushObject(new CanvasBitmap(this.logoTexture, new RectangleF(100, 100, this.logoTexture.Width, this.logoTexture.Height)));
			this.Canvas.PushObject(new CanvasRectangle(new RectangleF(96f, 96.0f, this.logoTexture.Width + 8, this.logoTexture.Height + 8), Brushes.White, false, 2f, null));
			this.Canvas.PushObject(new CanvasRectangle(new RectangleF(100f, 100.0f, this.logoTexture.Width, this.logoTexture.Height), Brushes.White, false, 2f, null));
			this.Canvas.PushObject(new CanvasRectangle(new RectangleF(104f, 104.0f, this.logoTexture.Width - 8, this.logoTexture.Height - 8), Brushes.White, false, 2f, null));
			this.Canvas.PushObject(new CanvasText(
				"Hello from Sparkiy",
				new TextFormat(Canvas.DirectWriteFactory, "Segoe UI Light", 24.0f),
				new RectangleF(10, 10, 900, 100),
				Brushes.White,
				DrawTextOptions.NoSnap,
				MeasuringMode.Natural));

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

		public SharpDX.Toolkit.Direct2D.Canvas Canvas { get; set; }

		public Color4 BackgroundColor
		{
			get { return this.backgroundColor; }
			set { this.backgroundColor = value; }
		}

		public IGraphicsBindings GraphicsBindings
		{
			get { return this.Services.GetService<IGraphicsBindings>(); }
		}
	}
}