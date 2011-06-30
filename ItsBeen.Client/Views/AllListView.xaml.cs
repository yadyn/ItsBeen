using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ItsBeen.Client.Views
{
	/// <summary>
	/// Interaction logic for AllListView.xaml
	/// </summary>
	public partial class AllListView : UserControl
	{
		public AllListView()
		{
			this.InitializeComponent();

			ItemsList.SelectionChanged += (s, e) =>
				{
					ICollectionView dataView = CollectionViewSource.GetDefaultView(ItemsList.ItemsSource);

					if (dataView != null)
						dataView.Refresh();
				};
		}
	}
}