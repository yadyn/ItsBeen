using System;
using System.Windows;
using System.Windows.Data;

namespace ItsBeen.Phone.Behaviors
{
	public class NotConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (targetType != typeof(bool))
			{
				throw new InvalidOperationException();
			}
			return !(bool)value;
		}
		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (targetType != typeof(bool))
			{
				throw new InvalidOperationException();
			}
			return !(bool)value;
		}
	}
	public class BooleanVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (targetType != typeof(Visibility))
			{
				throw new InvalidOperationException();
			}
			return (((bool)value) ? Visibility.Visible : Visibility.Collapsed);
		}
		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (targetType != typeof(bool))
			{
				throw new InvalidOperationException();
			}
			return (((Visibility)value) == Visibility.Visible);
		}
	}
	public class NotBooleanVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (targetType != typeof(Visibility))
			{
				throw new InvalidOperationException();
			}
			return ((!(bool)value) ? Visibility.Visible : Visibility.Collapsed);
		}
		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (targetType != typeof(bool))
			{
				throw new InvalidOperationException();
			}
			return (((Visibility)value) != Visibility.Visible);
		}
	}
}
