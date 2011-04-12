using System;

namespace ItsBeen.App.Messaging
{
	public static class Commands
	{
		public static readonly string AddItem = Guid.NewGuid().ToString();
		public static readonly string EditItem = Guid.NewGuid().ToString();
		public static readonly string SaveItem = Guid.NewGuid().ToString();
		public static readonly string ResetItem = Guid.NewGuid().ToString();
		public static readonly string ResetAll = Guid.NewGuid().ToString();
		public static readonly string DeleteItem = Guid.NewGuid().ToString();
		public static readonly string SelectItem = Guid.NewGuid().ToString();
	}
}
