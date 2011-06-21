using System;
using System.ComponentModel;

namespace ItsBeen.App.Model
{
	public class ItemModel
	{
		public Guid ID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Category { get; set; }
		public DateTime Created { get; set; }
		public DateTime LastReset { get; set; }
		public DateTime LastModified { get; set; }
		public DateTime? DueBy { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ItemModel"/> class.
		/// </summary>
		public ItemModel()
		{
			DateTime currentTime = DateTime.Now;

			this.ID = Guid.NewGuid();
			this.Name = String.Empty;
			this.Description = String.Empty;
			this.Category = String.Empty;
			this.Created = currentTime;
			this.LastReset = currentTime;
			this.LastModified = currentTime;
			this.DueBy = null;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="ItemModel"/> class.
		/// </summary>
		/// <param name="name">The name to initialize this _item with.</param>
		public ItemModel(string name)
			: this()
		{
			this.Name = name;
		}
	}
}
