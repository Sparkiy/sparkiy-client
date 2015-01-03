using System;

namespace SparkiyClient.Converters
{
	public class StringFormatConverter : Windows.UI.Xaml.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string arg = System.Convert.ToString(value);

            return string.Format((string)parameter, arg);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}