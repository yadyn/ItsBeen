using System;

using Funq;

using ItsBeen.App.Services;
using ItsBeen.Phone.Services;

namespace ItsBeen.Phone
{
	public class PhoneFunqlet : IFunqlet
	{
		public void Configure(Container container)
		{
			container.Register<IMessageBoxService>(c => new MessageBoxService());
		}
	}
}
