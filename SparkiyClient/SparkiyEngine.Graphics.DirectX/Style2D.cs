using SharpDX;
using SharpDX.DirectWrite;
using SharpDX.Toolkit.Direct2D;

namespace SparkiyEngine.Graphics.DirectX
{
    internal class Style2D
    {
		private static readonly Color4 DefaultStrokeColor = new Color4(new Vector4(0, 0, 0, 1));
		private static readonly Color4 DefaultFillColor = new Color4(new Vector4(0, 0, 0, 1));
		private static readonly Color4 DefaultFontColor = new Color4(new Vector4(0, 0, 0, 1));


		public Style2D()
        {
			this.StrokeColor = DefaultStrokeColor;
			this.StrokeThickness = 2f;
			this.IsStrokeEnabled = false;

			this.FillColor = DefaultFillColor;
			this.IsFillEnabled = true;

			this.FontFamily = "Segoe UI";
			this.FontSize = 24f;
			this.FontColor = DefaultFontColor;
			this.FontFormat = null;
		}


        public Color4 StrokeColor { get; set; }

	    public float StrokeThickness { get; set; }

	    public bool IsStrokeEnabled { get; set; }

	    public Color4 FillColor { get; set; }

	    public bool IsFillEnabled { get; set; }

		public string FontFamily { get; set; }

		public float FontSize { get; set; }

		public TextFormat FontFormat { get; set; }

		public Color4 FontColor { get; set; }
	}
}