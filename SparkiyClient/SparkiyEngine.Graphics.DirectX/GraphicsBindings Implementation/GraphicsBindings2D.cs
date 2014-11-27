using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SparkiyEngine.Bindings.Component.Graphics;

namespace SparkiyEngine.Graphics.DirectX
{
    public partial class GraphicsBindings : IGraphics2DBindings
    {
        public void DrawTexture(string assetName, double x, double y)
        {
            this.game.DrawTexture(assetName, (float)x, (float)y, -1, -1);
        }

        public void DrawTexture(string assetName, double x, double y, double width, double height)
        {
            this.game.DrawTexture(assetName, (float)x, (float)y, (float)width, (float)height);
        }
    }
}
