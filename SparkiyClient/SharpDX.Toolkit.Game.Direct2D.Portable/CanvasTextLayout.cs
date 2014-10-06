using System;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;

namespace SharpDX.Toolkit.Direct2D.Test.CanvasStub {
    public sealed class CanvasTextLayout : CanvasDrawing
    {
       


        internal override void DoWork(DeviceContext context) {
            context.DrawTextLayout(Origin,TextLayout,DefaultForegroundBrush,Options);

        }

        public CanvasTextLayout(Vector2 origin, TextLayout textLayout, Brush defaultForegroundBrush, DrawTextOptions options) {
            this.Origin = origin;
            this.TextLayout = textLayout;
            this.DefaultForegroundBrush = defaultForegroundBrush;
            this.Options = options;
        }

        public Brush DefaultForegroundBrush { get; set; }

        public TextLayout TextLayout { get; set; }

        public DrawTextOptions Options { get; set; }

        public Vector2 Origin { get; set; }

        internal override bool CanExecute() {
            throw new NotImplementedException();
        }
    }
}