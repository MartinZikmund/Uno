#nullable enable

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Uno;
using Uno.Extensions;
using Uno.UI.Extensions;
using Uno.UI.Xaml.Controls;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace Windows.UI.Xaml.Input
{
	public sealed partial class FocusManager
	{
		private static readonly Lazy<ILogger> _log = new Lazy<ILogger>(() => typeof(FocusManager).Log());
		private static readonly Dictionary<XamlRoot, object> _focusedElements = new Dictionary<XamlRoot, object>(1);

		/// <summary>
		/// Occurs when an element within a container element (a focus scope) receives focus.
		/// This event is raised asynchronously, so focus might move before bubbling is complete.
		/// </summary>		
		public static event EventHandler<FocusManagerGotFocusEventArgs>? GotFocus;

		/// <summary>
		/// Occurs when an element within a container element (a focus scope) loses focus.
		/// This event is raised asynchronously, so focus might move again before bubbling is complete.
		/// </summary>
		public static event EventHandler<FocusManagerLostFocusEventArgs>? LostFocus;

		/// <summary>
		/// Occurs before an element actually receives focus.
		/// This event is raised synchronously to ensure focus isn't moved while the event is bubbling.
		/// </summary>
		[NotImplemented]
		public static event EventHandler<GettingFocusEventArgs>? GettingFocus;

		/// <summary>
		/// Occurs before focus moves from the current element with focus to the target element.
		/// This event is raised synchronously to ensure focus isn't moved while the event is bubbling.
		/// </summary>
		[NotImplemented]
		public static event EventHandler<LosingFocusEventArgs>? LosingFocus;

		/// <summary>
		/// Retrieves the last element that can receive focus based on the specified scope.
		/// </summary>
		/// <param name="searchScope">The root object from which to search. If null, the search scope is the current window.</param>
		/// <returns>The first focusable object.</returns>
		public static DependencyObject? FindFirstFocusableElement(DependencyObject? searchScope) =>
			InnerFindFirstFocusableElement(searchScope);

		/// <summary>
		/// Retrieves the last element that can receive focus based on the specified scope.
		/// </summary>
		/// <param name="searchScope">The root object from which to search. If null, the search scope is the current window.</param>
		/// <returns>The first focusable object.</returns>
		public static DependencyObject? FindLastFocusableElement(DependencyObject? searchScope) =>
			InnerFindLastFocusableElement(searchScope);

		/// <summary>
		/// Retrieves the element that should receive focus based on the specified navigation direction.
		/// </summary>
		/// <param name="focusNavigationDirection">The direction that focus moves from element
		/// to element within the app UI.</param>
		/// <returns>The next object to receive focus.</returns>
		/// <remarks>We recommend using this method instead of <see cref="FindNextFocusableElement(FocusNavigationDirection)" />.
		/// FindNextFocusableElement retrieves a UIElement, which returns null if the next focusable element
		/// is not a UIElement (such as a Hyperlink object).</remarks>
		[NotImplemented]
		public static DependencyObject? FindNextElement(FocusNavigationDirection focusNavigationDirection)
		{
			if (!Enum.IsDefined(typeof(FocusNavigationDirection), focusNavigationDirection))
			{
				throw new ArgumentOutOfRangeException(
					nameof(focusNavigationDirection),
					"Undefined focus navigation direction was used.");
			}

			return null;
		}

		/// <summary>
		/// Retrieves the element that should receive focus based on the specified navigation direction
		/// (cannot be used with tab navigation, see remarks).
		/// </summary>
		/// <param name="focusNavigationDirection">The direction that focus moves from element
		/// to element within the app UI</param>
		/// <param name="focusNavigationOptions">The options to help identify the next element
		/// to receive focus with keyboard/controller/remote navigation.</param>
		/// <returns>The next object to receive focus.</returns>
		/// <remarks>
		/// We recommend using this method instead of <see cref="FindNextFocusableElement(FocusNavigationDirection)" />.
		/// FindNextFocusableElement retrieves a UIElement, which returns null if the next focusable element
		/// is not a UIElement (such as a Hyperlink object).
		/// Tab navigation (FocusNavigationDirection.Previous and FocusNavigationDirection.Next) cannot be used
		/// with FindNextElementOptions. Only directional navigation (FocusNavigationDirection.Up, FocusNavigationDirection.Down,
		/// FocusNavigationDirection.Left, or FocusNavigationDirection.Right ) is valid.
		/// </remarks>
		[NotImplemented]
		public static DependencyObject? FindNextElement(FocusNavigationDirection focusNavigationDirection, FindNextElementOptions focusNavigationOptions)
		{
			if (!Enum.IsDefined(typeof(FocusNavigationDirection), focusNavigationDirection))
			{
				throw new ArgumentOutOfRangeException(
					nameof(focusNavigationDirection),
					"Invalid value of focus navigation direction was used.");
			}

			if (focusNavigationDirection == FocusNavigationDirection.Next ||
				focusNavigationDirection == FocusNavigationDirection.Previous)
			{
				throw new ArgumentOutOfRangeException(
					"Focus navigation directions Next and Previous are not supported when using FindNextElementOptions",
					nameof(focusNavigationDirection));
			}

			if (focusNavigationOptions is null)
			{
				throw new ArgumentNullException(nameof(focusNavigationOptions));
			}

			return null;
		}

		/// <summary>
		/// Retrieves the element that should receive focus based on the specified navigation direction.
		/// </summary>
		/// <param name="focusNavigationDirection">The direction that focus moves from element to element within the application UI.</param>
		/// <returns>Next focusable view depending on FocusNavigationDirection, null if focus cannot be set in the specified direction.</returns>
		public static UIElement? FindNextFocusableElement(FocusNavigationDirection focusNavigationDirection)
		{
			if (!Enum.IsDefined(typeof(FocusNavigationDirection), focusNavigationDirection))
			{
				throw new ArgumentOutOfRangeException(
					nameof(focusNavigationDirection),
					"Undefined focus navigation direction was used.");
			}

			return InnerFindNextFocusableElement(focusNavigationDirection) as UIElement;
		}

		/// <summary>
		/// Retrieves the element that should receive focus based on the specified navigation direction and hint rectangle.
		/// </summary>
		/// <param name="focusNavigationDirection">The direction that focus moves from element to element within the app UI.</param>
		/// <param name="hintRect">A bounding rectangle used to influence which element is most likely to be considered the next to receive focus.</param>
		/// <returns>Next focusable view depending on FocusNavigationDirection, null if focus cannot be set in the specified direction.</returns>
		[NotImplemented]
		public static UIElement FindNextFocusableElement(FocusNavigationDirection focusNavigationDirection, Rect hintRect)
		{
			if (!Enum.IsDefined(typeof(FocusNavigationDirection), focusNavigationDirection))
			{
				throw new ArgumentOutOfRangeException(
					nameof(focusNavigationDirection),
					"Undefined focus navigation direction was used.");
			}

			return InnerFindNextFocusableElement(focusNavigationDirection);
		}

		/// <summary>
		/// Retrieves the element in the UI that has focus, if any.
		/// </summary>
		/// <returns>The object that has focus. Typically, this is a Control class.</returns>
		public static object? GetFocusedElement() => GetFocusedElement(XamlRoot.Current);

		/// <summary>
		/// Retrieves the focused element within the XAML island container.
		/// </summary>
		/// <param name="xamlRoot">XAML island container where to search.</param>
		/// <returns></returns>
		public static object? GetFocusedElement(XamlRoot xamlRoot)
		{
			if (xamlRoot is null)
			{
				throw new ArgumentNullException(nameof(xamlRoot));
			}

			return _focusedElements.TryGetValue(xamlRoot, out var element) ? element : null;
		}

		/// <summary>
		/// Asynchronously attempts to set focus on an element when the application is initialized.
		/// </summary>
		/// <param name="element">The object on which to set focus.</param>
		/// <param name="value">One of the values from the FocusState enumeration that specify how an element can obtain focus.</param>
		/// <returns>The <see cref="FocusMovementResult" /> that indicates whether focus was successfully set.</returns>
		/// <remarks>Completes synchronously when called on an element running in the app process.</remarks>
		[NotImplemented]
		public static IAsyncOperation<FocusMovementResult> TryFocusAsync(DependencyObject element, FocusState value)
		{
			if (element is null)
			{
				throw new ArgumentNullException(nameof(element));
			}

			if (!Enum.IsDefined(typeof(FocusState), value))
			{
				throw new ArgumentOutOfRangeException(
					nameof(value),
					"Undefined focus state was used.");
			}

			return AsyncOperation.FromTask(_ =>
			{
				var result = UpdateFocus(element, FocusNavigationDirection.None, value);
				return Task.FromResult(new FocusMovementResult(result));
			});
		}

		/// <summary>
		/// Attempts to change focus from the element with focus to the next focusable element in the specified direction.
		/// </summary>
		/// <param name="focusNavigationDirection">The direction to traverse.</param>
		/// <returns>true if focus moved; otherwise, false.</returns>
		/// <remarks>The tab order is the order in which a user moves from one control to another by pressing the Tab key (forward) or Shift+Tab (backward).
		/// This method uses tab order sequence and behavior to traverse all focusable elements in the UI.
		/// If the focus is on the first element in the tab order and FocusNavigationDirection.Previous is specified, focus moves to the last element.
		/// If the focus is on the last element in the tab order and FocusNavigationDirection.Next is specified, focus moves to the first element.
		/// Other directions are not supported on all platforms.
		/// </remarks>
		public static bool TryMoveFocus(FocusNavigationDirection focusNavigationDirection)
		{
			if (!Enum.IsDefined(typeof(FocusNavigationDirection), focusNavigationDirection))
			{
				throw new ArgumentOutOfRangeException(
					nameof(focusNavigationDirection),
					"Undefined focus navigation direction was used.");
			}

			return InnerTryMoveFocus(focusNavigationDirection);
		}

		/// <summary>
		/// Attempts to change focus from the element with focus to the next focusable element in the specified direction,
		/// using the specified navigation options.
		/// </summary>
		/// <param name="focusNavigationDirection">The direction to traverse (in tab order).</param>
		/// <param name="focusNavigationOptions">The options to help identify the next element to receive focus with keyboard/controller/remote navigation.</param>
		/// <returns>true if focus moved; otherwise, false.</returns>
		/// <remarks>The tab order is the order in which a user moves from one control to another by pressing the Tab key (forward) or Shift+Tab (backward).
		/// This method uses tab order sequence and behavior to traverse all focusable elements in the UI.
		/// If the focus is on the first element in the tab order and FocusNavigationDirection.Previous is specified, focus moves to the last element.
		/// If the focus is on the last element in the tab order and FocusNavigationDirection.Next is specified, focus moves to the first element.
		/// Other directions are not supported on all platforms.
		/// </remarks>
		[NotImplemented]
		public static bool TryMoveFocus(FocusNavigationDirection focusNavigationDirection, FindNextElementOptions focusNavigationOptions)
		{
			if (!Enum.IsDefined(typeof(FocusNavigationDirection), focusNavigationDirection))
			{
				throw new ArgumentOutOfRangeException(
					nameof(focusNavigationDirection),
					"Invalid value of focus navigation direction was used.");
			}

			if (focusNavigationDirection == FocusNavigationDirection.Next ||
				focusNavigationDirection == FocusNavigationDirection.Previous)
			{
				throw new ArgumentOutOfRangeException(
					"Focus navigation directions Next and Previous are not supported when using FindNextElementOptions",
					nameof(focusNavigationDirection));
			}

			if (focusNavigationOptions is null)
			{
				throw new ArgumentNullException(nameof(focusNavigationOptions));
			}

			return InnerTryMoveFocus(focusNavigationDirection);
		}

		/// <summary>
		/// Asynchronously attempts to change focus from the current element
		/// with focus to the next focusable element in the specified direction.
		/// </summary>
		/// <param name="focusNavigationDirection">The direction that focus moves from element to element within the app UI.</param>
		/// <returns>The <see cref="FocusMovementResult" /> that indicates whether focus was successfully set.</returns>
		[NotImplemented]
		public static IAsyncOperation<FocusMovementResult> TryMoveFocusAsync(FocusNavigationDirection focusNavigationDirection)
		{
			if (!Enum.IsDefined(typeof(FocusNavigationDirection), focusNavigationDirection))
			{
				throw new ArgumentOutOfRangeException(
					nameof(focusNavigationDirection),
					"Undefined focus navigation direction was used.");
			}

			return AsyncOperation.FromTask(_ =>
			{
				var result = InnerTryMoveFocus(focusNavigationDirection);
				return Task.FromResult(new FocusMovementResult(result));
			});
		}

		/// <summary>
		/// Asynchronously attempts to change focus from the current element
		/// with focus to the next focusable element in the specified direction.
		/// </summary>
		/// <param name="focusNavigationDirection">The direction that focus moves from element to element within the app UI.</param>
		/// <param name="focusNavigationOptions">The navigation options used to identify the focus candidate.</param>
		/// <returns>The <see cref="FocusMovementResult" /> that indicates whether focus was successfully set.</returns>
		[NotImplemented]
		public static IAsyncOperation<FocusMovementResult> TryMoveFocusAsync(FocusNavigationDirection focusNavigationDirection, FindNextElementOptions focusNavigationOptions)
		{
			if (!Enum.IsDefined(typeof(FocusNavigationDirection), focusNavigationDirection))
			{
				throw new ArgumentOutOfRangeException(
					nameof(focusNavigationDirection),
					"Invalid value of focus navigation direction was used.");
			}

			if (focusNavigationDirection == FocusNavigationDirection.Next ||
				focusNavigationDirection == FocusNavigationDirection.Previous)
			{
				throw new ArgumentOutOfRangeException(
					"Focus navigation directions Next and Previous are not supported when using FindNextElementOptions",
					nameof(focusNavigationDirection));
			}

			if (focusNavigationOptions is null)
			{
				throw new ArgumentNullException(nameof(focusNavigationOptions));
			}

			return AsyncOperation.FromTask(_ =>
			{
				var result = InnerTryMoveFocus(focusNavigationDirection);
				return Task.FromResult(new FocusMovementResult(result));
			});
		}

		internal static bool SetFocusedElement(DependencyObject newFocus, FocusNavigationDirection focusNavigationDirection, FocusState focusState)
		{
			var control = newFocus as Control; // For now only called for Control
			if (control != null && !control.IsFocusable)
			{
				control = control.FindFirstChild<Control>(c => c.IsFocusable);
			}

			if (control == null)
			{
				return false;
			}

			return UpdateFocus(control, focusNavigationDirection, focusState);
		}

		private static bool UpdateFocus(DependencyObject newFocus, FocusNavigationDirection focusNavigationDirection, FocusState focusState)
		{
			// TODO: check AllowFocusOnInteraction
			var oldFocusedElement = GetFocusedElement();

			if (_log.Value.IsEnabled(LogLevel.Debug))
			{
				_log.Value.LogDebug($"{nameof(UpdateFocus)}()- oldFocus={oldFocusedElement}, newFocus={newFocus}, oldFocus.FocusState={(oldFocusedElement as Control)?.FocusState}, focusState={focusState}");
			}

			if (newFocus == oldFocusedElement)
			{
				if (newFocus is Control newFocusAsControl && newFocusAsControl.FocusState != focusState)
				{
					// We do not raise GettingFocus here since the OldFocusedElement and NewFocusedElement
					// would be the same element.
					RaiseGotFocusEvent(oldFocusedElement);

					// Make sure the FocusState is up-to-date.
					newFocusAsControl.UpdateFocusState(focusState);
				}
				// No change in focus element - can skip the rest of this method.
				return true;
			}

			//TODO: RaiseAndProcessGettingAndLosingFocusEvents

			(oldFocusedElement as Control)?.UpdateFocusState(FocusState.Unfocused); // Set previous unfocused

			// Update the focused control
			SetFocusedElement(newFocus);

			(newFocus as Control)?.UpdateFocusState(focusState);

			FocusNative(newFocus as Control);
			UpdateFocusVisual(newFocus, focusState);

			if (oldFocusedElement != null)
			{
				RaiseLostFocusEvent(oldFocusedElement);
			}

			if (newFocus != null)
			{
				RaiseGotFocusEvent(newFocus);
			}

			return true;
		}

		private static void UpdateFocusVisual(DependencyObject newFocus, FocusState focusState)
		{
			var focusVisualLayer = Window.Current.FocusVisualLayer;
			SystemFocusVisual focusVisual;
			if (focusVisualLayer.Children.Count == 0)
			{
				focusVisualLayer.Children.Add(new SystemFocusVisual());
			}

			focusVisual = (SystemFocusVisual)focusVisualLayer.Children[0];

			if (newFocus is Control control && focusState == FocusState.Keyboard && control.UseSystemFocusVisuals)
			{
				focusVisual.FocusedElement = control;
				focusVisual.Visibility = Visibility.Visible;
			}
			else
			{
				focusVisual.FocusedElement = null;
				focusVisual.Visibility = Visibility.Collapsed;
			}
		}

		private static void RaiseLostFocusEvent(object oldFocus)
		{
			void OnLostFocus()
			{
				if (oldFocus is UIElement uiElement)
				{
					uiElement.RaiseEvent(UIElement.LostFocusEvent, new RoutedEventArgs(uiElement));
				}

				// we replay all "lost focus" events
				LostFocus?.Invoke(null, new FocusManagerLostFocusEventArgs { OldFocusedElement = oldFocus as DependencyObject });
			}

			CoreDispatcher.Main.RunAsync(CoreDispatcherPriority.Normal, OnLostFocus); // event is rescheduled, as on UWP
		}

		private static void RaiseGotFocusEvent(object newFocus)
		{
			void OnGotFocus()
			{
				if (newFocus is UIElement uiElement)
				{
					uiElement.RaiseEvent(UIElement.GotFocusEvent, new RoutedEventArgs(uiElement));
				}

				GotFocus?.Invoke(null, new FocusManagerGotFocusEventArgs { NewFocusedElement = newFocus as DependencyObject });
			}

			CoreDispatcher.Main.RunAsync(CoreDispatcherPriority.Normal, OnGotFocus); // event is rescheduled, as on UWP
		}

		private static void SetFocusedElement(object element) => SetFocusedElement(XamlRoot.Current, element);

		private static void SetFocusedElement(XamlRoot xamlRoot, object element)
		{
			_focusedElements[xamlRoot] = element;
		}
	}
}
