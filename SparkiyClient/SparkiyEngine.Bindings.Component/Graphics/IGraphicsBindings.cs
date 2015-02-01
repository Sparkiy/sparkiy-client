using SparkiyEngine.Bindings.Component.Common;
using SparkiyEngine.Bindings.Component.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.UI.Xaml.Media.Imaging;

namespace SparkiyEngine.Bindings.Component.Graphics
{
    public interface IGraphics2DBindings
    {
        [MethodDeclaration(SupportedLanguages.Lua, "texture", MethodTypes.Set)]
        [MethodDeclarationDocumentation("/Graphics/Shapes/", "")]
        [MethodDeclarationDocumentationParam("assetName", DataTypes.Number, "Asset name")]
        [MethodDeclarationDocumentationParam("x", DataTypes.Number, "Horizontal offset")]
        [MethodDeclarationDocumentationParam("y", DataTypes.Number, "Vertical offset")]
        void DrawTexture(string assetName, double x, double y);

        [MethodDeclaration(SupportedLanguages.Lua, "texture", MethodTypes.Set)]
        [MethodDeclarationDocumentation("/Graphics/Shapes/", "")]
        [MethodDeclarationDocumentationParam("assetName", DataTypes.Number, "Asset name")]
        [MethodDeclarationDocumentationParam("x", DataTypes.Number, "Horizontal offset")]
        [MethodDeclarationDocumentationParam("y", DataTypes.Number, "Vertical offset")]
        [MethodDeclarationDocumentationParam("width", DataTypes.Number, "Width")]
        [MethodDeclarationDocumentationParam("height", DataTypes.Number, "Height")]
        void DrawTexture(string assetName, double x, double y, double width, double height);
    }

