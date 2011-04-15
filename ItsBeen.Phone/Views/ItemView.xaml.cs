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
		}
	}
}
