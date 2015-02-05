using System.Collections.Generic;
using SparkiyEngine.Bindings.Component.Engine;
using SparkiyEngine.Bindings.Component.Language;
using SparkiyEngine.Bindings.Component.Graphics;
using SparkiyEngine.Bindings.Component.Common;
using SparkiyEngine.Bindings.Component.Common.Attributes;
using System.Reflection;
using System;
using System.Diagnostics.Contracts;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;
using MetroLog;
using SparkiyEngine.Input;

namespace SparkiyEngine.Engine
{
    internal class ScriptManager
    {
        private readonly List<string> activeScripts = new List<string>(); 
        private readonly List<string> inactiveScripts = new List<string>();

        public bool HasInactiveScripts => this.inactiveScripts.Any();

        public void AddScript(string name)
        {
            if (this.activeScripts.Contains(name))
                throw new InvalidOperationException("Script already added.");

            this.activeScripts.Add(name);
        }

        public void Reset()
        {
            this.activeScripts.Clear();
            this.inactiveScripts.Clear();
        }
    }

	public class Sparkiy : IEngineBindings
	{
		private static ILogger Log = LogManagerFactory.DefaultLogManager.GetLogger<Sparkiy>();

		private ILanguageBindings languageBindings;
		private IGraphicsBindings graphicsBindings;
	    private IGraphicsSettings graphicsSettings;

	    private ScriptManager scriptManager;
	    private PointerManager pointerManager;

	    private bool isInitialized;
	    private bool isReset;

	    private DateTime startedTime;
		private DateTime lastFrameTime;


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
		public void AssignBindings(ILanguageBindings languageBindings, IGraphicsSettings graphicsSettings)
		{
            Contract.Requires(languageBindings != null);
            Contract.Requires(graphicsSettings != null);
            Contract.Requires(graphicsSettings.GraphicsBindings != null);
            Contract.Requires(graphicsSettings.Panel != null);

            this.languageBindings = languageBindings;
		    this.graphicsSettings = graphicsSettings;
			this.graphicsBindings = this.graphicsSettings.GraphicsBindings;
		}

	    public void AssignPanel(object panel)
	    {
            Contract.Requires(panel != null);

            this.graphicsSettings.AssignPanel(panel);
        }
	    public void Initialize()
	    {
            // Map methods Graphics > Language
            this.LanguageBindings.MapToGraphicsMethods(
                MethodDeclarationResolver.ResolveAll(
                    this.GraphicsBindings.GetType(),
                    this.LanguageBindings.Language));

            // Instantiate pointer manager
            if (!(this.graphicsSettings.Panel is UIElement))
                throw new InvalidCastException("Panel must be of type UIElement in order to use PointerManager");
            this.pointerManager = new PointerManager((UIElement)this.graphicsSettings.Panel, this);

            // Instantiate script manager
            this.scriptManager = new ScriptManager();

            this.isReset = true;
            this.isInitialized = true;
        }

	    public void Play()
	    {
	        if (!this.isInitialized)
                throw new InvalidOperationException("Initialize engine before calling play.");

		    if (this.isReset)
		    {
			    this.startedTime = this.lastFrameTime = DateTime.Now;
		    }

		    this.GraphicsBindings.Play();

            if (this.scriptManager.HasInactiveScripts)
                throw new NotImplementedException();

            this.CallStarted();
	    }
        
	    public void Pause()
	    {
            this.GraphicsBindings.Pause();

            if (this.scriptManager.HasInactiveScripts)
                throw new NotImplementedException();

            this.CallStopped();
	    }

		public void Stop()
		{
			this.Reset();
			this.GraphicsBindings.Stop();
		}

		public bool AddScript(string name, string code)
	    {
			Contract.Requires(!String.IsNullOrWhiteSpace(name));

			Log.Debug("Added script \"{0}\"", name);

	        this.scriptManager.AddScript(name);
		    try
		    {
			    bool isLoaded = this.LanguageBindings.LoadScript(name, code ?? String.Empty);
				if (!isLoaded)
					throw new Exception();

				this.CallCreated(name);

			    return true;
		    }
		    catch (Exception ex)
		    {
				this.AddMessage(new EngineMessage()
				{
					Message = this.LanguageBindings.GetError().Message,
					Source = this.LanguageBindings,
					SourceType = BindingTypes.Language,
					IsError = true
				});

				this.Reset();
			}

		    return false;
	    }

