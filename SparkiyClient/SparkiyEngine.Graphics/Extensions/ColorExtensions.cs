using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using Windows.UI;
using Windows.UI.Xaml.Markup;
using Color = Microsoft.Xna.Framework.Color;

namespace SparkiyEngine.Graphics.Extensions
{
	/// <summary>
	/// Extension and helper methods for converting color values
	/// between different RGB data types and different color spaces.
	/// </summary>
	public static class ColorExtensions
	{
		#region IntColorFromBytes()
		/// <summary>
		/// Converts four bytes to an Int32 - 4 byte ARGB structure.
		/// </summary>
		/// <param name="a"></param>
		/// <param name="r"></param>
		/// <param name="g"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static int IntColorFromBytes(byte a, byte r, byte g, byte b)
		{
			var col =
				a << 24
				| r << 16
				| g << 8
				| b;
			return col;
		}
		#endregion
	}
}
