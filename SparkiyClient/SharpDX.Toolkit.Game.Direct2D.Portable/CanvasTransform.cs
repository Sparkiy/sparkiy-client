using System;
using SharpDX.Direct2D1;

namespace SharpDX.Toolkit.Direct2D.Test.CanvasStub {
    public sealed class CanvasTransform : CanvasObject {
        public CanvasTransform(Matrix3x2 transform) {
            Transform = transform;
        }

        public Matrix3x2 Transform { get; set; }

        internal override void DoWork(DeviceContext context) {
            context.Transform = Transform;
        }

        internal override bool CanExecute() {
            throw new NotImplementedException();
        }

        public override string ToString() {
            return string.Format("Transform: {0}", Transform);
        }
    }
}