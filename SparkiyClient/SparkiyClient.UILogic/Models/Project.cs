using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using GalaSoft.MvvmLight;
using SparkiyClient.Common;

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
		public string Name
		{
			get { return this.GetProperty<string>(); }
			set { this.SetProperty(value); }
		}

		public string Description
		{
			get { return this.GetProperty<string>(); }
			set { this.SetProperty(value); }
		}

		public string Author
		{
			get { return this.GetProperty<string>(); }
			set { this.SetProperty(value); }
		}

		public IEnumerable<Script> Scripts
		{
			get { return this.GetProperty<IEnumerable<Script>>(); }
			set { this.SetProperty(value); }
		}

		public ImageReference Image
		{
			get { return this.GetProperty<ImageReference>(); }
			set { this.SetProperty(value); }
		}
	}
}
