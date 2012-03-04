using System;

using TaskDialogInterop;

namespace ItsBeen.App.Services
{
	/// <summary>
	/// Service that handles displaying task dialogs.
	/// </summary>
	public interface ITaskDialogService
	{
		/// <summary>
		/// Shows a task dialog.
		/// </summary>
		/// <param name="sender">
		/// The owner of the dialog, or null.
		/// Should either be a window instance or the data context object of one.
		/// </param>
		/// <param name="options">A <see cref="T:TaskDialogOptions"/> config object.</param>
		/// <param name="callback">An optional callback method.</param>
		void ShowTaskDialog(object sender, TaskDialogOptions options, Action<TaskDialogResult> callback);
	}
}
