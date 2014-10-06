using SharpDX.Direct2D1;

namespace SharpDX.Toolkit.Direct2D.Test.CanvasStub {
    public abstract class CanvasObject : Component {
        public bool IsInitialized { get; private set; }
        internal abstract void DoWork(DeviceContext context);
        internal abstract bool CanExecute();

        internal virtual void Initialize(DeviceContext context) {
            IsInitialized = true;
        }
    }
}