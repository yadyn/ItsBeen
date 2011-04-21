using System;
using System.Text;
using System.Windows.Input;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

using ItsBeen.App.Model;
using ItsBeen.App.Services;
using ItsBeen.App.Messaging;

namespace ItsBeen.App.ViewModels
{
	/// <summary>
	/// This class contains properties that a View can data bind to.
	/// </summary>
	public class ItemViewModel : AppViewModel
	{
		private static readonly string NamePropertyName = "Name";
		private static readonly string DescriptionPropertyName = "Description";
		private static readonly string LastUpdatedPropertyName = "LastUpdated";
		private static readonly string CreatedPropertyName = "Created";
		private static readonly string DueByPropertyName = "DueBy";
		private static readonly string TimeSincePropertyName = "TimeSince";

		private static DateTime tickTime;
		private static event EventHandler tickEvent;

		private ItemModel item;
		private TimeSpan timeSince;

		/// <summary>
		/// Initializes a new instance of the <see cref="ItemViewModel"/> class.
		/// </summary>
		public ItemViewModel()
		{
			RegisterForMessages();

			if (IsInDesignMode)
				this.timeSince = new TimeSpan((TimeSpan.TicksPerDay * 397) + TimeSpan.TicksPerHour + TimeSpan.TicksPerMinute + TimeSpan.TicksPerSecond);
			else
				this.timeSince = TimeSpan.Zero;

			ItemViewModel.tickEvent += OnTick;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="ItemViewModel"/> class.
		/// </summary>
		/// <param name="_item">The _item model to work with.</param>
		public ItemViewModel(ItemModel item) : this()
		{
			this.item = item;
			UpdateTimeSince();
		}

		public ItemModel Item
		{
			get
			{
				return item;
			}
		}
		public string Name
		{
			get
			{
				return item == null ? NamePropertyName : item.Name;
			}
			set
			{
				item.Name = value;
				RaisePropertyChanged(NamePropertyName);
			}
		}
		public string Description
		{
			get
			{
				return item == null ? DescriptionPropertyName : item.Description;
			}
			set
			{
				item.Description = value;
				RaisePropertyChanged(DescriptionPropertyName);
			}
		}
		public DateTime LastUpdated
		{
			get
			{
				if (item == null)
				{
					return (IsInDesignMode) ? DateTime.Now.AddTicks(((TimeSpan.TicksPerDay * 397) + TimeSpan.TicksPerHour + TimeSpan.TicksPerMinute + TimeSpan.TicksPerSecond) * -1) : DateTime.Now;
				}
				else
					return item.LastUpdated;
			}
			private set
			{
				item.LastUpdated = value;
				RaisePropertyChanged(LastUpdatedPropertyName);
			}
		}
		public DateTime Created
		{
			get
			{
				if (item == null)
				{
					return (IsInDesignMode) ? DateTime.Now.AddTicks(TimeSpan.TicksPerDay * -400) : DateTime.Now;
				}
				else
					return item.Created;
			}
			set
			{
				item.Created = value;
				RaisePropertyChanged(CreatedPropertyName);
			}
		}
		public DateTime? DueBy
		{
			get
			{
				return item.DueBy;
			}
		}
		public TimeSpan TimeSince
		{
			get
			{
				return timeSince;
			}
			private set
			{
				timeSince = value;
				RaisePropertyChanged(TimeSincePropertyName);
			}
		}

		public void Reset()
		{
			LastUpdated = DateTime.Now;
			RaisePropertyChanged(DueByPropertyName);
		}
		public void UpdateTimeSince()
		{
			TimeSince = (DateTime.Now - LastUpdated).Duration();
		}

		/// <summary>
		/// Handles Tick timer events for all instances of this view model.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		public static void HandleTick(object sender, EventArgs e)
		{
			tickTime = DateTime.Now;
			RaiseTickEvent();
		}
		/// <summary>
		/// Raises a private internal tick event for instances.
		/// </summary>
		/// <remarks>
		/// The need for this separation is to ensure the static handler always
		/// goes first.
		/// The static handler will handle the real Tick event from a Timer,
		/// it will update an internal time value, and then raise a private
		/// tick event for all instances to handle on their own.
		/// </remarks>
		private static void RaiseTickEvent()
		{
			if (tickEvent != null)
				tickEvent(null, EventArgs.Empty);
		}

		/// <summary>
		/// Handles the private tick event raised by the static Tick handler.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void OnTick(object sender, EventArgs e)
		{
			UpdateTimeSince();
		}

		private void RegisterForMessages()
		{
		}
	}
}