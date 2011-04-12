using System;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace ItsBeen.Phone.Behaviors
{
	public class TimeSinceConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null)
				return null;
			if (targetType != typeof(string))
			{
				throw new InvalidOperationException();
			}
			return GetTimeSince((TimeSpan)value);
		}
		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null)
				return null;
			if (targetType != typeof(TimeSpan))
			{
				throw new InvalidOperationException();
			}
			// The conversion back isn't really used and it would be annoying to implement, so eh
			return TimeSpan.Zero;
		}

		private string GetTimeSince(TimeSpan timeSince)
		{
			StringBuilder str = new StringBuilder();

			DateTime age = DateTime.MinValue.Add(timeSince);

			// MinValue contributes 1 of each, so we must account for that
			int years = age.Year - 1;
			int months = age.Month - 1;
			int days = age.Day - 1;

			if (years > 0)
			{
				str.AppendFormat("{0} year{1}",
					years,
					(years == 1) ? "" : "s");
			}
			if (months > 0)
			{
				if (str.Length > 0)
					str.Append(", ");
				str.AppendFormat("{0} month{1}",
					months,
					(months == 1) ? "" : "s");
			}
			if (days > 0)
			{
				if (str.Length > 0)
					str.Append(", ");
				str.AppendFormat("{0} day{1}",
					days,
					(days == 1) ? "" : "s");
			}
			if (timeSince.Hours > 0)
			{
				if (str.Length > 0)
					str.Append(", ");
				str.AppendFormat("{0} hour{1}",
					timeSince.Hours,
					(timeSince.Hours == 1) ? "" : "s");
			}
			if (timeSince.Minutes > 0)
			{
				if (str.Length > 0)
					str.Append(", ");
				str.AppendFormat("{0} minute{1}",
					timeSince.Minutes,
					(timeSince.Minutes == 1) ? "" : "s");
			}
			if (timeSince.Seconds > 0)
			{
				if (str.Length > 0)
					str.Append(", ");
				str.AppendFormat("{0} second{1}",
					timeSince.Seconds,
					(timeSince.Seconds == 1) ? "" : "s");
			}

			return str.ToString();
		}
	}
}
