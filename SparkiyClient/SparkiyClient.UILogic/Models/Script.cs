using System;
using System.Collections.Generic;
using System.IO;
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
		}


		/// <summary>
		/// Gets the code asynchronously from given path.
		/// </summary>
		/// <returns>Returns the code</returns>
		public async Task<string> GetCodeAsync()
		{
			// Check if this is new file
			if (String.IsNullOrEmpty(this.Path))
				return String.Empty;

			string code;
			var file = await StorageFile.GetFileFromPathAsync(this.Path);
			using (var fs = await file.OpenStreamForReadAsync())
			using (var reader = new StreamReader(fs))
				code = await reader.ReadToEndAsync();
			return code;
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
		public string Code
		{
			get { return this.GetProperty<string>(); }
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