#nullable enable

using System;
using Uno.UI.Xaml.Input;

namespace Windows.UI.Xaml.Input
{
	/// <summary>
	/// Provides data for the FocusManager.GettingFocus and UIElement.GettingFocus events.
	/// </summary>
	public partial class GettingFocusEventArgs : RoutedEventArgs, IHandleableRoutedEventArgs
	{
		internal GettingFocusEventArgs(
			FocusNavigationDirection direction,
			FocusState focusState,
			FocusInputDeviceKind inputDevice,
			DependencyObject? oldFocusedElement,
			Guid correlationId)
		{
			Direction = direction;
			FocusState = focusState;
			InputDevice = inputDevice;
			OldFocusedElement = oldFocusedElement;
			CorrelationId = correlationId;
		}

		/// <summary>
		/// Gets the direction that focus moved from element to element within the app UI.
		/// </summary>
		public FocusNavigationDirection Direction { get; }

		/// <summary>
		/// Gets the input mode through which an element obtained focus.
		/// </summary>
		public FocusState FocusState { get; }

		/// <summary>
		/// Gets the input device type from which input events are received.
		/// </summary>
		public FocusInputDeviceKind InputDevice { get; }

		/// <summary>
		/// Gets the last focused object.
		/// </summary>
		public DependencyObject? OldFocusedElement { get; }

		/// <summary>
		/// Gets the unique ID generated when a focus movement event is initiated.
		/// </summary>
		public Guid CorrelationId { get; }

		/// <summary>
		/// Gets the most recent focused object.
		/// </summary>
		public DependencyObject? NewFocusedElement { get; set; }

		/// <summary>
		/// Gets or sets a value that marks the routed event as handled.
		/// A true value for Handled prevents most handlers along the event
		/// route from handling the same event again.
		/// </summary>
		public bool Handled { get; set; }

		/// <summary>
		/// Gets or sets whether focus navigation should be canceled.
		/// </summary>
		public bool Cancel { get; set; }

		/// <summary>
		/// Attempts to cancel the ongoing focus action.
		/// </summary>
		/// <returns>True, if the focus action is canceled; otherwise, false.</returns>
		public bool TryCancel()
		{
			Cancel = true;
			// Currently there is no scenario where cancellation would fail.
			return true;
		}

		/// <summary>
		/// Attempts to redirect focus from the targeted element to the specified element.
		/// </summary>
		/// <param name="element">The object on which to set focus.</param>
		/// <returns>True, if the focus action is redirected; otherwise, false.</returns>
		public bool TrySetNewFocusedElement(DependencyObject? element)
		{
			NewFocusedElement = element;
			// Currently there is no scenario where setting new focused element would fail.
			return true;
		}
	}
}
