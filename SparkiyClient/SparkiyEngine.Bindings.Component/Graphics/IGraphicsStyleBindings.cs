using SparkiyEngine.Bindings.Component.Common;
using SparkiyEngine.Bindings.Component.Common.Attributes;

namespace SparkiyEngine.Bindings.Component.Graphics
{
	public interface IGraphicsStyleBindings : IBindingsBase
	{
        /// <summary>
        /// Sets the color of the stroke.
        /// Color values are in range from `0` to `1` which can be directly mapped to hex values `0` to `255`.
        /// </summary>
        /// <param name="red">The red. Valid range is from `0` to `1` including those values.</param>
        /// <param name="green">The green. Valid range is from `0` to `1` including those values.</param>
        /// <param name="blue">The blue. Valid range is from `0` to `1` including those values.</param>
		[MethodDeclaration(SupportedLanguages.Lua, "stroke", MethodTypes.Set)]
        [MethodDeclarationDocumentation("/Graphics/Style/",
            "Sets or gets the color of the stroke.\r\n" +
            "\r\n" +
            "Color values are in range from `0` to `1` which can be directly mapped to hex values `0` to `255`.\r\n" +
            "\r\n" +
            "Returns three numbers cooresponding to the color values same - `red`, `green`, `blue`.")]
        [MethodDeclarationDocumentationParam("red", DataTypes.Number, "The red. Valid range is from `0` to `1` including those values.")]
        [MethodDeclarationDocumentationParam("green", DataTypes.Number, "The green. Valid range is from `0` to `1` including those values.")]
        [MethodDeclarationDocumentationParam("blue", DataTypes.Number, "The blue. Valid range is from `0` to `1` including those values.")]
        [MethodDeclarationDocumentationExample("",
            "function draw()\r\n" +
            "    stroke(0.18, 0.62, 0.78)\r\n" +
            "    rect(0, 0, 50, 50)\r\n" +
            "end")]
        void SetStrokeColor(double red, double green, double blue);

        /// <summary>
        /// Gets the color of the stroke.
        /// Color values are in range from `0` to `1` which can be directly mapped to hex values `0` to `255`.
        /// </summary>
        /// <returns>Returns three numbers cooresponding to the color values same - `red`, `green`, `blue`.</returns>
        [MethodDeclaration(SupportedLanguages.Lua, "stroke", MethodTypes.Get, new[] { "red", "green", "blue" })]
        [MethodDeclarationDocumentation("/Graphics/Style/",
            "Sets or gets the color of the stroke.\r\n" +
            "\r\n" +
            "Color values are in range from `0` to `1` which can be directly mapped to hex values `0` to `255`.\r\n" +
            "\r\n" +
            "Returns three numbers cooresponding to the color values same - `red`, `green`, `blue`.")]
        [MethodDeclarationDocumentationParam("red", DataTypes.Number, "The red. Valid range is from `0` to `1` including those values.")]
        [MethodDeclarationDocumentationParam("green", DataTypes.Number, "The green. Valid range is from `0` to `1` including those values.")]
        [MethodDeclarationDocumentationParam("blue", DataTypes.Number, "The blue. Valid range is from `0` to `1` including those values.")]
        NumberGroup3 GetStrokeColor();

		/// <summary>
		/// Sets the stroke thickness.
		/// </summary>
		/// <param name="thickness">The stroke thickness value of all following 2D shapes that have borders.</param>
		/// <remarks>
		/// Value must not be negative. Zero is valid value.
		/// 
		/// Stroke Thickness is used in all 2D shapes that have borders. The exceptions are sprites and textures.
		/// </remarks>
		[MethodDeclaration(SupportedLanguages.Lua, "strokeSize", MethodTypes.Set)]
        [MethodDeclarationDocumentation("/Graphics/Style/",
            "Sets or gets the stroke thickness.\r\n" +
            "\r\n" +
            "Value must not be negative. Zero is valid value.\r\n")]
        [MethodDeclarationDocumentationParam("thickness", DataTypes.Number, "The stroke thickness value of all following 2D shapes that have borders.")]
        [MethodDeclarationDocumentationExample("",
            "function draw()\r\n" +
            "    strokeSize(10)\r\n" +
            "    rect(0, 0, 50, 50)\r\n" +
            "end")]
        void SetStrokeThickness(double thickness);

