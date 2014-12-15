using System;

namespace SparkiyClient.UILogic.Models
{
	public class Class : CodeFile
	{
		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return String.Format("Class: {0}", this.Name);
		}
	}
}