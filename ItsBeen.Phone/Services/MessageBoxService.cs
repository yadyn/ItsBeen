using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using TaskDialogInterop;

using GalaSoft.MvvmLight.Messaging;

using ItsBeen.App.Services;

namespace ItsBeen.Phone.Services
{
	/// <summary>
	/// Service that listens for dialog messages and handles displaying them.
	/// </summary>
	public class MessageBoxService : IMessageBoxService, ITaskDialogService
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
		/// <summary>
		/// Shows a task dialog.
		/// </summary>
		/// <param name="sender">The owner of the dialog, or null.
		/// Should either be a window instance or the data context object of one.</param>
		/// <param name="options">A <see cref="T:TaskDialogOptions"/> config object.</param>
		/// <param name="callback">An optional callback method.</param>
		public void ShowTaskDialog(object sender, TaskDialogOptions options, Action<TaskDialogResult> callback)
		{
			MessageBoxButton buttons = MessageBoxButton.OK;

			if (options.AllowDialogCancellation
				|| (options.CommonButtons == TaskDialogCommonButtons.OKCancel
					|| options.CommonButtons == TaskDialogCommonButtons.RetryCancel
					|| options.CommonButtons == TaskDialogCommonButtons.YesNo
					|| options.CommonButtons == TaskDialogCommonButtons.YesNoCancel))
				buttons = MessageBoxButton.OKCancel;

			MessageBoxResult mbResult =
				MessageBox.Show(
					String.IsNullOrEmpty(options.MainInstruction) ? options.Content : options.MainInstruction,
					options.Title,
					buttons);

			TaskDialogResult tdResult = new TaskDialogResult((TaskDialogSimpleResult)mbResult);
		}
	}
}
