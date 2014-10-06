using System;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;

namespace SharpDX.Toolkit.Direct2D.Test.CanvasStub {
    public sealed class CanvasGlyphRun : CanvasDrawing {
        public CanvasGlyphRun(Vector2 baselineOrigin, GlyphRun glyphRun,
            Brush foregroundBrush, MeasuringMode measuringMode)
            : this(baselineOrigin, glyphRun, null, foregroundBrush, measuringMode) {
        }

        public CanvasGlyphRun(Vector2 baselineOrigin, GlyphRun glyphRun, GlyphRunDescription glyphRunDescription,
            Brush foregroundBrush, MeasuringMode measuringMode) {
            BaselineOrigin = baselineOrigin;
            GlyphRun = glyphRun;
            GlyphRunDescription = glyphRunDescription;
            ForegroundBrush = foregroundBrush;
            MeasuringMode = measuringMode;
        }

        public Vector2 BaselineOrigin { get; set; }

        public GlyphRun GlyphRun { get; set; }

        public GlyphRunDescription GlyphRunDescription { get; set; }

        public Brush ForegroundBrush { get; set; }

        public MeasuringMode MeasuringMode { get; set; }

        internal override void DoWork(DeviceContext context) {
            //context.DrawGlyphRun(BaselineOrigin, GlyphRun, ForegroundBrush, MeasuringMode);
            context.DrawGlyphRun(BaselineOrigin, GlyphRun, GlyphRunDescription, ForegroundBrush, MeasuringMode);
        }

        internal override bool CanExecute() {
            throw new NotImplementedException();
        }
    }
}