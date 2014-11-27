using SparkiyEngine.Bindings.Component.Common;
using SparkiyEngine.Bindings.Component.Graphics;
using SparkiyEngine.Bindings.Component.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace SparkiyEngine.Bindings.Component.Engine
{
	public interface IEngineBindings : IBindingsBase
    {
		#region Messages

		/// <summary>
		/// Occurs when message was created.
		/// </summary>
		event EngineMessagingEventHandler OnMessageCreated;

		/// <summary>
		/// Adds the message to the list of received messages.
		/// </summary>
		/// <param name="message">The message to add.</param>
		void AddMessage(EngineMessage message);

		/// <summary>
		/// Gets the messages that are currently on queue.
		/// </summary>
		/// <returns>Returns array of messages that are cached.</returns>
		EngineMessage[] GetMessages();

		/// <summary>
		/// Clears the messages queue.
		/// </summary>
		void ClearMessages();

		#endregion Messages

		#region Methods

		/// <summary>
		/// Handles method requests from sub-systems.
		/// </summary>
		/// <param name="declaration">The method declaration.</param>
		/// <param name="overload">The selected method overload. Must be one from declaration.</param>
		/// <param name="inputValues">The input values.</param>
		/// <returns>Returns object with return value.</returns>
 		object MethodRequested(MethodDeclarationDetails declaration, MethodDeclarationOverloadDetails overload, [ReadOnlyArray] Object[] inputValues);

        #endregion Methods

        #region Functions

	    void CallDrawFunction();

        #endregion

        /// <summary>
        /// Resets this instance and all sub-systems.
        /// </summary>
        void Reset();

		/// <summary>
		/// Gets the graphics bindings.
		/// </summary>
		/// <value>
		/// The graphics bindings.
		/// </value>
		IGraphicsBindings GraphicsBindings { get; }

		/// <summary>
		/// Gets the language bindings.
		/// </summary>
		/// <value>
		/// The language bindings.
		/// </value>
		ILanguageBindings LanguageBindings { get; }
	}
}
