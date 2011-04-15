using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Funq;

using ItsBeen.App;

namespace ItsBeen.Client
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
			if (Microsoft.Shell.SingleInstance<App>.InitializeAsFirstInstance(AppID.ToString()))
			{
				// Load in known funqlets so that they can run their configuration routines
				new AppFunqlet().Configure(Container);
				new ClientFunqlet().Configure(Container);

				// Allow single instance code to perform cleanup operations before shutdown
				System.Windows.Application.Current.Exit += (s, e) =>
					{
						Microsoft.Shell.SingleInstance<App>.Cleanup();
					};
			}
		}
	}
}
