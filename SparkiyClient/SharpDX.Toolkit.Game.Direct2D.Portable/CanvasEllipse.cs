using System;
using SharpDX.Direct2D1;

namespace SharpDX.Toolkit.Direct2D.Test.CanvasStub {
    public sealed class CanvasEllipse : CanvasDrawing {
        public CanvasEllipse(Ellipse ellipse, Brush brush, bool fill = false, float strokeWidth = 1.0f,
            StrokeStyle strokeStyle = null) {
            Ellipse = ellipse;
            Brush = brush;
            Fill = fill;
            StrokeWidth = strokeWidth;
            StrokeStyle = strokeStyle;
        }

        public Brush Brush { get; set; }

        public bool Fill { get; set; }

        public Ellipse Ellipse { get; set; }

        public float StrokeWidth { get; set; }

        public StrokeStyle StrokeStyle { get; set; }

        public override string ToString() {
            return string.Format("geometry: {0}, StrokeWidth: {1}", Ellipse, StrokeWidth);
        }

        internal override void DoWork(DeviceContext context) {
            if (Fill) {
                context.FillEllipse(Ellipse, Brush);
            }
            else {
                context.DrawEllipse(Ellipse, Brush, StrokeWidth, StrokeStyle);
            }
        }

        internal override bool CanExecute() {
            throw new NotImplementedException();
        }
    }
}