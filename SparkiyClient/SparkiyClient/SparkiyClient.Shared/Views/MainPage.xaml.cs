using Microsoft.Practices.Prism.StoreApps;
using SparkiyEngine.Bindings.Common;
using SparkiyEngine.Bindings.Common.Attributes;
using SparkiyEngine.Bindings.Graphics;
using SparkiyEngine.Bindings.Language;
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
		private Renderer renderer;
	    private IGraphicsBindings graphicsBindings;
	    private MethodDeclarationResolver methodDeclarationResolver;
		private LuaImplementation lua;
	    private ILanguageBindings languageBindings;

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
			// Lua graphics bindings methods resolver
			this.methodDeclarationResolver = new MethodDeclarationResolver(SupportedLanguages.Lua);
			this.methodDeclarationResolver.ResolveAll(typeof(IGraphicsBindings));

			// Initialize lua implementation and retrieve language bindings
			this.lua = new LuaImplementation();
			this.languageBindings = this.lua.GetLanguageBindings();

			// Initialize graphics implementation and retrieve graphics bindings
			this.renderer = new Renderer(this.SwapChainPanel);
			this.graphicsBindings = this.renderer.GraphicsBindings;


			this.languageBindings.LoadScript("001", "stroke(1, 1, 0)");
			this.languageBindings.StartScript("001");

			this.graphicsBindings.SetBackground(0.18f, 0.28f, 0.31f);
			this.graphicsBindings.SetStrokeColor(1f, 0f, 0f);
			this.graphicsBindings.DrawRectangle(100, 100, 50, 50);
        }

		protected override void OnNavigatedFrom(NavigationEventArgs e)
		{
			this.renderer.Dispose();

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
