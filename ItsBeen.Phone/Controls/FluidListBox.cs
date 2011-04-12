using System;
using System.Windows;
using System.Windows.Controls;

using Microsoft.Phone.Reactive;

namespace ItsBeen.Phone.Controls
{
	/// <summary>
	/// Contains a list of selectable items.
	/// </summary>
	/// <remarks>
	/// This version of the ListBox will properly utilize several new
	/// LayoutStates for use in animating.
	/// </remarks>
	public class FluidListBox : ListBox
	{
		public delegate void ItemChangedDelegate(int index);

		/// <summary>
		/// Gets or sets the interval, in milliseconds, between animations
		/// for the entire list contents. The default is 100.
		/// </summary>
		/// <remarks>
		/// This interval comes into play when all list items need animating,
		/// typically when the list is first loaded or when the entire list
		/// contents are changed or reset.
		/// </remarks>
		public double ListAnimationInterval { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="FluidListBox"/> class.
		/// </summary>
		public FluidListBox()
		{
			ListAnimationInterval = 100d;

			this.Loaded += FluidListBox_Loaded;

			if (!System.ComponentModel.DesignerProperties.IsInDesignTool)
			{
				this.Opacity = 0d;
				//this.ItemContainerStyle.Setters.Add(new Setter(OpacityProperty, 0));
				this.ItemContainerGenerator.ItemsChanged += ItemContainerGenerator_ItemsChanged;
			}
		}

		/// <summary>
		/// Handles the Loaded event of the FluidListBox control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void FluidListBox_Loaded(object sender, RoutedEventArgs e)
		{
			if (System.ComponentModel.DesignerProperties.IsInDesignTool)
				return;

			this.Opacity = 0d;

			for (int i = 0; i < this.Items.Count; i++)
			{
				ListBoxItem item = (ListBoxItem)this.ItemContainerGenerator.ContainerFromIndex(i);
				if (item != null)
				{
					VisualStateManager.GoToState(item, "BeforeLoaded", false);
				}
			}

			this.Opacity = 1.0d;

			var observable = from t in Observable.Interval(TimeSpan.FromMilliseconds(ListAnimationInterval)).TimeInterval().Take(this.Items.Count)
							 select t.Value;

			observable.Subscribe(i => Dispatcher.BeginInvoke(() =>
			{
				if (this.ItemContainerGenerator.ContainerFromIndex((int)i) == null)
					return;
				VisualStateManager.GoToState(
					(ListBoxItem)this.ItemContainerGenerator.ContainerFromIndex((int)i), "AfterLoaded", true);
			}));
		}
		/// <summary>
		/// Handles the ItemsChanged event of the ItemContainerGenerator control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Controls.Primitives.ItemsChangedEventArgs"/> instance containing the event data.</param>
		private void ItemContainerGenerator_ItemsChanged(object sender, System.Windows.Controls.Primitives.ItemsChangedEventArgs e)
		{
			if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
			{
				Dispatcher.BeginInvoke(new ItemChangedDelegate(i =>
				{
					ListBoxItem item = (ListBoxItem)this.ItemContainerGenerator.ContainerFromIndex(i);
					if (item != null)
					{
						VisualStateManager.GoToState(item, "BeforeLoaded", false);
						VisualStateManager.GoToState(item, "AfterLoaded", true);
					}
				}), e.Position.Index + e.Position.Offset);
			}
			else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
			{
				Dispatcher.BeginInvoke(new ItemChangedDelegate(i =>
				{
					ListBoxItem item = (ListBoxItem)this.ItemContainerGenerator.ContainerFromIndex(i);
					if (item != null)
					{
						VisualStateManager.GoToState(item, "BeforeUnloaded", true);
					}
				}), e.Position.Index + e.Position.Offset);
			}
			else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Replace)
			{
				Dispatcher.BeginInvoke(new ItemChangedDelegate(i =>
				{
					ListBoxItem item = (ListBoxItem)this.ItemContainerGenerator.ContainerFromIndex(i);
					if (item != null)
					{
						VisualStateManager.GoToState(item, "BeforeLoaded", true);
						VisualStateManager.GoToState(item, "AfterLoaded", true);
					}
				}), e.Position.Index + e.Position.Offset);
			}
			else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Reset)
			{
				var observableBefore = from t in Observable.Interval(TimeSpan.FromMilliseconds(ListAnimationInterval / 2.0d)).TimeInterval().Take(this.Items.Count)
									   select t.Value;
				var observableAfter = from t in Observable.Interval(TimeSpan.FromMilliseconds(ListAnimationInterval / 2.0d)).TimeInterval().Take(this.Items.Count)
									  select t.Value;

				// This timer will allow us to delay slightly the afterloaded states
				System.Windows.Threading.DispatcherTimer dt = new System.Windows.Threading.DispatcherTimer();
				dt.Tick += (sen, evt) =>
					{
						dt.Stop();
						observableAfter.Subscribe(i => Dispatcher.BeginInvoke(() =>
							{
								if (this.ItemContainerGenerator.ContainerFromIndex((int)i) == null)
									return;
								VisualStateManager.GoToState(
									(ListBoxItem)this.ItemContainerGenerator.ContainerFromIndex((int)i), "AfterLoaded", true);
							}));
					};
				dt.Interval = TimeSpan.FromMilliseconds(ListAnimationInterval * 2.0d);

				observableBefore.Subscribe(i => Dispatcher.BeginInvoke(() =>
					{
						if (this.ItemContainerGenerator.ContainerFromIndex((int)i) == null)
							return;
						VisualStateManager.GoToState(
							(ListBoxItem)this.ItemContainerGenerator.ContainerFromIndex((int)i), "BeforeLoaded", true);
					}));
				dt.Start();
			}
		}
	}
}
