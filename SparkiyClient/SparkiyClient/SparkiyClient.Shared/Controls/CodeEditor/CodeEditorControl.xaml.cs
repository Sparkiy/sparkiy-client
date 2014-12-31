using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using SparkiyClient.Common.Controls;
// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SparkiyClient.Controls.CodeEditor
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
			this.CodeEditor.TextView.TextChanged += (o, args) =>
		    {
		        this.OnCodeChanged?.Invoke(this, null);
		    };
	    }

		public string Code
		{
			get { return this.CodeEditor.Text; }
			set { this.CodeEditor.Text = value; }
		}
    }
}
