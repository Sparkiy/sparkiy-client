using System.Threading.Tasks;
using SparkiyClient.UILogic.Models;
using SparkiyEngine.Bindings.Component.Engine;

namespace SparkiyClient.UILogic.Services
{
	public interface IProjectPlayEngineManagement
	{
		void AssignProject(Project project);

		IEngineBindings Engine { get; }
	}
}