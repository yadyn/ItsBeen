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

		private ItemModel item;

		private ICommand commandSave;
		private ICommand commandDelete;

		/// <summary>
		/// Initializes a new instance of the EditItemViewModel class.
		/// </summary>
		public EditItemViewModel()
		{
			RegisterForMessages();
		}

		public string PageName
		{
			get
			{
				return "Edit Timer";
			}
		}
		public ItemModel Item
		{
			get
			{
				return item;
			}
		}
		public string Name
		{
			get
			{
				return item == null ? NamePropertyName : item.Name;
			}
			set
			{
				item.Name = value;
				RaisePropertyChanged(NamePropertyName);
			}
		}
		public string Description
		{
			get
			{
				return item == null ? DescriptionPropertyName : item.Description;
			}
			set
			{
				item.Description = value;
				RaisePropertyChanged(DescriptionPropertyName);
			}
		}
		public DateTime LastUpdated
		{
			get
			{
				if (item == null)
				{
					return (IsInDesignMode) ? DateTime.Now.AddTicks(((TimeSpan.TicksPerDay * 397) + TimeSpan.TicksPerHour + TimeSpan.TicksPerMinute + TimeSpan.TicksPerSecond) * -1) : DateTime.Now;
				}
				else
					return item.LastUpdated;
			}
			private set
			{
				item.LastUpdated = value;
				RaisePropertyChanged(LastUpdatedPropertyName);
			}
		}
		public DateTime Created
		{
			get
			{
				if (item == null)
				{
					return (IsInDesignMode) ? DateTime.Now.AddTicks(TimeSpan.TicksPerDay * -400) : DateTime.Now;
				}
				else
					return item.Created;
			}
			set
			{
				item.Created = value;
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
		public string NameLabel
		{
			get
			{
				return "Name";
			}
		}
		public string DescriptionLabel
		{
			get
			{
				return "Description";
			}
		}
		public string LastUpdatedLabel
		{
			get
			{
				return "Last Set";
			}
		}
		public string CreatedLabel
		{
			get
			{
				return "Created";
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
						Messenger.Default.Send(new NotificationMessage<ItemModel>(this, Item, Notifications.NotifyItemSaved));
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
						Messenger.Default.Send(new NotificationMessage<ItemModel>(this, Item, Commands.DeleteItem));
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
						item = message.Content;
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