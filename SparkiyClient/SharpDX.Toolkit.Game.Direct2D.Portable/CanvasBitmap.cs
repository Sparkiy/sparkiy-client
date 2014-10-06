using System;
using SharpDX.Direct2D1;
using SharpDX.DXGI;
using SharpDX.Toolkit.Graphics;

namespace SharpDX.Toolkit.Direct2D.Test.CanvasStub {
    public sealed class CanvasBitmap : CanvasDrawing {
        private readonly Texture2D _texture2D;
        private Bitmap1 _bitmap1;

        public CanvasBitmap(Texture2D texture2D) {
            _texture2D = texture2D;
        }

        internal override void Initialize(DeviceContext context) {
            var texture2D = (Direct3D11.Texture2D) _texture2D;
            var surface = texture2D.QueryInterface<Surface>();
            _bitmap1 = ToDispose(new Bitmap1(context, surface));
            base.Initialize(context);
        }

        internal override void DoWork(DeviceContext context) {
            context.DrawBitmap(_bitmap1, 1.0f, BitmapInterpolationMode.NearestNeighbor);
        }

        internal override bool CanExecute() {
            throw new NotImplementedException();
        }

        protected override void Dispose(bool disposeManagedResources) {
            if (disposeManagedResources) {
                RemoveAndDispose(ref _bitmap1);
            }
            base.Dispose(disposeManagedResources);
        }
    }
}