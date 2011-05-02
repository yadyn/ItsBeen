using System.Windows;

namespace ItsBeen.Client
{
    /// <summary>
    /// Description for EditItemWindow.
    /// </summary>
    public partial class EditItemWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the EditItemWindow class.
        /// </summary>
        public EditItemWindow()
        {
            InitializeComponent();
        }

		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
		private void DeleteButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
    }
}