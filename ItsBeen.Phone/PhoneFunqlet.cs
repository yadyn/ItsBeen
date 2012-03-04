using System;
using System.Collections.Generic;
using System.Windows.Controls;

using Funq;

using ItsBeen.App.Services;
using ItsBeen.App.ViewModels;
using ItsBeen.Phone.Services;
using ItsBeen.Phone.Views;

namespace ItsBeen.Phone
{
	public class PhoneFunqlet : IFunqlet
	{
		public void Configure(Container container)
		{
			container.Register<IMessageBoxService>(c => new MessageBoxService());
			container.Register<ITaskDialogService>(c => new MessageBoxService());
			container.Register<INavigationService>(c => new NavigationService());

			container.Register<IEnumerable<object>>("MainListViews", c =>
				{
					Control control = null;
					List<object> listViews = new List<object>();

					control = new AllListView(new ListViewModel("All", c.Resolve<IItemService>()));
					listViews.Add(control);
					control = new RecentListView(new ListViewModel("Recent", c.Resolve<IItemService>()));
					listViews.Add(control);
					control = new CategoryListView(new CategoryListViewModel("Categorized", c.Resolve<IItemService>()));
					listViews.Add(control);

					return listViews;
				});
		}
	}
}
