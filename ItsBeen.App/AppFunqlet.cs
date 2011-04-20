using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Funq;

using ItsBeen.App.Services;
using ItsBeen.App.ViewModels;

namespace ItsBeen.App
{
	public class AppFunqlet : IFunqlet
	{
		public void Configure(Container container)
		{
			// Services
			if (GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
				container.Register<IItemService>(c => new InMemoryItemService());
			else
				container.Register<IItemService>(c => new IsolatedStorageItemService());

			// ViewModels
			ViewModelLocator.Container = container;
			container.Register(c =>
				new MainViewModel(
					c.ResolveNamed<IEnumerable<object>>("MainListViews"),
					c.Resolve<IMessageBoxService>(),
					c.Resolve<IItemService>(),
					c.Resolve<INavigationService>()));
			container.Register(c => new EditItemViewModel());
		}
	}
}
