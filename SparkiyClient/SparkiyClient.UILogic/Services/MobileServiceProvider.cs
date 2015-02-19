using Microsoft.WindowsAzure.MobileServices;

namespace SparkiyClient.UILogic.Services
{
	public class MobileServiceProvider : IMobileServiceProvider
	{
		public MobileServiceProvider()
		{
			this.MobileService = new MobileServiceClient(
				"https://sparkiy.azure-mobile.net/",
				"grRPxbjQSCckHZLrPjYTAOSLCIWiEH16");
		}
		
		public MobileServiceClient MobileService { get; private set; }
	}
}