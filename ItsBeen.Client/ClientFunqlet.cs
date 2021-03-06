﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

using Funq;

using ItsBeen.App.Services;
using ItsBeen.App.ViewModels;
using ItsBeen.Client.Services;
using ItsBeen.Client.Views;

namespace ItsBeen.Client
{
	public class ClientFunqlet : IFunqlet
	{
		public void Configure(Container container)
		{
			container.Register<IMessageBoxService>(c => new TaskDialogService());
			container.Register<ITaskDialogService>(c => new TaskDialogService());
			container.Register<INavigationService>(c => new NavigationService());

			container.Register<IEnumerable<object>>("MainListViews", c =>
				{
					Control control = null;
					List<object> listViews = new List<object>();

					control = new AllListView();
					control.DataContext = new ListViewModel("All", c.Resolve<IItemService>());
					listViews.Add(control);
					control = new RecentListView();
					control.DataContext = new ListViewModel("Recent", c.Resolve<IItemService>());
					listViews.Add(control);
					control = new CategoryListView();
					control.DataContext = new CategoryListViewModel("Categorized", c.Resolve<IItemService>());
					listViews.Add(control);
					
					return listViews;
				});
		}
	}
}
