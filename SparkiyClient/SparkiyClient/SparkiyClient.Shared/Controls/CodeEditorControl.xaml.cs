using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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


        public CodeEditorControl()
        {
            this.InitializeComponent();

			this.Loaded += OnLoaded;
        }

	    private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
	    {
		    this.RichEditBox.TextChanged += (o, args) =>
		    {
				if (this.OnCodeChanged != null)
					this.OnCodeChanged(this, null);
		    };
	    }


	    public string Code
	    {
		    get
		    {
			    string code = String.Empty;
			    this.RichEditBox.Document.GetText(TextGetOptions.None, out code);
			    return code;
		    }
		    set { this.RichEditBox.Document.SetText(TextSetOptions.None, value); }
	    }
    }
}
