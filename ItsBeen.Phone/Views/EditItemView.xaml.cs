﻿using Microsoft.Phone.Controls;

using ItsBeen.Phone.Controls;

namespace ItsBeen.Phone.Views
{
	/// <summary>
	/// Description for EditItemView.
	/// </summary>
	public partial class EditItemView : PageBase
	{
		private ItsBeen.App.ViewModels.EditItemViewModel ViewModel
		{
			get
			{
				return DataContext as ItsBeen.App.ViewModels.EditItemViewModel;
			}
		}

		/// <summary>
		/// Initializes a new instance of the EditItemView class.
		/// </summary>
		public EditItemView()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Handles the Click event of the buttonSave control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void buttonSave_Click(object sender, System.EventArgs e)
		{
			if (ViewModel != null)
			{
				// Ensure any textboxes lose focus and thus update their source bindings
				this.Focus();
				// Execute the command asynchronously so that the source binding
				//update happens first
				Dispatcher.BeginInvoke(() =>
					{
						ViewModel.CommandSave.Execute(null);
					});
			}
		}
		/// <summary>
		/// Handles the Click event of the buttonDelete control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void buttonDelete_Click(object sender, System.EventArgs e)
		{
			if (ViewModel != null)
			{
				ViewModel.CommandDelete.Execute(null);
			}
		}
	}
}