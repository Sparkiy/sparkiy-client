using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace SparkiyClient.Converters
{
	/// <summary>
	/// Converts Boolean Values to Control.Visibility values
	/// </summary>
	public class BooleanToVisibilityConverter : IValueConverter
	{
		/// <summary>
		/// Set to true if you want to show control when boolean value is true. Set to false if you want to hide/collapse control when value is true
		/// </summary>
		public bool TriggerValue { get; set; } = false;

		/// <summary>
		/// Set to true if you just want to hide the control else set to false if you want to collapse the control
		/// </summary>
		public bool IsHidden { get; set; }

		/// <summary>
		/// Set to true if you want result to be inverted.
		/// </summary>
		public bool IsNegated { get; set; }

		private object GetVisibility(object value)
		{
			if (!(value is bool))
				return DependencyProperty.UnsetValue;
			bool objValue = (bool)value;
			if (this.IsNegated)
				objValue = !objValue;

			if ((objValue && TriggerValue && IsHidden) || (!objValue && !TriggerValue && IsHidden))
			{
				return Visibility.Collapsed;
			}
			if ((objValue && TriggerValue && !IsHidden) || (!objValue && !TriggerValue && !IsHidden))
			{
				return Visibility.Collapsed;
			}
			return Visibility.Visible;
		}

		public object Convert(object value, Type targetType, object parameter, string culture)
		{
			return GetVisibility(value);
		}

		public object ConvertBack(object value, Type targetType, object parameter, string culture)
		{
			throw new NotImplementedException();
		}
	}
}
