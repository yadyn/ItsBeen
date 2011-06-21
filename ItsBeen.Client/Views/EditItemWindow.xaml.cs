using System.Windows;

using ItsBeen.Client.Controls;

namespace ItsBeen.Client
{
	/// <summary>
	/// Description for EditItemWindow.
	/// </summary>
	public partial class EditItemWindow : WindowBase
	{
		/// <summary>
		/// Initializes a new instance of the EditItemWindow class.
		/// </summary>
		public EditItemWindow()
		{
			InitializeComponent();

			KeyDown += (s, e) =>
				{
					if (e.Key == System.Windows.Input.Key.Escape)
						this.Close();
				};
		}
	}
}