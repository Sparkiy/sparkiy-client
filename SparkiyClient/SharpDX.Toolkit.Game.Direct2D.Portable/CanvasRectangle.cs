using System;
using SharpDX.Direct2D1;

namespace SharpDX.Toolkit.Direct2D.Test.CanvasStub {
    public sealed class CanvasRectangle : CanvasDrawing {
        public CanvasRectangle(RectangleF bounds, Brush brush, bool fill = false, float strokeWidth = 1.0f,
            StrokeStyle strokeStyle = null) {
            Bounds = bounds;
            Brush = brush;
            Fill = fill;
            StrokeWidth = strokeWidth;
            StrokeStyle = strokeStyle;
        }

        public Brush Brush { get; set; }

        public bool Fill { get; set; }

        public RectangleF Bounds { get; set; }

        public float StrokeWidth { get; set; }

        public StrokeStyle StrokeStyle { get; set; }

        public override string ToString() {
            return string.Format("geometry: {0}, StrokeWidth: {1}", Bounds, StrokeWidth);
        }

        internal override void DoWork(DeviceContext context) {
            if (Fill) {
                context.FillRectangle(Bounds, Brush);
            }
            else {
                context.DrawRectangle(Bounds, Brush, StrokeWidth, StrokeStyle);
            }
        }

        internal override bool CanExecute() {
            throw new NotImplementedException();
        }
    }
}