using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparkiyEngine.Bindings.Common;
using SparkiyEngine.Bindings.Common.Attributes;

namespace SparkiyEngine.Bindings.Graphics
{
	public struct FloatGroup3
	{
		public float First;
		public float Second;
		public float Third;
	}

	public interface IGraphicsStyleBindings
	{
		/// <summary>
		/// Gets the color of the stroke.
		/// </summary>
		/// <returns>
		/// Returns group of three float numbers cooresponding to the color 
		/// values in this order: red, green, blue
		/// </returns>
		/// <remarks>
		/// Stroke is used in all 2D shapes that have borders. The obvious exceptions are sprites and textures.
		/// </remarks>
		[MethodDeclaration(SupportedLanguages.Lua, "stroke", MethodTypes.Get)]
		FloatGroup3 GetStrokeColor();

		/// <summary>
		/// Sets the color of the stroke.
		/// </summary>
		/// <param name="red">The red. Valid range is from 0 to 1 including those values.</param>
		/// <param name="green">The green. Valid range is from 0 to 1 including those values.</param>
		/// <param name="blue">The blue. Valid range is from 0 to 1 including those values.</param>
		/// <remarks>
		/// Color values are in range from 0 to 1 which can be directly mapped to hex values 0 to 255.
		/// 
		/// If provided value is out of range, it will be rounded to closest valid value. 
		/// For example, if provided value for red is 1.2, resulting color will have red of value 1.
		/// Another example would be if you provide value for red -0.5, resulting color will have red of value 0.
		/// 
		/// Stroke is used in all 2D shapes that have borders. The obvious exceptions are sprites and textures.
		/// </remarks>
		[MethodDeclaration(SupportedLanguages.Lua, "stroke", MethodTypes.Set)]
		void SetStrokeColor(float red, float green, float blue);


		//float GetStrokeThickness();

		//void SetStrokeThickness(float thickness);
	}

	public interface IGraphicsBindings : IGraphicsStyleBindings
	{
		void SetBackground(float r, float g, float b);

		void DrawRectangle(float x, float y, float w, float h);
	}
}
