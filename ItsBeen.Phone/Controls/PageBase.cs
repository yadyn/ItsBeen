using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using Microsoft.Phone.Controls;

using ItsBeen.App;

namespace ItsBeen.Phone.Controls
{
	/// <summary>
	/// A base class for all application windows.
	/// </summary>
	public abstract class PageBase : PhoneApplicationPage
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PageBase"/> class.
		/// </summary>
		public PageBase()
			: base()
		{
			this.Loaded += new RoutedEventHandler(PageBase_Loaded);
			this.Unloaded += new RoutedEventHandler(PageBase_Unloaded);
		}

		/// <summary>
		/// Handles the Loaded event of the PageBase control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void PageBase_Loaded(object sender, RoutedEventArgs e)
		{
			if (this.DataContext is IRequestCloseViewModel)
			{
				((IRequestCloseViewModel)this.DataContext).RequestClose += PageBase_RequestClose;
			}
		}
		private void PageBase_Unloaded(object sender, RoutedEventArgs e)
		{
			if (this.DataContext is IRequestCloseViewModel)
			{
				((IRequestCloseViewModel)this.DataContext).RequestClose -= PageBase_RequestClose;
			}
		}

		private void PageBase_RequestClose(object sender, EventArgs e)
		{
			NavigationService.GoBack();
		}
	}
}
