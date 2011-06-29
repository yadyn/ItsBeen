using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using ItsBeen.App.ViewModels;

namespace ItsBeen.Phone.Views
{
	public partial class AllListView : UserControl
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AllListView"/> class.
		/// </summary>
		public AllListView()
		{
			InitializeComponent();
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="AllListView"/> class.
		/// </summary>
		/// <param name="vm">A view model.</param>
		public AllListView(ListViewModel vm)
			: this()
		{
			this.DataContext = vm;
		}
	}
}