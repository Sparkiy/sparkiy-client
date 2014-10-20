using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparkiyEngine.Bindings.Common;
using SparkiyEngine.Bindings.Common.Attributes;
using SparkiyEngine.Bindings.Common.Component;

namespace SparkiyEngine.Bindings.Graphics
{
	public interface IGraphicsStyleBindings : IBindingsBase
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
		NumberGroup3 GetStrokeColor();

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
		void SetStrokeColor(double red, double green, double blue);


		//float GetStrokeThickness();

		//void SetStrokeThickness(float thickness);
	}

	public interface IGraphicsBindings : IGraphicsStyleBindings
	{
		[MethodDeclaration(SupportedLanguages.Lua, "background", MethodTypes.Set)]
		void SetBackground(double red, double green, double blue);

		void DrawRectangle(double x, double y, double width, double height);
	}
}
