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
	/// Interaction logic for CategoryListView.xaml
	/// </summary>
	public partial class CategoryListView : UserControl
	{
		public CategoryListView()
		{
			this.InitializeComponent();

			CategoryComboBox.SelectionChanged += (s, e) =>
				{
					CollectionViewSource cvs = FindResource("FilteredItems") as CollectionViewSource;

					if (cvs != null && cvs.View != null)
					{
						cvs.View.Refresh();
					}
				};
		}

		private void FilteredItems_Filter(object sender, FilterEventArgs e)
		{
			ItemViewModel itemVM = e.Item as ItemViewModel;
			string category = (CategoryComboBox.SelectedValue ?? String.Empty).ToString();

			if (itemVM == null)
				e.Accepted = false;
			else
				e.Accepted = itemVM.Item.Category == category;
		}
	}
}