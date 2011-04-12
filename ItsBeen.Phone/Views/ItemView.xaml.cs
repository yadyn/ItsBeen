using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ItsBeen.Phone.Views
{
	public partial class ItemView : UserControl
	{
		private ItsBeen.App.ViewModels.ItemViewModel ViewModel
		{
			get
			{
				return DataContext as ItsBeen.App.ViewModels.ItemViewModel;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ItemView"/> class.
		/// </summary>
		public ItemView()
		{
			InitializeComponent();

			this.Loaded += new RoutedEventHandler(ItemView_Loaded);
			ResetStoryboard.Completed += ResetStoryboard_Completed;
		}

		/// <summary>
		/// Handles the Loaded event of the ItemView control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void ItemView_Loaded(object sender, RoutedEventArgs e)
		{
			if (ViewModel != null)
			{
				ViewModel.BeforeReset += ViewModel_BeforeReset;
			}
		}
		/// <summary>
		/// Handles the BeforeReset event of the ViewModel control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void ViewModel_BeforeReset(object sender, EventArgs e)
		{
			ResetStoryboard.Stop();
			ResetStoryboard.Begin();
		}
		/// <summary>
		/// Handles the Completed event of the ResetStoryboard control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void ResetStoryboard_Completed(object sender, EventArgs e)
		{
			if (ViewModel != null)
			{
				ViewModel.UpdateTimeSince();
			}
			ResetStoryboard.Stop();
		}
	}
}
