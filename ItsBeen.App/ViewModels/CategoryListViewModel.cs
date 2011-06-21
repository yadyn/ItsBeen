using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using GalaSoft.MvvmLight.Messaging;

using ItsBeen.App.Model;
using ItsBeen.App.Messaging;
using ItsBeen.App.Services;

namespace ItsBeen.App.ViewModels
{
	public class CategoryListViewModel : ListViewModel
	{
		private ObservableCollection<string> _categories;

		/// <summary>
		/// Initializes a new instance of the <see cref="CategoryListViewModel"/> class.
		/// </summary>
		/// <param name="listType">The type of list.</param>
		/// <param name="itemService">An item service.</param>
		public CategoryListViewModel(string listType, IItemService itemService)
			: base(listType, itemService)
		{
			BuildCategoriesList();

			RegisterForMessages();
		}

		public ObservableCollection<string> Categories
		{
			get
			{
				return _categories;
			}
		}

		private void BuildCategoriesList()
		{
			if (_categories == null)
				_categories = new ObservableCollection<string>();
			else
				_categories.Clear();

			var categories = _itemService.GetItems()
				.Select(item => item.Category)
				.Distinct()
				.OrderBy(c => c);

			foreach (string category in categories)
				_categories.Add(category);
		}
		private void RegisterForMessages()
		{
			Messenger.Default.Register<NotificationMessage<ItemModel>>(this, message =>
			{
				if (message.Notification == Notifications.NotifyItemAdded
					|| message.Notification == Notifications.NotifyItemDeleted
					|| message.Notification == Notifications.NotifyItemSaved)
				{
					BuildCategoriesList();
				}
			});
		}
	}
}
