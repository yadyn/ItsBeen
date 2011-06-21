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

		protected readonly IItemService _itemService;
		protected readonly string _listType;

		private ObservableCollection<ItemViewModel> _items;
		private System.Windows.Threading.DispatcherTimer _ticker;
		private ItemViewModel _selectedItem;

		private ICommand commandSelect;

		/// <summary>
		/// Initializes a new instance of the <see cref="ListViewModel"/> class.
		/// </summary>
		/// <param name="listType">The type of list.</param>
		/// <param name="itemService">An item service.</param>
		public ListViewModel(string listType, IItemService itemService)
		{
			if (itemService == null)
				throw new ArgumentNullException("itemService");

			this._listType = listType;
			this._itemService = itemService;

			// Must initialize ticker BEFORE building item collection!
			_ticker = new System.Windows.Threading.DispatcherTimer();
			_ticker.Interval = new TimeSpan(TimeSpan.TicksPerSecond); // 1 second interval
			_ticker.Tick += ItemViewModel.HandleTick; // register to static handler

			BuildItemsCollection();

			RegisterForMessages();

			if (!IsInDesignMode)
				_ticker.Start();
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
				return _items;
			}
			private set
			{
				_items = value;
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
			if (_items == null)
				_items = new ObservableCollection<ItemViewModel>();
			else
				_items.Clear();

			_itemService.GetItems().ToList().ForEach(item =>
				{
					ItemViewModel itemVM = new ItemViewModel(item);
					_items.Add(itemVM);
				});
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
						_items.Add(newItem);
					}
					else if (message.Notification == Notifications.NotifyItemDeleted)
					{
						ItemViewModel itemVM = _items.Where(i => i.Item.ID == message.Content.ID).FirstOrDefault();
						if (itemVM != null)
						{
							SelectedItem = null;
							_items.Remove(itemVM);
							itemVM.Cleanup();
						}
					}
					else if (message.Notification == Notifications.NotifyItemSaved)
					{
						ItemViewModel itemVM = _items.Where(i => i.Item.ID == message.Content.ID).FirstOrDefault();
						if (itemVM != null)
						{
							SelectedItem = null;
							_items[_items.IndexOf(itemVM)] = new ItemViewModel(message.Content);
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

			foreach (ItemViewModel item in _items)
			{
				item.Cleanup();
			}
		}
	}
}