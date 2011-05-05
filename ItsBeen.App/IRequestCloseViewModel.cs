using System;

namespace ItsBeen.App
{
	/// <summary>
	/// A ViewModel that supports requesting the view to close.
	/// </summary>
	public interface IRequestCloseViewModel
	{
		/// <summary>
		/// Occurs when the ViewModel requests that the View close.
		/// </summary>
		event EventHandler RequestClose;
	}
}
