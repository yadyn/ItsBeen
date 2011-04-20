using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

using ItsBeen.App.Services;

namespace ItsBeen.Phone.Services
{
	public class NavigationService : INavigationService
	{
		internal static System.Windows.Navigation.NavigationService Service { get; set; }
		
		public void ShowEdit(ItsBeen.App.Model.ItemModel item)
		{
			Service.Navigate(new Uri("/Views/EditItemView.xaml", UriKind.Relative));
		}
	}
}
