using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparkiyEngine.Bindings.Common;
using SparkiyEngine.Bindings.Common.Attributes;
using SparkiyEngine.Bindings.Common.Component;
using SparkiyEngine.Bindings.Graphics.Component;

namespace SparkiyEngine.Bindings.Graphics
{
	public interface IGraphicsBindings : IGraphicsStyleBindings
	{
		[MethodDeclaration(SupportedLanguages.Lua, "background", MethodTypes.Set)]
		void SetBackground(double red, double green, double blue);

		[MethodDeclaration(SupportedLanguages.Lua, "rect", MethodTypes.Set)]
		void DrawRectangle(double x, double y, double width, double height);

		/// <summary>
		/// Resets this instance.
		/// </summary>
		void Reset();

		/// <summary>
		/// Occurs before 2D draw is called so that user can fill collection with drawable objects
		/// </summary>
		event MethodCallRequestEventHandler Pre2DDraw;

		/// <summary>
		/// Triggers the Pre2DDraw event.
		/// </summary>
		void TriggerPre2DDraw();
	}
}
