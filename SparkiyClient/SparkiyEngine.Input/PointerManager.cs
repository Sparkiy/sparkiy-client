using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Windows.Devices.Enumeration;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using SparkiyEngine.Bindings.Component.Engine;

namespace SparkiyEngine.Input
{
    public class PointerManager
    {
        private readonly IEngineBindings engine;
        private readonly UIElement surface;
        private readonly Dictionary<uint, TrackingPointer> trackedPointers = new Dictionary<uint, TrackingPointer>();
        private uint primaryPointerId;


        public PointerManager(UIElement surface, IEngineBindings engine)
        {
            Contract.Requires(surface != null);

            this.engine = engine;

            this.surface = surface;
            this.surface.PointerPressed += SurfaceOnPointerPressed;
            this.surface.PointerReleased += SurfaceOnPointerReleased;
            this.surface.PointerMoved += SurfaceOnPointerMoved;
        }

        private void SurfaceOnPointerMoved(object sender, PointerRoutedEventArgs pointerRoutedEventArgs)
        {
            Contract.Requires(pointerRoutedEventArgs != null);
            Contract.Requires(this.surface != null);

            this.UpdatePointer(
                pointerRoutedEventArgs.Pointer.PointerId,
                pointerRoutedEventArgs.GetCurrentPoint(this.surface));
        }

        private void SurfaceOnPointerReleased(object sender, PointerRoutedEventArgs pointerRoutedEventArgs)
        {
            Contract.Requires(pointerRoutedEventArgs != null);
            Contract.Requires(this.surface != null);

            this.UnregisterPointer(
                pointerRoutedEventArgs.Pointer,
                pointerRoutedEventArgs.GetCurrentPoint(this.surface));
        }

        private void SurfaceOnPointerPressed(object sender, PointerRoutedEventArgs pointerRoutedEventArgs)
        {
            Contract.Requires(pointerRoutedEventArgs != null);
            Contract.Requires(this.surface != null);

            this.RegisterPointer(
                pointerRoutedEventArgs.Pointer,
                pointerRoutedEventArgs.GetCurrentPoint(this.surface));
        }

        private void RegisterPointer(Pointer pointer, PointerPoint point)
        {
            Contract.Requires(pointer != null);
            Contract.Requires(point != null);
            Contract.Requires(this.trackedPointers != null);

            this.ClearNotTracked();

            if (!this.trackedPointers.ContainsKey(pointer.PointerId))
                this.trackedPointers.Add(
                    pointer.PointerId,
                    new TrackingPointer(pointer, point));
            this.trackedPointers[pointer.PointerId].RealType = InputTypes.Down;

            // Assign primary pointer if first on tracking
            if (this.trackedPointers.Count == 1 ||
                this.PrimaryPointer.Pointer.PointerDeviceType != pointer.PointerDeviceType)
                this.primaryPointerId = pointer.PointerId;
        }

        private void UnregisterPointer(Pointer pointer, PointerPoint point)
        {
            Contract.Requires(pointer != null);
            Contract.Requires(point != null);
            Contract.Requires(this.trackedPointers != null);

            this.ClearNotTracked();

            if (!this.trackedPointers.ContainsKey(pointer.PointerId))
            {
                System.Diagnostics.Debug.WriteLine("Can't UNREGISTER, doesn't exist");
                return;
            }

            this.UpdatePointer(pointer.PointerId, point);
            this.trackedPointers[pointer.PointerId].RealType = InputTypes.NotTracked;

            // Remove from tracked if not primary
            if (pointer.PointerId != this.primaryPointerId)
                this.trackedPointers.Remove(pointer.PointerId);
        }

        private void UpdatePointer(uint pointerId, PointerPoint point)
        {
            Contract.Requires(point != null);
            Contract.Requires(this.trackedPointers != null);

            //this.ClearNotTracked();

            if (!this.trackedPointers.ContainsKey(pointerId))
            {
                return;
            }

            this.trackedPointers[pointerId].Update(point);
        }

        private void ClearNotTracked()
        {
            var toRemove = this.trackedPointers.Where(t =>
                t.Value.InGameType == InputTypes.NotTracked).ToList();
            foreach (var tToRemove in toRemove)
                this.trackedPointers.Remove(tToRemove.Key);
        }

        public TrackingPointer PrimaryPointer
        {
            get
            {
                if (!this.trackedPointers.ContainsKey(this.primaryPointerId))
                    return null;
                return this.trackedPointers[this.primaryPointerId];
            }
        }

        public class TrackingPointer
        {
            public uint Id
            {
                get { return this.Pointer.PointerId; }
            }

            public Pointer Pointer { get; set; }

            /// <summary>
            /// Real pointer state.
            /// This can only be in Down and NotTracked states.
            /// </summary>
            public InputTypes RealType { get; set; }

            /// <summary>
            /// Reported pointer state.
            /// Once real pointer is Down this must go through all states Down, Hold, Up, NotTracked in that order
            /// </summary>
            public InputTypes InGameType { get; set; }

            /// <summary>
            /// Gets or sets the x.
            /// </summary>
            /// <value>
            /// The x.
            /// </value>
            public float X { get; set; }

            /// <summary>
            /// Gets or sets the y.
            /// </summary>
            /// <value>
            /// The y.
            /// </value>
            public float Y { get; set; }


            public TrackingPointer(Pointer pointer, PointerPoint point)
            {
                Contract.Requires(pointer != null);
                Contract.Requires(point != null);

                this.InGameType = InputTypes.NotTracked;
                this.RealType = InputTypes.NotTracked;

                this.Pointer = pointer;
                this.Update(point);
            }


            public void UpdateType()
            {
                if (this.RealType == InputTypes.Down && this.InGameType == InputTypes.NotTracked)
                    this.InGameType = InputTypes.Down;
                else if (this.InGameType == InputTypes.Down)
                    this.InGameType = InputTypes.Hold;
                else if (this.InGameType == InputTypes.Hold && this.RealType == InputTypes.NotTracked)
                    this.InGameType = InputTypes.Up;
                else if (this.InGameType == InputTypes.Up)
                    this.InGameType = InputTypes.NotTracked;
            }

            public void Update(PointerPoint point)
            {
                Contract.Requires(point != null);

                this.X = (float) point.Position.X;
                this.Y = (float) point.Position.Y;
            }
        }
    }

    public enum InputTypes
    {
        NotTracked,
        Down,
        Hold,
        Up
    }
}
