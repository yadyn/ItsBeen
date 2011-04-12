using System;
using System.Collections.Generic;
using System.ComponentModel;

using ItsBeen.App.Model;

namespace ItsBeen.App.Services
{
	/// <summary>
	/// Provides a means for working with Item business objects.
	/// </summary>
	public interface IItemService : INotifyPropertyChanged
	{
		IEnumerable<ItemModel> Items { get; }
		void AddItem(ItemModel item);
		void DeleteItem(ItemModel item);
		ItemModel NewItem();
	}
}
