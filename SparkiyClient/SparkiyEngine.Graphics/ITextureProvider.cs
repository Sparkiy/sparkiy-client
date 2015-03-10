using Microsoft.Xna.Framework.Graphics;

namespace SparkiyEngine.Graphics
{
	internal interface ITextureProvider
	{
		Texture2D GetTexture(string assetName);
	}
}