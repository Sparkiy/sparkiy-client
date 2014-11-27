using System.Collections.Generic;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;

namespace SparkiyEngine.Graphics.DirectX
{
    internal class BasicTextureProvider : ITextureProvider
    {
        private readonly Game game;
        private readonly Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();


        public BasicTextureProvider(Game game)
        {
            this.game = game;
        }


        public Texture2D GetTexture(string assetName)
        {
            if (!this.textures.ContainsKey(assetName))
                this.LoadTexture(assetName);
            return this.textures[assetName];
        }

        private void LoadTexture(string assetName)
        {
            this.textures[assetName] = this.game.Content.Load<Texture2D>(assetName);
        }
    }
}