#nullable enable

using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Uno.UI.Xaml.Controls
{
	/// <summary>
	/// Represents an element which may have SVG-based border
	/// </summary>
	internal interface ISupportSvgBorder
	{
		Brush BorderBrush { get; set; }

		CornerRadius CornerRadius { get; set; }
	}
}
