using SparkiyEngine.Bindings.Component.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SparkiyEngine.Bindings.Component.Common;
using SharpDX;

namespace SparkiyEngine.Graphics.DirectX
{
	public partial class GraphicsBindings : IGraphicsTextBindings
	{
		public void DrawText(string text, double x, double y)
		{
			this.game.DrawText(text, (float)x, (float)y);
		}

		public string GetFont()
		{
			return this.game.FontFamily;
		}

		public NumberGroup3 GetFontColor()
		{
			return new NumberGroup3()
			{
				First = this.game.FontColor.Red,
				Second = this.game.FontColor.Green,
				Third = this.game.FontColor.Blue
			};
		}

		public double GetFontSize()
		{
			return this.game.FontSize;
		}

		public void SetFont(string fontFamily)
		{
			this.game.FontFamily = fontFamily;
		}

		public void SetFontColor(double red, double green, double blue)
		{
			this.game.FontColor = new Color4((float)red, (float)green, (float)blue, 1f);
		}

		public void SetFontSize(double fontSize)
		{
			this.game.FontSize = (float)fontSize;
		}
	}
}
