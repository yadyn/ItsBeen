using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

// Inspired by Karl Shifflet's LOB Form Layout series:
// http://karlshifflett.wordpress.com/2008/10/23/wpf-silverlight-lob-form-layout-searching-for-a-better-solution/

namespace ItsBeen.Client.Controls
{
	public class FormItem : ContentControl
	{
		/// <summary>
		/// Initializes the <see cref="FormItem"/> class.
		/// </summary>
		static FormItem()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(FormItem), new FrameworkPropertyMetadata(typeof(FormItem)));

			IsTabStopProperty.OverrideMetadata(typeof(FormItem), new FrameworkPropertyMetadata(false));
		}

		/// <summary>
		/// The <see cref="ContentHorizontalAlignment" /> dependency property's name.
		/// </summary>
		public const string ContentHorizontalAlignmentPropertyName = "ContentHorizontalAlignment";
		/// <summary>
		/// Gets or sets the value of the <see cref="ContentHorizontalAlignment" />
		/// property. This is a dependency property.
		/// </summary>
		[Category("Layout")]
		public HorizontalAlignment ContentHorizontalAlignment
		{
			get
			{
				return (HorizontalAlignment)GetValue(ContentHorizontalAlignmentProperty);
			}
			set
			{
				SetValue(ContentHorizontalAlignmentProperty, value);
			}
		}
		/// <summary>
		/// Identifies the <see cref="ContentHorizontalAlignment" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty ContentHorizontalAlignmentProperty =
			DependencyProperty.Register(
				ContentHorizontalAlignmentPropertyName,
				typeof(HorizontalAlignment),
				typeof(FormItem),
				new UIPropertyMetadata(HorizontalAlignment.Left));

		/// <summary>
		/// The <see cref="ContentPadding" /> dependency property's name.
		/// </summary>
		public const string ContentPaddingPropertyName = "ContentPadding";
		/// <summary>
		/// Gets or sets the value of the <see cref="ContentPadding" />
		/// property. This is a dependency property.
		/// </summary>
		[Category("Layout")]
		public Thickness ContentPadding
		{
			get
			{
				return (Thickness)GetValue(ContentPaddingProperty);
			}
			set
			{
				SetValue(ContentPaddingProperty, value);
			}
		}
		/// <summary>
		/// Identifies the <see cref="ContentPadding" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty ContentPaddingProperty =
			DependencyProperty.Register(
				ContentPaddingPropertyName,
				typeof(Thickness),
				typeof(FormItem),
				new UIPropertyMetadata(new Thickness(0)));

		/// <summary>
		/// The <see cref="LabelVerticalAlignment" /> dependency property's name.
		/// </summary>
		public const string LabelVerticalAlignmentPropertyName = "LabelVerticalAlignment";
		/// <summary>
		/// Gets or sets the value of the <see cref="LabelVerticalAlignment" />
		/// property. This is a dependency property.
		/// </summary>
		[Category("Layout")]
		public VerticalAlignment LabelVerticalAlignment
		{
			get
			{
				return (VerticalAlignment)GetValue(LabelVerticalAlignmentProperty);
			}
			set
			{
				SetValue(LabelVerticalAlignmentProperty, value);
			}
		}
		/// <summary>
		/// Identifies the <see cref="LabelVerticalAlignment" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty LabelVerticalAlignmentProperty =
			DependencyProperty.Register(
				LabelVerticalAlignmentPropertyName,
				typeof(VerticalAlignment),
				typeof(FormItem),
				new UIPropertyMetadata(VerticalAlignment.Center));

		/// <summary>
		/// The <see cref="LabelHorizontalContentAlignment" /> dependency property's name.
		/// </summary>
		public const string LabelHorizontalContentAlignmentPropertyName = "LabelHorizontalContentAlignment";
		/// <summary>
		/// Gets or sets the value of the <see cref="LabelHorizontalContentAlignment" />
		/// property. This is a dependency property.
		/// </summary>
		[Category("Layout")]
		public HorizontalAlignment LabelHorizontalContentAlignment
		{
			get
			{
				return (HorizontalAlignment)GetValue(LabelHorizontalContentAlignmentProperty);
			}
			set
			{
				SetValue(LabelHorizontalContentAlignmentProperty, value);
			}
		}
		/// <summary>
		/// Identifies the <see cref="LabelHorizontalContentAlignment" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty LabelHorizontalContentAlignmentProperty =
			DependencyProperty.Register(
				LabelHorizontalContentAlignmentPropertyName,
				typeof(HorizontalAlignment),
				typeof(FormItem),
				new UIPropertyMetadata(HorizontalAlignment.Left));

		/// <summary>
		/// The <see cref="LabelPosition" /> dependency property's name.
		/// </summary>
		public const string LabelPositionPropertyName = "LabelPosition";
		/// <summary>
		/// Gets or sets the value of the <see cref="LabelPosition" />
		/// property. This is a dependency property.
		/// </summary>
		[Category("Layout")]
		public Dock LabelPosition
		{
			get
			{
				return (Dock)GetValue(LabelPositionProperty);
			}
			set
			{
				SetValue(LabelPositionProperty, value);
			}
		}
		/// <summary>
		/// Identifies the <see cref="LabelPosition" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty LabelPositionProperty =
			DependencyProperty.Register(
				LabelPositionPropertyName,
				typeof(Dock),
				typeof(FormItem),
				new UIPropertyMetadata(Dock.Left));

		/// <summary>
		/// The <see cref="LabelPadding" /> dependency property's name.
		/// </summary>
		public const string LabelPaddingPropertyName = "LabelPadding";
		/// <summary>
		/// Gets or sets the value of the <see cref="LabelPadding" />
		/// property. This is a dependency property.
		/// </summary>
		[Category("Layout")]
		public Thickness LabelPadding
		{
			get
			{
				return (Thickness)GetValue(LabelPaddingProperty);
			}
			set
			{
				SetValue(LabelPaddingProperty, value);
			}
		}
		/// <summary>
		/// Identifies the <see cref="LabelPadding" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty LabelPaddingProperty =
			DependencyProperty.Register(
				LabelPaddingPropertyName,
				typeof(Thickness),
				typeof(FormItem),
				new UIPropertyMetadata(new Thickness(0)));

		/// <summary>
		/// The <see cref="LabelWidth" /> dependency property's name.
		/// </summary>
		public const string LabelWidthPropertyName = "LabelWidth";
		/// <summary>
		/// Gets or sets the value of the <see cref="LabelWidth" />
		/// property. This is a dependency property.
		/// </summary>
		[Category("Layout")]
		public double LabelWidth
		{
			get
			{
				return (double)GetValue(LabelWidthProperty);
			}
			set
			{
				SetValue(LabelWidthProperty, value);
			}
		}
		/// <summary>
		/// Identifies the <see cref="LabelWidth" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty LabelWidthProperty =
			DependencyProperty.Register(
				LabelWidthPropertyName,
				typeof(double),
				typeof(FormItem),
				new UIPropertyMetadata(Double.NaN));

		/// <summary>
		/// The <see cref="LabelContent" /> dependency property's name.
		/// </summary>
		public const string LabelContentPropertyName = "LabelContent";
		/// <summary>
		/// Gets or sets the value of the <see cref="LabelContent" />
		/// property. This is a dependency property.
		/// </summary>
		[Category("Common Properties"),
		TypeConverter(typeof(StringConverter))]
		public object LabelContent
		{
			get
			{
				return (object)GetValue(LabelContentProperty);
			}
			set
			{
				SetValue(LabelContentProperty, value);
			}
		}
		/// <summary>
		/// Identifies the <see cref="LabelContent" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty LabelContentProperty =
			DependencyProperty.Register(
				LabelContentPropertyName,
				typeof(object),
				typeof(FormItem),
				new UIPropertyMetadata(null));

		/// <summary>
		/// The <see cref="LabelContentTemplate" /> dependency property's name.
		/// </summary>
		public const string LabelContentTemplatePropertyName = "LabelContentTemplate";
		/// <summary>
		/// Gets or sets the value of the <see cref="LabelContentTemplate" />
		/// property. This is a dependency property.
		/// </summary>
		public DataTemplate LabelContentTemplate
		{
			get
			{
				return (DataTemplate)GetValue(LabelContentTemplateProperty);
			}
			set
			{
				SetValue(LabelContentTemplateProperty, value);
			}
		}
		/// <summary>
		/// Identifies the <see cref="LabelContentTemplate" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty LabelContentTemplateProperty =
			DependencyProperty.Register(
				LabelContentTemplatePropertyName,
				typeof(DataTemplate),
				typeof(FormItem),
				new UIPropertyMetadata(null));

		/// <summary>
		/// The <see cref="LabelContentTemplateSelector" /> dependency property's name.
		/// </summary>
		public const string LabelContentTemplateSelectorPropertyName = "LabelContentTemplateSelector";
		/// <summary>
		/// Gets or sets the value of the <see cref="LabelContentTemplateSelector" />
		/// property. This is a dependency property.
		/// </summary>
		public DataTemplateSelector LabelContentTemplateSelector
		{
			get
			{
				return (DataTemplateSelector)GetValue(LabelContentTemplateSelectorProperty);
			}
			set
			{
				SetValue(LabelContentTemplateSelectorProperty, value);
			}
		}
		/// <summary>
		/// Identifies the <see cref="LabelContentTemplateSelector" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty LabelContentTemplateSelectorProperty =
			DependencyProperty.Register(
				LabelContentTemplateSelectorPropertyName,
				typeof(DataTemplateSelector),
				typeof(FormItem),
				new UIPropertyMetadata(null));
	}
}
