using SparkiyEngine.Bindings.Component.Engine;
using SparkiyEngine.Engine;
using SparkiyEngine.Graphics;
using SparkiyEngine_Language_LuaImplementation;

namespace SparkiyClient.Services
{
	public class EngineProviderService
	{
		public IEngineBindings GetLuaDxEngine(object panel)
		{
			var engine = new Sparkiy();
			var language = LuaImplementation.Instantiate(engine);
			var graphics = new Renderer(engine, panel);

			engine.AssignBindings(language.GetLanguageBindings(), graphics);

			return engine;
		}
	}
}