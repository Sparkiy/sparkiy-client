using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Media.Imaging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SparkiyEngine.Graphics.Extensions;

namespace SparkiyEngine.Graphics
{
	internal class BitmapImageTextureProvider : GameComponent, ITextureProvider
	{
		private readonly Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();


		/// <summary>
		/// Initializes a new instance of the <see cref="BitmapImageTextureProvider"/> class.
		/// </summary>
		/// <param name="game">The game.</param>
		public BitmapImageTextureProvider(Game game) : base(game)
		{
		}


		public void AddImageAsset(string name, WriteableBitmap imageAsset)
		{
			this.textures[name] = FromImage(imageAsset, this.Game.GraphicsDevice);
		}

		public Texture2D GetTexture(string assetName)
		{
			if (!this.textures.ContainsKey(assetName))
				throw new NullReferenceException("Requested asset was not loaded.");

			return this.textures[assetName];
		}

		#region Static methods

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
			Texture2D texture = new Texture2D(device, width, height, true, SurfaceFormat.Bgra32);

			texture.SetData(data);

			return texture;
		}

		#endregion /Static methods
	}
}