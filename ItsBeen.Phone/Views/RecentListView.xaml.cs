using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using GalaSoft.MvvmLight;

namespace ItsBeen.Phone.Views
{
	/// <summary>
	/// Description for RecentListView.
	/// </summary>
	public partial class RecentListView : UserControl
	{
		private ItsBeen.App.ViewModels.ListViewModel ViewModel
		{
			get
			{
				return DataContext as ItsBeen.App.ViewModels.ListViewModel;
			}
		}

		/// <summary>
		/// Initializes a new instance of the RecentListView class.
		/// </summary>
		public RecentListView()
		{
			InitializeComponent();

			CollectionViewSource cvs = this.Resources["recentList"] as CollectionViewSource;
			Binding cvsBinding = new Binding();
			cvsBinding.Source = this.DataContext;
			cvsBinding.Path = new PropertyPath("Items");
			cvsBinding.Mode = BindingMode.OneWay;
			BindingOperations.SetBinding(cvs, CollectionViewSource.SourceProperty, cvsBinding);
		}
	}
}