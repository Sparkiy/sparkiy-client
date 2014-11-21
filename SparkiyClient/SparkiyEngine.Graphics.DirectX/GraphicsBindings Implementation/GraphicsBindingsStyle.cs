using SparkiyEngine.Bindings.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SparkiyEngine.Bindings.Common.Component;
using SharpDX;

namespace SparkiyEngine.Graphics.DirectX
{
	public partial class GraphicsBindings : IGraphicsStyleBindings
	{
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

		#region Fill

		public void SetFill(double red, double green, double blue)
		{
			this.game.FillColor = new Color4((float)red, (float)green, (float)blue, 1f);
		}

		public NumberGroup3 GetFill()
		{
			return new NumberGroup3()
			{
				First = this.game.FillColor.Red,
				Second = this.game.FillColor.Green,
				Third = this.game.FillColor.Blue
			};
		}

		#endregion Fill

		public void FillDisable()
		{
			this.game.IsFillEnabled = false;
		}
	}
}
