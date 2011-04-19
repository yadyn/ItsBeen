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
				
				// Silverlight has no CommandManager and the AppBar doesn't support binding so
				//we have to manually wire CanExecuteChanged events to update them
				ViewModel.CommandEdit.CanExecuteChanged += new EventHandler(AppBarCommands_CanExecuteChanged);
				ViewModel.CommandReset.CanExecuteChanged += new EventHandler(AppBarCommands_CanExecuteChanged);
				ViewModel.CommandDelete.CanExecuteChanged += new EventHandler(AppBarCommands_CanExecuteChanged);
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
		/// Handles the CanExecuteChanged event of any AppBar Command controls.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void AppBarCommands_CanExecuteChanged(object sender, EventArgs e)
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
			    ViewModel.CommandDelete.Execute(null);
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
				ViewModel.CommandResetAll.Execute(null);
			}
		}

		private void SetEnabledAppBarButtons()
		{
			bool enable = ViewModel.IsItemSelected;
			
			(ApplicationBar.Buttons[1] as Microsoft.Phone.Shell.ApplicationBarIconButton).IsEnabled = enable;
			(ApplicationBar.Buttons[2] as Microsoft.Phone.Shell.ApplicationBarIconButton).IsEnabled = enable;
			(ApplicationBar.Buttons[3] as Microsoft.Phone.Shell.ApplicationBarIconButton).IsEnabled = enable;
		}
	}
}