using System.Collections.Generic;
using SparkiyEngine.Bindings.Component.Engine;
using SparkiyEngine.Bindings.Component.Language;
using SparkiyEngine.Bindings.Component.Graphics;
using SparkiyEngine.Bindings.Component.Common;
using SparkiyEngine.Bindings.Component.Common.Attributes;
using System.Reflection;
using System;
using System.Diagnostics.Contracts;
using Windows.UI.Xaml;
using SparkiyEngine.Input;

namespace SparkiyEngine.Engine
{
	public class Sparkiy : IEngineBindings
	{
		private ILanguageBindings languageBindings;
		private IGraphicsBindings graphicsBindings;
	    private IGraphicsSettings graphicsSettings;

	    private PointerManager pointerManager;


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
		public void AssignBindings(SupportedLanguages language, ILanguageBindings languageBindings, IGraphicsSettings graphicsSettings)
		{
            Contract.Requires(languageBindings != null);
            Contract.Requires(graphicsSettings != null);
            Contract.Requires(graphicsSettings.GraphicsBindings != null);
            Contract.Requires(graphicsSettings.Panel != null);

            this.languageBindings = languageBindings;
		    this.graphicsSettings = graphicsSettings;
			this.graphicsBindings = this.graphicsSettings.GraphicsBindings;

			// Map methods Graphics > Language
			this.LanguageBindings.MapToGraphicsMethods(
				MethodDeclarationResolver.ResolveAll(
					this.GraphicsBindings.GetType(),
					language));
		}

	    public void AssignPanel(object panel)
	    {
            this.graphicsSettings.AssignPanel(panel);

            // Instantiate pointer manager
            if (!(this.graphicsSettings.Panel is UIElement))
                throw new InvalidCastException("Panel must be of type UIElement in order to use PointerManager");
            this.pointerManager = new PointerManager((UIElement)this.graphicsSettings.Panel, this);
        }

	    public void CallDrawFunction()
	    {
            // Call touched method if there are any pointers active
	        if (this.pointerManager.PrimaryPointer != null)
	        {
	            if (this.pointerManager.PrimaryPointer.InGameType != InputTypes.NotTracked)
	                this.LanguageBindings.CallMethod("Touched",
	                    new MethodDeclarationOverloadDetails()
	                    {
	                        Type = MethodTypes.Set,
	                        Input = new[]
	                        {
	                            DataTypes.Number,
                                DataTypes.Number,
                                DataTypes.Number
	                        }
	                    }, new object[]
	                    {
	                        (double)this.pointerManager.PrimaryPointer.InGameType,
	                        (double)this.pointerManager.PrimaryPointer.X,
                            (double)this.pointerManager.PrimaryPointer.Y
	                    });

	            this.pointerManager.PrimaryPointer.UpdateType();
	        }

	        //if (this.pointerManager.PrimaryPointer.)

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

        /// <summary>
        /// Gets the graphics settings.
        /// </summary>
        /// <value>
        /// The graphics settings.
        /// </value>
        public IGraphicsSettings GraphicsSettings
	    {
	        get { return this.graphicsSettings; }
	    }
    }
}
