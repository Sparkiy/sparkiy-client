using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkiyClient.UILogic.Services
{
	/// <summary>
	/// Implement this for each platform to enable alert messages
	/// </summary>
	public interface IAlertMessageService
	{
		Task ShowAsync(string message, string title);

		Task ShowAsync(string message, string title, IEnumerable<DialogCommand> dialogCommands);
	}
}
