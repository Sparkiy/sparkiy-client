using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using MetroLog;

namespace SparkiyClient.UILogic.Services
{
	public class StorageService : IStorageService
	{
		private static readonly ILogger Log = LogManagerFactory.DefaultLogManager.GetLogger<StorageService>();
		private const string WorkspaceFolderTokenKey = "WorkspaceFolderToken";

		private StorageFolder workspaceFolder;


		/// <summary>
		/// Initializes the storage.
		/// This will trigger hard initialization if access token is not available.
		/// </summary>
		public async Task InitializeStorageAsync()
		{
			Log.Debug("Initializing storage...");

			// Get workspace folder
			var workspaceFolderToken = ApplicationData.Current.LocalSettings.Values[WorkspaceFolderTokenKey];
			if (workspaceFolderToken == null)
			{
				Log.Debug("Storage initialization requires user interaction.");
				var picker = new FolderPicker
				{
					SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
					ViewMode = PickerViewMode.List
				};
				picker.FileTypeFilter.Add("*");
				var workspaceFolderSelected = await picker.PickSingleFolderAsync();
				if (workspaceFolderSelected == null)
				{
					Log.Warn("User canceled workspace folder selection.");
					return;
				}

				workspaceFolderToken = StorageApplicationPermissions.FutureAccessList.Add(workspaceFolderSelected);
				ApplicationData.Current.LocalSettings.Values[WorkspaceFolderTokenKey] = workspaceFolderToken;
			}
			this.workspaceFolder = await StorageApplicationPermissions.FutureAccessList.GetFolderAsync(workspaceFolderToken.ToString());

			Log.Debug("Storage successfully initialized.");
		}

		/// <summary>
		/// Requireses the hard storage initialization.
		/// Hard initialization means that user needs to interact with the component to initialize.
		/// </summary>
		/// <returns>
		/// Returns <c>true</c> if storage requires hard initialization; false otherwise.
		/// </returns>
		public bool RequiresHardStorageInitialization()
		{
			return ApplicationData.Current.LocalSettings.Values[WorkspaceFolderTokenKey] == null;
		}

		/// <summary>
		/// Saves file by writing to temporary file and then renaming it to replace existing file if any.
		/// </summary>
		/// <param name="folder">Folder in which to create or overwrite file</param>
		/// <param name="name">Name with extension of file</param>
		/// <param name="saveFuncAsync">This function will be invoked. You save data to file here. This is async method.</param>
		public async Task SaveFileSafeAsync(
			StorageFolder folder, 
			string name, 
			Func<StorageFile, Task> saveFuncAsync)
		{
			Log.Debug("Saving file {0}", name);

			var tempName = String.Format("{0}.temp", name);
			var tempFile = await folder.CreateFileAsync(tempName, CreationCollisionOption.OpenIfExists);
			await saveFuncAsync.Invoke(tempFile);
			await tempFile.RenameAsync(name, NameCollisionOption.ReplaceExisting);
		}

		/// <summary>
		/// The workspace folder
		/// </summary>
		public StorageFolder WorkspaceFolder => this.workspaceFolder;
	}
}