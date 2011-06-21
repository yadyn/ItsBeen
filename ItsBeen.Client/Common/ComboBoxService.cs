using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ItsBeen.Client.Common
{
	public class ComboBoxService
	{
		public static int GetMaxLength(DependencyObject obj)
		{
			return (int)obj.GetValue(MaxLengthProperty);
		}

		public static void SetMaxLength(DependencyObject obj, int value)
		{
			obj.SetValue(MaxLengthProperty, value);
		}

		public static readonly DependencyProperty MaxLengthProperty = DependencyProperty.RegisterAttached("MaxLength", typeof(int), typeof(ComboBoxService), new UIPropertyMetadata(OnMaxLenghtChanged));

		private static void OnMaxLenghtChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			var comboBox = obj as ComboBox;
			if (comboBox == null) return;

			comboBox.Loaded +=
				(s, e) =>
				{
					var textBox = comboBox.FindChild("PART_EditableTextBox", typeof(TextBox));
					if (textBox == null) return;

					textBox.SetValue(TextBox.MaxLengthProperty, args.NewValue);
				};
		}

	}
}
