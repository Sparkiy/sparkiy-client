using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using SparkiyEngine.Core;
using SparkiyEngine.Engine.Implementation;
using SparkiyEngine.Graphics.DirectX;
using SparkiyEngine_Language_LuaImplementation;


namespace SparkiyClient.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PlaygroundPage : Page
    {
		// Code editor
	    private Timer editorTimer;

		private SparkiyBootstrap bootstrap;


		/// <summary>
		/// Initializes a new instance of the <see cref="PlaygroundPage"/> class.
		/// </summary>
        public PlaygroundPage()
        {
            this.InitializeComponent();
        }

		/// <summary>
		/// Invoked when this page is about to be displayed in a Frame.
		/// </summary>
		/// <param name="e">Event data that describes how this page was reached.
		/// This parameter is typically used to configure the page.</param>
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			this.RichEditBox.TextChanged += RichEditBoxOnTextChanged;
			this.editorTimer = new Timer(this.EditorCallback, null, TimeSpan.FromMilliseconds(0), TimeSpan.FromMilliseconds(-1));
		}

	    private void EditorCallback(object state)
	    {
			System.Diagnostics.Debug.WriteLine("Script restart triggered by editor");
		    CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, this.RunScript);
	    }

	    private void RichEditBoxOnTextChanged(object sender, RoutedEventArgs routedEventArgs)
	    {
		    this.editorTimer.Change(TimeSpan.FromMilliseconds(1000), TimeSpan.FromMilliseconds(-1));
	    }

	    private void RunScript()
	    {
			// Initialize rendering using DirectX 
			var renderer = new Renderer(this.SwapChainPanel);

			// Initialize language using Lua
			var language = new LuaImplementation();

			// Initialize engine 
			var engine = new Sparkiy();

			// Connect components using bootstrap
			this.bootstrap = new SparkiyBootstrap();
			this.bootstrap.InitializeLua(language.GetLanguageBindings(), renderer.GraphicsBindings, engine);

			// Retrieve script code
		    string playgroundCode;
		    this.RichEditBox.Document.GetText(TextGetOptions.NoHidden, out playgroundCode);

			// Run script
			this.bootstrap.Bindings.Language.LoadScript("playground", playgroundCode);
			this.bootstrap.Bindings.Language.StartScript("playground");
	    }
    }
}
