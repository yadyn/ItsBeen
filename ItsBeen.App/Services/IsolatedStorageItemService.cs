using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;

using ItsBeen.App.Model;

namespace ItsBeen.App.Services
{
	public class IsolatedStorageItemService : IItemService
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
		}

		public IEnumerable<ItemModel> GetItems()
		{
			if (items == null)
				LoadDataFromStore();
			return items.AsReadOnly();
		}
		public void AddItem(ItemModel item)
		{
			if (item == null)
				return;
			
			if (items == null)
				LoadDataFromStore();
			
			items.Add(item);
			SaveDataToStore();
		}
		public void DeleteItem(ItemModel item)
		{
			if (item == null)
				return;

			if (items == null)
				LoadDataFromStore();

			items.Remove(item);
			SaveDataToStore();
		}
		public void SaveItems()
		{
			SaveDataToStore();
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
