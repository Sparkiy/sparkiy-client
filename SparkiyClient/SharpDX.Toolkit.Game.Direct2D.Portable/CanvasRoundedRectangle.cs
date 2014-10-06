using System;
using SharpDX.Direct2D1;

namespace SharpDX.Toolkit.Direct2D.Test.CanvasStub {
    public sealed class CanvasRoundedRectangle : CanvasDrawing {
        // TODO : wrap types in constructor or in ??
        public CanvasRoundedRectangle(RoundedRectangle bounds, Brush brush, float strokeWidth = 1.0f,
            StrokeStyle strokeStyle = null) {
            Bounds = bounds;
            Brush = brush;
            StrokeWidth = strokeWidth;
            StrokeStyle = strokeStyle;
        }

        public Brush Brush { get; set; }

        public RoundedRectangle Bounds { get; set; }

        public float StrokeWidth { get; set; }

        public StrokeStyle StrokeStyle { get; set; }


        public override string ToString() {
            return string.Format("geometry: {0}, StrokeWidth: {1}", Bounds, StrokeWidth);
        }

        internal override void DoWork(DeviceContext context) {
            context.DrawRoundedRectangle(Bounds, Brush, StrokeWidth, StrokeStyle);
        }

        internal override bool CanExecute() {
            throw new NotImplementedException();
        }
    }
}