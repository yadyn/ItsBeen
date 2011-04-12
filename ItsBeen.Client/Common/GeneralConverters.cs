using System;
using System.Windows;
using System.Windows.Data;

namespace ItsBeen.Client.Common
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
	public class IsNullOrEmptyVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return (String.IsNullOrEmpty((string)value) ? Visibility.Visible : Visibility.Collapsed);
		}
		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
	public class NotNullOrEmptyVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return (String.IsNullOrEmpty((string)value) ? Visibility.Collapsed : Visibility.Visible);
		}
		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
	public class IsNullVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return ((value == null) ? Visibility.Visible : Visibility.Collapsed);
		}
		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
	public class NotNullVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return ((value == null) ? Visibility.Collapsed : Visibility.Visible);
		}
		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
