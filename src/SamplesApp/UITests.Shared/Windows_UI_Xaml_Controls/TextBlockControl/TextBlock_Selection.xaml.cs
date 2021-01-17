using Uno.UI.Samples.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UITests.Windows_UI_Xaml_Controls.TextBlockControl
{
	[Sample]
    public sealed partial class TextBlock_Selection : Page
    {
        public TextBlock_Selection()
        {
            this.InitializeComponent();
        }

		private void SelectAll_Click(object sender, RoutedEventArgs e)
		{
			Text.SelectAll();
			Text.Focus(FocusState.Programmatic);
		}
	}
}
