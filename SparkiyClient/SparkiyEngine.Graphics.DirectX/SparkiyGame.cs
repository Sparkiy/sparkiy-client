using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Direct2D;
using SharpDX.Toolkit.Graphics;

namespace SparkiyEngine.Graphics.DirectX
{
	public class SparkiyGame : Game
	{
		private GraphicsDeviceManager graphicsDeviceManager;
		private SpriteBatch spriteBatch;
		private SpriteFont debugFont;
		private Texture2D logoTexture;
		public SharpDX.Toolkit.Direct2D.Canvas Canvas { get; set; }

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
			Content.RootDirectory = "Assets";

			// Add Direct2D service
			Services.AddService(typeof(IDirect2DService), new Direct2DService(this.graphicsDeviceManager));
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
			spriteBatch = new SpriteBatch(GraphicsDevice);

			// Create 2D Canvas for caching 
			Canvas = new SharpDX.Toolkit.Direct2D.Canvas(this);
			Brushes.Initialize(Canvas.DeviceContext);

			Canvas.Clear();
			Canvas.PushObject(new CanvasBitmap(this.logoTexture, new RectangleF(100, 100, this.logoTexture.Width, this.logoTexture.Height)));
			Canvas.PushObject(new CanvasRectangle(new RectangleF(97f, 97.0f, this.logoTexture.Width + 5, this.logoTexture.Height + 5), Brushes.Orange, false, 2f, null));
			Canvas.PushObject(new CanvasRectangle(new RectangleF(100f, 100.0f, this.logoTexture.Width, this.logoTexture.Height), Brushes.Orange, false, 2f, null));
			Canvas.PushObject(new CanvasRectangle(new RectangleF(103f, 103.0f, this.logoTexture.Width-5, this.logoTexture.Height-5), Brushes.Orange, false, 2f, null));
			Canvas.PushObject(new CanvasText(
				"Hello there",
				new TextFormat(Canvas.DirectWriteFactory, "Consolas", 12.0f),
				new RectangleF(300, 300, 100, 100),
				Brushes.White,
				DrawTextOptions.NoSnap,
				MeasuringMode.Natural));

			base.LoadContent();
		}

		protected override void UnloadContent()
		{
			spriteBatch.Dispose();

			base.UnloadContent();
		}

		protected override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);

			// Clears the screen with the Color.CornflowerBlue
			GraphicsDevice.Clear(Color.Purple);

			Canvas.Render();
		}
	}
}