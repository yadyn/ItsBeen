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
		}
	}
}