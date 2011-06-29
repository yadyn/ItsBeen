using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using GalaSoft.MvvmLight;

using ItsBeen.App.ViewModels;

namespace ItsBeen.Phone.Views
{
	public partial class CategoryListView : UserControl
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CategoryListView"/> class.
		/// </summary>
		public CategoryListView()
		{
			InitializeComponent();

			CategoryListPicker.SelectionChanged += (s, e) =>
				{
					CollectionViewSource cvs = Resources["FilteredItems"] as CollectionViewSource;

					if (cvs != null && cvs.View != null)
					{
						cvs.View.Refresh();
					}
				};
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="CategoryListView"/> class.
		/// </summary>
		/// <param name="vm">A view model.</param>
		public CategoryListView(CategoryListViewModel vm)
			: this()
		{
			this.DataContext = vm;

			// Workaround for the CVS Source XAML Binding error
			// See: http://stackoverflow.com/questions/4643963/
			CollectionViewSource cvs = Resources["FilteredItems"] as CollectionViewSource;

			if (cvs != null && vm != null)
				cvs.Source = vm.Items;
		}

		private void FilteredItems_Filter(object sender, FilterEventArgs e)
		{
			ItemViewModel itemVM = e.Item as ItemViewModel;
			string category = (CategoryListPicker.SelectedItem ?? String.Empty).ToString();

			if (itemVM == null)
				e.Accepted = false;
			else
				e.Accepted = itemVM.Item.Category == category;
		}
	}
}