	    public void AddImageAsset(string name, WriteableBitmap imageAsset)
	    {
	        this.GraphicsBindings.AddImageAsset(name, imageAsset);
	    }

	    public void CallDrawFunction()
	    {
            if (this.scriptManager.HasInactiveScripts)
                throw new NotImplementedException();

			// Calculate delta
		    var delta = DateTime.Now - this.lastFrameTime;
		    this.lastFrameTime = DateTime.Now;

			// Calculate total time
		    var total = DateTime.Now - this.startedTime;

			this.LanguageBindings.SetVariable("DELTA", delta.TotalMilliseconds, DataTypes.Number);
			this.LanguageBindings.SetVariable("TOTAL", total.TotalMilliseconds, DataTypes.Number);

			// Call touched method if there are any pointers active
			if (this.pointerManager.PrimaryPointer != null)
	        {
	            if (this.pointerManager.PrimaryPointer.InGameType != InputTypes.NotTracked)
	                this.CallTouched(
                        this.pointerManager.PrimaryPointer.InGameType,
	                    this.pointerManager.PrimaryPointer.X,
	                    this.pointerManager.PrimaryPointer.Y);

	            this.pointerManager.PrimaryPointer.UpdateType();
	        }

			// Call use draw method
			this.CallFunction(null, "draw", MethodTypes.Call);
	    }

	    private void CallCreated(string script)
	    {
		    this.AddMessage(new EngineMessage()
		    {
			    Message = "Script \"" + script + "\" created",
			    SourceType = BindingTypes.Engine,
			    Source = this
		    });

	        this.CallFunction(script, "created", MethodTypes.Call);
	    }

	    private void CallStarted(string script = null)
	    {
			this.AddMessage(new EngineMessage()
			{
				Message = script == null ? "All scripts started" : "Script \"" + script + "\" started",
				SourceType = BindingTypes.Engine,
				Source = this
			});

			this.CallFunction(script, "started", MethodTypes.Call);
	    }

        private void CallTouched(InputTypes type, float x, float y, string script = null)
	    {
	        this.CallFunction(script, "touched", MethodTypes.Set, new Dictionary<object, DataTypes>
	        {
	            {(double) type, DataTypes.Number },
                {(double) x, DataTypes.Number },
                {(double) y, DataTypes.Number }
            });
        }

	    private void CallStopped(string script = null)
	    {
			this.AddMessage(new EngineMessage()
			{
				Message = script == null ? "All scripts stopped" : "Script \"" + script + "\" stopped",
				SourceType = BindingTypes.Engine,
				Source = this
			});

			this.CallFunction(script, "stopped", MethodTypes.Call);
	    }

	    public object CallFunction(string script, string name, MethodTypes type, Dictionary<object, DataTypes> inputParameters = null)
	    {
		    try
		    {
			    if (script != null)
			    {
				    return this.LanguageBindings.CallMethod(script, name,
					    new MethodDeclarationOverloadDetails()
					    {
						    Type = type,
						    Input = inputParameters?.Values.ToArray() ?? new DataTypes[0]
					    },
					    inputParameters?.Keys.ToArray() ?? new object[0]);
			    }
			    else
			    {
				    return this.LanguageBindings.CallMethod(name,
					    new MethodDeclarationOverloadDetails()
					    {
						    Type = type,
						    Input = inputParameters?.Values.ToArray() ?? new DataTypes[0]
					    },
					    inputParameters?.Keys.ToArray() ?? new object[0]);
			    }
		    }
		    catch (Exception ex)
		    {
				this.Reset();

				this.AddMessage(new EngineMessage()
				{
					Message = this.LanguageBindings.GetError().Message,
					Source = this,
					SourceType = BindingTypes.Engine,
					IsError = true
				});

			    return null;
		    }
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

		    this.OnMessageCreated?.Invoke(this);
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

            // Clear scripts
		    this.scriptManager.Reset();

		    this.isReset = true;
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

		public void Dispose()
		{
			this.scriptManager.Reset();
			this.scriptManager = null;

			this.pointerManager = null;

			this.GraphicsBindings.Stop();
			this.graphicsSettings = null;

			this.LanguageBindings.Reset();
			this.languageBindings = null;
		}
	}
}
