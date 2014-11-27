using SparkiyEngine.Bindings.Component.Common;
using SparkiyEngine.Bindings.Component.Common.Attributes;

namespace SparkiyEngine.Bindings.Component.Graphics
{
	public interface IGraphicsStyleBindings : IBindingsBase
	{
		/// <summary>
		/// Gets the color of the stroke.
		/// </summary>
		/// <returns>
		/// Returns group of three decimal numbers cooresponding to the color 
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

		/// <summary>
		/// Gets the stroke thickness.
		/// </summary>
		/// <returns>Returns decimal number cooresponding to set stroke thickness.</returns>
		/// <remarks>
		/// Value will not be negative.
		/// 
		/// Stroke Thickness is used in all 2D shapes that have borders. The obvious exceptions are sprites and textures.
		/// </remarks>
		[MethodDeclaration(SupportedLanguages.Lua, "strokeSize", MethodTypes.Get)]
		float GetStrokeThickness();

		/// <summary>
		/// Gets the stroke thickness.
		/// </summary>
		/// <param name="thickness">The stroke thickness value of all following 2D shapes that have borders.</param>
		/// <remarks>
		/// Value must not be negative. Zero is valid value.
		/// 
		/// Stroke Thickness is used in all 2D shapes that have borders. The obvious exceptions are sprites and textures.
		/// </remarks>
		[MethodDeclaration(SupportedLanguages.Lua, "strokeSize", MethodTypes.Set)]
		void SetStrokeThickness(double thickness);

		/// <summary>
		/// Disables the stroke of all 2D shaped that have borders.
		/// </summary>
		/// <remarks>
		/// This will be called when stroke thickness is set to zero.
		/// 
		/// To enable stroke, set stroke thickness to a value larger than zero or re-set stroke color.
		/// </remarks>
		[MethodDeclaration(SupportedLanguages.Lua, "noStroke", MethodTypes.Call)]
		void StrokeDisable();

		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "fill", MethodTypes.Set)]
		void SetFill(double red, double green, double blue);

		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "fill", MethodTypes.Get)]
		NumberGroup3 GetFill();

		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "noFill", MethodTypes.Call)]
		void FillDisable();

        // TODO Comment
        [MethodDeclaration(SupportedLanguages.Lua, "pushStyle", MethodTypes.Call)]
        void PushStyle();

        // TODO Comment
        [MethodDeclaration(SupportedLanguages.Lua, "popStyle", MethodTypes.Call)]
        void PopStyle();

        // TODO Comment
        [MethodDeclaration(SupportedLanguages.Lua, "saveStyle", MethodTypes.Set)]
        void SaveStyle(string key);

        // TODO Comment
        [MethodDeclaration(SupportedLanguages.Lua, "loadStyle", MethodTypes.Set)]
        void LoadStyle(string key);

        // TODO Comment
        [MethodDeclaration(SupportedLanguages.Lua, "resetStyle", MethodTypes.Call)]
        void ResetStyle();
	}
}