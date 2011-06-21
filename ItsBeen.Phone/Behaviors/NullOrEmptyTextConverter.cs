using System;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace ItsBeen.Phone.Behaviors
{
	public class NullOrEmptyTextConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return String.IsNullOrEmpty((value ?? String.Empty).ToString()) ? parameter : value;
		}
		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}