using System.Runtime.InteropServices;
using SparkiyClient.Common;

namespace SparkiyClient.UILogic.Models
{
	[ComVisible(false)]
	public abstract class Asset : ExtendedObservableObject
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name
		{
			get { return this.GetProperty<string>(); }
			set { this.SetProperty(value); }
		}

		/// <summary>
		/// Gets or sets the path.
		/// </summary>
		/// <value>
		/// The path.
		/// </value>
		public string Path
		{
			get { return this.GetProperty<string>(); }
			set { this.SetProperty(value); }
		}

		
	}
}