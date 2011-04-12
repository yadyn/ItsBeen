using System.Windows;

using ItsBeen.App.ViewModels;

namespace ItsBeen.Client.Views
{
	/// <summary>
	/// Interaction logic for ShellWindow.xaml
	/// </summary>
	public partial class ShellWindow : Window
	{
		public ShellWindow()
		{
			InitializeComponent();
			Closing += (s, e) => ViewModelLocator.Cleanup();
		}
	}
}
