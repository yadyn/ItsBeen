using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using ItsBeen.App.ViewModels;

namespace ItsBeen.Client.Views
{
	/// <summary>
	/// Interaction logic for RecentListView.xaml
	/// </summary>
	public partial class RecentListView : UserControl
	{
		private readonly TimeSpan recentTimeSpan = new TimeSpan(7, 0, 0, 0);

		public RecentListView()
		{
			this.InitializeComponent();
		}

		private void RecentItems_Filter(object sender, FilterEventArgs e)
		{
			ItemViewModel itemVM = e.Item as ItemViewModel;

			if (itemVM == null)
				e.Accepted = false;
			else
				e.Accepted = itemVM.Item.LastModified > DateTime.Now.Subtract(recentTimeSpan);
		}
	}
}