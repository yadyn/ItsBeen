using System;

using Funq;

using GalaSoft.MvvmLight;

namespace ItsBeen.App.ViewModels
{
	/// <summary>
	/// This class contains static references to all the view models in the
	/// application and provides an entry point for the bindings.
	/// </summary>
	public class ViewModelLocator
	{
		private static MainViewModel _main;

		internal static Container Container;

		/// <summary>
		/// Initializes a new instance of the ViewModelLocator class.
		/// </summary>
		public ViewModelLocator()
		{
			if (ViewModelBase.IsInDesignModeStatic)
			{
			}
			else
			{
				CreateMain();
				CreateEditItem();
			}
		}

		/// <summary>
		/// Gets the Main property.
		/// </summary>
		public static MainViewModel MainStatic
		{
			get
			{
				if (_main == null)
				{
					CreateMain();
				}

				return _main;
			}
		}

		/// <summary>
		/// Gets the Main property.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
			"CA1822:MarkMembersAsStatic",
			Justification = "This non-static member is needed for data binding purposes.")]
		public MainViewModel Main
		{
			get
			{
				return MainStatic;
			}
		}

		/// <summary>
		/// Provides a deterministic way to delete the Main property.
		/// </summary>
		public static void ClearMain()
		{
			if (_main != null)
			{
				_main.Cleanup();
				_main = null;
			}
		}

		/// <summary>
		/// Provides a deterministic way to create the Main property.
		/// </summary>
		public static void CreateMain()
		{
			if (_main == null)
			{
				_main = Container.Resolve<MainViewModel>();
			}
		}

		private static EditItemViewModel _editItem;

		/// <summary>
		/// Gets the EditItem property.
		/// </summary>
		public static EditItemViewModel EditItemStatic
		{
			get
			{
				if (_editItem == null)
				{
					CreateEditItem();
				}

				return _editItem;
			}
		}

		/// <summary>
		/// Gets the EditItem property.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
			"CA1822:MarkMembersAsStatic",
			Justification = "This non-static member is needed for data binding purposes.")]
		public EditItemViewModel EditItem
		{
			get
			{
				return EditItemStatic;
			}
		}

		/// <summary>
		/// Provides a deterministic way to delete the EditItem property.
		/// </summary>
		public static void ClearEditItem()
		{
			_editItem.Cleanup();
			_editItem = null;
		}

		/// <summary>
		/// Provides a deterministic way to create the EditItem property.
		/// </summary>
		public static void CreateEditItem()
		{
			if (_editItem == null)
			{
				_editItem = Container.Resolve<EditItemViewModel>();
			}
		}

		/// <summary>
		/// Cleans up all the resources.
		/// </summary>
		public static void Cleanup()
		{
			ClearMain();
			ClearEditItem();
		}
	}
}