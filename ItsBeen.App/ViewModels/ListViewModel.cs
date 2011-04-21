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
	/// </summary>
	public class ListViewModel : AppViewModel
	{
		private static readonly string ItemsPropertyName = "Items";

		private readonly IItemService _itemService;
		private readonly string _listType;

		private ObservableCollection<ItemViewModel> items;
		private System.Windows.Threading.DispatcherTimer ticker;
		private ItemViewModel _selectedItem;

		private ICommand commandSelect;

		/// <summary>
		/// Initializes a new instance of the <see cref="ListViewModel"/> class.
		/// </summary>
		/// <param name="itemService">An _item service.</param>
		public ListViewModel(string listType, IItemService itemService)
		{
			if (itemService == null)
				throw new ArgumentNullException("itemService");

			this._itemService = itemService;
			this._listType = listType;

			// Must initialize ticker BEFORE building _item collection!
			ticker = new System.Windows.Threading.DispatcherTimer();
			ticker.Interval = new TimeSpan(TimeSpan.TicksPerSecond); // 1 second interval
			ticker.Tick += ItemViewModel.HandleTick; // register to static handler

			BuildItemsCollection();

			RegisterForMessages();

			if (!IsInDesignMode)
				ticker.Start();
		}

		public string ListType
		{
			get
			{
				return _listType;
			}
		}
		public bool IsItemSelected
		{
			get
			{
				return _selectedItem != null;
			}
		}
		public ItemViewModel SelectedItem
		{
			get
			{
				return _selectedItem;
			}
			private set
			{
				_selectedItem = value;

				RaisePropertyChanged("SelectedItem");
				RaisePropertyChanged("IsItemSelected");
			}
		}
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
							SelectedItem = (e.AddedItems.Count > 0) ? (e.AddedItems[0] as ItemViewModel) : null;
							// This command is also executed when a de-select happens so make sure something is selected
							Messenger.Default.Send(new NotificationMessage<ItemModel>(this, (SelectedItem == null) ? null : SelectedItem.Item, Notifications.NotifyItemSelected));
						});
				}
				return commandSelect;
			}
		}

		private void BuildItemsCollection()
		{
			ObservableCollection<ItemViewModel> itemsCol = new ObservableCollection<ItemViewModel>();

			_itemService.GetItems().ToList().ForEach(item =>
				{
					ItemViewModel itemVM = new ItemViewModel(item);
					itemsCol.Add(itemVM);
				});

			items = itemsCol;
		}
		[System.Diagnostics.CodeAnalysis.SuppressMessage(
			"Microsoft.Reliability",
			"CA2000:Dispose objects before losing scope",
			Justification = "Object remains in collection after method ends and thus should not be disposed.")]
		private void RegisterForMessages()
		{
			Messenger.Default.Register<NotificationMessage<ItemModel>>(this, message =>
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
					else if (message.Notification == Commands.SelectItem)
					{
						if (CommandSelect.CanExecute(null))
							CommandSelect.Execute(null);
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

			foreach (ItemViewModel item in items)
			{
				item.Cleanup();
			}
		}
	}
}