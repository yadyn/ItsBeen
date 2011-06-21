using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
	public class EditItemViewModel : AppViewModel, IRequestCloseViewModel
	{
		private static readonly string ItemPropertyName = "Item";
		private static readonly string NamePropertyName = "Name";
		private static readonly string DescriptionPropertyName = "Description";
		private static readonly string CategoryPropertyName = "Category";
		private static readonly string LastResetPropertyName = "LastReset";
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
		public string Category
		{
			get
			{
				return _item.Category;
			}
			set
			{
				_item.Category = value;
				RaisePropertyChanged(CategoryPropertyName);
			}
		}
		public DateTime LastReset
		{
			get
			{
				return _item.LastReset;
			}
			private set
			{
				_item.LastReset = value;
				RaisePropertyChanged(LastResetPropertyName);
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
		public ReadOnlyCollection<string> Categories
		{
			get
			{
				return _itemService.GetItems()
					.Select(item => item.Category)
					.Where(c => !String.IsNullOrEmpty(c))
					.Distinct()
					.ToList()
					.AsReadOnly();
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
		public int CategoryMaxLength
		{
			get
			{
				return 20;
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
						_item.LastModified = DateTime.Now;
						_itemService.SaveItems();
						Messenger.Default.Send(new NotificationMessage<ItemModel>(this, _item, Notifications.NotifyItemSaved));

						OnRequestClose(EventArgs.Empty);
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

									OnRequestClose(EventArgs.Empty);
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

		/// <summary>
		/// Occurs when the ViewModel requests that the View close.
		/// </summary>
		public event EventHandler RequestClose;

		/// <summary>
		/// Raises the <see cref="E:RequestClose"/> event.
		/// </summary>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void OnRequestClose(EventArgs e)
		{
			if (RequestClose != null)
				RequestClose(this, e);
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
						RaisePropertyChanged(LastResetPropertyName);
						RaisePropertyChanged(CreatedPropertyName);
					}
				});
		}
	}
}