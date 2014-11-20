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
	/// <summary>
	/// Graphics Bindings implementation for SparkiyGame game instance
	/// </summary>
	public class GraphicsBindings : Component, IGraphicsBindings
	{
		private readonly SparkiyGame game;


		/// <summary>
		/// Initializes a new instance of the <see cref="GraphicsBindings"/> class.
		/// </summary>
		/// <param name="game">The game.</param>
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

		/// <summary>
		/// Gets the color of the stroke.
		/// </summary>
		/// <returns>
		/// Returns group of three decimal numbers cooresponding to the color values in this order: red, green, blue
		/// </returns>
		/// <remarks>
		/// Stroke is used in all 2D shapes that have borders. The obvious exceptions are sprites and textures.
		/// </remarks>
		public NumberGroup3 GetStrokeColor()
		{
			return new NumberGroup3()
			{
				First = this.game.StrokeColor.Red,
				Second = this.game.StrokeColor.Green,
				Third = this.game.StrokeColor.Blue
			};
		}

		/// <summary>
		/// Sets the color of the stroke.
		/// </summary>
		/// <param name="red">The red. Valid range is from 0 to 1 including those values.</param>
		/// <param name="green">The green. Valid range is from 0 to 1 including those values.</param>
		/// <param name="blue">The blue. Valid range is from 0 to 1 including those values.</param>
		/// <remarks>
		/// Color values are in range from 0 to 1 which can be directly mapped to hex values 0 to 255. If provided value is out of range, it will be rounded to closest valid value. For example, if provided value for red is 1.2, resulting color will have red of value 1. Another example would be if you provide value for red -0.5, resulting color will have red of value 0. Stroke is used in all 2D shapes that have borders. The obvious exceptions are sprites and textures.
		/// </remarks>
		public void SetStrokeColor(double red, double green, double blue)
		{
			this.game.StrokeColor = new Color4((float)red, (float)green, (float)blue, 1f);
		}

		#endregion StrokeColor

		#region StrokeThickness

		/// <summary>
		/// Gets the stroke thickness.
		/// </summary>
		/// <returns>
		/// Returns decimal number cooresponding to set stroke thickness.
		/// </returns>
		/// <remarks>
		/// Value will not be negative. Stroke Thickness is used in all 2D shapes that have borders. The obvious exceptions are sprites and textures.
		/// </remarks>
		public float GetStrokeThickness()
		{
			return this.game.StrokeThickness;
		}

		/// <summary>
		/// Gets the stroke thickness.
		/// </summary>
		/// <param name="thickness">The stroke thickness value of all following 2D shapes that have borders.</param>
		/// <remarks>
		/// Value must not be negative. Zero is valid value. Stroke Thickness is used in all 2D shapes that have borders. The obvious exceptions are sprites and textures.
		/// </remarks>
		public void SetStrokeThickness(double thickness)
		{
			this.game.StrokeThickness = (float)thickness;
		}

		#endregion StrokeThickness

		/// <summary>
		/// Disables the stroke of all 2D shaped that have borders.
		/// </summary>
		/// <remarks>
		/// This will be called when stroke thickness is set to zero. To enable stroke, set stroke thickness to a value larger than zero or re-set stroke color.
		/// </remarks>
		public void StrokeDisable()
		{
			this.game.IsStrokeEnabled = false;
		}

		/// <summary>
		/// Resets this instance.
		/// </summary>
		public void Reset()
		{
			this.game.Reset();
		}

		#region Pre2DDraw

		/// <summary>
		/// Occurs before 2D draw is called so that user can fill collection with drawable objects
		/// </summary>
		public event MethodCallRequestEventHandler Pre2DDraw;

		/// <summary>
		/// Triggers the pre2 d draw.
		/// </summary>
		public void TriggerPre2DDraw()
		{
			if (this.Pre2DDraw != null)
				this.Pre2DDraw(this);
		}

		#endregion Pre2DDraw
	}

	public class SparkiyGame : Game
	{
		private GraphicsDeviceManager graphicsDeviceManager;

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
			this.GraphicsBindings.TriggerPre2DDraw();
			this.Canvas.Render();
		}

		public void Reset()
		{
			// Clear the canvas
			this.Canvas.Clear();

			// Clear all properties
			this.BackgroundColor = Brushes.Black.Color;
			this.StrokeColor = Brushes.White.Color;
			this.StrokeThickness = 2f;
		}

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

		public Canvas Canvas { get; set; }

		public IGraphicsBindings GraphicsBindings
		{
			get { return this.Services.GetService<IGraphicsBindings>(); }
		}
	}
}