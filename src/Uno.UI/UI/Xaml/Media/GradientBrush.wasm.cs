using System;
using Windows.Foundation;
using Windows.UI.Xaml.Shapes;

namespace Windows.UI.Xaml.Media
{
	partial class GradientBrush
	{
		internal abstract string ToCssString(Size size);

		internal abstract (UIElement, IDisposable) ToSvgElement(Shape target, Action invalidate);
	}
}
