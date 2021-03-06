﻿using System;
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
	/// <summary>
	/// Represents a data entry form.
	/// </summary>
	[Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
	[StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(FormItem))]
	public class Form : HeaderedItemsControl
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
		/// The ContentHorizontalAlignment attached property's name.
		/// </summary>
		public const string ContentHorizontalAlignmentPropertyName = "ContentHorizontalAlignment";
		/// <summary>
		/// Identifies the ContentHorizontalAlignment attached property.
		/// </summary>
		public static readonly DependencyProperty ContentHorizontalAlignmentProperty =
			DependencyProperty.RegisterAttached(
				ContentHorizontalAlignmentPropertyName,
				typeof(HorizontalAlignment),
				typeof(Form),
				new FrameworkPropertyMetadata(HorizontalAlignment.Left));
		/// <summary>
		/// Gets the value of the ContentHorizontalAlignment attached property 
		/// for a given dependency object.
		/// </summary>
		/// <param name="d">The object for which the property value
		/// is read.</param>
		/// <returns>The value of the ContentHorizontalAlignment property of the specified object.</returns>
		[Category("Layout"),
		AttachedPropertyBrowsableForChildren]
		public static HorizontalAlignment GetContentHorizontalAlignment(DependencyObject d)
		{
			return (HorizontalAlignment)d.GetValue(ContentHorizontalAlignmentProperty);
		}
		/// <summary>
		/// Sets the value of the ContentHorizontalAlignment attached property
		/// for a given dependency object.
		/// </summary>
		/// <param name="d">The object to which the property value
		/// is written.</param>
		/// <param name="value">Sets the ContentHorizontalAlignment value of the specified object.</param>
		public static void SetContentHorizontalAlignment(DependencyObject d, HorizontalAlignment value)
		{
			d.SetValue(ContentHorizontalAlignmentProperty, value);
		}

		/// <summary>
		/// The ContentPadding attached property's name.
		/// </summary>
		public const string ContentPaddingPropertyName = "ContentPadding";
		/// <summary>
		/// Identifies the ContentPadding attached property.
		/// </summary>
		public static readonly DependencyProperty ContentPaddingProperty =
			DependencyProperty.RegisterAttached(
				ContentPaddingPropertyName,
				typeof(Thickness),
				typeof(Form),
				new FrameworkPropertyMetadata(new Thickness(0)));
		/// <summary>
		/// Gets the value of the ContentPadding attached property 
		/// for a given dependency object.
		/// </summary>
		/// <param name="d">The object for which the property value
		/// is read.</param>
		/// <returns>The value of the ContentPadding property of the specified object.</returns>
		[Category("Layout"),
		AttachedPropertyBrowsableForChildren]
		public static Thickness GetContentPadding(DependencyObject d)
		{
			return (Thickness)d.GetValue(ContentPaddingProperty);
		}
		/// <summary>
		/// Sets the value of the ContentPadding attached property
		/// for a given dependency object.
		/// </summary>
		/// <param name="d">The object to which the property value
		/// is written.</param>
		/// <param name="value">Sets the ContentPadding value of the specified object.</param>
		public static void SetContentPadding(DependencyObject d, Thickness value)
		{
			d.SetValue(ContentPaddingProperty, value);
		}

		/// <summary>
		/// The LabelVerticalAlignment attached property's name.
		/// </summary>
		public const string LabelVerticalAlignmentPropertyName = "LabelVerticalAlignment";
		/// <summary>
		/// Identifies the LabelVerticalAlignment attached property.
		/// </summary>
		public static readonly DependencyProperty LabelVerticalAlignmentProperty =
			DependencyProperty.RegisterAttached(
				LabelVerticalAlignmentPropertyName,
				typeof(VerticalAlignment),
				typeof(Form),
				new FrameworkPropertyMetadata(VerticalAlignment.Center));
		/// <summary>
		/// Gets the value of the LabelVerticalAlignment attached property 
		/// for a given dependency object.
		/// </summary>
		/// <param name="d">The object for which the property value
		/// is read.</param>
		/// <returns>The value of the LabelVerticalAlignment property of the specified object.</returns>
		[Category("Layout"),
		AttachedPropertyBrowsableForChildren]
		public static VerticalAlignment GetLabelVerticalAlignment(DependencyObject d)
		{
			return (VerticalAlignment)d.GetValue(LabelVerticalAlignmentProperty);
		}
		/// <summary>
		/// Sets the value of the LabelVerticalAlignment attached property
		/// for a given dependency object. 
		/// </summary>
		/// <param name="d">The object to which the property value
		/// is written.</param>
		/// <param name="value">Sets the LabelVerticalAlignment value of the specified object.</param>
		public static void SetLabelVerticalAlignment(DependencyObject d, VerticalAlignment value)
		{
			d.SetValue(LabelVerticalAlignmentProperty, value);
		}

		/// <summary>
		/// The LabelHorizontalContentAlignment attached property's name.
		/// </summary>
		public const string LabelHorizontalContentAlignmentPropertyName = "LabelHorizontalContentAlignment";
		/// <summary>
		/// Identifies the LabelHorizontalContentAlignment attached property.
		/// </summary>
		public static readonly DependencyProperty LabelHorizontalContentAlignmentProperty =
			DependencyProperty.RegisterAttached(
				LabelHorizontalContentAlignmentPropertyName,
				typeof(HorizontalAlignment),
				typeof(Form),
				new FrameworkPropertyMetadata(HorizontalAlignment.Left));
		/// <summary>
		/// Gets the value of the LabelHorizontalContentAlignment attached property 
		/// for a given dependency object.
		/// </summary>
		/// <param name="d">The object for which the property value
		/// is read.</param>
		/// <returns>The value of the LabelHorizontalContentAlignment property of the specified object.</returns>
		[Category("Layout"),
		AttachedPropertyBrowsableForChildren]
		public static HorizontalAlignment GetLabelHorizontalContentAlignment(DependencyObject d)
		{
			return (HorizontalAlignment)d.GetValue(LabelHorizontalContentAlignmentProperty);
		}
		/// <summary>
		/// Sets the value of the LabelHorizontalContentAlignment attached property
		/// for a given dependency object.
		/// </summary>
		/// <param name="d">The object to which the property value
		/// is written.</param>
		/// <param name="value">Sets the LabelHorizontalContentAlignment value of the specified object.</param>
		public static void SetLabelHorizontalContentAlignment(DependencyObject d, HorizontalAlignment value)
		{
			d.SetValue(LabelHorizontalContentAlignmentProperty, value);
		}

		/// <summary>
		/// The LabelPosition attached property's name.
		/// </summary>
		public const string LabelPositionPropertyName = "LabelPosition";
		/// <summary>
		/// Identifies the LabelPosition attached property.
		/// </summary>
		public static readonly DependencyProperty LabelPositionProperty =
			DependencyProperty.RegisterAttached(
				LabelPositionPropertyName,
				typeof(Dock),
				typeof(Form),
				new FrameworkPropertyMetadata(Dock.Left));
		/// <summary>
		/// Gets the value of the LabelPosition attached property 
		/// for a given dependency object.
		/// </summary>
		/// <param name="d">The object for which the property value
		/// is read.</param>
		/// <returns>The value of the LabelPosition property of the specified object.</returns>
		[Category("Layout"),
		AttachedPropertyBrowsableForChildren]
		public static Dock GetLabelPosition(DependencyObject d)
		{
			return (Dock)d.GetValue(LabelPositionProperty);
		}
		/// <summary>
		/// Sets the value of the LabelPosition attached property
		/// for a given dependency object. 
		/// </summary>
		/// <param name="d">The object to which the property value
		/// is written.</param>
		/// <param name="value">Sets the LabelPosition value of the specified object.</param>
		public static void SetLabelPosition(DependencyObject d, Dock value)
		{
			d.SetValue(LabelPositionProperty, value);
		}

		/// <summary>
		/// The LabelPadding attached property's name.
		/// </summary>
		public const string LabelPaddingPropertyName = "LabelPadding";
		/// <summary>
		/// Identifies the LabelPadding attached property.
		/// </summary>
		public static readonly DependencyProperty LabelPaddingProperty = DependencyProperty.RegisterAttached(
			LabelPaddingPropertyName,
			typeof(Thickness),
			typeof(Form),
			new FrameworkPropertyMetadata(new Thickness(0)));
		/// <summary>
		/// Gets the value of the LabelPadding attached property 
		/// for a given dependency object.
		/// </summary>
		/// <param name="d">The object for which the property value
		/// is read.</param>
		/// <returns>The value of the LabelPadding property of the specified object.</returns>
		[Category("Layout"),
		AttachedPropertyBrowsableForChildren]
		public static Thickness GetLabelPadding(DependencyObject d)
		{
			return (Thickness)d.GetValue(LabelPaddingProperty);
		}
		/// <summary>
		/// Sets the value of the LabelPadding attached property
		/// for a given dependency object.
		/// </summary>
		/// <param name="d">The object to which the property value
		/// is written.</param>
		/// <param name="value">Sets the LabelPadding value of the specified object.</param>
		public static void SetLabelPadding(DependencyObject d, Thickness value)
		{
			d.SetValue(LabelPaddingProperty, value);
		}

		/// <summary>
		/// The LabelWidth attached property's name.
		/// </summary>
		public const string LabelWidthPropertyName = "LabelWidth";
		/// <summary>
		/// Identifies the LabelWidth attached property.
		/// </summary>
		public static readonly DependencyProperty LabelWidthProperty =
			DependencyProperty.RegisterAttached(
				LabelWidthPropertyName,
				typeof(double),
				typeof(Form),
				new FrameworkPropertyMetadata(Double.NaN));
		/// <summary>
		/// Gets the value of the LabelWidth attached property 
		/// for a given dependency object.
		/// </summary>
		/// <param name="d">The object for which the property value
		/// is read.</param>
		/// <returns>The value of the LabelWidth property of the specified object.</returns>
		[Category("Layout"),
		AttachedPropertyBrowsableForChildren]
		public static double GetLabelWidth(DependencyObject d)
		{
			return (double)d.GetValue(LabelWidthProperty);
		}
		/// <summary>
		/// Sets the value of the LabelWidth attached property
		/// for a given dependency object. 
		/// </summary>
		/// <param name="d">The object to which the property value
		/// is written.</param>
		/// <param name="value">Sets the LabelWidth value of the specified object.</param>
		public static void SetLabelWidth(DependencyObject d, double value)
		{
			d.SetValue(LabelWidthProperty, value);
		}

		/// <summary>
		/// The LabelContent attached property's name.
		/// </summary>
		public const string LabelContentPropertyName = "LabelContent";
		/// <summary>
		/// Identifies the LabelContent attached property.
		/// </summary>
		[TypeConverter(typeof(StringConverter))]
		public static readonly DependencyProperty LabelContentProperty =
			DependencyProperty.RegisterAttached(
				LabelContentPropertyName,
				typeof(object),
				typeof(Form),
				new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
		/// <summary>
		/// Gets the value of the LabelContent attached property 
		/// for a given dependency object.
		/// </summary>
		/// <param name="d">The object for which the property value
		/// is read.</param>
		/// <returns>The value of the LabelContent property of the specified object.</returns>
		[Category("Common Properties"),
		AttachedPropertyBrowsableForChildren,
		TypeConverter(typeof(StringConverter))]
		public static object GetLabelContent(DependencyObject d)
		{
			return (object)d.GetValue(LabelContentProperty);
		}
		/// <summary>
		/// Sets the value of the LabelContent attached property
		/// for a given dependency object. 
		/// </summary>
		/// <param name="d">The object to which the property value
		/// is written.</param>
		/// <param name="value">Sets the LabelContent value of the specified object.</param>
		[TypeConverter(typeof(StringConverter))]
		public static void SetLabelContent(DependencyObject d, object value)
		{
			d.SetValue(LabelContentProperty, value);
		}

		/// <summary>
		/// The LabelContentTemplate attached property's name.
		/// </summary>
		public const string LabelContentTemplatePropertyName = "LabelContentTemplate";
		/// <summary>
		/// Identifies the LabelContentTemplate attached property.
		/// </summary>
		public static readonly DependencyProperty LabelContentTemplateProperty =
			DependencyProperty.RegisterAttached(
				LabelContentTemplatePropertyName,
				typeof(DataTemplate),
				typeof(Form),
				new FrameworkPropertyMetadata(null));
		/// <summary>
		/// Gets the value of the LabelContentTemplate attached property 
		/// for a given dependency object.
		/// </summary>
		/// <param name="d">The object for which the property value
		/// is read.</param>
		/// <returns>The value of the LabelContentTemplate property of the specified object.</returns>
		[AttachedPropertyBrowsableForChildren]
		public static DataTemplate GetLabelContentTemplate(DependencyObject d)
		{
			return (DataTemplate)d.GetValue(LabelContentTemplateProperty);
		}
		/// <summary>
		/// Sets the value of the LabelContentTemplate attached property
		/// for a given dependency object. 
		/// </summary>
		/// <param name="d">The object to which the property value
		/// is written.</param>
		/// <param name="value">Sets the LabelContentTemplate value of the specified object.</param>
		public static void SetLabelContentTemplate(DependencyObject d, DataTemplate value)
		{
			d.SetValue(LabelContentTemplateProperty, value);
		}

		/// <summary>
		/// The LabelContentTemplateSelector attached property's name.
		/// </summary>
		public const string LabelContentTemplateSelectorPropertyName = "LabelContentTemplateSelector";
		/// <summary>
		/// Identifies the LabelContentTemplateSelector attached property.
		/// </summary>
		public static readonly DependencyProperty LabelContentTemplateSelectorProperty =
			DependencyProperty.RegisterAttached(
				LabelContentTemplateSelectorPropertyName,
				typeof(DataTemplateSelector),
				typeof(Form),
				new FrameworkPropertyMetadata(null));
		/// <summary>
		/// Gets the value of the LabelContentTemplateSelector attached property 
		/// for a given dependency object.
		/// </summary>
		/// <param name="d">The object for which the property value
		/// is read.</param>
		/// <returns>The value of the LabelContentTemplateSelector property of the specified object.</returns>
		[AttachedPropertyBrowsableForChildren]
		public static DataTemplateSelector GetLabelContentTemplateSelector(DependencyObject d)
		{
			return (DataTemplateSelector)d.GetValue(LabelContentTemplateSelectorProperty);
		}
		/// <summary>
		/// Sets the value of the LabelContentTemplateSelector attached property
		/// for a given dependency object. 
		/// </summary>
		/// <param name="d">The object to which the property value
		/// is written.</param>
		/// <param name="value">Sets the LabelContentTemplateSelector value of the specified object.</param>
		public static void SetLabelContentTemplateSelector(DependencyObject d, DataTemplateSelector value)
		{
			d.SetValue(LabelContentTemplateSelectorProperty, value);
		}

		/// <summary>
		/// Determines if the specified item is (or is eligible to be) its own container.
		/// </summary>
		/// <param name="item">The item to check.</param>
		/// <returns>
		/// true if the item is (or is eligible to be) its own container; otherwise, false.
		/// </returns>
		protected override bool IsItemItsOwnContainerOverride(object item)
		{
			return item is FormItem;
		}
		/// <summary>
		/// Creates or identifies the element that is used to display the given item.
		/// </summary>
		/// <returns>
		/// The element that is used to display the given item.
		/// </returns>
		protected override DependencyObject GetContainerForItemOverride()
		{
			return new FormItem();
		}
	}
}
