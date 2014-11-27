using SharpDX.Toolkit.Graphics;

namespace SparkiyEngine.Graphics.DirectX
{
    internal interface ITextureProvider
    {
        Texture2D GetTexture(string assetName);
    }
}