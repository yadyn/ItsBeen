using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

using ItsBeen.App.Messaging;
using ItsBeen.App.Model;
using ItsBeen.App.Services;

namespace ItsBeen.App.ViewModels
{
	/// <summary>
	/// This class contains properties that a View can data bind to.
	/// <para>
	public class ListViewModel : AppViewModel
	{
		private static readonly string ItemsPropertyName = "Items";

		private IItemService itemService;
		private ObservableCollection<ItemViewModel> items;
		private System.Windows.Threading.DispatcherTimer ticker;
		private ItemModel selectedItem;

		private ICommand commandSelect;

		public ObservableCollection<ItemViewModel> Items
		{
			get
			{
				return items;
			}
			private set
			{
				items = value;
				RaisePropertyChanged(ItemsPropertyName);
			}
		}
		public ICommand CommandSelect
		{
			get
			{
				if (commandSelect == null)
				{
					commandSelect = new RelayCommand<System.Windows.Controls.SelectionChangedEventArgs>(e =>
					{
						selectedItem = (e.AddedItems.Count > 0) ? (e.AddedItems[0] as ItemViewModel).Item : null;
						Messenger.Default.Send(new NotificationMessage<System.Windows.Controls.SelectionChangedEventArgs>(this, e, Commands.SelectItem));
					});
				}
				return commandSelect;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ListViewModel"/> class.
		/// </summary>
		public ListViewModel()
		{
			if (IsInDesignMode)
			{
				itemService = new InMemoryItemService();
			}
			else
			{
				itemService = new IsolatedStorageItemService();
			}

			// Must initialize ticker BEFORE building item collection!
			ticker = new System.Windows.Threading.DispatcherTimer();
			ticker.Interval = new TimeSpan(TimeSpan.TicksPerSecond); // 1 second interval
			ticker.Tick += ItemViewModel.HandleTick; // register to static handler

			BuildItemsCollection();

			this.itemService.PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == ItemsPropertyName)
				{
					RaisePropertyChanged(ItemsPropertyName);
				}
			};

			RegisterForMessages();

			if (!IsInDesignMode)
				ticker.Start();
		}

		private void BuildItemsCollection()
		{
			ObservableCollection<ItemViewModel> newItems = new ObservableCollection<ItemViewModel>();

			foreach (ItemModel item in itemService.Items)
			{
				ItemViewModel newItem = new ItemViewModel(item);
				newItems.Add(newItem);
			}

			items = newItems;
		}
		[System.Diagnostics.CodeAnalysis.SuppressMessage(
			"Microsoft.Reliability",
			"CA2000:Dispose objects before losing scope",
			Justification = "Object remains in collection after method ends and thus should not be disposed.")]
		private void RegisterForMessages()
		{
			Messenger.Default.Register<NotificationMessage<ItemModel>>(this,
				message =>
				{
					if (message.Notification == Notifications.NotifyItemAdded)
					{
						ItemViewModel newItem = new ItemViewModel(message.Content);
						items.Add(newItem);
					}
					else if (message.Notification == Notifications.NotifyItemDeleted)
					{
						ItemViewModel itemVM = items.Where(i => i.Item.ID == message.Content.ID).FirstOrDefault();
						if (itemVM != null)
						{
							items.Remove(itemVM);
							itemVM.Cleanup();
						}
					}
					else if (message.Notification == Notifications.NotifyItemSaved)
					{
						ItemViewModel itemVM = items.Where(i => i.Item.ID == message.Content.ID).FirstOrDefault();
						if (itemVM != null)
						{
							items[items.IndexOf(itemVM)] = new ItemViewModel(message.Content);
							itemVM.Cleanup();
							itemVM = null;
						}
					}
				});
			Messenger.Default.Register<NotificationMessage>(this,
				message =>
				{
					if (message.Notification == Commands.ResetItem)
					{
						ItemViewModel itemVM = items.Where(i => i.Item.ID == selectedItem.ID).FirstOrDefault();
						if (itemVM != null)
						{
							itemVM.Reset();
							Messenger.Default.Send(new NotificationMessage<ItemModel>(this, itemVM.Item, Notifications.NotifyItemReset));
						}
					}
					else if (message.Notification == Commands.DeleteItem)
					{
						Messenger.Default.Send(new NotificationMessage<ItemModel>(this, selectedItem, Commands.DeleteItem));
					}
					else if (message.Notification == Commands.EditItem)
					{
						Messenger.Default.Send(new NotificationMessage<ItemModel>(this, selectedItem, Commands.EditItem));
					}
					else if (message.Notification == Commands.ResetAll)
					{
						foreach (ItemViewModel itemVM in items)
						{
							itemVM.Reset();
						}
						Messenger.Default.Send(new NotificationMessage(this, Notifications.NotifyResetAll));
					}
				});
		}

		/// <summary>
		/// Unregisters this instance from the Messenger class and cleans up
		/// other resources.
		/// </summary>
		public override void Cleanup()
		{
			base.Cleanup();

			selectedItem = null;

			foreach (ItemViewModel item in items)
			{
				item.Cleanup();
			}
		}
	}
}