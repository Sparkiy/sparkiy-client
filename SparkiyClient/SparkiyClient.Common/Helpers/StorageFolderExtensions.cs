using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace SparkiyClient.Common.Helpers
{
	public static class StorageFolderExtensions
	{
		/// <summary>
		/// Returns true if a folder contains another folder with the given name
		/// </summary>
		/// <param name="folder"></param>
		/// <param name="name"></param>
		/// <returns>True if the folder contains the folder with given name. False - otherwise</returns>
		public static async Task<bool> ContainsFolderAsync(this StorageFolder folder, string name)
		{
			return (await folder.GetFoldersAsync()).Any(l => l.Name == name);
		}

		/// <summary>
		/// Ensures that a folder with given name exists in given folder
		/// </summary>
		/// <param name="folder"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public static async Task EnsureFolderExistsAsync(this StorageFolder folder, string name)
		{
			if (await ContainsFolderAsync(folder, name))
			{
				return;
			}

			await folder.CreateFolderAsync(name);
		}
	}
}