using System;
using System.Text;
using System.Windows.Input;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

using ItsBeen.App.Model;
using ItsBeen.App.Services;
using ItsBeen.App.Messaging;

namespace ItsBeen.App.ViewModels
{
	/// <summary>
	/// This class contains properties that a View can data bind to.
	/// </summary>
	public class EditItemViewModel : AppViewModel
	{
		private static readonly string ItemPropertyName = "Item";
		private static readonly string NamePropertyName = "Name";
		private static readonly string DescriptionPropertyName = "Description";
		private static readonly string LastUpdatedPropertyName = "LastUpdated";
		private static readonly string CreatedPropertyName = "Created";

		private readonly IMessageBoxService _messageBoxService;
		private readonly IItemService _itemService;

		private ItemModel _item;

		private ICommand commandSave;
		private ICommand commandDelete;

		/// <summary>
		/// Initializes a new instance of the EditItemViewModel class.
		/// </summary>
		/// <param name="messageBoxService">A message box service.</param>
		/// <param name="itemService">An item service.</param>
		public EditItemViewModel(IMessageBoxService messageBoxService, IItemService itemService)
		{
			if (messageBoxService == null)
				throw new ArgumentNullException("messageBoxService");
			if (itemService == null)
				throw new ArgumentNullException("itemService");

			_messageBoxService = messageBoxService;
			_itemService = itemService;

			RegisterForMessages();
		}

		public string Name
		{
			get
			{
				return _item.Name;
			}
			set
			{
				_item.Name = value;
				RaisePropertyChanged(NamePropertyName);
			}
		}
		public string Description
		{
			get
			{
				return _item.Description;
			}
			set
			{
				_item.Description = value;
				RaisePropertyChanged(DescriptionPropertyName);
			}
		}
		public DateTime LastUpdated
		{
			get
			{
				return _item.LastUpdated;
			}
			private set
			{
				_item.LastUpdated = value;
				RaisePropertyChanged(LastUpdatedPropertyName);
			}
		}
		public DateTime Created
		{
			get
			{
				return _item.Created;
			}
			set
			{
				_item.Created = value;
				RaisePropertyChanged(CreatedPropertyName);
			}
		}
		public int NameMaxLength
		{
			get
			{
				return 20;
			}
		}
		public int DescriptionMaxLength
		{
			get
			{
				return 40;
			}
		}

		public ICommand CommandSave
		{
			get
			{
				if (commandSave == null)
				{
					commandSave = new RelayCommand(() =>
					{
						Messenger.Default.Send(new NotificationMessage<ItemModel>(this, _item, Notifications.NotifyItemSaved));
						_itemService.SaveItems();
					});
				}
				return commandSave;
			}
		}
		public ICommand CommandDelete
		{
			get
			{
				if (commandDelete == null)
				{
					commandDelete = new RelayCommand(() =>
					{
						DialogMessage message = new DialogMessage(this,
							String.Format(Properties.Resources.ConfirmItemDeleteContent, _item.Name),
							result =>
							{
								if (result == System.Windows.MessageBoxResult.OK || result == System.Windows.MessageBoxResult.Yes)
								{
									_itemService.DeleteItem(_item);
									Messenger.Default.Send(new NotificationMessage<ItemModel>(this, _item, Notifications.NotifyItemDeleted));
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
					});
				}
				return commandDelete;
			}
		}

		private void RegisterForMessages()
		{
			Messenger.Default.Register<NotificationMessage<ItemModel>>(this,
				message =>
				{
					if (message.Notification == Notifications.NotifyItemEdited)
					{
						_item = message.Content;
						RaisePropertyChanged(ItemPropertyName);
						RaisePropertyChanged(NamePropertyName);
						RaisePropertyChanged(DescriptionPropertyName);
						RaisePropertyChanged(LastUpdatedPropertyName);
						RaisePropertyChanged(CreatedPropertyName);
					}
				});
		}
	}
}