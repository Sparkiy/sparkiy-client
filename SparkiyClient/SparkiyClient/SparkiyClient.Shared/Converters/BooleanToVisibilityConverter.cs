using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace SparkiyClient.Converters
{
	public class ToVisibilityConverter<T> : IValueConverter
	{
		/// <summary>
		/// Set to true if you want to show control when expression value is true. Set to false if you want to hide/collapse control when expression value is true.
		/// </summary>
		public bool TriggerValue { get; set; }

		/// <summary>
		/// Set to true if you just want to hide the control else set to false if you want to collapse the control
		/// </summary>
		public bool IsHidden { get; set; }

		/// <summary>
		/// Expression that is evaluated on passed value.
		/// </summary>
		public Func<T, bool> ExpressionFunc { get; set; }


		private object GetVisibility(object value)
		{
			if (value != null && !(value is T))
				return DependencyProperty.UnsetValue;
			bool objValue = this.ExpressionFunc.Invoke((T) value);

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

	/// <summary>
	/// Converts null values to Control.Visibility values
	/// </summary>
	public class NullToVisibilityConverter : ToVisibilityConverter<object>
	{
		public NullToVisibilityConverter()
		{
			this.ExpressionFunc = o => o == null;
		}
	}

	/// <summary>
	/// Converts Boolean Values to Control.Visibility values
	/// </summary>
	public class BooleanToVisibilityConverter : ToVisibilityConverter<bool>
	{
		public BooleanToVisibilityConverter()
		{
			this.ExpressionFunc = b => b;
		}
	}
}
