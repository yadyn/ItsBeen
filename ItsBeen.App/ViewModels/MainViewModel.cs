using System;
using System.Collections.Generic;
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
	/// This class contains properties that the main View can data bind to.
	/// </summary>
	public class MainViewModel : AppViewModel
	{
		private readonly IMessageBoxService _messageBoxService;
		private readonly IItemService _itemService;
		private readonly ObservableCollection<object> _listViews;

		private object _activeListView;
		private ListViewModel _activeListViewModel;

		private ICommand _commandAdd;
		private ICommand _commandEdit;
		private ICommand _commandReset;
		private ICommand _commandResetAll;
		private ICommand _commandDelete;

		/// <summary>
		/// Initializes a new instance of the MainViewModel class.
		/// </summary>
		/// <param name="defaultListView">The default list view.</param>
		/// <param name="filterListView">The filter list view.</param>
		/// <param name="messageBoxService">A message box service.</param>
		/// <param name="itemService">An item service.</param>
		public MainViewModel(IEnumerable<object> listViews,
			IMessageBoxService messageBoxService, IItemService itemService)
		{
			if (messageBoxService == null)
				throw new ArgumentNullException("messageBoxService");
			if (itemService == null)
				throw new ArgumentNullException("itemService");

			_messageBoxService = messageBoxService;
			_itemService = itemService;

			_listViews = new ObservableCollection<object>();

			if (listViews != null)
			{
				foreach (object obj in listViews)
				{
					_listViews.Add(obj);
				}
			}

			RegisterForMessages();
		}

		private ListViewModel ActiveListViewModel
		{
			get
			{
				return _activeListViewModel;
			}
			set
			{
				_activeListViewModel = value;

				RaisePropertyChanged("IsItemSelected");
				RaiseCanExecuteChanged(CommandEdit);
				RaiseCanExecuteChanged(CommandReset);
				RaiseCanExecuteChanged(CommandDelete);
			}
		}

		public bool IsItemSelected
		{
			get
			{
				return ActiveListViewModel != null && ActiveListViewModel.IsItemSelected;
			}
		}
		public object ActiveListView
		{
			get
			{
				return _activeListView;
			}
			set
			{
				_activeListView = value;

				if (value is System.Windows.Controls.Control)
					ActiveListViewModel = (value as System.Windows.Controls.Control).DataContext as ListViewModel;

				RaisePropertyChanged("ActiveListView");
			}
		}
		public ObservableCollection<object> ListViews
		{
			get
			{
				return _listViews;
			}
		}

		public ICommand CommandAdd
		{
			get
			{
				if (_commandAdd == null)
				{
					_commandAdd = new RelayCommand(() =>
					{
						var item = _itemService.NewItem();
						_itemService.AddItem(item);
						Messenger.Default.Send(new NotificationMessage<ItemModel>(this, item, Notifications.NotifyItemAdded));
					});
				}
				return _commandAdd;
			}
		}
		public ICommand CommandEdit
		{
			get
			{
				if (_commandEdit == null)
				{
					_commandEdit = new RelayCommand(() =>
					{
						// TODO
						//Messenger.Default.Send(new NotificationMessage(this, CommandContext.SelectedItem.Item, Commands.EditItem));
					}, () => IsItemSelected);
				}
				return _commandEdit;
			}
		}
		public ICommand CommandReset
		{
			get
			{
				if (_commandReset == null)
				{
					_commandReset = new RelayCommand(() =>
					{
						ActiveListViewModel.SelectedItem.Reset();
						_itemService.SaveItems();
						Messenger.Default.Send(new NotificationMessage<ItemModel>(this, ActiveListViewModel.SelectedItem.Item, Notifications.NotifyItemReset));
					}, () => IsItemSelected);
				}
				return _commandReset;
			}
		}
		public ICommand CommandResetAll
		{
			get
			{
				if (_commandResetAll == null)
				{
					_commandResetAll = new RelayCommand(() =>
					{
						DialogMessage message = new DialogMessage(this, Properties.Resources.ConfirmResetAllContent, result =>
						{
							if (result == System.Windows.MessageBoxResult.OK || result == System.Windows.MessageBoxResult.Yes)
							{
								ActiveListViewModel.Items.ToList().ForEach(ivm => ivm.Reset());
								_itemService.SaveItems();
								Messenger.Default.Send(new NotificationMessage(this, Notifications.NotifyResetAll));
							}
						});
						message.Caption = Properties.Resources.ConfirmResetAllCaption;
						_messageBoxService.ShowMessageBox(message);
#if WINDOWS_PHONE
						message.Button = System.Windows.MessageBoxButton.OKCancel;
#else
						message.Button = System.Windows.MessageBoxButton.YesNo;
						message.DefaultResult = System.Windows.MessageBoxResult.No;
						message.Icon = System.Windows.MessageBoxImage.Question;
#endif
					});
				}
				return _commandResetAll;
			}
		}
		public ICommand CommandDelete
		{
			get
			{
				if (_commandDelete == null)
				{
					_commandDelete = new RelayCommand(() =>
					{
						DialogMessage message = new DialogMessage(this,
							String.Format(Properties.Resources.ConfirmItemDeleteContent, ActiveListViewModel.SelectedItem.Item.Name),
							result =>
								{
									if (result == System.Windows.MessageBoxResult.OK || result == System.Windows.MessageBoxResult.Yes)
									{
										_itemService.DeleteItem(ActiveListViewModel.SelectedItem.Item);
										Messenger.Default.Send(new NotificationMessage<ItemModel>(this, ActiveListViewModel.SelectedItem.Item, Notifications.NotifyItemDeleted));
									}
								});
						message.Caption = Properties.Resources.ConfirmItemDeleteCaption;
#if WINDOWS_PHONE
						message.Button = System.Windows.MessageBoxButton.OKCancel;
#else
						message.Button = System.Windows.MessageBoxButton.YesNo;
						message.DefaultResult = System.Windows.MessageBoxResult.No;
						message.Icon = System.Windows.MessageBoxImage.Question;
#endif
						_messageBoxService.ShowMessageBox(message);
					}, () => IsItemSelected);
				}
				return _commandDelete;
			}
		}

		private void RegisterForMessages()
		{
			Messenger.Default.Register<NotificationMessage>(this, message =>
				{
					if (message.Notification == Commands.AddItem)
					{
						if (CommandAdd.CanExecute(null))
							CommandAdd.Execute(null);
					}
					else if (message.Notification == Commands.EditItem)
					{
						if (CommandEdit.CanExecute(null))
							CommandEdit.Execute(null);
					}
					if (message.Notification == Commands.ResetItem)
					{
						if (CommandReset.CanExecute(null))
							CommandReset.Execute(null);
					}
					else if (message.Notification == Commands.DeleteItem)
					{
						if (CommandDelete.CanExecute(null))
							CommandDelete.Execute(null);
					}
					else if (message.Notification == Commands.ResetAll)
					{
						if (CommandResetAll.CanExecute(null))
							CommandResetAll.Execute(null);
					}
				});
			Messenger.Default.Register<NotificationMessage<ItemModel>>(this, message =>
				{
					if (message.Notification == Notifications.NotifyItemSelected)
					{
						RaisePropertyChanged("IsItemSelected");
						RaiseCanExecuteChanged(CommandEdit);
						RaiseCanExecuteChanged(CommandReset);
						RaiseCanExecuteChanged(CommandDelete);
					}
				});
		}
	}
}