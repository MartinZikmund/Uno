using System;
using Uno.UI.Samples.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UITests.Windows_UI_Xaml.FocusManager
{
	[Sample]
    public sealed partial class Focus_FocusCycle : Page
    {
        public Focus_FocusCycle()
        {
            InitializeComponent();
        }

		private void FocusFirstClick(object sender, RoutedEventArgs args)
		{
			B1.Focus(FocusState.Keyboard);
		}

		private async void FocusNextClick(object sender, RoutedEventArgs args)
		{
			var nextElement = Windows.UI.Xaml.Input.FocusManager.FindNextElement(Windows.UI.Xaml.Input.FocusNavigationDirection.Next);
			await Windows.UI.Xaml.Input.FocusManager.TryFocusAsync(nextElement, FocusState.Keyboard);
		}

		private async void FocusPreviousClick(object sender, RoutedEventArgs args)
		{
			var previousElement = Windows.UI.Xaml.Input.FocusManager.FindNextElement(Windows.UI.Xaml.Input.FocusNavigationDirection.Previous);
			await Windows.UI.Xaml.Input.FocusManager.TryFocusAsync(previousElement, FocusState.Keyboard);
		}
	}
}
