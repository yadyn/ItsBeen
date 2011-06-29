using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

using Microsoft.Phone.Controls;
using Microsoft.Phone.Reactive;

using ItsBeen.App.ViewModels;

namespace ItsBeen.Phone.Views
{
	public partial class RecentListView : UserControl
	{
		private readonly TimeSpan recentTimeSpan = new TimeSpan(7, 0, 0, 0);

		/// <summary>
		/// Initializes a new instance of the <see cref="RecentListView"/> class.
		/// </summary>
		public RecentListView()
		{
			InitializeComponent();
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="RecentListView"/> class.
		/// </summary>
		/// <param name="vm">A view model.</param>
		public RecentListView(ListViewModel vm)
			: this()
		{
			this.DataContext = vm;

			SetCVSSource(vm);

			if (vm != null)
			{
				vm.PropertyChanged += (s, e) =>
				{
					if (e.PropertyName == "Items")
						SetCVSSource(vm);
				};
				vm.Items.CollectionChanged += (s, e) =>
				{
					RefreshCVSSource();
				};
			}
		}

		private void RecentItems_Filter(object sender, FilterEventArgs e)
		{
			ItemViewModel itemVM = e.Item as ItemViewModel;

			if (itemVM == null)
				e.Accepted = false;
			else
				e.Accepted = itemVM.Item.LastModified > DateTime.Now.Subtract(recentTimeSpan);
		}

		private void SetCVSSource(ListViewModel vm)
		{
			CollectionViewSource cvs = Resources["RecentItems"] as CollectionViewSource;

			if (cvs != null && vm != null)
				cvs.Source = vm.Items;
		}
		private void RefreshCVSSource()
		{
			CollectionViewSource cvs = Resources["RecentItems"] as CollectionViewSource;

			if (cvs != null && cvs.View != null)
			{
				cvs.View.Refresh();
			}
		}
	}
}