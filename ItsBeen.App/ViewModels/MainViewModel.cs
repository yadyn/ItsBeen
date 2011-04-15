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
	/// This class contains properties that the main View can data bind to.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
	/// </para>
	/// </remarks>
	public class MainViewModel : AppViewModel
	{
		private readonly IMessageBoxService _messageBoxService;
		private readonly IItemService _itemService;
		private readonly ObservableCollection<ListViewModel> _listViewModels;

		private ListViewModel _commandContext;

		private ICommand _commandAdd;
		private ICommand _commandEdit;
		private ICommand _commandReset;
		private ICommand _commandResetAll;
		private ICommand _commandDelete;

		/// <summary>
		/// Initializes a new instance of the MainViewModel class.
		/// </summary>
		/// <param name="messageBoxService">A message box service.</param>
		/// <param name="itemService">An item service.</param>
		public MainViewModel(IMessageBoxService messageBoxService, IItemService itemService)
		{
			if (messageBoxService == null)
				throw new ArgumentNullException("messageBoxService");
			if (itemService == null)
				throw new ArgumentNullException("itemService");

			_messageBoxService = messageBoxService;
			_itemService = itemService;

			_listViewModels = new ObservableCollection<ListViewModel>();
			_listViewModels.Add(new ListViewModel(_itemService));
			_listViewModels.Add(new ListViewModel(_itemService));
			_listViewModels.Add(new ListViewModel(_itemService));

			_commandContext = _listViewModels[0];

			RegisterForMessages();
		}

		private ListViewModel CommandContext
		{
			get
			{
				return _commandContext;
			}
			set
			{
				_commandContext = value;

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
				return false;
			}
		}
		public ObservableCollection<ListViewModel> ListViewModels
		{
			get
			{
				return _listViewModels;
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
						CommandContext.SelectedItem.Reset();
						_itemService.SaveItems();
						Messenger.Default.Send(new NotificationMessage<ItemModel>(this, CommandContext.SelectedItem.Item, Notifications.NotifyItemReset));
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
								CommandContext.Items.ToList().ForEach(ivm => ivm.Reset());
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
						DialogMessage message = new DialogMessage(this, Properties.Resources.ConfirmItemDeleteContent, result =>
						{
							if (result == System.Windows.MessageBoxResult.OK || result == System.Windows.MessageBoxResult.Yes)
							{
								_itemService.DeleteItem(CommandContext.SelectedItem.Item);
								Messenger.Default.Send(new NotificationMessage<ItemModel>(this, CommandContext.SelectedItem.Item, Notifications.NotifyItemDeleted));
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
			Messenger.Default.Register<NotificationMessage>(this,
				message =>
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
			Messenger.Default.Register<NotificationMessage<ItemModel>>(this,
				message =>
				{
					if (message.Notification == Notifications.NotifyItemSelected)
					{
						CommandContext = message.Sender as ListViewModel;
					}
				});
		}
	}
}