using System.Diagnostics.Contracts;
using SparkiyEngine.Bindings.Component.Language;

namespace SparkiyEngine.Bindings.Component.Graphics
{
	/// <summary>
	/// Graphics Settings manager should implement this interface
	/// </summary>
	public interface IGraphicsSettings
	{
		/// <summary>
		/// Gets or sets a value indicating whether this instance is mouse visible.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is mouse visible; otherwise, <c>false</c>.
		/// </value>
		bool IsMouseVisible { get; set; }

		/// <summary>
		/// Assigns the panel as drawing surface.
		/// </summary>
		/// <param name="panel">The panel to assign.</param>
		void AssignPanel(object panel);

		/// <summary>
		/// Gets the graphics bindings.
		/// </summary>
		/// <value>
		/// The graphics bindings.
		/// </value>
		IGraphicsBindings GraphicsBindings { get; }

        /// <summary>
        /// Gets the panel.
        /// </summary>
        /// <value>
        /// The panel.
        /// </value>
        object Panel { get; }
	}
}