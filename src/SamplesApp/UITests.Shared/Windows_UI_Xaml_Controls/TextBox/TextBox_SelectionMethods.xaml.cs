using Uno.UI.Samples.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UITests.Windows_UI_Xaml_Controls.TextBox
{
	[Sample]
	public sealed partial class TextBox_SelectionMethods : Page
    {
        public TextBox_SelectionMethods()
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