	public interface IGraphicsShapesBindings
	{
		[MethodDeclaration(SupportedLanguages.Lua, "rect", MethodTypes.Set)]
        [MethodDeclarationDocumentation("/Graphics/Shapes/", "Draws a rectangle with its lower-left corner positioned at `x`, `y` and sized at `width`, `height`.")]
        [MethodDeclarationDocumentationParam("x", DataTypes.Number, "Horizontal offset")]
        [MethodDeclarationDocumentationParam("y", DataTypes.Number, "Vertical offset")]
        [MethodDeclarationDocumentationParam("width", DataTypes.Number, "Width")]
        [MethodDeclarationDocumentationParam("height", DataTypes.Number, "Height")]
        [MethodDeclarationDocumentationExample("",
            "function Draw()\r\n" +
            "    rect(0, 0, 50, 50)\r\n" +
            "end")]
        void DrawRectangle(double x, double y, double width, double height);

		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "square", MethodTypes.Set)]
        [MethodDeclarationDocumentation("/Graphics/Shapes/", "")]
        [MethodDeclarationDocumentationParam("x", DataTypes.Number, "Horizontal offset")]
        [MethodDeclarationDocumentationParam("y", DataTypes.Number, "Vertical offset")]
        [MethodDeclarationDocumentationParam("size", DataTypes.Number, "Size")]
        void DrawSquare(double x, double y, double size);

		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "ellipse", MethodTypes.Set)]
        [MethodDeclarationDocumentation("/Graphics/Shapes/", "")]
        [MethodDeclarationDocumentationParam("x", DataTypes.Number, "Horizontal offset")]
        [MethodDeclarationDocumentationParam("y", DataTypes.Number, "Vertical offset")]
        [MethodDeclarationDocumentationParam("majorRadius", DataTypes.Number, "Major radius")]
        [MethodDeclarationDocumentationParam("minorRadius", DataTypes.Number, "Minor radius")]
        void DrawEllipse(double x, double y, double majorRadius, double minorRadius);

		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "circle", MethodTypes.Set)]
        [MethodDeclarationDocumentation("/Graphics/Shapes/", "")]
        [MethodDeclarationDocumentationParam("x", DataTypes.Number, "Horizontal offset")]
        [MethodDeclarationDocumentationParam("y", DataTypes.Number, "Vertical offset")]
        [MethodDeclarationDocumentationParam("radius", DataTypes.Number, "Radius")]
        void DrawCircle(double x, double y, double radius);

		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "line", MethodTypes.Set)]
        [MethodDeclarationDocumentation("/Graphics/Shapes/", "")]
        [MethodDeclarationDocumentationParam("x1", DataTypes.Number, "Start point horizontal offset")]
        [MethodDeclarationDocumentationParam("y1", DataTypes.Number, "Start point vertical offset")]
        [MethodDeclarationDocumentationParam("x2", DataTypes.Number, "End point horizontal offset")]
        [MethodDeclarationDocumentationParam("y2", DataTypes.Number, "End point vertical offset")]
        void DrawLine(double x1, double y1, double x2, double y2);

		// TODO DrawPath which accepts normal path string
		// TODO DrawGroup which accepts string with shapes eg. R x y w h C x y r ...
	}

	public interface IGraphicsSurfaceBindings
	{
		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "background", MethodTypes.Set)]
        [MethodDeclarationDocumentation("/Graphics/Screen/", "")]
        [MethodDeclarationDocumentationParam("red", DataTypes.Number, "The red. Valid range is from `0` to `1` including those values.")]
        [MethodDeclarationDocumentationParam("green", DataTypes.Number, "The green. Valid range is from `0` to `1` including those values.")]
        [MethodDeclarationDocumentationParam("blue", DataTypes.Number, "The blue. Valid range is from `0` to `1` including those values.")]
        void SetBackground(double red, double green, double blue);
	}

	public interface IGraphicsTextBindings
	{
		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "font", MethodTypes.Set)]
        [MethodDeclarationDocumentation("/Graphics/Text/", "")]
        [MethodDeclarationDocumentationParam("fontFamily", DataTypes.String, "Font family")]
        void SetFont(string fontFamily);

		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "font", MethodTypes.Get, new []{ "fontFamily" })]
        [MethodDeclarationDocumentation("/Graphics/Text/", "")]
        [MethodDeclarationDocumentationParam("fontFamily", DataTypes.String, "Font family")]
        string GetFont();

		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "fontSize", MethodTypes.Set)]
        [MethodDeclarationDocumentation("/Graphics/Text/", "")]
        [MethodDeclarationDocumentationParam("fontSize", DataTypes.Number, "Font size")]
        void SetFontSize(double fontSize);

		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "fontSize", MethodTypes.Get, new[] { "fontSize" })]
        [MethodDeclarationDocumentation("/Graphics/Text/", "")]
        [MethodDeclarationDocumentationParam("fontSize", DataTypes.Number, "Font size")]
        double GetFontSize();

		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "fontColor", MethodTypes.Set)]
        [MethodDeclarationDocumentation("/Graphics/Text/", "")]
        [MethodDeclarationDocumentationParam("red", DataTypes.Number, "The red. Valid range is from `0` to `1` including those values.")]
        [MethodDeclarationDocumentationParam("green", DataTypes.Number, "The green. Valid range is from `0` to `1` including those values.")]
        [MethodDeclarationDocumentationParam("blue", DataTypes.Number, "The blue. Valid range is from `0` to `1` including those values.")]
        void SetFontColor(double red, double green, double blue);

		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "fontColor", MethodTypes.Get, new[] { "red", "green", "blue" })]
        [MethodDeclarationDocumentation("/Graphics/Text/", "")]
        NumberGroup3 GetFontColor();

		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "text", MethodTypes.Set)]
        [MethodDeclarationDocumentation("/Graphics/Text/", "")]
        [MethodDeclarationDocumentationParam("text", DataTypes.Number, "Text")]
        void DrawText(string text, double x, double y);
	}

	public interface IGraphicsTransformBindings
	{
		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "translate", MethodTypes.Set)]
        [MethodDeclarationDocumentation("/Graphics/Transforms/", "")]
        [MethodDeclarationDocumentationParam("x", DataTypes.Number, "Horizontal offset")]
        [MethodDeclarationDocumentationParam("y", DataTypes.Number, "Vertical offset")]
        void SetTranslation(double x, double y);

		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "rotate", MethodTypes.Set)]
        [MethodDeclarationDocumentation("/Graphics/Transforms/", "")]
        [MethodDeclarationDocumentationParam("angle", DataTypes.Number, "Angle")]
        void SetRotation(double angle);

		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "scale", MethodTypes.Set)]
        [MethodDeclarationDocumentation("/Graphics/Transforms/", "")]
        [MethodDeclarationDocumentationParam("scale", DataTypes.Number, "Scale")]
        void SetScale(double scale);

		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "pushTransform", MethodTypes.Call)]
        [MethodDeclarationDocumentation("/Graphics/Transforms/", "")]
        void PushTransform();

		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "popTransform", MethodTypes.Call)]
        [MethodDeclarationDocumentation("/Graphics/Transforms/", "")]
        void PopTransform();

		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "saveTransform", MethodTypes.Set)]
        [MethodDeclarationDocumentation("/Graphics/Transforms/", "")]
        [MethodDeclarationDocumentationParam("key", DataTypes.String, "key")]
        void SaveTransform(string key);

		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "loadTransform", MethodTypes.Set)]
        [MethodDeclarationDocumentation("/Graphics/Transforms/", "")]
        [MethodDeclarationDocumentationParam("key", DataTypes.String, "key")]
        void LoadTransform(string key);

		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "resetTransform", MethodTypes.Call)]
        [MethodDeclarationDocumentation("/Graphics/Transforms/", "")]
        void ResetTransform();
	}

	public interface IGraphicsBindings : IBindingsBase, IGraphicsStyleBindings, IGraphicsShapesBindings, IGraphics2DBindings, IGraphicsSurfaceBindings, IGraphicsTransformBindings
	{
		/// <summary>
		/// Resets this instance.
		/// </summary>
		void Reset();

        /// <summary>
        /// Plays this instance.
        /// </summary>
        void Play();

        /// <summary>
        /// Pauses this instance.
        /// </summary>
        void Pause();

		/// <summary>
		/// Stops this instance.
		/// This will dispose all resources.
		/// </summary>
		void Stop();

	    void AddImageAsset(string name, WriteableBitmap imageAsset);
	}
}
