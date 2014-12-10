using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Windows.Storage;
using Nito.AsyncEx;
using SparkiyClient.Common;

namespace SparkiyClient.UILogic.Models
{
	[ComVisible(false)]
	[DataContract]
	public class Script : ExtendedObservableObject
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Script"/> class.
		/// </summary>
		public Script()
		{
			this.Code = NotifyTaskCompletion.Create(this.GetCodeAsync);
		}


		/// <summary>
		/// Gets the code asynchronously from given path.
		/// </summary>
		/// <returns>Returns the code</returns>
		private async Task<string> GetCodeAsync()
		{
			return await PathIO.ReadTextAsync(this.Path);
		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		[DataMember]
		public string Name
		{
			get { return this.GetProperty<string>(); }
			set { this.SetProperty(value); }
		}

		/// <summary>
		/// Gets or sets the path of the script.
		/// </summary>
		/// <value>
		/// The path.
		/// </value>
		[DataMember]
		public string Path
		{
			get { return this.GetProperty<string>(); }
			set { this.SetProperty(value); }
		}

		/// <summary>
		/// Gets or sets the code of the script. Async Lazy loaded. Not Serialized.
		/// </summary>
		/// <value>
		/// The code.
		/// </value>
		[IgnoreDataMember]
		public INotifyTaskCompletion<string> Code
		{
			get { return this.GetProperty<INotifyTaskCompletion<string>>(); }
			set { this.SetProperty(value); }
		}

		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return this.Name;
		}
	}
}