using System.Threading.Tasks;

namespace SparkiyClient.UILogic.Models
{
	public abstract class AssetWithData<T> : Asset
	{
		/// <summary>
		/// Gets the data asynchronously from given path.
		/// </summary>
		public abstract Task GetDataAsync();

		/// <summary>
		/// Gets or sets the data.
		/// </summary>
		/// <value>
		/// The data.
		/// </value>
		public T Data
		{
			get { return this.GetProperty<T>(); }
			set { this.SetProperty(value); }
		}
	}
}