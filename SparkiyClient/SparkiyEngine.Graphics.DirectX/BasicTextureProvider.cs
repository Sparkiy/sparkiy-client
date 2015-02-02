using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml.Media.Imaging;
using SharpDX.DXGI;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;
using WinRTXamlToolkit.Imaging;

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

    internal class BitmapImageTextureProvider : ITextureProvider
    {
        private readonly Game game;
        private readonly Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();


        public BitmapImageTextureProvider(Game game)
        {
            this.game = game;
        }


        public void AddImageAsset(string name, WriteableBitmap imageAsset)
        {
            this.textures[name] = FromImage(imageAsset, this.game.GraphicsDevice);
        }

        public Texture2D GetTexture(string assetName)
        {
            if (!this.textures.ContainsKey(assetName))
                throw new NullReferenceException("Requested asset was not loaded.");

            return this.textures[assetName];
        }


        public static Texture2D FromImage(WriteableBitmap image, GraphicsDevice device)
        {
            var imageData = image.PixelBuffer.GetPixels();
            int[] pixels = new int[imageData.Bytes.Length / 4];
            for (int index = 0; index < pixels.Length; index++)
                pixels[index] = imageData[index];

            return Texture2DFromImageData(pixels, image.PixelWidth, image.PixelHeight, device);
        }

        public static Texture2D Texture2DFromImageData(int[] data, int width, int height, GraphicsDevice device)
        {
            Texture2D texture = Texture2D.New(device, width, height, PixelFormat.B8G8R8A8.UNorm);

            texture.SetData(data);

            return texture;
        }
    }
}