        /// <summary>
        /// Gets the stroke thickness.
        /// </summary>
        /// <returns>Returns decimal number cooresponding to set stroke thickness.</returns>
        /// <remarks>
        /// Value will not be negative.
        /// 
        /// Stroke Thickness is used in all 2D shapes that have borders. The exceptions are sprites and textures.
        /// </remarks>
        [MethodDeclaration(SupportedLanguages.Lua, "strokeSize", MethodTypes.Get, new[] { "strokeSize" })]
        [MethodDeclarationDocumentation("/Graphics/Style/",
            "Sets or gets the stroke thickness.\r\n" +
            "\r\n" +
            "Value must not be negative. Zero is valid value.\r\n")]
        [MethodDeclarationDocumentationParam("thickness", DataTypes.Number, "The stroke thickness value of all following 2D shapes that have borders.")]
        double GetStrokeThickness();

        /// <summary>
        /// Disables the stroke of all 2D shaped that have borders.
        /// </summary>
        /// <remarks>
        /// This will be called when stroke thickness is set to zero.
        /// 
        /// To enable stroke, set stroke thickness to a value larger than zero or re-set stroke color.
        /// </remarks>
        [MethodDeclaration(SupportedLanguages.Lua, "noStroke", MethodTypes.Call)]
        [MethodDeclarationDocumentation("/Graphics/Style/", "Disables the stroke of all 2D shaped that have borders.\r\n\r\nTo enable stroke, set stroke thickness to a value larger than zero or re-set stroke color.")]
        void StrokeDisable();

		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "fill", MethodTypes.Set)]
        [MethodDeclarationDocumentation("/Graphics/Style/", "")]
        [MethodDeclarationDocumentationParam("red", DataTypes.Number, "The red. Valid range is from `0` to `1` including those values.")]
        [MethodDeclarationDocumentationParam("green", DataTypes.Number, "The green. Valid range is from `0` to `1` including those values.")]
        [MethodDeclarationDocumentationParam("blue", DataTypes.Number, "The blue. Valid range is from `0` to `1` including those values.")]
        void SetFill(double red, double green, double blue);

		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "fill", MethodTypes.Get, new []{ "red", "green", "blue" })]
        [MethodDeclarationDocumentation("/Graphics/Style/", "")]
        [MethodDeclarationDocumentationParam("red", DataTypes.Number, "The red. Valid range is from `0` to `1` including those values.")]
        [MethodDeclarationDocumentationParam("green", DataTypes.Number, "The green. Valid range is from `0` to `1` including those values.")]
        [MethodDeclarationDocumentationParam("blue", DataTypes.Number, "The blue. Valid range is from `0` to `1` including those values.")]
        NumberGroup3 GetFill();

		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "noFill", MethodTypes.Call)]
        [MethodDeclarationDocumentation("/Graphics/Style/", "")]
        void FillDisable();

        // TODO Comment
        [MethodDeclaration(SupportedLanguages.Lua, "pushStyle", MethodTypes.Call)]
        [MethodDeclarationDocumentation("/Graphics/Style/", "")]
        void PushStyle();

        // TODO Comment
        [MethodDeclaration(SupportedLanguages.Lua, "popStyle", MethodTypes.Call)]
        [MethodDeclarationDocumentation("/Graphics/Style/", "")]
        void PopStyle();

        // TODO Comment
        [MethodDeclaration(SupportedLanguages.Lua, "saveStyle", MethodTypes.Set)]
        [MethodDeclarationDocumentation("/Graphics/Style/", "")]
        [MethodDeclarationDocumentationParam("key", DataTypes.String, "key")]
        void SaveStyle(string key);

        // TODO Comment
        [MethodDeclaration(SupportedLanguages.Lua, "loadStyle", MethodTypes.Set)]
        [MethodDeclarationDocumentation("/Graphics/Style/", "")]
        [MethodDeclarationDocumentationParam("key", DataTypes.String, "key")]
        void LoadStyle(string key);

        // TODO Comment
        [MethodDeclaration(SupportedLanguages.Lua, "resetStyle", MethodTypes.Call)]
        [MethodDeclarationDocumentation("/Graphics/Style/", "")]
        void ResetStyle();
	}
}