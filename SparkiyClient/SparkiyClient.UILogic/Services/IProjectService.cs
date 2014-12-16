using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using SparkiyClient.UILogic.Models;

namespace SparkiyClient.UILogic.Services
{
	public interface IProjectService
	{
		Task<IEnumerable<Project>> GetAvailableProjectsAsync();

		Task SaveAsync();

		Task<IEnumerable<CodeFile>> GetFilesAsync(Project project);

		Task<IEnumerable<Asset>> GetAssetsAsync(Project project);

		Task CreateProjectAsync(Project project);

		Task ImportAsset(Project project, StorageFile file);
	}
}
