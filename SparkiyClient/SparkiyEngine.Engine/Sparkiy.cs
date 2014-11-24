using System.Collections.Generic;
using SparkiyEngine.Bindings.Component.Engine;
using SparkiyEngine.Bindings.Component.Language;
using SparkiyEngine.Bindings.Component.Graphics;
using SparkiyEngine.Bindings.Component.Common;
using SparkiyEngine.Bindings.Component.Common.Attributes;
using System.Reflection;

namespace SparkiyEngine.Engine
{
	public class Sparkiy : IEngineBindings
	{
		private ILanguageBindings languageBindings;
		private IGraphicsBindings graphicsBindings;

		public event EngineMessagingEventHandler OnMessageCreated;
		private readonly List<EngineMessage> messages;


		public Sparkiy()
		{
			this.messages = new List<EngineMessage>();
		}

		public void AssignBindings(SupportedLanguages language, ILanguageBindings languageBindings, IGraphicsBindings graphicsBindings)
		{
			this.languageBindings = languageBindings;
			this.graphicsBindings = graphicsBindings;

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

			// Connect functions
			this.GraphicsBindings.Pre2DDraw += s => this.LanguageBindings.CallMethod(
				"Draw", new MethodDeclarationOverloadDetails() { Type = MethodTypes.Call }, new object[] { });
		}


		#region Messages

		public void AddMessage(EngineMessage message)
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

		#endregion

		public void Reset()
		{
			// Clear subsystems
			this.LanguageBindings.Reset();
			this.GraphicsBindings.Reset();

			// Clear engine
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
