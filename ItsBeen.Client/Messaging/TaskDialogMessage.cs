using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TaskDialogInterop;

namespace ItsBeen.Client.Messaging
{
	/// <summary>
	/// Use this class to send a message requesting to display a task dialog
	/// with options corresponding to this message's properties. The Callback
	/// property should be used to notify the message's sender about the user's
	/// action in the task dialog.
	/// </summary>
	public class TaskDialogMessage
	{
		/// <summary>
		/// Gets or sets the message's sender.
		/// </summary>
		public object Sender { get; protected set; }
		/// <summary>
		/// Gets or sets the message's intended target. This property can be used
		/// to give an indication as to whom the message was intended for. Of course
		/// this is only an indication, and may be null.
		/// </summary>
		public object Target { get; protected set; }
		/// <summary>
		/// Gets or sets the configuration options for the task dialog.
		/// </summary>
		public TaskDialogOptions Options { get; protected set; }
		/// <summary>
		/// Gets a callback method that should be executed to deliver the result
		/// of the task dialog to the object that sent the message.
		/// </summary>
		public Action<TaskDialogResult> Callback { get; protected set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskDialogMessage"/> class.
		/// </summary>
		/// <param name="options">The configuration options for the task dialog.</param>
		/// <param name="callback">A callback method that should be executed to deliver the result
		/// of the task dialog to the object that sent the message.</param>
		public TaskDialogMessage(
			TaskDialogOptions options,
			Action<TaskDialogResult> callback)
		{
			this.Options = options;
			this.Callback = callback;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="TaskDialogMessage"/> class.
		/// </summary>
		/// <param name="sender">The message's original sender.</param>
		/// <param name="options">The configuration options for the task dialog.</param>
		/// <param name="callback">A callback method that should be executed to deliver the result
		/// of the task dialog to the object that sent the message.</param>
		public TaskDialogMessage(
			object sender,
			TaskDialogOptions options,
			Action<TaskDialogResult> callback)
			: this(options, callback)
		{
			this.Sender = sender;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="TaskDialogMessage"/> class.
		/// </summary>
		/// <param name="sender">The message's original sender.</param>
		/// <param name="target">The message's intended target. This parameter can be used
		/// to give an indication as to whom the message was intended for. Of course
		/// this is only an indication, and may be null.</param>
		/// <param name="options">The configuration options for the task dialog.</param>
		/// <param name="callback">A callback method that should be executed to deliver the result
		/// of the task dialog to the object that sent the message.</param>
		public TaskDialogMessage(
			object sender,
			object target,
			TaskDialogOptions options,
			Action<TaskDialogResult> callback)
			: this(sender, options, callback)
		{
			this.Target = target;
		}

		/// <summary>
		/// Utility method, checks if the <see cref="Callback" /> property is
		/// null, and if it is not null, executes it.
		/// </summary>
		/// <param name="result">The result that must be passed
		/// to the task dialog message caller.</param>
		public void ProcessCallback(TaskDialogResult result)
		{
			if (Callback != null)
				Callback(result);
		}
	}
}
