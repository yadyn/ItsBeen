using System;
using System.Linq;
using System.Collections.Generic;

using ItsBeen.App.Model;

namespace ItsBeen.App.Services
{
	/// <summary>
	/// A simple in-memory item service.
	/// </summary>
	/// <remarks>
	/// This item service does not provide any persistence between runs.
	/// It is mostly useful for testing against.
	/// </remarks>
	public class InMemoryItemService : IItemService
	{
		int i = 0;
		private List<ItemModel> items;

		/// <summary>
		/// Initializes a new instance of the <see cref="InMemoryItemService"/> class.
		/// </summary>
		public InMemoryItemService()
		{
			Initialize();
		}

		public IEnumerable<ItemModel> GetItems()
		{
			return items.AsReadOnly();
		}
		public void AddItem(ItemModel item)
		{
			if (item == null)
				return;

			items.Add(item);
		}
		public void DeleteItem(ItemModel item)
		{
			if (item == null)
				return;

			items.Remove(item);
		}
		public void SaveItems()
		{
			// do nothing, items are in memory only
		}
		public ItemModel NewItem()
		{
			return new ItemModel(String.Format("New Timer {0}", ++i));
		}

		private void Initialize()
		{
			ItemModel item = null;
			
			items = new List<ItemModel>();

			item = NewItem();
			item.LastUpdated = DateTime.Now.Add(new TimeSpan(91, 5, 9, 55, 853));
			items.Add(item);

			item = NewItem();
			item.LastUpdated = DateTime.Now.Add(new TimeSpan(0, 15, 42, 12, 585));
			items.Add(item);

			item = NewItem();
			item.LastUpdated = DateTime.Now.Add(new TimeSpan(0, 0, 3, 8, 233));
			items.Add(item);
		}
	}
}
