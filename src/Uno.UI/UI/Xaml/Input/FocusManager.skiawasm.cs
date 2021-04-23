#if __WASM__ || __SKIA__
#nullable enable

using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace Windows.UI.Xaml.Input
{
	public partial class FocusManager
	{
		private static DependencyObject? InnerGetFocusedElement()
		{
			throw new NotImplementedException();
		}

		private static bool InnerTryMoveFocus(FocusNavigationDirection focusNavigationDirection)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Enumerates focusable views ordered by "cousin level".
		/// "Sister" views will be returned first, then first cousins, then second cousins, and so on.
		/// </summary>
		private static IEnumerable<DependencyObject?> SearchOtherFocusableViews(DependencyObject? currentView)
		{
			throw new NotImplementedException();
		}


		private static bool IsFocusableView(DependencyObject? view)
		{
			throw new NotImplementedException();
		}

		private static int[] GetAbsolutePosition(DependencyObject? v)
		{
			throw new NotImplementedException();
		}

		public static DependencyObject? InnerFindNextFocusableElement(FocusNavigationDirection focusNavigationDirection)
		{
			var focusedElement = GetFocusedElement() as DependencyObject;
			if (focusedElement == null)
			{
				switch (focusNavigationDirection)
				{
					case FocusNavigationDirection.Next: return InnerFindFirstFocusableElement(null);
					case FocusNavigationDirection.Previous: return InnerFindLastFocusableElement(null);
					default: return null; // TODO: Support other focus navigation directions
				}
			}

			var panel = GetParentPanel(focusedElement);
			if (panel == null)
			{
				// TODO: Implement bubbling logic when parent is not a panel
				return null;
			}
			var focusedUIElement = focusedElement as UIElement;
			var navigationMode = GetParentNavigationMode(focusedElement);
			var indexInParent = panel.Children.IndexOf(focusedUIElement);
			if (indexInParent == -1)
			{
				// TODO:
				return null;
			}

			return null;
		}

		private static Panel? GetParentPanel(DependencyObject focusedElement)
		{
			var frameworkElement = focusedElement as FrameworkElement;
			return frameworkElement?.Parent as Panel;
		}

		private static KeyboardNavigationMode GetParentNavigationMode(DependencyObject focusedElement)
		{
			var frameworkElement = focusedElement as FrameworkElement;
			var parent = frameworkElement?.Parent as UIElement;
			return parent?.TabFocusNavigation ?? KeyboardNavigationMode.Local;
		}

		public static DependencyObject? InnerFindFirstFocusableElement(DependencyObject? searchScope)
		{
			throw new NotImplementedException();
		}

		private static DependencyObject? InnerFindLastFocusableElement(DependencyObject? searchScope)
		{
			throw new NotImplementedException();
		}
	}
}
#endif
