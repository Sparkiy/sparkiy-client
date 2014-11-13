using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparkiyEngine.Bindings.Common;
using SparkiyEngine.Bindings.Common.Attributes;
using SparkiyEngine.Bindings.Common.Component;

namespace SparkiyEngine.Bindings.Graphics
{
	public interface IGraphicsBindings : IGraphicsStyleBindings
	{
		[MethodDeclaration(SupportedLanguages.Lua, "background", MethodTypes.Set)]
		void SetBackground(double red, double green, double blue);

		void DrawRectangle(double x, double y, double width, double height);

		void Reset();
	}
}
