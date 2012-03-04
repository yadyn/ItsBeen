using System;
using System.Net;
using System.Windows;

namespace TaskDialogInterop
{
	/// <summary>
	/// The System icons the TaskDialog supports.
	/// </summary>
	public enum VistaTaskDialogIcon : uint
	{
		/// <summary>
		/// No Icon.
		/// </summary>
		None = 0,
		/// <summary>
		/// System warning icon.
		/// </summary>
		Warning = 0xFFFF, // MAKEINTRESOURCEW(-1)
		/// <summary>
		/// System Error icon.
		/// </summary>
		Error = 0xFFFE, // MAKEINTRESOURCEW(-2)
		/// <summary>
		/// System Information icon.
		/// </summary>
		Information = 0xFFFD, // MAKEINTRESOURCEW(-3)
		/// <summary>
		/// Shield icon.
		/// </summary>
		Shield = 0xFFFC, // MAKEINTRESOURCEW(-4)
	}
	/// <summary>
	/// Specifies the standard buttons that are displayed on a task dialog.
	/// </summary>
	public enum TaskDialogCommonButtons
	{
		/// <summary>
		/// The message box displays no buttons.
		/// </summary>
		None = 0,
		/// <summary>
		/// The message box displays a Close button.
		/// </summary>
		Close = 1,
		/// <summary>
		/// The message box displays Yes and No buttons.
		/// </summary>
		YesNo = 2,
		/// <summary>
		/// The message box displays Yes, No, and Cancel buttons.
		/// </summary>
		YesNoCancel = 3,
		/// <summary>
		/// The message box displays OK and Cancel buttons.
		/// </summary>
		OKCancel = 4,
		/// <summary>
		/// The message box displays Retry and Cancel buttons.
		/// </summary>
		RetryCancel = 5
	}
	/// <summary>
	/// Defines configuration options for showing a task dialog.
	/// </summary>
	public struct TaskDialogOptions
	{
		/// <summary>
		/// The default <see cref="T:TaskDialogOptions"/> to be used
		/// by all new <see cref="T:TaskDialog"/>s.
		/// </summary>
		/// <remarks>
		/// Use this to make application-wide defaults, such as for
		/// the caption.
		/// </remarks>
		public static TaskDialogOptions Default;

		/// <summary>
		/// The owner window of the task dialog box.
		/// </summary>
		public object Owner;
		/// <summary>
		/// Caption of the window.
		/// </summary>
		public string Title;
		/// <summary>
		/// A large 32x32 icon that signifies the purpose of the dialog, using
		/// one of the built-in system icons.
		/// </summary>
		public VistaTaskDialogIcon MainIcon;
		/// <summary>
		/// A large 32x32 icon that signifies the purpose of the dialog, using
		/// a custom Icon resource. If defined <see cref="MainIcon"/> will be
		/// ignored.
		/// </summary>
		public Icon CustomMainIcon;
		/// <summary>
		/// Principal text.
		/// </summary>
		public string MainInstruction;
		/// <summary>
		/// Supplemental text that expands on the principal text.
		/// </summary>
		public string Content;
		/// <summary>
		/// Extra text that will be hidden by default.
		/// </summary>
		public string ExpandedInfo;
		/// <summary>
		/// Indicates that the expanded info should be displayed when the
		/// dialog is initially displayed.
		/// </summary>
		public bool ExpandedByDefault;
		/// <summary>
		/// Indicates that the expanded info should be displayed at the bottom
		/// of the dialog's footer area instead of immediately after the
		/// dialog's content.
		/// </summary>
		public bool ExpandToFooter;
		/// <summary>
		/// Standard buttons.
		/// </summary>
		public TaskDialogCommonButtons CommonButtons;
		/// <summary>
		/// Application-defined options for the user.
		/// </summary>
		public string[] RadioButtons;
		/// <summary>
		/// Buttons that are not from the set of standard buttons. Use an
		/// ampersand to denote an access key.
		/// </summary>
		public string[] CustomButtons;
		/// <summary>
		/// Command link buttons.
		/// </summary>
		public string[] CommandButtons;
		/// <summary>
		/// Zero-based index of the button to have focus by default.
		/// </summary>
		public int? DefaultButtonIndex;
		/// <summary>
		/// Text accompanied by a checkbox, typically for user feedback such as
		/// Do-not-show-this-dialog-again options.
		/// </summary>
		public string VerificationText;
		/// <summary>
		/// Indicates that the verification checkbox in the dialog is checked
		/// when the dialog is initially displayed.
		/// </summary>
		public bool VerificationByDefault;
		/// <summary>
		/// A small 16x16 icon that signifies the purpose of the footer text,
		/// using one of the built-in system icons.
		/// </summary>
		public VistaTaskDialogIcon FooterIcon;
		/// <summary>
		/// A small 16x16 icon that signifies the purpose of the footer text,
		/// using a custom Icon resource. If defined <see cref="FooterIcon"/>
		/// will be ignored.
		/// </summary>
		public Icon CustomFooterIcon;
		/// <summary>
		/// Additional footer text.
		/// </summary>
		public string FooterText;
		/// <summary>
		/// Indicates that the dialog should be able to be closed using Alt-F4,
		/// Escape, and the title bar's close button even if no cancel button
		/// is specified the CommonButtons.
		/// </summary>
		/// <remarks>
		/// You'll want to set this to true if you use CustomButtons and have
		/// a Cancel-like button in it.
		/// </remarks>
		public bool AllowDialogCancellation;
		/// <summary>
		/// Indicates that a Progress Bar is to be displayed.
		/// </summary>
		/// <remarks>
		/// You can set the state, whether paused, in error, etc., as well as
		/// the range and current value by setting a callback and timer to
		/// control the dialog at custom intervals.
		/// </remarks>
		public bool ShowProgressBar;
		/// <summary>
		/// Indicates that an Marquee Progress Bar is to be displayed.
		/// </summary>
		/// <remarks>
		/// You can set start and stop the animation by setting a callback and
		/// timer to control the dialog at custom intervals.
		/// </remarks>
		public bool ShowMarqueeProgressBar;
		/// <summary>
		/// A callback that receives messages from the Task Dialog when
		/// various events occur.
		/// </summary>
		public Action Callback;
		/// <summary>
		/// Reference object that is passed to the callback.
		/// </summary>
		public object CallbackData;
		/// <summary>
		/// Indicates that the task dialog's callback is to be called
		/// approximately every 200 milliseconds.
		/// </summary>
		/// <remarks>
		/// Enable this in order to do updates on the task dialog periodically,
		/// such as for a progress bar, current download speed, or estimated
		/// time to complete, etc.
		/// </remarks>
		public bool EnableCallbackTimer;
	}
	/// <summary>
	/// Specifies identifiers to indicate the return value of a task dialog box.
	/// </summary>
	public enum TaskDialogSimpleResult
	{
		// IMPORTANT
		// Values 0 - 8 are in a very specific order to match that of the DialogResult
		//enum in the WinForms namespace. This explains the skipped numbers, as they
		//are unused values (such as Abort and Ignore). Close is not technically in the
		//original enum, but is consistent with Win32 TaskDialogIndirect behavior.

