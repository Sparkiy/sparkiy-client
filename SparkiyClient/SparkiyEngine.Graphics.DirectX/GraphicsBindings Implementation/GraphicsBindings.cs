using SharpDX;
using SparkiyEngine.Bindings.Graphics;
using SparkiyEngine.Bindings.Graphics.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkiyEngine.Graphics.DirectX
{
	/// <summary>
	/// Graphics Bindings implementation for SparkiyGame game instance
	/// </summary>
	public partial class GraphicsBindings : Component, IGraphicsBindings
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
}
