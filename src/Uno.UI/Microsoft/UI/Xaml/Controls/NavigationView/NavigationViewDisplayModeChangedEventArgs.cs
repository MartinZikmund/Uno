﻿namespace Microsoft.UI.Xaml.Controls
{
	/// <summary>
	/// Provides data for the NavigationView.DisplayModeChanged event.
	/// </summary>
	public sealed partial class NavigationViewDisplayModeChangedEventArgs
    {
		internal NavigationViewDisplayModeChangedEventArgs(NavigationViewDisplayMode displayMode) =>
			DisplayMode = displayMode;

		/// <summary>
		/// Gets the new display mode.
		/// </summary>
		public NavigationViewDisplayMode DisplayMode { get; }
	}
}
