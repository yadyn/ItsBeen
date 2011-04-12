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
		private static readonly string IsItemSelectedPropertyName = "IsItemSelected";

		private bool isItemSelected;
		private object itemSelectedKey;

		private ICommand commandAdd;
		private ICommand commandEdit;
		private ICommand commandReset;
		private ICommand commandResetAll;
		private ICommand commandDelete;

		public string PageTitle
		{
			get
			{
				return AppViewModel.ApplicationName;
			}
		}
		public string Page1Name
		{
			get
			{
				return "All";
			}
		}
		public string Page2Name
		{
			get
			{
				return "Recent";
			}
		}
		public bool IsItemSelected
		{
			get
			{
				return isItemSelected;
			}
		}
		public object ItemSelectedKey
		{
			get
			{
				return itemSelectedKey;
			}
		}

		public ICommand CommandAdd
		{
			get
			{
				if (commandAdd == null)
				{
					commandAdd = new RelayCommand(() =>
					{
						Messenger.Default.Send(new NotificationMessage(this, Commands.AddItem));
					});
				}
				return commandAdd;
			}
		}
		public ICommand CommandEdit
		{
			get
			{
				if (commandEdit == null)
				{
					commandEdit = new RelayCommand(() =>
					{
						Messenger.Default.Send(new NotificationMessage(this, itemSelectedKey, Commands.EditItem));
					});
				}
				return commandEdit;
			}
		}
		public ICommand CommandReset
		{
			get
			{
				if (commandReset == null)
				{
					commandReset = new RelayCommand(() =>
					{
						Messenger.Default.Send(new NotificationMessage(this, itemSelectedKey, Commands.ResetItem));
					});
				}
				return commandReset;
			}
		}
		public ICommand CommandResetAll
		{
			get
			{
				if (commandResetAll == null)
				{
					commandResetAll = new RelayCommand(() =>
					{
						Messenger.Default.Send(new NotificationMessage(this, Commands.ResetAll));
					});
				}
				return commandResetAll;
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
						Messenger.Default.Send(new NotificationMessage(this, itemSelectedKey, Commands.DeleteItem));
					});
				}
				return commandDelete;
			}
		}

		/// <summary>
		/// Initializes a new instance of the MainViewModel class.
		/// </summary>
		public MainViewModel()
		{
			RegisterForMessages();
		}

		///// <summary>
		///// Unregisters this instance from the Messenger class and cleans up
		///// other resources.
		///// </summary>
		//public override void Cleanup()
		//{
		//    base.Cleanup();
		//}

		private void RegisterForMessages()
		{
			Messenger.Default.Register<NotificationMessage<System.Windows.Controls.SelectionChangedEventArgs>>(this,
				message =>
				{
					if (message.Notification == Commands.SelectItem)
					{
						isItemSelected = (message.Content.AddedItems.Count > 0);
						itemSelectedKey = message.Sender;
						RaisePropertyChanged(IsItemSelectedPropertyName);
					}
				});
		}
	}
}