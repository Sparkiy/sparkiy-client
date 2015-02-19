using Microsoft.WindowsAzure.MobileServices;

namespace SparkiyClient.UILogic.Services
{
	public interface IMobileServiceProvider
	{
		MobileServiceClient MobileService { get; }
	}
}