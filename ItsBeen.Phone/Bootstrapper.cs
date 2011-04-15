using System;

using Funq;

using ItsBeen.App;

namespace ItsBeen.Phone
{
	public static class Bootstrapper
	{
		internal static readonly Guid AppID = new Guid();

		public static readonly Container Container;

		/// <summary>
		/// Initializes the <see cref="Bootstrapper"/> class.
		/// </summary>
		static Bootstrapper()
		{
			Container = new Container();
		}

		public static void Initialize()
		{
			// Load in known funqlets so that they can run their configuration routines
			new AppFunqlet().Configure(Container);
			new PhoneFunqlet().Configure(Container);
		}
	}
}
