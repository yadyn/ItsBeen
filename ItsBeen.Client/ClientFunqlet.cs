using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Funq;

using ItsBeen.App.Services;
using ItsBeen.Client.Services;

namespace ItsBeen.Client
{
	public class ClientFunqlet : IFunqlet
	{
		public void Configure(Container container)
		{
			container.Register<IMessageBoxService>(c => new TaskDialogService());
			container.Register<ITaskDialogService>(c => new TaskDialogService());
		}
	}
}
