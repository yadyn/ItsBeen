using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ItsBeen.App.Services;

namespace ItsBeen.Client.Services
{
	public class NavigationService : INavigationService
	{
		public void ShowEdit(ItsBeen.App.Model.ItemModel item)
		{
			EditItemWindow window = new EditItemWindow();
			window.Owner = System.Windows.Application.Current.MainWindow;
			window.ShowDialog();
		}
	}
}
