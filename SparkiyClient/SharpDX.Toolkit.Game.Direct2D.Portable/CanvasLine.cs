using System;
using SharpDX.Direct2D1;

namespace SharpDX.Toolkit.Direct2D.Test.CanvasStub {
    public sealed class CanvasLine : CanvasDrawing {
        // TODO abstract base that has stroke and brush ??

        public CanvasLine(Vector2 point0, Vector2 point1, Brush brush, float strokeWidth = 1.0f,
            StrokeStyle strokeStyle = null) {
            Point0 = point0;
            Point1 = point1;
            Brush = brush;
            StrokeWidth = strokeWidth;
            StrokeStyle = strokeStyle;
        }

        public float StrokeWidth { get; set; }

        public StrokeStyle StrokeStyle { get; set; }

        public Brush Brush { get; set; }

        public Vector2 Point1 { get; set; }

        public Vector2 Point0 { get; set; }


        internal override void DoWork(DeviceContext context) {
            context.DrawLine(Point0, Point1, Brush, StrokeWidth, StrokeStyle);
        }

        internal override bool CanExecute() {
            throw new NotImplementedException();
        }
    }
}