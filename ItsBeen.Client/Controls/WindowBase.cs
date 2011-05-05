using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using ItsBeen.App;

namespace ItsBeen.Client.Controls
{
	/// <summary>
	/// A base class for all application windows.
	/// </summary>
	public abstract class WindowBase : Window
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="WindowBase"/> class.
		/// </summary>
		public WindowBase()
			: base()
		{
			this.DataContextChanged += new DependencyPropertyChangedEventHandler(WindowBase_DataContextChanged);
		}

		private void WindowBase_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			if (e.NewValue is IRequestCloseViewModel)
			{
				((IRequestCloseViewModel)e.NewValue).RequestClose += (s, args) => this.Close();
			}
		}
	}
}
