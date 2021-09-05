using System;
using System.Linq;
using Uno.Disposables;
using Uno.Extensions;
using Windows.Foundation;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Wasm;

namespace Windows.UI.Xaml.Media
{
	public partial class LinearGradientBrush
	{
		internal override string ToCssString(Size size)
		{
			var startPoint = StartPoint;
			var endPoint = EndPoint;

			if (MappingMode != BrushMappingMode.RelativeToBoundingBox)
			{
				startPoint = new Point(startPoint.X * size.Width, startPoint.Y * size.Height);
				endPoint = new Point(endPoint.X * size.Width, endPoint.Y * size.Height);
			}

			var xDiff = endPoint.X - startPoint.X;
			var yDiff = startPoint.Y - endPoint.Y;

			var angle = Math.Atan2(xDiff, yDiff).ToStringInvariant();

			var stops = string.Join(
				",",
				GradientStops.Select(p => $"{GetColorWithOpacity(p.Color).ToHexString()} {(p.Offset * 100).ToStringInvariant()}%"));

			return $"linear-gradient({angle}rad,{stops})";
		}

		/// <summary>
		/// Generates a linearGradient element that can be used inside SVG-based views (Path, etc)
		/// </summary>
		internal override (UIElement, IDisposable) ToSvgElement(Shape target)
		{
			var linearGradient = new SvgElement("linearGradient");

			if (MappingMode == BrushMappingMode.Absolute)
			{
				linearGradient.SetAttribute("gradientUnits", "userSpaceOnUse");
			}

			linearGradient.SetAttribute(
				("x1", StartPoint.X.ToStringInvariant()),
				("y1", StartPoint.Y.ToStringInvariant()),
				("x2", EndPoint.X.ToStringInvariant()),
				("y2", EndPoint.Y.ToStringInvariant())
			);

			var disposable = new CompositeDisposable();

			if (RelativeTransform != null)
			{
				void UpdateTransform()
				{
					var size = target.RenderSize;
					var matrix = RelativeTransform.ToMatrix(Foundation.Point.Zero, size);
					matrix.M31 *= (float)size.Width;
					matrix.M32 *= (float)size.Height;
					linearGradient.SetAttribute("gradientTransform", $"matrix({matrix.M11.ToStringInvariant()}, {matrix.M12.ToStringInvariant()}, {matrix.M21.ToStringInvariant()}, {matrix.M22.ToStringInvariant()}, {matrix.M31.ToStringInvariant()}, {matrix.M32.ToStringInvariant()})");
				}

				UpdateTransform();

				target.SizeChanged += OnSizeChanged;
				disposable.Add(Disposable.Create(() => target.SizeChanged -= OnSizeChanged));

				void OnSizeChanged(object sender, object e) => UpdateTransform();
			}

			var stops = GradientStops.Select(stop => $"<stop offset=\"{stop.Offset.ToStringInvariant()}\" style=\"stop-color:{stop.Color.ToHexString()}\" />");

			linearGradient.SetHtmlContent(string.Join(Environment.NewLine, stops));

			return (linearGradient, disposable);
		}
	}
}
