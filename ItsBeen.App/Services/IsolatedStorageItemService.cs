using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;

using GalaSoft.MvvmLight.Messaging;

using ItsBeen.App.Messaging;
using ItsBeen.App.Model;

namespace ItsBeen.App.Services
{
	public class IsolatedStorageItemService : ServiceBase, IItemService
	{
		int i = 0;
		private List<ItemModel> items;
		private IsolatedStorageFile store;
		private XmlSerializer serializer;

		private readonly string storageAreaName = "ItsBeenTasks";
		private readonly string itemsFileName = "list.xml";

		/// <summary>
		/// Initializes a new instance of the <see cref="IsolatedStorageItemService"/> class.
		/// </summary>
		public IsolatedStorageItemService()
		{
			Initialize();
			RegisterForMessages();
		}

		[XmlArray("Items")]
		public IEnumerable<ItemModel> Items
		{
			get
			{
				if (items == null)
					LoadDataFromStore();
				return items.AsReadOnly();
			}
		}

		public void AddItem(ItemModel item)
		{
			if (item == null)
				return;
			
			if (items == null)
				LoadDataFromStore();
			
			items.Add(item);
			SaveDataToStore();

			OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("Items"));

			Messenger.Default.Send(new NotificationMessage<ItemModel>(this, item, Notifications.NotifyItemAdded));
		}
		public void DeleteItem(ItemModel item)
		{
			if (item == null)
				return;

			if (items == null)
				LoadDataFromStore();

			items.Remove(item);
			SaveDataToStore();

			OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("Items"));

			Messenger.Default.Send(new NotificationMessage<ItemModel>(this, item, Notifications.NotifyItemDeleted));
		}
		public ItemModel NewItem()
		{
			return new ItemModel(String.Format("New Timer {0}", ++i));
		}

		private void Initialize()
		{
#if WINDOWS_PHONE
			store = IsolatedStorageFile.GetUserStoreForApplication();
#else
			// Application-scoped IS requires ClickOnce security, which we don't
			//want to depend on in the regular Windows environment
			// WP7 apps are always deployed with manifests, though, so it is not an issue
			// http://blogs.msdn.com/b/shawnhar/archive/2010/12/16/isolated-storage-windows-and-clickonce.aspx
			store = IsolatedStorageFile.GetUserStoreForDomain();
#endif
			if (store.GetDirectoryNames(storageAreaName).Length == 0)
			{
				// avoid creating if already exists - work around for partial trust
				store.CreateDirectory(storageAreaName);
			}

			serializer = new XmlSerializer(typeof(List<ItemModel>), new Type[] { typeof(ItemModel) });
		}
		private void RegisterForMessages()
		{
			Messenger.Default.Register<NotificationMessage>(this,
				message =>
				{
					if (message.Notification == Commands.AddItem)
					{
						AddItem(NewItem());
					}
					else if (message.Notification == Notifications.NotifyResetAll)
					{
						SaveDataToStore();
					}
				});
			Messenger.Default.Register<NotificationMessage<ItemModel>>(this,
				message =>
				{
					if (message.Notification == Notifications.NotifyItemReset)
					{
						SaveDataToStore();
					}
					else if (message.Notification == Notifications.NotifyItemSaved)
					{
						ItemModel oldItem = items.Where(i => i.ID == message.Content.ID).FirstOrDefault();
						if (oldItem != null)
						{
							items[items.IndexOf(oldItem)] = message.Content;
							SaveDataToStore();
						}
					}
					else if (message.Notification == Commands.DeleteItem)
					{
						DeleteItem(message.Content);
					}
				});
		}
		[System.Diagnostics.CodeAnalysis.SuppressMessage(
			"Microsoft.Usage",
			"CA2202:Do not dispose objects multiple times",
			Justification="Code checks for null before attempting what could be a second dispose.")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage(
			"Microsoft.Reliability",
			"CA2000:Dispose objects before losing scope",
			Justification="Object is in scope for entire method (defined at the beginning) and is always disposed in finally block.")]
		private void LoadDataFromStore()
		{
			IsolatedStorageFileStream isStream = null;

			try
			{
				lock (store)
				{
					isStream = new IsolatedStorageFileStream(storageAreaName + @"\" + itemsFileName, FileMode.Open, FileAccess.Read, FileShare.None, store);
					
					using (StreamReader reader = new StreamReader(isStream))
					{
						if (!reader.EndOfStream)
						{
							items = (List<ItemModel>)serializer.Deserialize(reader);
						}
					} // this should implicitly dispose of isStream too
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				if (items == null)
					items = new List<ItemModel>();
				if (isStream != null)
					isStream.Dispose();
			}
		}
		private void SaveDataToStore()
		{
			try
			{
				lock (store)
				{
					using (IsolatedStorageFileStream isStream =
						new IsolatedStorageFileStream(storageAreaName + @"\" + itemsFileName, FileMode.Create, FileAccess.Write, FileShare.None, store))
					{
						serializer.Serialize(isStream, items);
					}
				}
			}
			catch (Exception)
			{
			}
		}
	}
}
