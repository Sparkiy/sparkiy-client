using System.Collections.Generic;
using SparkiyEngine.Bindings.Component.Engine;
using SparkiyEngine.Bindings.Component.Language;
using SparkiyEngine.Bindings.Component.Graphics;
using SparkiyEngine.Bindings.Component.Common;
using SparkiyEngine.Bindings.Component.Common.Attributes;
using System.Reflection;
using System;

namespace SparkiyEngine.Engine
{
	public class Sparkiy : IEngineBindings
	{
		private ILanguageBindings languageBindings;
		private IGraphicsBindings graphicsBindings;


		/// <summary>
		/// Initializes a new instance of the <see cref="Sparkiy"/> class.
		/// </summary>
		public Sparkiy()
		{
		}


		/// <summary>
		/// Assigns the bindings (sub-systems).
		/// </summary>
		/// <param name="language">The language that language binding represents.</param>
		/// <param name="languageBindings">The language bindings.</param>
		/// <param name="graphicsBindings">The graphics bindings.</param>
		public void AssignBindings(SupportedLanguages language, ILanguageBindings languageBindings, IGraphicsBindings graphicsBindings)
		{
			this.languageBindings = languageBindings;
			this.graphicsBindings = graphicsBindings;

			// Map methods Graphics > Language
			this.LanguageBindings.MapToGraphicsMethods(
				MethodDeclarationResolver.ResolveAll(
					this.GraphicsBindings.GetType(),
					language));
		}

	    public void CallDrawFunction()
	    {
            // Call use draw method
	        this.LanguageBindings.CallMethod("Draw", new MethodDeclarationOverloadDetails() {Type = MethodTypes.Call}, new object[] {});
	    }

	    #region Messages

		/// <summary>
		/// The messages queue.
		/// </summary>
		private readonly List<EngineMessage> messages = new List<EngineMessage>();

		/// <summary>
		/// Occurs when message was created.
		/// </summary>
		public event EngineMessagingEventHandler OnMessageCreated;

		/// <summary>
		/// Adds the message to the list of received messages.
		/// </summary>
		/// <param name="message">The message to add.</param>
		public void AddMessage(EngineMessage message)
		{
			this.messages.Add(message);

			if (this.OnMessageCreated != null)
				this.OnMessageCreated(this);
		}

		/// <summary>
		/// Gets the messages that are currently on queue.
		/// </summary>
		/// <returns>
		/// Returns array of messages that are cached.
		/// </returns>
		public EngineMessage[] GetMessages()
		{
			return this.messages.ToArray();
		}

		/// <summary>
		/// Clears the messages queue.
		/// </summary>
		public void ClearMessages()
		{
			this.messages.Clear();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Handles method requests from sub-systems.
		/// </summary>
		/// <param name="declaration">The method declaration.</param>
		/// <param name="overload">The selected method overload. Must be one from declaration.</param>
		/// <param name="inputValues">The input values.</param>
		/// <returns>Returns object with return value.</returns>
		public object MethodRequested(MethodDeclarationDetails declaration, MethodDeclarationOverloadDetails overload, Object[] inputValues)
		{
			var method = (MethodInfo)overload.Method;
			return method.Invoke(this.GraphicsBindings, inputValues);
		}

		#endregion Methods

		/// <summary>
		/// Resets this instance and all sub-systems.
		/// </summary>
		public void Reset()
		{
			// Clear subsystems
			this.LanguageBindings.Reset();
			this.GraphicsBindings.Reset();

			// Clear engine
			this.ClearMessages();
		}

		/// <summary>
		/// Gets the language bindings.
		/// </summary>
		/// <value>
		/// The language bindings.
		/// </value>
		public ILanguageBindings LanguageBindings
		{
			get { return this.languageBindings; }
		}

		/// <summary>
		/// Gets the graphics bindings.
		/// </summary>
		/// <value>
		/// The graphics bindings.
		/// </value>
		public IGraphicsBindings GraphicsBindings
		{
			get { return this.graphicsBindings; }
		}
    }
}
