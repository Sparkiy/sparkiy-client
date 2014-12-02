using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SparkiyClient.UILogic.Models;

namespace SparkiyClient.UILogic.Services
{
	public interface IStorageService
	{
		Task InitializeStorageAsync();

		bool RequiresStorageInitialization();
	}

	public interface IProjectService
	{
		Task<IEnumerable<Project>> GetAvailableProjectsAsync();

		void Save();
	}
}
