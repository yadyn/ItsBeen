using System;
using System.Windows.Input;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace ItsBeen.App.ViewModels
{
	public class AppViewModel : ViewModelBase
	{
		public string ApplicationName
		{
			get { return "ItsBeen"; }
		}

		/// <summary>
		/// Raises the CanExecuteChanged event for the given command.
		/// </summary>
		/// <param name="viewModel">A view model.</param>
		/// <param name="command">A command.</param>
		/// <remarks>
		/// If <paramref name="command"/> is a <see cref="T:GalaSoft.MvvmLight.Command.RelayCommand"/>
		/// then this calls its raise method directly. For all other ICommand
		/// types, the conventional CommandManager method is used.
		/// </remarks>
		public static void RaiseCanExecuteChanged(ICommand command)
		{
			RelayCommand relayCommand = (RelayCommand)command;

			if (relayCommand != null)
				relayCommand.RaiseCanExecuteChanged();
#if !WINDOWS_PHONE
			else
				CommandManager.InvalidateRequerySuggested();
#endif
		}
	}
}
