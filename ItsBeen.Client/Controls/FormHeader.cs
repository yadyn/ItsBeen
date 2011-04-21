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
	/// Represents customizable header content for a
	/// <see cref="ItsBeen.Client.Controls.Form"/> control.
	/// </summary>
	public class FormHeader : ContentControl
	{
		/// <summary>
		/// Initializes the <see cref="FormHeader"/> class.
		/// </summary>
		static FormHeader()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(FormHeader), new FrameworkPropertyMetadata(typeof(FormHeader)));

			IsTabStopProperty.OverrideMetadata(typeof(FormHeader), new FrameworkPropertyMetadata(false));
		}
	}
}
