using System.Collections.Generic;
using SparkiyEngine.Bindings.Engine;

namespace SparkiyEngine.Engine.Implementation
{
	public class Sparkiy : IEngineBindings
	{
		public event EngineMessagingEventHandler OnMessageCreated;
		private readonly List<EngineMessage> messages; 


	    public Sparkiy()
	    {
		    this.messages = new List<EngineMessage>();
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
    }
}
