using SharpDX;
using SparkiyEngine.Bindings.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkiyEngine.Graphics.DirectX
{
	public partial class GraphicsBindings : IGraphicsSurfaceBindings
	{
		public void SetBackground(double red, double green, double blue)
		{
			this.game.BackgroundColor = new Color4((float)red, (float)green, (float)blue, 1f);
		}
	}
}
