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
				var returnTypes = ResolveDataTypes(method.ReturnType);

				// Retrieve calling types
				var parameters = method.GetParameters().Select(pi => pi.ParameterType).SelectMany(ResolveDataTypes).ToArray();

				// Retrieve or create new declaration
				MethodDeclarationDetails details;
				if (availableMethods.ContainsKey(attribute.Name))
				{
					details = availableMethods[attribute.Name];
				}
				else
				{
					details = new MethodDeclarationDetails()
					{
						Name = attribute.Name,
					};

					availableMethods.Add(attribute.Name, details);
				}

				// Add method overload to the declaration
				var overloadsList = details.Overloads as List<MethodDeclarationOverloadDetails>;
				if (overloadsList == null) 
					throw new NullReferenceException("Couldn't retrieve list containing method declaration overloads");
				overloadsList.Add(new MethodDeclarationOverloadDetails()
				{
					Type = attribute.Type,
					Input = parameters,
					Return = returnTypes
				});
			}

			// Replace old list with new list
			this.AvailableMethods = availableMethods;
		}

		private static DataTypes[] ResolveDataTypes(Type type)
		{
			DataTypes[] dataTypes;
			if (IsOfType(type, typeof (void)))
			{
				dataTypes = new DataTypes[0];
			}
			else if (IsOfType(type, typeof(int)) ||
				IsOfType(type, typeof(float)) ||
				IsOfType(type, typeof(double)))
			{
				dataTypes = new DataTypes[] { DataTypes.Number };
			}
			else if (IsOfType(type, typeof(string)))
			{
				dataTypes = new DataTypes[] { DataTypes.String };
			}
			else if (IsOfType(type, typeof(NumberGroup2)))
			{
				dataTypes = new DataTypes[] { DataTypes.Number, DataTypes.Number };
			}
			else if (IsOfType(type, typeof(NumberGroup3)))
			{
				dataTypes = new DataTypes[] { DataTypes.Number, DataTypes.Number, DataTypes.Number };
			}
			else
			{
				throw new NotSupportedException("Given type is not supported.");
			}

			return dataTypes;
		}

		private static bool IsOfType(Type source, Type reference)
		{
			return source.GetTypeInfo().IsAssignableFrom(reference.GetTypeInfo());
		}

		public SupportedLanguages Language { get; private set; }

		public IReadOnlyDictionary<string, MethodDeclarationDetails> AvailableMethods { get; private set; }  
	}
}
