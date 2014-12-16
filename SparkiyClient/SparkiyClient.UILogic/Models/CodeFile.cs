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
	public abstract class CodeFile : ExtendedObservableObject
	{
		/// <summary>
		/// Gets the code asynchronously from given path.
		/// </summary>
		/// <returns>Returns the code</returns>
		public async Task GetCodeAsync()
		{
			// Check if this is new file
			if (String.IsNullOrEmpty(this.Path))
				return;

			var file = await StorageFile.GetFileFromPathAsync(this.Path);
			var code = await FileIO.ReadTextAsync(file);

			this.Code = code;
		}


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
		/// Gets or sets the code of the script. 
		/// </summary>
		/// <value>
		/// The code.
		/// </value>
		public string Code
		{
			get { return this.GetProperty<string>(); }
			set { this.SetProperty(value); }
		}

		/// <summary>
		/// Gets or sets the path of the file.
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