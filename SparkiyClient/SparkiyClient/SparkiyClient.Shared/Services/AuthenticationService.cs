using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Windows.Security.Credentials;
using Windows.UI.Popups;
using MetroLog;
using Microsoft.Live;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using SparkiyClient.UILogic.Services;

namespace SparkiyClient.Services
{
	public class AuthenticationService : IAuthenticationService
	{
		private static readonly ILogger Log = LogManagerFactory.DefaultLogManager.GetLogger<AuthenticationService>();
		private readonly IMobileServiceProvider mobileServiceProvider;
		private MobileServiceUser user;


		public AuthenticationService(IMobileServiceProvider mobileServiceProvider)
		{
			this.mobileServiceProvider = mobileServiceProvider;
		}


		public async Task GetUserData()
		{
			
		}

		public async Task AuthenticateAsync()
		{
			// Use the PasswordVault to securely store and access credentials.
			PasswordVault vault = new PasswordVault();
			PasswordCredential credential = null;

			try
			{
				// Try to get an existing credential from the vault.
				credential = vault.FindAllByResource("microsoftaccount").FirstOrDefault();
			}
			catch (Exception)
			{
				// When there is no matching resource an error occurs, which we ignore.
			}

			// Try create user data if credentials were saved
			if (credential != null)
			{
				this.user = new MobileServiceUser(credential.UserName);
				credential.RetrievePassword();
				user.MobileServiceAuthenticationToken = credential.Password;
				this.mobileServiceProvider.MobileService.CurrentUser = this.user;

				try
				{
					
				}
				catch (MobileServiceInvalidOperationException ex)
				{
					if (ex.Response.StatusCode == HttpStatusCode.Unauthorized)
					{
						vault.Remove(credential);
						credential = null;
					}
				}

				Log.Debug("User credentials successfully recovered.");
			}

			// If credentials were not recovered, request user to sign-in
			if (credential == null)
			{
				try
				{
					this.user = await this.mobileServiceProvider.MobileService.LoginAsync(
						MobileServiceAuthenticationProvider.MicrosoftAccount, true, new Dictionary<string, string>());

					// Create and store the user credentials.
					credential = new PasswordCredential("microsoftaccount", this.user.UserId, this.user.MobileServiceAuthenticationToken);
					vault.Add(credential);

					this.mobileServiceProvider.MobileService.CurrentUser = this.user;
					Log.Debug("User successfully logged in.");
				}
				catch (InvalidOperationException ex)
				{
					Log.Warn("Failed to login user.", ex);
				}
			}
		}

		public enum AvailablePrviders
		{
			MicrosoftAccount
		}
	}
}
