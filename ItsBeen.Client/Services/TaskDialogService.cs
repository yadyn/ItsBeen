using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using GalaSoft.MvvmLight.Messaging;

using TaskDialogInterop;

using ItsBeen.App.Services;
using ItsBeen.Client.Messaging;

namespace ItsBeen.Client.Services
{
	/// <summary>
	/// Service that handles displaying task dialogs.
	/// </summary>
	public class TaskDialogService : ITaskDialogService, IMessageBoxService
	{
		/// <summary>
		/// Shows a task dialog.
		/// </summary>
		/// <param name="sender">The owner of the dialog, or null.
		/// Should either be a window instance or the data context object of one.</param>
		/// <param name="options">A <see cref="T:TaskDialogOptions"/> config object.</param>
		/// <param name="callback">An optional callback method.</param>
		public void ShowTaskDialog(object sender, TaskDialogOptions options, Action<TaskDialogResult> callback)
		{
			if (options.Owner == null)
			{
				options.Owner = DialogHelper.TryGetOwnerFromSender(sender);
			}

			TaskDialogResult result = TaskDialog.Show(options);

			if (callback != null)
				callback(result);
		}
		/// <summary>
		/// Shows a message box.
		/// </summary>
		/// <param name="message">
		/// A <see cref="T:DialogMessage"/> that defines the message box.
		/// </param>
		public void ShowMessageBox(DialogMessage message)
		{
			TaskDialogOptions options = new TaskDialogOptions();

			options.Owner = DialogHelper.TryGetOwnerFromSender(message.Sender);
			options.MainInstruction = message.Content;
			options.Title = message.Caption;

			switch (message.Button)
			{
				case MessageBoxButton.OK:
					options.CommonButtons = TaskDialogCommonButtons.Close;
					options.DefaultButtonIndex = 0;
					break;
				case MessageBoxButton.OKCancel:
					options.CommonButtons = TaskDialogCommonButtons.OKCancel;
					if (message.DefaultResult == MessageBoxResult.OK)
						options.DefaultButtonIndex = 0;
					else
						options.DefaultButtonIndex = 1;
					break;
				case MessageBoxButton.YesNo:
					options.CommonButtons = TaskDialogCommonButtons.YesNo;
					if (message.DefaultResult == MessageBoxResult.Yes)
						options.DefaultButtonIndex = 0;
					else
						options.DefaultButtonIndex = 1;
					break;
				case MessageBoxButton.YesNoCancel:
					options.CommonButtons = TaskDialogCommonButtons.YesNoCancel;
					if (message.DefaultResult == MessageBoxResult.Yes)
						options.DefaultButtonIndex = 0;
					else if (message.DefaultResult == MessageBoxResult.No)
						options.DefaultButtonIndex = 1;
					else
						options.DefaultButtonIndex = 2;
					break;
			}

			switch (message.Icon)
			{
				default:
				case MessageBoxImage.None:
				case MessageBoxImage.Question:
					break;
				case MessageBoxImage.Information:
					options.MainIcon = VistaTaskDialogIcon.Information;
					break;
				case MessageBoxImage.Warning:
					options.MainIcon = VistaTaskDialogIcon.Warning;
					break;
				case MessageBoxImage.Error:
					options.MainIcon = VistaTaskDialogIcon.Error;
					break;
			}

			TaskDialogResult result = TaskDialog.Show(options);

			// TaskDialogSimpleResult is directly convertable to DialogResult (WinForms) and MessageBoxResult (WPF)
			message.ProcessCallback((MessageBoxResult)result.Result);
		}
	}
}
