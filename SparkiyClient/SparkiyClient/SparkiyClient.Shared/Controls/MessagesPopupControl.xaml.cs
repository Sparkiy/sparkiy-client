using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using SharpDX.DXGI;
using SparkiyClient.Common.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SparkiyClient.Controls
{
    public sealed partial class MessagesPopupControl : UserControl, IMessagesPopup
    {
        private static readonly Brush MessageBackgroundBrushDefault =
            new SolidColorBrush(new Color() {R = 0x42, G = 0x98, B = 0xED, A = 255});

        public ObservableCollection<string> Messages { get; } = new ObservableCollection<string>();

        public Brush MessageBackgroundBrush { get; set; } = MessageBackgroundBrushDefault;

        public Brush ErrorBackgroundBrush { get; set; } = new SolidColorBrush(Colors.OrangeRed);

        public int TemporaryMessageDisplayTime { get; set; } = 5000;


        public Brush CurrentBackgroundBrush
        {
            get { return (Brush)GetValue(CurrentBackgroundBrushProperty); }
            set { SetValue(CurrentBackgroundBrushProperty, value); }
        }

        public static readonly DependencyProperty CurrentBackgroundBrushProperty =
            DependencyProperty.Register("CurrentBackgroundBrush", typeof(Brush), typeof(MessagesPopupControl), new PropertyMetadata(MessageBackgroundBrushDefault));


        public bool IsVisible
        {
            get { return (bool)GetValue(IsVisibleProperty); }
            set { SetValue(IsVisibleProperty, value); }
        }

        public static readonly DependencyProperty IsVisibleProperty =
            DependencyProperty.Register("IsVisible", typeof(bool), typeof(MessagesPopupControl), new PropertyMetadata(false, PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs e)
        {
            var control = dependencyObject as MessagesPopupControl;
            if (control == null)
                throw new NullReferenceException("Couldn't retrieve control from sender");

            var newValue = (bool) e.NewValue;

            if (newValue)
                control.ShowItemsControl.Begin();
            else control.HideItemsControl.Begin();
        }


        public MessagesPopupControl()
        {
            this.InitializeComponent();

            this.DataContext = this;

            // Hide messages popup
            this.HideItemsControl.Begin();
            this.HideItemsControl.SkipToFill();
            
            this.Messages.CollectionChanged += Messages_CollectionChanged;
        }

        private async void Messages_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => this.IsVisible = this.Messages.Count != 0);
        }

        public async Task AddTemporaryMessageAsync(string message)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                this.Messages.Add(message);
                this.RemoveMessageAfter(TimeSpan.FromMilliseconds(this.TemporaryMessageDisplayTime));
            });
        }

        public async Task AddErrorMessageAsync(string message)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                this.Messages.Add(message);
                this.CurrentBackgroundBrush = ErrorBackgroundBrush;
            });
        }

        public async Task ClearAsync()
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                this.Messages.Clear();
                this.CurrentBackgroundBrush = MessageBackgroundBrush;
            });
        }

        private void RemoveMessageAfter(TimeSpan delay)
        {
            var timer = new DispatcherTimer() { Interval = delay };
            timer.Tick += (sender, o) =>
            {
                this.Messages.RemoveAt(0);
                timer.Stop();
            };
            timer.Start();
        }
    }
}
