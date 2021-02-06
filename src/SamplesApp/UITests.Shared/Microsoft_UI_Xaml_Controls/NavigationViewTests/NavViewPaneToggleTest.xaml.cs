using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using MUXC = Microsoft.UI.Xaml.Controls;

namespace UITests.Microsoft_UI_Xaml_Controls.NavigationViewTests
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
	[Sample("NavigationView")]
    public sealed partial class NavViewPaneToggleTest : Page
    {
        public NavViewPaneToggleTest()
        {
            this.InitializeComponent();
        }

		private void NavViewToggleButton_Click(object sender, RoutedEventArgs e)
		{
			NavigationViewControl.PaneDisplayMode = MUXC.NavigationViewPaneDisplayMode.Left;


			Debug.WriteLine("Before IsPaneVisible" + NavigationViewControl.IsPaneVisible);
			Debug.WriteLine("PaneDisplayMode" + NavigationViewControl.PaneDisplayMode);
			Debug.WriteLine("IsPaneOpen" + NavigationViewControl.IsPaneOpen);
			if (NavigationViewControl.PaneDisplayMode == MUXC.NavigationViewPaneDisplayMode.LeftMinimal)
			{
				NavigationViewControl.IsPaneOpen = !NavigationViewControl.IsPaneOpen;
			}
			else if (NavigationViewControl.PaneDisplayMode == MUXC.NavigationViewPaneDisplayMode.Left)
			{
				NavigationViewControl.IsPaneVisible = !NavigationViewControl.IsPaneVisible;
				NavigationViewControl.IsPaneOpen = NavigationViewControl.IsPaneVisible;
			}
			Debug.WriteLine("After IsPaneVisible" + NavigationViewControl.IsPaneVisible);
			Debug.WriteLine("PaneDisplayMode" + NavigationViewControl.PaneDisplayMode);
			Debug.WriteLine("IsPaneOpen" + NavigationViewControl.IsPaneOpen);
		}
	}
}
