using SampleControl.Presentation;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace SamplesApp
{
	public sealed partial class MainPage : Page
	{
		public MainPage()
		{
			this.InitializeComponent();

			//sampleControl.DataContext = new SampleChooserViewModel();
		}

		private void RootTap(object sender, PointerRoutedEventArgs e)
		{
			if (TimeFace.Visibility == Visibility.Visible)
			{
				TimeFace.Visibility = Visibility.Collapsed;
				StepsFace.Visibility = Visibility.Visible;
			}
			else if (StepsFace.Visibility == Visibility.Visible)
			{
				StepsFace.Visibility = Visibility.Collapsed;
				HeartRateFace.Visibility = Visibility.Visible;
			}
			else if (HeartRateFace.Visibility == Visibility.Visible)
			{
				HeartRateFace.Visibility = Visibility.Collapsed;
				TimeFace.Visibility = Visibility.Visible;
			}
		}
	}
}
