using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

using ItsBeen.App.Services;
using ItsBeen.App.ViewModels;
using ItsBeen.Phone.Services;
using ItsBeen.Phone.Views;

namespace ItsBeen.Phone.DesignData
{
	internal class DesignerMainViewModel : MainViewModel
	{
		public DesignerMainViewModel()
			: base(new DesignerListViews(), new MessageBoxService(), new InMemoryItemService(), new NavigationService())
		{
		}

		internal class DesignerListViews : List<object>
		{
			private readonly IItemService itemService = new InMemoryItemService();

			public DesignerListViews()
			{
				Control control = null;

				control = new AllListView();
				control.DataContext = new ListViewModel("All", itemService);
				this.Add(control);
				control = new RecentListView();
				control.DataContext = new ListViewModel("Recent", itemService);
				this.Add(control);
				control = new CategoryListView();
				control.DataContext = new CategoryListViewModel("Categorized", itemService);
				this.Add(control);
			}
		}
	}
}
