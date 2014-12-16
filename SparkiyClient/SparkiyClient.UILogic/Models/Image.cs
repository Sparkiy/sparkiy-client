using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using MetroLog;

namespace SparkiyClient.UILogic.Models
{
	public class Image : AssetWithData<BitmapImage>
	{
		private static readonly ILogger Log = LogManagerFactory.DefaultLogManager.GetLogger<Image>();

		/// <summary>
		/// Gets the data asynchronously from given path.
		/// </summary>
		/// <returns></returns>
		public override async Task GetDataAsync()
		{
			this.Data = new BitmapImage(new Uri(this.Path, UriKind.Absolute));
			Log.Debug("Loaded Image \"{0}\"", this.Name);
		}
	}
}