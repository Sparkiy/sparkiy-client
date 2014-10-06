using System;
using Windows.Foundation;
using SharpDX.Direct2D1;
using SharpDX.DXGI;
using SharpDX.Toolkit.Graphics;
using PixelFormat = SharpDX.Toolkit.Graphics.PixelFormat;

namespace SharpDX.Toolkit.Direct2D.Test.CanvasStub {
    public sealed class CanvasBitmap : CanvasDrawing {
        private readonly Texture2D texture2D;
		private readonly RectangleF destination;
        private Bitmap1 bitmap1;

		public CanvasBitmap(Texture2D texture2D, RectangleF destination)
		{
            this.texture2D = texture2D;
			this.destination = destination;
		}

        internal override void Initialize(DeviceContext context) {
            var d3D11Texture2D = (Direct3D11.Texture2D) this.texture2D;
			var surface = d3D11Texture2D.QueryInterface<Surface>();
			bitmap1 = ToDispose(new Bitmap1(context, surface));
            base.Initialize(context);
        }

        internal override void DoWork(DeviceContext context) {
            context.DrawBitmap(bitmap1, this.destination, 1.0f, BitmapInterpolationMode.NearestNeighbor);
        }

        internal override bool CanExecute() {
            throw new NotImplementedException();
        }

        protected override void Dispose(bool disposeManagedResources) {
            if (disposeManagedResources) {
                RemoveAndDispose(ref bitmap1);
            }
            base.Dispose(disposeManagedResources);
        }
    }
}