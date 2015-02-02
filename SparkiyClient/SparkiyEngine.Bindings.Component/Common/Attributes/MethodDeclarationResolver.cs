using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SparkiyEngine.Bindings.Component.Common.Attributes
{
	public static class MethodDeclarationResolver
	{
		public static IReadOnlyDictionary<string, MethodDeclarationDetails> ResolveAll(Type target, SupportedLanguages language)
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
				// Retrieve method declaration attributes from method, if none available skip this method
				var attributes = method.GetCustomAttributes<MethodDeclarationAttribute>().ToList();
				if (attributes == null || !attributes.Any())
				{
					continue;
				}

				foreach (var attribute in attributes)
				{
					// Skip if language doesn't match
					if (!attribute.Languages.HasFlag(language))
					{
						continue;
					}

					// Retrieve returning type
					var returnTypes = ResolveDataTypes(method.ReturnType);
				    var returnTypeNames = attribute.ReturnParams;

					// Retrieve calling types
					var parameters = method.GetParameters().Select(pi => pi.ParameterType).SelectMany(ResolveDataTypes).ToArray();
				    var parameterNames = method.GetParameters().Select(pi => pi.Name).ToArray();

                    // Go through all names
				    foreach (var name in attribute.Names)
				    {
                        // Retrieve or create new declaration
                        MethodDeclarationDetails details;
                        if (availableMethods.ContainsKey(name))
                        {
                            details = availableMethods[name];
                        }
                        else
                        {
                            details = new MethodDeclarationDetails()
                            {
                                Name = name,
                            };

                            availableMethods.Add(name, details);
                        }

                        // Add method overload to the declaration
                        var overloadsList = details.Overloads as List<MethodDeclarationOverloadDetails>;
                        if (overloadsList == null)
                            throw new NullReferenceException("Couldn't retrieve list containing method declaration overloads");
                        overloadsList.Add(new MethodDeclarationOverloadDetails()
                        {
                            Type = attribute.Type,
                            Input = parameters,
                            InputNames = parameterNames,
                            Return = returnTypes,
                            ReturnNames = returnTypeNames,
                            Uid = Guid.NewGuid().ToString(),
                            Method = method
                        });
                    }
				}
			}

			return availableMethods;
		}

	    public static IReadOnlyDictionary<string, MethodDeclarationDocumentationDetails> GenerateDocumentation(Type target, SupportedLanguages language)
	    {
	        var documentation = new Dictionary<string, MethodDeclarationDocumentationDetails>();

	        var declarations = ResolveAll(target, language);

	        foreach (var declaration in declarations)
	        {
                // Retrieve or create new declaration
	            MethodDeclarationDocumentationDetails docDetail;
	            if (documentation.ContainsKey(declaration.Key))
	            {
	                docDetail = documentation[declaration.Key];
	            }
	            else
	            {
	                docDetail = new MethodDeclarationDocumentationDetails();
	                docDetail.Declaration = declaration.Value;
                    documentation.Add(declaration.Key, docDetail);
	            }

                foreach (var overload in docDetail.Declaration.Overloads)
                {
                    var methodInfo = (overload.Method as MethodInfo);

                    // Retrieve method declaration documentation attribute from method, if not available skip this method
                    var docAttribute = methodInfo.GetCustomAttribute<MethodDeclarationDocumentationAttribute>();
                    if (docAttribute == null)
                    {
                        continue;
                    }

                    docDetail.Category = docAttribute.Category;
                    docDetail.Summary = docAttribute.Summary;

                    // Retrive method declaration documentation param attributes from method if any available
                    var paramList = new List<MethodDeclarationDocumentationDetailsParam>(docDetail.Params ?? new List<MethodDeclarationDocumentationDetailsParam>());
                    var paramAttributes = methodInfo.GetCustomAttributes<MethodDeclarationDocumentationParamAttribute>();
                    if (paramAttributes != null)
                    {
                        paramList.AddRange(
                            paramAttributes.Select(
                                paramAttribute => new MethodDeclarationDocumentationDetailsParam()
                                {
                                    Name = paramAttribute.Name,
                                    Type = paramAttribute.Type,
                                    Description = paramAttribute.Description
                                }));
                    }
                    docDetail.Params = paramList.Distinct();

                    // Retrive method declaration documentation param attributes from method if any available
                    var exampleList = new List<MethodDeclarationDocumentationDetailsExample>(docDetail.Examples ?? new List<MethodDeclarationDocumentationDetailsExample>());
                    var exampleAttributes = methodInfo.GetCustomAttributes<MethodDeclarationDocumentationExampleAttribute>();
                    if (exampleAttributes != null)
                    {
                        exampleList.AddRange(
                            exampleAttributes.Select(
                                exampleAttribute => new MethodDeclarationDocumentationDetailsExample()
                                {
                                    ImageLink = exampleAttribute.ImageLink,
                                    Code = exampleAttribute.Code
                                }));
                    }
                    docDetail.Examples = exampleList;
                }
            }

            // TODO Link seealso tags

            return documentation;
	    }

	    private static DataTypes[] ResolveDataTypes(Type type)
		{
			DataTypes[] dataTypes;
			if (IsOfType(type, typeof (void)))
			{
				dataTypes = new DataTypes[0];
			}
			else if (
                IsOfType(type, typeof(int)) ||
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
	}
}
