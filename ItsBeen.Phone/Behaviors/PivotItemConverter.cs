using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;

using Microsoft.Phone.Controls;

namespace ItsBeen.Phone.Behaviors
{
	public class PivotItemConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			IEnumerable<object> values = (IEnumerable<object>)value;

			ObservableCollection<object> newValues = new ObservableCollection<object>();

			foreach (object obj in values)
			{
				PivotItem pvItem = new PivotItem();
				pvItem.Content = obj;
				if (obj is Control)
				{
					pvItem.DataContext = (obj as Control).DataContext;
					pvItem.Header = (obj as Control).DataContext;
				}
				newValues.Add(pvItem);
			}

			return newValues;
		}
		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
