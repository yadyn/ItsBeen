using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

using ItsBeen.App.Services;
using ItsBeen.App.ViewModels;
using ItsBeen.Client.Services;
using ItsBeen.Client.Views;

namespace ItsBeen.Client.DesignData
{
	internal class DesignerMainViewModel : MainViewModel
	{
		public DesignerMainViewModel()
			: base(new DesignerListViews(), new TaskDialogService(), new InMemoryItemService(), new NavigationService())
		{
		}

		internal class DesignerListViews : List<object>
		{
			private readonly IItemService itemService = new InMemoryItemService();

			public DesignerListViews ()
			{
				Control control = null;

				control = new DefaultListView();
				control.DataContext = new ListViewModel("All", itemService);
				this.Add(control);
				control = new DefaultListView();
				control.DataContext = new ListViewModel("Recent", itemService);
				this.Add(control);
				control = new FilterListView();
				control.DataContext = new ListViewModel("Categorized", itemService);
				this.Add(control);
			}
		}
	}
}
