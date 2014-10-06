// TODO : unsolved stuff
// Out of memory
// Drawing before breaks 3D
// ToDispose vs ToDisposeContent
// Dispose(bool) usage | bitmap1
// Encompass drawing inside a transform ?
// Implement append drawing

using System;
using System.Collections.Generic;
using SharpDX.Direct2D1;
using SharpDX.Toolkit.Graphics;

namespace SharpDX.Toolkit.Direct2D.Test.CanvasStub {
	/// <summary>
	/// Provides Direct2D support for drawing on D3D11.1 SwapChain
	/// </summary>
	public interface IDirect2DService : IDisposable
	{
		/// <summary>
		/// Gets a reference to the Direct2D device. Can be used to create additional <see cref="DeviceContext"/>.
		/// </summary>
		Device Device { get; }

		/// <summary>
		/// Gets a reference to the default <see cref="DeviceContext"/> which will draw directly over SwapChain.
		/// The developer is responsible to restore default render target states.
		/// </summary>
		DeviceContext Context { get; }

		/// <summary>
		/// Gets a reference to the default <see cref="SharpDX.DirectWrite.Factory1"/> used to create all DirectWrite objects.
		/// </summary>
		SharpDX.DirectWrite.Factory1 DwFactory { get; }
	}

	/// <summary>
	/// Provides Direct2D support for drawing on D3D11.1 SwapChain
	/// </summary>
	public sealed class Direct2DService : Component, IDirect2DService
	{
		// default debug level
		private const DebugLevel D2DDebugLevel = DebugLevel.Information;

		// a reference to service responsible for GraphicsDevice management
		private readonly IGraphicsDeviceService _graphicsDeviceService;

		private Device _device;	// D2D device
		private DeviceContext _deviceContext;  // Default D2D device context
		private SharpDX.DirectWrite.Factory1 _dwFactory; // DirectWrite factory

		/// <summary>
		/// Initializes a new instance of <see cref="Direct2DService"/>, subscribes to <see cref="GraphicsDevice"/> changes events via
		/// <see cref="IGraphicsDeviceService"/>.
		/// </summary>
		/// <param name="graphicsDeviceService">The service responsible for <see cref="GraphicsDevice"/> management.</param>
		/// <exception cref="ArgumentNullException">Then either <paramref name="graphicsDeviceService"/> is null.</exception>
		public Direct2DService(IGraphicsDeviceService graphicsDeviceService)
		{
			if (graphicsDeviceService == null) throw new ArgumentNullException("graphicsDeviceService");

			_graphicsDeviceService = graphicsDeviceService;

			graphicsDeviceService.DeviceCreated += HandleDeviceCreated;
			graphicsDeviceService.DeviceDisposing += HandleDeviceDisposing;
		}

		/// <summary>
		/// Gets a reference to the Direct2D device. Can be used to create additional <see cref="DeviceContext"/>.
		/// </summary>
		public Device Device { get { return _device; } }

		/// <summary>
		/// Gets a reference to the default <see cref="DeviceContext"/> which will draw directly over SwapChain.
		/// </summary>
		public DeviceContext Context { get { return _deviceContext; } }

		/// <summary>
		/// Gets a reference to the default <see cref="SharpDX.DirectWrite.Factory1"/> used to create all DirectWrite objects.
		/// </summary>
		public SharpDX.DirectWrite.Factory1 DwFactory { get { return _dwFactory; } }

		/// <summary>
		/// Diposes all resources associated with the current <see cref="Direct2DService"/> instance.
		/// </summary>
		/// <param name="disposeManagedResources">Indicates whether to dispose management resources.</param>
		protected override void Dispose(bool disposeManagedResources)
		{
			base.Dispose(disposeManagedResources);

			DisposeAll();
		}

		/// <summary>
		/// Handles the <see cref="IGraphicsDeviceService.DeviceCreated"/> event.
		/// Initializes the <see cref="Direct2DService.Device"/> and <see cref="Direct2DService.Context"/>.
		/// </summary>
		/// <param name="sender">Ignored.</param>
		/// <param name="e">Ignored.</param>
		private void HandleDeviceCreated(object sender, EventArgs e)
		{
			var d3D11Device = (SharpDX.Direct3D11.Device)_graphicsDeviceService.GraphicsDevice;

			using (var dxgiDevice = d3D11Device.QueryInterface<SharpDX.DXGI.Device>())
			{
				_device = new Device(dxgiDevice, new CreationProperties { DebugLevel = D2DDebugLevel });
				_deviceContext = new DeviceContext(_device, DeviceContextOptions.EnableMultithreadedOptimizations);
			}

			_dwFactory = new SharpDX.DirectWrite.Factory1();
		}

		/// <summary>
		/// Handles the <see cref="IGraphicsDeviceService.DeviceDisposing"/> event.
		/// Disposes the <see cref="Direct2DService.Device"/>, <see cref="Direct2DService.Context"/> and its render target associated with the current <see cref="Direct2DService"/> instance.
		/// </summary>
		/// <param name="sender">Ignored.</param>
		/// <param name="e">Ignored.</param>
		private void HandleDeviceDisposing(object sender, EventArgs e)
		{
			DisposeAll();
		}

		/// <summary>
		/// Disposes the <see cref="Direct2DService.Device"/>, <see cref="Direct2DService.Context"/> and its render target associated with the current <see cref="Direct2DService"/> instance.
		/// </summary>
		private void DisposeAll()
		{
			Utilities.Dispose(ref _deviceContext);
			Utilities.Dispose(ref _device);
			Utilities.Dispose(ref _dwFactory);
		}
	}


