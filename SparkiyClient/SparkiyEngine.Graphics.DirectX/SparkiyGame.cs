using System;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using SharpDX.DXGI;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Direct2D;
using SparkiyEngine.Bindings.Component.Common;
using SparkiyEngine.Bindings.Component.Engine;
using SparkiyEngine.Bindings.Component.Graphics;

namespace SparkiyEngine.Graphics.DirectX
{
    [ComVisible(false)]
    public class SparkiyGame : Game
    {
        private readonly IEngineBindings engine;
		private GraphicsDeviceManager graphicsDeviceManager;

        private bool isPlaying = false;

		private Color4 backgroundColor;
        
		// Styles
        private Style2D style2D;
        private readonly PushPopManagement<Style2D> stylePushPopManagement = new PushPopManagement<Style2D>(); 

		// Text
		private string fontFamily;
		private float fontSize;
		private TextFormat fontFormat;
		private Color4 fontColor;

		// Transform
		private Matrix transformMatrix;
		private readonly PushPopManagement<Matrix> transformPushPopManagement = new PushPopManagement<Matrix>();


		/// <summary>
		/// Initializes a new instance of the <see cref="SparkiyGame" /> class.
		/// </summary>
		public SparkiyGame(IEngineBindings engine)
		{
		    this.engine = engine;

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

            // Add Texture provider
            this.Services.AddService(typeof(ITextureProvider), new BasicTextureProvider(this));
		}

		protected override void Initialize()
		{
			base.Initialize();
		}

		protected override void LoadContent()
		{
			// Create 2D Canvas for caching 
			this.Canvas = new Canvas(this);
			Brushes.Initialize(this.Canvas.DeviceContext);

			// Reset variables
			this.Reset();

			base.LoadContent();
		}

		protected override void UnloadContent()
		{
			// Clean canvas
			this.Canvas.Clear();
			this.Canvas.Dispose();

			base.UnloadContent();
		}

        public void Play()
        {
            this.isPlaying = true;
        }

        public void Pause()
        {
            this.isPlaying = false;
        }

		public void AddTranslate(float x, float y)
		{
			var tranalateVector = new Vector3(x, y, 0);

			Matrix tranalateMatrix;
			Matrix.Translation(ref tranalateVector, out tranalateMatrix);

			this.transformMatrix *= tranalateMatrix;

			this.SetTransform(this.transformMatrix);
		}

		public void AddRotate(float angle)
		{
			Matrix rotateMatrix;
			Matrix.RotationZ(MathUtil.DegreesToRadians(angle), out rotateMatrix);

			this.transformMatrix *= rotateMatrix;

			this.SetTransform(this.transformMatrix);
		}

		public void AddScale(float scale)
		{
			var scaleVector = new Vector3(scale, scale, 1f);

			Matrix scaleMatrix;
			Matrix.Scaling(ref scaleVector, out scaleMatrix);

			this.transformMatrix *= scaleMatrix;

			this.SetTransform(this.transformMatrix);
		}

		public void PushTransform()
		{
			this.transformPushPopManagement.Push(this.transformMatrix);
		}

		public void PopTransform()
		{
		    this.transformMatrix = this.transformPushPopManagement.Pop();
            this.SetTransform(this.transformMatrix);
		}

		public void SaveTransform(string key)
		{
			this.transformPushPopManagement.Save(key, this.transformMatrix);
		}

		public void LoadTransform(string key)
		{
			this.SetTransform(this.transformPushPopManagement.Load(key));
		}

		public void ResetTransform()
		{
			this.transformMatrix = Matrix.Identity;
			this.SetTransform(this.transformMatrix);
		}

		private void SetTransform(Matrix transform)
		{
			this.Canvas.PushObject(new CanvasTransform(transform));
		}

		public void DrawText(string text, float x, float y)
		{
			this.Canvas.PushObject(
				new CanvasText(
					text,
					this.fontFormat,
					new RectangleF(x, y, this.GraphicsDevice.Viewport.Width - x, this.GraphicsDevice.Viewport.Height - y),
					new SolidColorBrush(this.Canvas.DeviceContext, this.FontColor),
					DrawTextOptions.NoSnap,
					MeasuringMode.Natural));
		}

		private void RebuildFontFormat()
		{
			if (String.IsNullOrEmpty(this.FontFamily) || MathUtil.NearEqual(0, this.FontSize))
				return;

			this.fontFormat = new TextFormat(this.Canvas.DirectWriteFactory, this.FontFamily, this.FontSize);
		}

		public void DrawLine(float x1, float y1, float x2, float y2)
		{
			if (this.IsStrokeEnabled)
			{
				// Draw outline
				this.Canvas.PushObject(
					new CanvasLine(
						new Vector2(x1, y1), 
						new Vector2(x2, y2),
						new SolidColorBrush(this.Canvas.DeviceContext, this.StrokeColor),
						this.StrokeThickness));
			}
		}

		public void DrawEllipse(float x, float y, float radiusX, float radiusY)
		{
			if (this.IsFillEnabled)
			{
				// Draw fill
				this.Canvas.PushObject(
					new CanvasEllipse(
						new Ellipse() { Point = new Vector2(x, y), RadiusX = radiusX, RadiusY = radiusY },
						new SolidColorBrush(this.Canvas.DeviceContext, this.FillColor),
						true));
			}

			if (this.IsStrokeEnabled)
			{
				// Draw outline
				this.Canvas.PushObject(
					new CanvasEllipse(
						new Ellipse() { Point = new Vector2(x, y), RadiusX = radiusX, RadiusY = radiusY },
						new SolidColorBrush(this.Canvas.DeviceContext, this.StrokeColor),
						false,
						this.StrokeThickness));
			}
		}

