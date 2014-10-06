using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparkiyEngine.Bindings.Graphics
{
    public interface IGraphicsBindings
    {
	    void SetBackground(float r, float g, float b);

	    void DrawRectangle(int x, int y, int w, int h);
    }
}
