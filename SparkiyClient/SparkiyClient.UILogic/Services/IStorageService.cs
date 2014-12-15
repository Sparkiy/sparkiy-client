using System;
using System.Threading.Tasks;
using Windows.Storage;

namespace SparkiyClient.UILogic.Services
{
	public interface IStorageService
	{
		/// <summary>
		/// Initializes the storage.
		/// This will trigger hard initialization if access token is not available.
		/// </summary>
		Task InitializeStorageAsync();

		/// <summary>
		/// Requireses the hard storage initialization.
		/// Hard initialization means that user needs to interact with the component to initialize.
		/// </summary>
		/// <returns>Returns <c>true</c> if storage requires hard initialization; false otherwise.</returns>
		bool RequiresHardStorageInitialization();

		/// <summary>
		/// Saves file by writing to temporary file and then renaming it to replace existing file if any.
		/// </summary>
		/// <param name="folder">Folder in which to create or overwrite file</param>
		/// <param name="name">Name with extension of file</param>
		/// <param name="saveFuncAsync">This function will be invoked. You save data to file here. This is async method.</param>
		Task SaveFileSafeAsync(StorageFolder folder, string name, Func<StorageFile, Task> saveFuncAsync);

		/// <summary>
		/// Gets the workspace folder.
		/// </summary>
		/// <value>
		/// The workspace folder.
		/// </value>
		StorageFolder WorkspaceFolder { get; }
	}
}