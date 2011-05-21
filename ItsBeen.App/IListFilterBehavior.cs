using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ItsBeen.App.Model;

namespace ItsBeen.App
{
	public interface IListFilterBehavior
	{
		object View { get; }
		IEnumerable<ItemModel> Items { get; set; }
	}
}
