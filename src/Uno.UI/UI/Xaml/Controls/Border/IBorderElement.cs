using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Uno.UI.Xaml.Controls.Border
{
	/// <summary>
	/// Represents an element with a border.
	/// </summary>
	internal interface IBorderElement
	{
		Brush BorderBrush { get; set; }

		CornerRadius CornerRadius { get; set; }
	}
}
