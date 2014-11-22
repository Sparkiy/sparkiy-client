using System.Reflection;
using Microsoft.Practices.Prism.StoreApps;
using SparkiyEngine.Bindings.Component.Common;
using SparkiyEngine.Bindings.Component.Common.Attributes;
using SparkiyEngine.Bindings.Component.Graphics;
using SparkiyEngine.Bindings.Component.Language;
using SparkiyEngine.Core;
using SparkiyEngine.Engine.Implementation;
using SparkiyEngine.Graphics.DirectX;
using SparkiyEngine_Language_LuaImplementation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SparkiyClient.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : VisualStateAwarePage
    {
	    private SparkiyBootstrap bootstrap;


        public MainPage()
        {
            this.InitializeComponent();
			this.SizeChanged += this.PageSizeChanged;
		}

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
			// Initialize rendering using DirectX 
	        var renderer = new Renderer(this.SwapChainPanel);

			// Initialize language using Lua
	        var language = new LuaImplementation();

			// Initialize engine 
	        var engine = new Sparkiy(SupportedLanguages.Lua, language.GetLanguageBindings(), renderer.GraphicsBindings);

			// Connect components using bootstrap
	        this.bootstrap = new SparkiyBootstrap();
			this.bootstrap.InitializeLua(language.GetLanguageBindings(), renderer.GraphicsBindings, engine);

			this.bootstrap.Bindings.Language.LoadScript("001", "background(1, 1, 1) stroke(0, 0.8, 1)");
			this.bootstrap.Bindings.Language.StartScript("001");

			this.bootstrap.Bindings.Graphics.DrawRectangle(100, 100, 50, 50);
        }

	    protected override void OnNavigatedFrom(NavigationEventArgs e)
		{
			base.OnNavigatedFrom(e);
		}

		protected override void SaveState(System.Collections.Generic.Dictionary<string, object> pageState)
		{
			if (pageState == null) return;

			base.SaveState(pageState);
		}

		protected override void LoadState(object navigationParameter, System.Collections.Generic.Dictionary<string, object> pageState)
		{
			if (pageState == null) return;

			base.LoadState(navigationParameter, pageState);
		}

		private void PageSizeChanged(object sender, SizeChangedEventArgs e)
		{
			
		}
    }
}