	public class Canvas : Component {
        #region Private fields

        private readonly Game _game;
        private readonly GraphicsDeviceManager _graphicsDeviceManager;
        private readonly List<CanvasObject> _objects;
        private readonly Queue<CanvasObject> _queue;
        private readonly IDirect2DService _service;
        private readonly SpriteBatch _spriteBatch;
        private Bitmap1 _bitmap1;
        private RenderTarget2D _renderTarget2D;

        #endregion

        #region Public constructor

        public Canvas(Game game) {
            if (game == null) throw new ArgumentNullException("game");
            _game = game;

			_service = game.Services.GetService<IDirect2DService>();

            _graphicsDeviceManager = (GraphicsDeviceManager) game.Services.GetService<IGraphicsDeviceManager>();
            _graphicsDeviceManager.DeviceChangeBegin += _graphicsDeviceManager_DeviceChangeBegin;
            _graphicsDeviceManager.DeviceChangeEnd += _graphicsDeviceManager_DeviceChangeEnd;

            GraphicsDevice graphicsDevice = _game.GraphicsDevice;
            _spriteBatch = new SpriteBatch(graphicsDevice);

            // Set default values for this class
            _objects = new List<CanvasObject>();
            State = CanvasState.Refresh;
            _queue = new Queue<CanvasObject>();

			DeviceContext.UnitMode = UnitMode.Pixels;
        }

        public Factory Direct2DFactory {
            get { return _service.Device.Factory; }
        }

        private void _graphicsDeviceManager_DeviceChangeBegin(object sender, EventArgs e) {
            RemoveAndDispose(ref _renderTarget2D);
            RemoveAndDispose(ref _bitmap1);
        }

        private void _graphicsDeviceManager_DeviceChangeEnd(object sender, EventArgs e) {
            GraphicsDevice graphicsDevice = _game.GraphicsDevice;
            RenderTarget2D backBuffer = graphicsDevice.BackBuffer;
            _renderTarget2D = ToDispose(RenderTarget2D.New(graphicsDevice, backBuffer.Width, backBuffer.Height, backBuffer.Format));
			_bitmap1 = ToDispose(new Bitmap1(DeviceContext, _renderTarget2D));

            State = CanvasState.Refresh;
        }

        #endregion

        #region Private properties

        public DeviceContext DeviceContext {
            get { return _service.Context; }
        }
        public DirectWrite.Factory DirectWriteFactory { get { return _service.DwFactory; } }
        private CanvasState State { get; set; }

        #endregion

        #region Public properties

        public Color ClearColor { get; set; }

        #endregion

        #region Public methods

        public void Clear() {
            State = CanvasState.Refresh;
            _objects.Clear();
        }

        public SolidColorBrush GetSolidColorBrush(Color color) {
            return ToDispose(new SolidColorBrush(DeviceContext, color));
        }

        public StrokeStyle GetStrokeStyle(StrokeStyleProperties strokeStyleProperties, float[] dashes) {
            throw new NotImplementedException();
            //if (dashes == null) throw new ArgumentNullException("dashes");
            //return ToDispose(new StrokeStyle(DeviceContext.Factory, strokeStyleProperties, dashes));
        }

        public StrokeStyle GetStrokeStyle(StrokeStyleProperties strokeStyleProperties) {
            return ToDispose(new StrokeStyle(DeviceContext.Factory, strokeStyleProperties));
        }

        public void PushObject(CanvasObject canvasObject) {
            if (canvasObject == null) throw new ArgumentNullException("canvasObject");
            bool isInitialized = canvasObject.IsInitialized;
            if (!isInitialized) {
                canvasObject.Initialize(DeviceContext);
            }
            _objects.Add(canvasObject);
            _queue.Enqueue(canvasObject);

            State = CanvasState.Append;
        }

        public void PushObjects(params CanvasObject[] canvasObjects) {
            throw new NotImplementedException();
            if (canvasObjects == null) throw new ArgumentNullException("canvasObjects");
            _objects.AddRange(canvasObjects);
        }

        public void Render() {
            if (_bitmap1 == null || _renderTarget2D == null) return;
            switch (State) {
                case CanvasState.Append:
                    DeviceContext.Target = _bitmap1;
                    using (DeviceContext.Target) {
                        DeviceContext.BeginDraw();
                        while (_queue.Count > 0) {
                            CanvasObject o = _queue.Dequeue();
                            o.DoWork(DeviceContext);
                        }
                        DeviceContext.EndDraw();
                    }
                    DeviceContext.Target = null;
                    State = CanvasState.Cache;
                    break;
                case CanvasState.Refresh:
                    DeviceContext.Target = _bitmap1;
                    using (DeviceContext.Target) {
                        DeviceContext.BeginDraw();
                        DeviceContext.Clear(ClearColor);
                        foreach (CanvasObject o in _objects) {
                            o.DoWork(DeviceContext);
                        }
                        DeviceContext.EndDraw();
                    }
                    DeviceContext.Target = null;
                    State = CanvasState.Cache;
                    break;
                case CanvasState.Cache:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

			_spriteBatch.Begin(SpriteSortMode.Immediate, this._graphicsDeviceManager.GraphicsDevice.BlendStates.NonPremultiplied);
            _spriteBatch.Draw(_renderTarget2D, Vector2.Zero, Color.White);
            _spriteBatch.End();
        }

        #endregion
    }
}
