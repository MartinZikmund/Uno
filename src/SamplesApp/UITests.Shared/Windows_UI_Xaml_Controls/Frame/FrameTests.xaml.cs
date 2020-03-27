using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Uno.UI.Samples.Controls;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using DiagDebug = System.Diagnostics.Debug;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UITests.Windows_UI_Xaml_Controls.FrameTests
{
	[SampleControlInfo("Frame", "FrameNavigationOrder")]
    public sealed partial class FrameNavigationOrderTest : Page
    {
        public FrameNavigationOrderTest()
        {
            this.InitializeComponent();
			
        }

		public void Click(object sender, RoutedEventArgs args)
		{
			var button = new Button();
			button.Loaded += Button_Loaded;
			Content = button;
			global::System.Diagnostics.Debug.WriteLine("COntent set");
		}

		private void Button_Loaded(object sender, RoutedEventArgs e)
		{
			global::System.Diagnostics.Debug.WriteLine("Button loaded");
		}
	}

	public partial class NavigationTrackingPage : Page
	{
		public NavigationTrackingPage()
		{
			Content = new TextBlock() { Text = "Page" };
			DiagDebug.WriteLine("Page constructor");
			this.Loading += NavigationTrackingPage_Loading;
			this.Loaded += NavigationTrackingPage_Loaded;
		}

		private void NavigationTrackingPage_Loading(DependencyObject sender, object args)
		{
			DiagDebug.WriteLine("Page loading");
		}

		private void NavigationTrackingPage_Loaded(object sender, RoutedEventArgs e)
		{
			DiagDebug.WriteLine("Page loaded");
		}

		protected internal override void OnNavigatedTo(NavigationEventArgs e)
		{
			DiagDebug.WriteLine("Page NavigatedTo");
		}

		protected internal override void OnNavigatingFrom(NavigatingCancelEventArgs e)
		{
			DiagDebug.WriteLine("Page NavigatingFrom");
		}

		protected internal override void OnNavigatedFrom(NavigationEventArgs e)
		{
			DiagDebug.WriteLine("Page NavigatedFrom");
		}
	}
}
