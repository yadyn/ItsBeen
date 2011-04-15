using System;

using GalaSoft.MvvmLight.Messaging;

namespace ItsBeen.App.Services
{
	/// <summary>
	/// Service that listens for dialog messages and handles displaying them.
	/// </summary>
	public interface IMessageBoxService
	{
		/// <summary>
		/// Shows a message box.
		/// </summary>
		/// <param name="message">
		/// A <see cref="T:DialogMessage"/> that defines the message box.
		/// </param>
		void ShowMessageBox(DialogMessage message);
	}
}
