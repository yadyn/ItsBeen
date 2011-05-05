using System;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;

using ItsBeen.App.ViewModels;
using ItsBeen.Client.Controls;

namespace ItsBeen.Client.Views
{
	/// <summary>
	/// Interaction logic for ShellWindow.xaml
	/// </summary>
	public partial class ShellWindow : WindowBase
	{
		public ShellWindow()
		{
			InitializeComponent();
			Closing += (s, e) => ViewModelLocator.Cleanup();
		}
	}
}
