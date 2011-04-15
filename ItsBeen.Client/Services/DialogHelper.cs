using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ItsBeen.Client.Services
{
	internal class DialogHelper
	{
		internal static Window TryGetOwnerFromSender(object sender)
		{
			if (sender == null)
				return null;

			if (sender is Window)
				return (sender as Window);

			return (from window in Application.Current.Windows.OfType<Window>()
					where window.DataContext == sender
					select window)
					.AsEnumerable()
					.FirstOrDefault();
		}
	}
}
