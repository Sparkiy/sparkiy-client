using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;

namespace SparkiyClient.UILogic.Services
{
	public interface IAuthenticationService
	{
		Task AuthenticateAsync();
	}
}
