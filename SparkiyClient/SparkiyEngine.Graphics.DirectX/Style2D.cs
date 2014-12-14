using SharpDX;
using SharpDX.Toolkit.Direct2D;

namespace SparkiyEngine.Graphics.DirectX
{
    internal struct Style2D
    {
        private Color4 strokeColor;
        private float strokeThickness;
        private bool isStrokeEnabled;
        private Color4 fillColor;
        private bool isFillEnabled;


        public Style2D()
        {
            this.strokeColor = Brushes.White.Color;
            this.strokeThickness = 2f;
            this.isStrokeEnabled = false;

            this.fillColor = Brushes.White.Color;
            this.isFillEnabled = true;
        }


        public Color4 StrokeColor
        {
            get { return strokeColor; }
            set { strokeColor = value; }
        }

        public float StrokeThickness
        {
            get { return strokeThickness; }
            set { strokeThickness = value; }
        }

        public bool IsStrokeEnabled
        {
            get { return isStrokeEnabled; }
            set { isStrokeEnabled = value; }
        }

        public Color4 FillColor
        {
            get { return fillColor; }
            set { fillColor = value; }
        }

        public bool IsFillEnabled
        {
            get { return isFillEnabled; }
            set { isFillEnabled = value; }
        }
    }
}