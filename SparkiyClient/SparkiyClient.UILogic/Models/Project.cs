using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using GalaSoft.MvvmLight;
using Nito.AsyncEx;
using SparkiyClient.Common;
using SparkiyClient.UILogic.Services;

namespace SparkiyClient.UILogic.Models
{
	[ComVisible(false)]
	public class ImageReference : ExtendedObservableObject
	{
		public ImageSources Source
		{
			get { return this.GetProperty<ImageSources>(); }
			set { this.SetProperty(value); }
		}

		public string Path
		{
			get { return this.GetProperty<string>(); }
			set { this.SetProperty(value); }
		}


		public async Task<StorageFile> GetImageAsync()
		{
			switch (this.Source)
			{
				case ImageSources.PicturesLibrary:
					return await KnownFolders.PicturesLibrary.GetFileAsync(this.Path);
				case ImageSources.Package:
					return await Package.Current.InstalledLocation.GetFileAsync(this.Path);
				case ImageSources.LocalProject:
					return await StorageFile.GetFileFromPathAsync(this.Path);
				default:
					throw new InvalidOperationException("Requested source is not supported.");
			}
		}

		public async Task<ImageSource> GetImageSourceAsync(CoreDispatcher dispatcher)
		{
			Contract.Requires(dispatcher != null);

			using (var fs = await (await this.GetImageAsync()).OpenReadAsync())
			{
				BitmapImage img = null;
				await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => { img = new BitmapImage(); });
				await img.SetSourceAsync(fs);
				return img;
			}
		}
	}

	[ComVisible(false)]
	public class Project : ExtendedObservableObject
	{
		public Project()
		{

		}


		public async Task LoadScriptsAsync(IProjectService projectService)
		{
			this.Scripts = NotifyTaskCompletion.Create(projectService.GetScriptsAsync(this));
			await this.Scripts.Task;
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
		public INotifyTaskCompletion<ObservableCollection<Script>> Scripts
		{
			get { return this.GetProperty<INotifyTaskCompletion<ObservableCollection<Script>>>(); }
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
