using System;
using SharpDX.Direct2D1;

namespace SharpDX.Toolkit.Direct2D.Test.CanvasStub {
    public sealed class CanvasGeometry : CanvasDrawing {
        public CanvasGeometry(Geometry geometry, Brush brush, float strokeWidth = 1.0f, StrokeStyle strokeStyle = null) {
            Geometry = geometry;
            Brush = brush;
            StrokeWidth = strokeWidth;
            StrokeStyle = strokeStyle;
        }

        public Brush Brush { get; set; }

        public Geometry Geometry { get; set; }

        public float StrokeWidth { get; set; }

        public StrokeStyle StrokeStyle { get; set; }

        public override string ToString() {
            return string.Format("Geometry: {0}, StrokeWidth: {1}", Geometry, StrokeWidth);
        }

        internal override void DoWork(DeviceContext context) {
            context.DrawGeometry(Geometry, Brush, StrokeWidth, StrokeStyle);
        }

        internal override bool CanExecute() {
            throw new NotImplementedException();
        }
    }
}