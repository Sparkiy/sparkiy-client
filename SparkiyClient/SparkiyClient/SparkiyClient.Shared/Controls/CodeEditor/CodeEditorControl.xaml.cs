using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236
using SparkiyClient.Common.Controls;

namespace SparkiyClient.Controls
{
	public sealed partial class CodeEditorControl : UserControl, ICodeEditor
    {
	    public event EventHandler OnCodeChanged;

	    private string debugTemplate = "function Created()\n\t\nend\n\nfunction Started()\n\t\nend\n\nfunction Draw()\n\t\nend\n\nfunction Touched(state, x, y)\n\t\nend\n\nfunction Stopped()\n\t\nend\n";

	    private string fontFamily = "Consolas";
	    private double fontSize = 16;

	    private string tabValue = "    ";


        public CodeEditorControl()
        {
            this.InitializeComponent();

			this.Loaded += OnLoaded;
        }

	    private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
	    {
            // Setup editor style
            //this.CodeEditor.FontFamily = new FontFamily(fontFamily);
	        //this.CodeEditor.FontSize = this.fontSize;

#if DEBUG
            //this.CodeEditor.Document.SetText(TextSetOptions.None, this.debugTemplate.Replace("\t", this.tabValue));
#endif

      //      this.CodeEditor.TextChanged += (o, args) =>
		    //{
		    //    this.OnCodeChanged?.Invoke(this, null);
		    //};
      //      this.CodeEditor.KeyDown += RichEditBoxOnKeyDown;
	    }

	    private void RichEditBoxOnKeyDown(object sender, KeyRoutedEventArgs e)
	    {
	        //if (e.Key == VirtualKey.Tab)
	        //{
	        //    this.RichEditBox.Document.Selection.TypeText(this.tabValue);
	        //    e.Handled = true;
	        //}
	    }


		//public string Code
		//{
		//	get
		//	{
		//		string code = String.Empty;
		//		this.RichEditBox.Document.GetText(TextGetOptions.None, out code);
		//		return code;
		//	}
		//	set { this.RichEditBox.Document.SetText(TextSetOptions.None, value ?? String.Empty); }
		//}

		public string Code { get; set; }
    }
}
