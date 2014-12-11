using SparkiyClient.Views;

namespace SparkiyClient.Common
{
	public interface IPageBase
	{
		/// <summary>
		/// NavigationHelper is used on each page to aid in navigation and 
		/// process lifetime management
		/// </summary>
		NavigationHelper NavigationHelper { get; }
	}
}