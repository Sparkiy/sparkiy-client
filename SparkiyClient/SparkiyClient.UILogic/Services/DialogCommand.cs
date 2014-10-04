using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkiyClient.UILogic.Services
{
	public class DialogCommand
	{
		public object Id { get; set; }
		public string Label { get; set; }
		public Action Invoked { get; set; }
	}
}
