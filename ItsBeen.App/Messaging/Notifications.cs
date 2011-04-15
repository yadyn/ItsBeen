using System;

namespace ItsBeen.App.Messaging
{
	public static class Notifications
	{
		public static readonly string NotifyItemAdded = Guid.NewGuid().ToString();
		public static readonly string NotifyItemSaved = Guid.NewGuid().ToString();
		public static readonly string NotifyItemReset = Guid.NewGuid().ToString();
		public static readonly string NotifyItemDeleted = Guid.NewGuid().ToString();
		public static readonly string NotifyItemSelected = Guid.NewGuid().ToString();
		public static readonly string NotifyResetAll = Guid.NewGuid().ToString();
	}
}
