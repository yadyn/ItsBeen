using System;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;

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

		private void TabRoot_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			new Button[] { EditButton, ResetButton, DeleteButton }.ToList().ForEach(b =>
				{
					var expression = BindingOperations.GetMultiBindingExpression(b, Button.IsEnabledProperty);
					if (expression != null)
						expression.UpdateTarget();
				});
		}
	}
}
