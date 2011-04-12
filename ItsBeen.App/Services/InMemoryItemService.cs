using System;
using System.Linq;
using System.Collections.Generic;

using GalaSoft.MvvmLight.Messaging;

using ItsBeen.App.Messaging;
using ItsBeen.App.Model;

namespace ItsBeen.App.Services
{
	/// <summary>
	/// A simple in-memory item service.
	/// </summary>
	/// <remarks>
	/// This item service does not provide any persistence between runs.
	/// It is mostly useful for testing against.
	/// </remarks>
	public class InMemoryItemService : ServiceBase, IItemService
	{
		int i = 0;
		private List<ItemModel> items;

		/// <summary>
		/// Initializes a new instance of the <see cref="InMemoryItemService"/> class.
		/// </summary>
		public InMemoryItemService()
		{
			Initialize();
			RegisterForMessages();
		}

		public IEnumerable<ItemModel> Items
		{
			get { return items.AsReadOnly(); }
		}

		public void AddItem(ItemModel item)
		{
			if (item == null)
				return;

			items.Add(item);

			OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("Items"));

			Messenger.Default.Send(new NotificationMessage<ItemModel>(this, item, Notifications.NotifyItemAdded));
		}
		public void DeleteItem(ItemModel item)
		{
			if (item == null)
				return;

			items.Remove(item);

			OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("Items"));

			Messenger.Default.Send(new NotificationMessage<ItemModel>(this, item, Notifications.NotifyItemDeleted));
		}

		public ItemModel NewItem()
		{
			return new ItemModel(String.Format("New Timer {0}", ++i));
		}

		private void Initialize()
		{
			ItemModel item = null;
			
			items = new List<ItemModel>();

			item = NewItem();
			item.LastUpdated = DateTime.Now.Add(new TimeSpan(91, 5, 9, 55, 853));
			items.Add(item);

			item = NewItem();
			item.LastUpdated = DateTime.Now.Add(new TimeSpan(0, 15, 42, 12, 585));
			items.Add(item);

			item = NewItem();
			item.LastUpdated = DateTime.Now.Add(new TimeSpan(0, 0, 3, 8, 233));
			items.Add(item);
		}
		private void RegisterForMessages()
		{
			Messenger.Default.Register<NotificationMessage>(this,
				message =>
				{
					if (message.Notification == Commands.AddItem)
					{
						AddItem(NewItem());
					}
					else if (message.Notification == Notifications.NotifyResetAll)
					{
						// do nothing; no persistence
					}
				});
			Messenger.Default.Register<NotificationMessage<ItemModel>>(this,
				message =>
				{
					if (message.Notification == Notifications.NotifyItemReset)
					{
						// do nothing; no persistence
					}
					else if (message.Notification == Notifications.NotifyItemSaved)
					{
						ItemModel oldItem = items.Where(i => i.ID == message.Content.ID).FirstOrDefault();
						if (oldItem != null)
						{
							items[items.IndexOf(oldItem)] = message.Content;
						}
					}
					else if (message.Notification == Commands.DeleteItem)
					{
						DeleteItem(message.Content);
					}
				});
		}
	}
}
