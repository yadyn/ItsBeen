using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using GalaSoft.MvvmLight.Messaging;

namespace ItsBeen.Phone.Services
{
	/// <summary>
	/// Service that listens for dialog messages and handles displaying them.
	/// </summary>
	public class MessageBoxService : ItsBeen.App.Services.IMessageBoxService
	{
		/// <summary>
		/// Shows a message box.
		/// </summary>
		/// <param name="message">
		/// A <see cref="T:DialogMessage"/> that defines the message box.
		/// </param>
		public void ShowMessageBox(DialogMessage message)
		{
			MessageBoxResult result =
				MessageBox.Show(
					message.Content,
					message.Caption,
					message.Button);

			message.ProcessCallback(result);
		}
	}
}
