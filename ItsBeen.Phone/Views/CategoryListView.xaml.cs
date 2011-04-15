using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using GalaSoft.MvvmLight;

namespace ItsBeen.Phone.Views
{
	/// <summary>
	/// Description for CategoryListView.
	/// </summary>
	public partial class CategoryListView : UserControl
	{
		private ItsBeen.App.ViewModels.ListViewModel ViewModel
		{
			get
			{
				return DataContext as ItsBeen.App.ViewModels.ListViewModel;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CategoryListView"/> class.
		/// </summary>
		public CategoryListView()
		{
			InitializeComponent();
		}
	}
}