		public void DrawRectangle(float x, float y, float width, float height)
		{
			if (this.IsFillEnabled)
			{
				// Draw fill
				this.Canvas.PushObject(
					new CanvasRectangle(
						new RectangleF((float)x, (float)y, (float)width, (float)height),
						new SolidColorBrush(this.Canvas.DeviceContext, this.FillColor),
						true));
			}

			if (this.IsStrokeEnabled)
			{
				// Draw outline
				this.Canvas.PushObject(
					new CanvasRectangle(
						new RectangleF((float)x, (float)y, (float)width, (float)height),
						new SolidColorBrush(this.Canvas.DeviceContext, this.StrokeColor),
						false,
						this.StrokeThickness));
			}
		}

        public void DrawTexture(string assetName, float x, float y, float width = -1f, float height = -1f)
        {
            var texture = this.Services.GetService<ITextureProvider>().GetTexture(assetName);
            this.Canvas.PushObject(
                new CanvasBitmap(
                    texture, 
                    new RectangleF(
                        x, 
                        y, 
                        width < 0 ? texture.Width : width,
                        height < 0 ? texture.Height : height)));
        }

		protected override void Draw(GameTime gameTime)
		{
		    if (!isPlaying) return;

			base.Draw(gameTime);

			// Clears the screen with the Color.CornflowerBlue
			this.GraphicsDevice.Clear(this.BackgroundColor);

			// Draw 2D
			this.Reset();
			//this.Canvas.Render();

			// Execute users draw loop
			try
			{
			    this.engine.CallDrawFunction();
			}
			catch(Exception ex)
			{
				this.StoppedByException(ex);
			}

			this.Canvas.Render();
		}

		public void Reset()
		{
			// Clear the canvas
			this.Canvas.Clear();

			// Clear all properties
			this.BackgroundColor = Brushes.White.Color;
			this.StrokeColor = Brushes.Gray.Color;
			this.StrokeThickness = 2f;
			this.IsStrokeEnabled = false;
			this.FontSize = 24f;
			this.FillColor = Brushes.Black.Color;
			this.FontFamily = "Segoe UI";
			this.FontColor = Brushes.Black.Color;

			// Reset transform
			this.transformMatrix = Matrix.Identity;
			this.transformPushPopManagement.Clear();

			// Set variables
			this.engine.LanguageBindings.SetVariable("DELTA", 0d, DataTypes.Number);
			this.engine.LanguageBindings.SetVariable("TOTAL", 0d, DataTypes.Number);
			this.engine.LanguageBindings.SetVariable("WIDTH", (double)this.GraphicsDevice.Viewport.Width, DataTypes.Number);
            this.engine.LanguageBindings.SetVariable("HEIGHT", (double)this.GraphicsDevice.Viewport.Height, DataTypes.Number);
        }

		private void StoppedByException(Exception ex)
		{
			this.Pause();
            this.Reset();
        }

		#region Surface

		public Color4 BackgroundColor
		{
			get { return this.backgroundColor; }
			set { this.backgroundColor = value; }
		}

		#endregion Surface

		#region Text

		public string FontFamily
		{
			get { return this.fontFamily; }
			set
			{
				this.fontFamily = value;
				this.RebuildFontFormat();
			}
		}

		public float FontSize
		{
			get { return this.fontSize; }
			set
			{
				this.fontSize = Math.Max(0, value);
				this.RebuildFontFormat();
			}
		}

		public Color4 FontColor
		{
			get { return this.fontColor; }
			set { this.fontColor = value; }
		}

		#endregion Text

		#region Styles

        public void PushStyle2D()
        {
            this.stylePushPopManagement.Push(this.style2D);
        }

        public void PopStyle2D()
        {
            this.style2D = this.stylePushPopManagement.Pop();
        }

        public void SaveStyle2D(string key)
        {
            this.stylePushPopManagement.Save(key, this.style2D);
        }

        public void LoadStyle2D(string key)
        {
            this.style2D = this.stylePushPopManagement.Load(key);
        }

        public void ResetStyle2D()
        {
            this.style2D = new Style2D();
        }


		public bool IsStrokeEnabled
		{
			get { return this.style2D.IsStrokeEnabled; }
			set { this.style2D.IsStrokeEnabled = value; }
		}

		public Color4 StrokeColor
		{
			get { return this.style2D.StrokeColor; }
			set
			{
				this.style2D.StrokeColor = value;

				// Enable stroke if possible
				if (this.StrokeThickness != 0)
					this.IsStrokeEnabled = true;
			}
		}

		public float StrokeThickness
		{
			get { return this.style2D.StrokeThickness; }
			set
			{
				this.style2D.StrokeThickness = Math.Max(0, value);

				// Disable/Enable stroke depending on thickness
				if (this.StrokeThickness == 0)
					this.IsStrokeEnabled = false;
				else this.IsStrokeEnabled = true;
			}
		}

		public bool IsFillEnabled
		{
			get { return this.style2D.IsFillEnabled; }
			set { this.style2D.IsFillEnabled = value; }
		}

		public Color4 FillColor
		{
			get { return this.style2D.FillColor; }
			set
			{
				this.style2D.FillColor = value;

				// Enable fill on every fill color set call
				this.IsFillEnabled = true;
			}
		}

		#endregion Styles

		public Canvas Canvas { get; set; }

		public IGraphicsBindings GraphicsBindings
		{
			get { return this.Services.GetService<IGraphicsBindings>(); }
		}
	}
}