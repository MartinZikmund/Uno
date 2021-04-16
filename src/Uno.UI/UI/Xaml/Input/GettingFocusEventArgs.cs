using System;
using Uno;

namespace Windows.UI.Xaml.Input
{
	/// <summary>
	/// Provides data for the FocusManager.GettingFocus and UIElement.GettingFocus events.
	/// </summary>
	public partial class GettingFocusEventArgs : RoutedEventArgs
	{
		internal GettingFocusEventArgs(
			FocusNavigationDirection direction,
			FocusState focusState,
			FocusInputDeviceKind inputDevice,
			DependencyObject oldFocusedElement,
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
		public DependencyObject OldFocusedElement { get; }

		/// <summary>
		/// Gets the unique ID generated when a focus movement event is initiated.
		/// </summary>
		public Guid CorrelationId { get; }

		/// <summary>
		/// Gets the most recent focused object.
		/// </summary>
		public DependencyObject NewFocusedElement { get; set; }

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

		[NotImplemented("__ANDROID__", "__IOS__", "NET461", "__WASM__", "__SKIA__", "__NETSTD_REFERENCE__", "__MACOS__")]
		public bool TryCancel()
		{
			throw new NotImplementedException("The member bool GettingFocusEventArgs.TryCancel() is not implemented in Uno.");
		}

		[NotImplemented("__ANDROID__", "__IOS__", "NET461", "__WASM__", "__SKIA__", "__NETSTD_REFERENCE__", "__MACOS__")]
		public bool TrySetNewFocusedElement(DependencyObject element)
		{
			throw new NotImplementedException("The member bool GettingFocusEventArgs.TrySetNewFocusedElement(DependencyObject element) is not implemented in Uno.");
		}
	}
}
