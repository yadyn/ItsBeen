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

		private ItemModel _item;

		private ICommand commandSave;
		private ICommand commandDelete;

		/// <summary>
		/// Initializes a new instance of the EditItemViewModel class.
		/// </summary>
		public EditItemViewModel()
		{
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
						Messenger.Default.Send(new NotificationMessage<ItemModel>(this, _item, Commands.DeleteItem));
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