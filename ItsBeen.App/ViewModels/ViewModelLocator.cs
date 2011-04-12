/*
  In App.xaml:
  <Application.Resources>
	  <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:ItsBeen.App.ViewModel"
								   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
  
  OR (WPF only):
  
  xmlns:vm="clr-namespace:ItsBeen.App.ViewModel"
  DataContext="{Binding Source={x:Static vm:ViewModelLocatorTemplate.ViewModelNameStatic}}"
*/

using GalaSoft.MvvmLight;

namespace ItsBeen.App.ViewModels
{
	/// <summary>
	/// This class contains static references to all the view models in the
	/// application and provides an entry point for the bindings.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Use the <strong>mvvmlocatorproperty</strong> snippet to add ViewModels
	/// to this locator.
	/// </para>
	/// <para>
	/// In Silverlight and WPF, place the ViewModelLocatorTemplate in the App.xaml resources:
	/// </para>
	/// <code>
	/// &lt;Application.Resources&gt;
	///     &lt;vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:ItsBeen.App.ViewModel"
	///                                  x:Key="Locator" /&gt;
	/// &lt;/Application.Resources&gt;
	/// </code>
	/// <para>
	/// Then use:
	/// </para>
	/// <code>
	/// DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
	/// </code>
	/// <para>
	/// You can also use Blend to do all this with the tool's support.
	/// </para>
	/// <para>
	/// See http://www.galasoft.ch/mvvm/getstarted
	/// </para>
	/// <para>
	/// In <strong>*WPF only*</strong> (and if databinding in Blend is not relevant), you can delete
	/// the Main property and bind to the ViewModelNameStatic property instead:
	/// </para>
	/// <code>
	/// xmlns:vm="clr-namespace:ItsBeen.App.ViewModel"
	/// DataContext="{Binding Source={x:Static vm:ViewModelLocatorTemplate.ViewModelNameStatic}}"
	/// </code>
	/// </remarks>
	public class ViewModelLocator
	{
		private static MainViewModel _main;

		/// <summary>
		/// Initializes a new instance of the ViewModelLocator class.
		/// </summary>
		public ViewModelLocator()
		{
			CreateMain();
			CreateList();
			CreateEditItem();

			if (ViewModelBase.IsInDesignModeStatic)
			{
				CreateItem();
			}
			else
			{
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
				_main = new MainViewModel();
			}
		}

		private static ItemViewModel _item;

		/// <summary>
		/// Gets the Item property.
		/// </summary>
		public static ItemViewModel ItemStatic
		{
			get
			{
				if (_item == null)
				{
					CreateItem();
				}

				return _item;
			}
		}

		/// <summary>
		/// Gets the Item property.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
			"CA1822:MarkMembersAsStatic",
			Justification = "This non-static member is needed for data binding purposes.")]
		public ItemViewModel Item
		{
			get
			{
				return ItemStatic;
			}
		}

		/// <summary>
		/// Provides a deterministic way to delete the Item property.
		/// </summary>
		public static void ClearItem()
		{
			if (_item != null)
			{
				_item.Cleanup();
				_item = null;
			}
		}

		/// <summary>
		/// Provides a deterministic way to create the Item property.
		/// </summary>
		public static void CreateItem()
		{
			if (_item == null)
			{
				_item = new ItemViewModel();
			}
		}

		private static ListViewModel _list;

		/// <summary>
		/// Gets the List property.
		/// </summary>
		public static ListViewModel ListStatic
		{
			get
			{
				if (_list == null)
				{
					CreateList();
				}

				return _list;
			}
		}

		/// <summary>
		/// Gets the List property.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
			"CA1822:MarkMembersAsStatic",
			Justification = "This non-static member is needed for data binding purposes.")]
		public ListViewModel List
		{
			get
			{
				return ListStatic;
			}
		}

		/// <summary>
		/// Provides a deterministic way to delete the List property.
		/// </summary>
		public static void ClearList()
		{
			if (_list != null)
			{
				_list.Cleanup();
				_list = null;
			}
		}

		/// <summary>
		/// Provides a deterministic way to create the List property.
		/// </summary>
		public static void CreateList()
		{
			if (_list == null)
			{
				_list = new ListViewModel();
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
				_editItem = new EditItemViewModel();
			}
		}

		/// <summary>
		/// Cleans up all the resources.
		/// </summary>
		public static void Cleanup()
		{
			ClearMain();
			ClearItem();
			ClearEditItem();
			ClearList();
		}
	}
}