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

using Microsoft.Phone.Controls;

namespace ItsBeen.Phone
{
	public partial class MainPage : PhoneApplicationPage
	{
		private ItsBeen.App.ViewModels.MainViewModel ViewModel
		{
			get
			{
				return DataContext as ItsBeen.App.ViewModels.MainViewModel;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MainPage"/> class.
		/// </summary>
		public MainPage()
		{
			InitializeComponent();

			this.Loaded += new RoutedEventHandler(MainPage_Loaded);
		}

		/// <summary>
		/// Handles the Loaded event of the MainPage control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void MainPage_Loaded(object sender, RoutedEventArgs e)
		{
			if (ViewModel != null)
			{
				ViewModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ViewModel_PropertyChanged);
			}
		}
		/// <summary>
		/// Handles the PropertyChanged event of the view model.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
		private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (ViewModel != null && e.PropertyName == "IsItemSelected")
			{
				SetEnabledAppBarButtons();
			}
		}
		/// <summary>
		/// Handles the SelectionChanged event of the PivotMenu control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
		private void PivotMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			SetEnabledAppBarButtons();
		}
		/// <summary>
		/// Handles the Click event of the buttonAdd control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void buttonAdd_Click(object sender, EventArgs e)
		{
			if (ViewModel != null)
			{
				ViewModel.CommandAdd.Execute(null);
			}
		}
		/// <summary>
		/// Handles the Click event of the buttonEdit control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void buttonEdit_Click(object sender, EventArgs e)
		{
			if (ViewModel != null)
			{
				NavigationService.Navigate(new Uri("/Views/EditItemView.xaml", UriKind.Relative));
				ViewModel.CommandEdit.Execute(null);
			}
		}
		/// <summary>
		/// Handles the Click event of the buttonReset control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void buttonReset_Click(object sender, EventArgs e)
		{
			if (ViewModel != null)
			{
				ViewModel.CommandReset.Execute(null);
			}
		}
		/// <summary>
		/// Handles the Click event of the buttonDelete control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void buttonDelete_Click(object sender, EventArgs e)
		{
			if (ViewModel != null)
			{
			    System.Windows.MessageBoxResult result = System.Windows.MessageBox.Show("Tap OK to delete the selected timer.", "Confirm Delete", System.Windows.MessageBoxButton.OKCancel);

				if (result == System.Windows.MessageBoxResult.OK)
				{
					ViewModel.CommandDelete.Execute(null);
				}
			}
		}
		/// <summary>
		/// Handles the Click event of the menuResetAll control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void menuResetAll_Click(object sender, EventArgs e)
		{
			if (ViewModel != null)
			{
				System.Windows.MessageBoxResult result = System.Windows.MessageBox.Show("Tap OK to reset all timers.", "Confirm Reset All", System.Windows.MessageBoxButton.OKCancel);

				if (result == System.Windows.MessageBoxResult.OK)
				{
					ViewModel.CommandResetAll.Execute(null);
				}
			}
		}

		private void SetEnabledAppBarButtons()
		{
			bool enable = ViewModel.IsItemSelected && ViewModel.ItemSelectedKey == ((PivotMenu.SelectedItem as PivotItem).Content as Control).DataContext;
			
			(ApplicationBar.Buttons[1] as Microsoft.Phone.Shell.ApplicationBarIconButton).IsEnabled = enable;
			(ApplicationBar.Buttons[2] as Microsoft.Phone.Shell.ApplicationBarIconButton).IsEnabled = enable;
			(ApplicationBar.Buttons[3] as Microsoft.Phone.Shell.ApplicationBarIconButton).IsEnabled = enable;
		}
	}
}