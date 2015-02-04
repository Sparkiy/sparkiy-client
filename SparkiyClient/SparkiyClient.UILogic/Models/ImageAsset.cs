using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;
using MetroLog;

namespace SparkiyClient.UILogic.Models
{
	public class ImageAsset : AssetWithData<WriteableBitmap>
	{
		private static readonly ILogger Log = LogManagerFactory.DefaultLogManager.GetLogger<ImageAsset>();

		/// <summary>
		/// Gets the data asynchronously from given path.
		/// </summary>
		/// <returns></returns>
		public override async Task GetDataAsync()
		{
		    if (this.Data != null) return;

		    var imageFile = await StorageFile.GetFileFromPathAsync(this.Path);
		    var imageStream = await imageFile.OpenReadAsync();

            this.Data = new WriteableBitmap(1, 1);
            await this.Data.SetSourceAsync(imageStream);

            Log.Debug("Loaded ImageAsset \"{0}\"", this.Name);
		}
	}
}