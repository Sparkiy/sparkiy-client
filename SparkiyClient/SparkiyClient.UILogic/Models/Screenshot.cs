using System.Runtime.InteropServices;
using SparkiyClient.Common;

namespace SparkiyClient.UILogic.Models
{
	[ComVisible(false)]
	public class Screenshot : ExtendedObservableObject
	{
		public string Title
		{
			get { return this.GetProperty<string>(); }
			set { this.SetProperty(value); }
		}

		public string Description
		{
			get { return this.GetProperty<string>(); }
			set { this.SetProperty(value); }
		}

		public ImageReference Image
		{
			get { return this.GetProperty<ImageReference>(); }
			set { this.SetProperty(value); }
		}
	}
}