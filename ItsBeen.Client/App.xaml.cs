using System;
using System.Windows;

using TaskDialogInterop;

using GalaSoft.MvvmLight.Threading;

using ItsBeen.App.Services;
using ItsBeen.Client.Services;

namespace ItsBeen.Client
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application, Microsoft.Shell.ISingleInstanceApp
	{
		/// <summary>
		/// Initializes the <see cref="App"/> class.
		/// </summary>
		/// <exception cref="T:System.InvalidOperationException">More than one instance of the <see cref="T:System.Windows.Application"/> class is created per <see cref="T:System.AppDomain"/>.</exception>
		static App()
		{
			DispatcherHelper.Initialize();
		}

		private readonly ITaskDialogService _taskDialogService;

		/// <summary>
		/// Initializes a new instance of the <see cref="App"/> class.
		/// </summary>
		/// <exception cref="T:System.InvalidOperationException">More than one instance of the <see cref="T:System.Windows.Application"/> class is created per <see cref="T:System.AppDomain"/>.</exception>
		public App()
		{
			Bootstrapper.Initialize();

			_taskDialogService = Bootstrapper.Container.Resolve<ITaskDialogService>();

			Startup += App_Startup;

#if (DEBUG != true)
			// Don't handle global exceptions in debug mode
			DispatcherUnhandledException += App_DispatcherUnhandledException;
			AppDomain.CurrentDomain.UnhandledException += AppDomain_UnhandledException;
#endif

			ShutdownMode = System.Windows.ShutdownMode.OnMainWindowClose;
		}

		/// <summary>
		/// Called when a second instance of the application tries to run.
		/// </summary>
		/// <param name="args">Command-line args passed to second instance.</param>
		/// <returns>A <see cref="T:System.Boolean"/>.</returns>
		public bool SignalExternalCommandLineArgs(System.Collections.Generic.IList<string> args)
		{
			// Handle command line args here, if needed
			// currently, this app does not support command line args

			return true;
		}

		private void App_Startup(object sender, StartupEventArgs e)
		{
		}
		private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
		{
			if (System.Diagnostics.Debugger.IsAttached)
				System.Diagnostics.Debugger.Break();
		}
		private void AppDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			if (System.Diagnostics.Debugger.IsAttached)
				System.Diagnostics.Debugger.Break();

			Exception ex = (Exception)e.ExceptionObject;

			if (ex != null)
			{
				if (e.IsTerminating)
				{
					TaskDialogOptions options = TaskDialogOptions.Default;
					options.Title = "ItsBeen";
					options.MainInstruction = "An unknown error occurred";
					options.Content = "The application must close.  We are sorry for the inconvenience.";
					options.ExpandedInfo = "Details of this error message have been saved to the error log." + Environment.NewLine + Environment.NewLine
										 + "You can access this log file from the Help menu after restarting the application." + Environment.NewLine + Environment.NewLine
										 + "Error summary:" + Environment.NewLine
										 + ex.Message;
					options.CommonButtons = TaskDialogCommonButtons.Close;
					options.MainIcon = VistaTaskDialogIcon.Error;

					_taskDialogService.ShowTaskDialog(MainWindow, options, null);
				}
			}
		}
	}
}
