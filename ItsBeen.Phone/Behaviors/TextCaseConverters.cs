using System;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace ItsBeen.Phone.Behaviors
{
	public class ToUpperTextConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null || value.GetType() != typeof(String))
				return value;
			
			string text = (string)value;
			return text.ToUpper();
		}
		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
	public class ToLowerTextConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null || value.GetType() != typeof(String))
				return value;

			string text = (string)value;
			return text.ToLower();
		}
		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}