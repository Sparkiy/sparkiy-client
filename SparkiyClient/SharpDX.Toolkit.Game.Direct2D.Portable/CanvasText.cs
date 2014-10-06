using System;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;

namespace SharpDX.Toolkit.Direct2D.Test.CanvasStub {
    public sealed class CanvasText : CanvasDrawing
    {
        public CanvasText(string text, TextFormat textFormat, RectangleF layoutRect, Brush defaultForegroundBrush,
            DrawTextOptions options, MeasuringMode measuringMode) {
            Text = text;
            TextFormat = textFormat;
            LayoutRect = layoutRect;
            DefaultForegroundBrush = defaultForegroundBrush;
            Options = options;
            MeasuringMode = measuringMode;
        }

        public MeasuringMode MeasuringMode { get; set; }

        public DrawTextOptions Options { get; set; }

        public Brush DefaultForegroundBrush { get; set; }

        public RectangleF LayoutRect { get; set; }

        public TextFormat TextFormat { get; set; }

        public string Text { get; set; }

        internal override void DoWork(DeviceContext context) {
            //context.DrawGlyphRun(BaselineOrigin, GlyphRun, ForegroundBrush, MeasuringMode);
            context.DrawText(Text, TextFormat, LayoutRect, DefaultForegroundBrush, Options, MeasuringMode);
        }

        internal override bool CanExecute() {
            throw new NotImplementedException();
        }
    }
}