using SparkiyEngine.Bindings.Component.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkiyEngine.Graphics.DirectX
{
	public partial class GraphicsBindings : IGraphicsTransformBindings
	{
		public void SetRotation(double angle)
		{
			this.game.AddRotate((float)angle);
		}

		public void SetScale(double scale)
		{
			this.game.AddScale((float)scale);
		}

		public void SetTranslation(double x, double y)
		{
			this.game.AddTranslate((float)x, (float)y);
		}
	}
}
