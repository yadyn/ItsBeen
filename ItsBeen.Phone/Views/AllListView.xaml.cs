using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using Microsoft.Phone.Controls;
using Microsoft.Phone.Reactive;

namespace ItsBeen.Phone.Views
{
	public partial class AllListView : UserControl
	{
		private ItsBeen.App.ViewModels.ListViewModel ViewModel
		{
			get
			{
				return DataContext as ItsBeen.App.ViewModels.ListViewModel;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AllListView"/> class.
		/// </summary>
		public AllListView()
		{
			InitializeComponent();
		}
	}
}