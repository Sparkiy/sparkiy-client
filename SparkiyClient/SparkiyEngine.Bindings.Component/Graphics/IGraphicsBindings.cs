using SparkiyEngine.Bindings.Component.Common;
using SparkiyEngine.Bindings.Component.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparkiyEngine.Bindings.Component.Graphics
{
	public interface IGraphicsShapesBindings
	{
		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "rectangle", MethodTypes.Set)]
		[MethodDeclaration(SupportedLanguages.Lua, "rect", MethodTypes.Set)]
		void DrawRectangle(double x, double y, double width, double height);

		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "square", MethodTypes.Set)]
		void DrawSquare(double x, double y, double size);

		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "ellipse", MethodTypes.Set)]
		void DrawEllipse(double x, double y, double majorRadius, double minorRadius);

		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "circle", MethodTypes.Set)]
		void DrawCircle(double x, double y, double radius);

		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "line", MethodTypes.Set)]
		void DrawLine(double x1, double y1, double x2, double y2);

		// TODO DrawPath which accepts normal path string
		// TODO DrawGroup which accepts string with shapes eg. R x y w h C x y r ...
	}

	public interface IGraphicsSurfaceBindings
	{
		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "background", MethodTypes.Set)]
		void SetBackground(double red, double green, double blue);
	}

	public interface IGraphicsTextBindings
	{
		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "font", MethodTypes.Set)]
		void SetFont(string fontFamily);

		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "font", MethodTypes.Get)]
		string GetFont();

		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "fontSize", MethodTypes.Set)]
		void SetFontSize(double fontSize);

		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "fontSize", MethodTypes.Get)]
		double GetFontSize();

		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "fontColor", MethodTypes.Set)]
		void SetFontColor(double red, double green, double blue);

		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "fontColor", MethodTypes.Get)]
		NumberGroup3 GetFontColor();

		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "text", MethodTypes.Set)]
		void DrawText(string text, double x, double y);
	}

	public interface IGraphicsTransformBindings
	{
		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "translate", MethodTypes.Set)]
		void SetTranslation(double x, double y);

		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "rotate", MethodTypes.Set)]
		void SetRotation(double angle);

		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "scale", MethodTypes.Set)]
		void SetScale(double scale);

		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "pushTransform", MethodTypes.Set)]
		void PushTransform();

		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "popTransform", MethodTypes.Set)]
		void PopTransform();

		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "saveTransform", MethodTypes.Set)]
		void SaveTransform(string key);

		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "loadTransform", MethodTypes.Set)]
		void LoadTransform(string key);

		// TODO Comment
		[MethodDeclaration(SupportedLanguages.Lua, "resetTransform", MethodTypes.Set)]
		void ResetTransform();
	}

	public interface IGraphicsBindings : IBindingsBase, IGraphicsStyleBindings, IGraphicsShapesBindings, IGraphicsSurfaceBindings, IGraphicsTransformBindings
	{
		/// <summary>
		/// Resets this instance.
		/// </summary>
		void Reset();
	}
}
