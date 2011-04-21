using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

// Taken from Karl Shifflet's LOB Form Layout series:
// http://karlshifflett.wordpress.com/2008/10/23/wpf-silverlight-lob-form-layout-searching-for-a-better-solution/

namespace ItsBeen.Client.Controls
{
	/// <summary>
	/// Represents a data entry form.
	/// </summary>
	public class Form : ItemsControl
	{
		/// <summary>
		/// Initializes the <see cref="Form"/> class.
		/// </summary>
		static Form()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(Form), new FrameworkPropertyMetadata(typeof(Form)));

			IsTabStopProperty.OverrideMetadata(typeof(Form), new FrameworkPropertyMetadata(false));
		}

		/// <summary>
		/// Determines if the specified _item is (or is eligible to be) its own container.
		/// </summary>
		/// <param name="_item">The _item to check.</param>
		/// <returns>
		/// true if the _item is (or is eligible to be) its own container; otherwise, false.
		/// </returns>
		protected override bool IsItemItsOwnContainerOverride(object item)
		{
			return (item is FormItem || item is FormHeader || item is Form);
		}
		/// <summary>
		/// Creates or identifies the element that is used to display the given _item.
		/// </summary>
		/// <returns>
		/// The element that is used to display the given _item.
		/// </returns>
		protected override DependencyObject GetContainerForItemOverride()
		{
			return new FormItem();
		}
	}
}
