using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SparkiyEngine.Bindings.Common.Component;

namespace SparkiyEngine.Bindings.Common.Attributes
{
	public sealed class MethodDeclarationResolver
	{
		public MethodDeclarationResolver(SupportedLanguages language)
		{
			this.Language = language;

			// Create empty dictionary
			this.AvailableMethods = 
				new ReadOnlyDictionary<string, MethodDeclarationDetails>(
					new Dictionary<string, MethodDeclarationDetails>());
		}


		public void ResolveAll(Type target)
		{
			var availableMethods = new Dictionary<string, MethodDeclarationDetails>();

			var methods = target
				.GetTypeInfo()
				.DeclaredMethods.Union(
					target
						.GetTypeInfo()
						.ImplementedInterfaces
						.SelectMany(
							inter => inter
								.GetTypeInfo()
								.DeclaredMethods));

			foreach (var method in methods)
			{
				// Retrieve method declaration attribute from method, if not available skip this method
				var attribute = method.GetCustomAttribute<MethodDeclarationAttribute>();
				if (attribute == null)
				{
					continue;
				}

				// Skip if language doesn't match
				if (!attribute.Languages.HasFlag(SupportedLanguages.Lua))
				{
					continue;
				}

				// Retrieve returning type
				// Retrieve calling types

				if (availableMethods.ContainsKey(attribute.Name))
				{
					var details = availableMethods[attribute.Name];

					details.Type &= attribute.Type;
				}
				else
				{
					availableMethods.Add(attribute.Name, new MethodDeclarationDetails()
					{
						Name = attribute.Name,
						Type = attribute.Type
					});
				}
			}

			this.AvailableMethods = availableMethods;
		}

		public SupportedLanguages Language { get; private set; }

		public IReadOnlyDictionary<string, MethodDeclarationDetails> AvailableMethods { get; private set; }  
	}
}