		/// <summary>
		/// Nothing is returned from the dialog box.
		/// </summary>
		None = 0,
		/// <summary>
		/// The dialog box return value is Ok (usually sent from a button
		/// labeled Ok).
		/// </summary>
		Ok = 1,
		/// <summary>
		/// The dialog box return value is Cancel (usually sent from a button
		/// labeled Cancel). Can also be as a result of clicking the red X in
		/// the top right corner.
		/// </summary>
		Cancel = 2,
		/// <summary>
		/// The dialog box return value is Retry (usually sent from a button
		/// labeled Retry).
		/// </summary>
		Retry = 4,
		/// <summary>
		/// The dialog box return value is Yes (usually sent from a button
		/// labeled Yes).
		/// </summary>
		Yes = 6,
		/// <summary>
		/// The dialog box return value is No (usually sent from a button
		/// labeled No).
		/// </summary>
		No = 7,
		/// <summary>
		/// The dialog box return value is Close (usually sent from a button
		/// labeled Close),
		/// </summary>
		Close = 8,
		/// <summary>
		/// The dialog box return value is a custom command (usually sent from
		/// a command button).
		/// </summary>
		Command = 20,
		/// <summary>
		/// The dialog box return value is a custom button (usually sent from
		/// a custom-defined button).
		/// </summary>
		Custom = 21
	}
	/// <summary>
	/// Specifies data for the return values of a task dialog box.
	/// </summary>
	public class TaskDialogResult
	{
		/// <summary>
		/// Represents a result with no data.
		/// </summary>
		public static readonly TaskDialogResult Empty = new TaskDialogResult();

		/// <summary>
		/// Gets the <see cref="T:TaskDialogSimpleResult"/> of the TaskDialog.
		/// </summary>
		public TaskDialogSimpleResult Result { get; private set; }
		/// <summary>
		/// Gets a value indicating whether or not the verification checkbox
		/// was checked. A null value indicates that the checkbox wasn't shown.
		/// </summary>
		public bool? VerificationChecked { get; private set; }
		/// <summary>
		/// Gets the zero-based index of the radio button that was clicked.
		/// A null value indicates that no radio button was clicked.
		/// </summary>
		public int? RadioButtonResult { get; private set; }
		/// <summary>
		/// Gets the zero-based index of the command button that was clicked.
		/// A null value indicates that no command button was clicked.
		/// </summary>
		public int? CommandButtonResult { get; private set; }
		/// <summary>
		/// Gets the zero-based index of the custom button that was clicked.
		/// A null value indicates that no custom button was clicked.
		/// </summary>
		public int? CustomButtonResult { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:TaskDialog.TaskDialogResult"/> class.
		/// </summary>
		private TaskDialogResult()
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="TaskDialogResult"/> class.
		/// </summary>
		/// <param name="result">The simple TaskDialog result.</param>
		/// <param name="verificationChecked">Wether the verification checkbox was checked.</param>
		/// <param name="radioButtonResult">The radio button result, if any.</param>
		/// <param name="commandButtonResult">The command button result, if any.</param>
		/// <param name="customButtonResult">The custom button result, if any.</param>
		public TaskDialogResult(TaskDialogSimpleResult result, bool? verificationChecked = null, int? radioButtonResult = null, int? commandButtonResult = null, int? customButtonResult = null)
			: this()
		{
			Result = result;
			VerificationChecked = verificationChecked;
			RadioButtonResult = radioButtonResult;
			CommandButtonResult = commandButtonResult;
			CustomButtonResult = customButtonResult;
		}
	}
}
