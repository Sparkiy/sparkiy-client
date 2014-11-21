using SparkiyEngine.Bindings.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkiyEngine.Graphics.DirectX
{
	public partial class GraphicsBindings : IGraphicsShapesBindings
	{
		public void DrawCircle(double x, double y, double radius)
		{
			this.game.DrawEllipse((float)x, (float)y, (float)radius, (float)radius);
		}

		public void DrawEllipse(double x, double y, double majorRadius, double minorRadius)
		{
			this.game.DrawEllipse((float)x, (float)y, (float)majorRadius, (float)minorRadius);
		}

		public void DrawRectangle(double x, double y, double width, double height)
		{
			this.game.DrawRectangle((float)x, (float)y, (float)width, (float)height);
		}

		public void DrawSquare(double x, double y, double size)
		{
			this.game.DrawRectangle((float)x, (float)y, (float)size, (float)size);
		}

		public void DrawLine(double x1, double y1, double x2, double y2)
		{
			this.game.DrawLine((float)x1, (float)y1, (float)x2, (float)y2);
		}
	}
}
