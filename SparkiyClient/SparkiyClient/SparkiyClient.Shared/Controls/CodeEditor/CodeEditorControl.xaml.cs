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

	    private const string DebugTemplate = "function Created()\n\t\nend\n\nfunction Started()\n\t\nend\n\nfunction Draw()\n\t\nend\n\nfunction Touched(state, x, y)\n\t\nend\n\nfunction Stopped()\n\t\nend\n";


        public CodeEditorControl()
        {
            this.InitializeComponent();

			this.Loaded += OnLoaded;
        }

	    private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
	    {
#if DEBUG
		    this.CodeEditor.Text = DebugTemplate;
#endif

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
