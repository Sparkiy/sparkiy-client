using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Nito.AsyncEx;
using SparkiyClient.Common;
using SparkiyClient.UILogic.Services;

namespace SparkiyClient.UILogic.Models
{
	[ComVisible(false)]
	public class Project : ExtendedObservableObject
	{
		/// <summary>
		/// Loads the files and its' content.
		/// </summary>
		/// <param name="projectService">The project service.</param>
		public async Task LoadFilesAsync(IProjectService projectService)
		{
			this.Files = NotifyTaskCompletion.Create(async () => new ObservableCollection<CodeFile>(await projectService.GetFilesAsync(this)));
			await this.Files.Task;

			// Load code to files
			foreach (var file in this.Files.Result)
				await file.GetCodeAsync();
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
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		[DataMember]
		public string Description
		{
			get { return this.GetProperty<string>(); }
			set { this.SetProperty(value); }
		}

		/// <summary>
		/// Gets or sets the author.
		/// </summary>
		/// <value>
		/// The author.
		/// </value>
		[DataMember]
		public string Author
		{
			get { return this.GetProperty<string>(); }
			set { this.SetProperty(value); }
		}

		/// <summary>
		/// Gets or sets the scripts.
		/// </summary>
		/// <value>
		/// The scripts.
		/// </value>
		[IgnoreDataMember]
		public INotifyTaskCompletion<ObservableCollection<CodeFile>> Files
		{
			get { return this.GetProperty<INotifyTaskCompletion<ObservableCollection<CodeFile>>>(); }
			set { this.SetProperty(value); }
		}

		/// <summary>
		/// Gets or sets the image.
		/// </summary>
		/// <value>
		/// The image.
		/// </value>
		[DataMember]
		public ImageReference Image
		{
			get { return this.GetProperty<ImageReference>(); }
			set { this.SetProperty(value); }
		}
	}
}