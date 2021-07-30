using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace App1
{
	public partial class TestControl : Control
	{
		public TestControl()
		{
			DefaultStyleKey = typeof(TestControl);
		}

		public static readonly DependencyProperty TextProperty =
			DependencyProperty.Register(nameof(Text), typeof(string), typeof(TestControl), new PropertyMetadata(null));

		[Bindable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Category("Test")]
		public string Text
		{
			get { return (string)GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}
	}
}
