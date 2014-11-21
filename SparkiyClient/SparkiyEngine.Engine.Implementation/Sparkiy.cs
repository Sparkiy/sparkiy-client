using System.Collections.Generic;
using SparkiyEngine.Bindings.Engine;
using SparkiyEngine.Bindings.Language;
using SparkiyEngine.Bindings.Graphics;
using SparkiyEngine.Bindings.Common.Attributes;
using System.Reflection;
using SparkiyEngine.Bindings.Common.Component;
using SparkiyEngine.Bindings.Common;

namespace SparkiyEngine.Engine.Implementation
{
	public class Sparkiy : IEngineBindings
	{
		private readonly ILanguageBindings languageBindings;
		private readonly IGraphicsBindings graphicsBindings;

		public event EngineMessagingEventHandler OnMessageCreated;
		private readonly List<EngineMessage> messages;


		public Sparkiy(SupportedLanguages language, ILanguageBindings languageBindings, IGraphicsBindings graphicsBindings)
		{
			this.languageBindings = languageBindings;
			this.graphicsBindings = graphicsBindings;

			this.messages = new List<EngineMessage>();

			// Map methods Graphics > Language
			this.LanguageBindings.MapToGraphicsMethods(
				MethodDeclarationResolver.ResolveAll(
					this.GraphicsBindings.GetType(),
					language));

			// Map methods Graphics < Language
			this.LanguageBindings.OnMethodRequested += (sender, args) =>
			{
				var method = (MethodInfo)args.Overload.Method;
				method.Invoke(this.GraphicsBindings, args.InputValues);
			};

			// Catch messsage creating
			this.LanguageBindings.OnMessageCreated += (sender, args) =>
			{
				var message = new EngineMessage()
				{
					Message = args.Message,
					Source = sender as ILanguageBindings,
					SourceType = BindingTypes.Language
				};

				this.HandleMessageCreated(message);
			};

			// Connect functions
			this.GraphicsBindings.Pre2DDraw += s => this.LanguageBindings.CallMethod(
				"Draw", new MethodDeclarationOverloadDetails() { Type = MethodTypes.Call }, new object[] { });
		}


		public void HandleMessageCreated(EngineMessage message)
		{
			this.messages.Add(message);

			if (this.OnMessageCreated != null)
				this.OnMessageCreated(this);
		}

		public EngineMessage[] GetMessages()
		{
			return this.messages.ToArray();
		}

		public void ClearMessages()
		{
			this.messages.Clear();
		}

		public void Reset()
		{
			this.ClearMessages();
		}

		public ILanguageBindings LanguageBindings
		{
			get { return this.languageBindings; }
		}

		public IGraphicsBindings GraphicsBindings
		{
			get { return this.graphicsBindings; }
		}
    }
}
