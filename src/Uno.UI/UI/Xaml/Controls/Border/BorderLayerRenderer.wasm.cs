using System;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Uno.Disposables;
using Uno.Extensions;
using Uno.UI.Xaml;
using Uno.UI.Xaml.Controls.Border;
using Uno.UI.Xaml.Controls;

namespace Windows.UI.Xaml.Shapes
{
	internal class BorderLayerRenderer
	{
		private Brush _background;
		private (Brush, Thickness) _border;
		private CornerRadius _cornerRadius;

		private SerialDisposable _backgroundSubscription;
		private SerialDisposable _borderSubscription;

		public void UpdateLayer(
			UIElement element,
			Brush background,
			BackgroundSizing backgroundSizing,
			Thickness borderThickness,
			Brush borderBrush,
			CornerRadius cornerRadius,
			object image)
		{
			if (_background != background && element is FrameworkElement fwElt)
			{
				_background = background;
				var subscription = _backgroundSubscription ??= new SerialDisposable();

				subscription.Disposable = null;
				subscription.Disposable = SetAndObserveBackgroundBrush(fwElt, background);
			}

			if (_border != (borderBrush, borderThickness))
			{
				_border = (borderBrush, borderThickness);

				var subscription = _borderSubscription ??= new SerialDisposable();

				subscription.Disposable = null;
				subscription.Disposable = SetAndObserveBorder(element, borderThickness, borderBrush);
			}

			if (_cornerRadius != cornerRadius)
			{
				_cornerRadius = cornerRadius;
				SetCornerRadius(element, cornerRadius);
			}
		}

		public static void SetCornerRadius(UIElement element, CornerRadius cornerRadius)
		{
			if (cornerRadius == CornerRadius.None)
			{
				element.ResetStyle("border-radius", "overflow");
			}
			else
			{
				var borderRadiusCssString = $"{cornerRadius.TopLeft.ToStringInvariant()}px {cornerRadius.TopRight.ToStringInvariant()}px {cornerRadius.BottomRight.ToStringInvariant()}px {cornerRadius.BottomLeft.ToStringInvariant()}px";
				element.SetStyle(
					("border-radius", borderRadiusCssString),
					("overflow", "hidden")); // overflow: hidden is required here because the clipping can't do its job when it's non-rectangular.
			}
		}

		public static IDisposable SetAndObserveBorder(UIElement element, Thickness thickness, Brush brush)
		{
			if (thickness == Thickness.Empty)
			{
				element.SetStyle(
					("border-style", "none"),
					("border-color", ""),
					("border-width", ""));
				return null;
			}
			else
			{
				var borderWidth = $"{thickness.Top.ToStringInvariant()}px {thickness.Right.ToStringInvariant()}px {thickness.Bottom.ToStringInvariant()}px {thickness.Left.ToStringInvariant()}px";
				switch (brush)
				{
					case SolidColorBrush solidColorBrush:
						var borderColor = solidColorBrush.ColorWithOpacity;
						element.SetStyle(
							("border", ""),
							("border-style", "solid"),
							("border-color", borderColor.ToHexString()),
							("border-width", borderWidth));
						return null;
					case GradientBrush gradientBrush:
						//todo:if (!RequiresSvgBasedGradientBorder(element))
						{
							var border = gradientBrush.ToCssString(element.RenderSize); // TODO: Reevaluate when size is changing
							element.SetStyle(
								("border-style", "solid"),
								("border-color", "transparent"));
								//todo:("border-image", border),
								//todo:("border-width", borderWidth));
							//todo:return null;
						}
						//else
						{
							var rectangle = new Rectangle();
							element.SetStyle("padding", borderWidth);
							rectangle.Margin = new Thickness(-thickness.Left, -thickness.Top);
							rectangle.Width = rectangle.RenderSize.Width;
							rectangle.Height= rectangle.RenderSize.Height;
							rectangle.Fill = new SolidColorBrush(Colors.Red);
							element.AddChild(rectangle);
							return Disposable.Create(() => element.RemoveChild(rectangle));
						}
					case AcrylicBrush acrylicBrush:
						var acrylicFallbackColor = acrylicBrush.FallbackColorWithOpacity;
						element.SetStyle(
							("border", ""),
							("border-style", "solid"),
							("border-color", acrylicFallbackColor.ToHexString()),
							("border-width", borderWidth));
						return null;
					default:
						element.ResetStyle("border-style", "border-color", "border-image", "border-width");
						return null;
				}
			}
		}

		private static void UpdateSvgBorder(ISupportSvgBorder element)
		{
			var svgBorder = GetCurrentSvgBorder(element);
			if (svgBorder is Rectangle rectangle)
			{
				// TODO: We currently only support uniform radius scenario.
				// A better solution would be to generate appropriate SVG shape
				// and apply radius according to corner radius.
				rectangle.RadiusX = element.CornerRadius.TopLeft;
				rectangle.RadiusY = element.CornerRadius.TopLeft;
			}
		}

		private static Shape GetCurrentSvgBorder(ISupportSvgBorder element) =>
			element switch
			{
				Border border => border.SvgBorder,
				_ => null
			};

		public static IDisposable SetAndObserveBackgroundBrush(FrameworkElement element, Brush brush)
		{
			SetBackgroundBrush(element, brush);

			if (brush is ImageBrush imgBrush)
			{
				RecalculateBrushOnSizeChanged(element, false);
				return imgBrush.Subscribe(img =>
				{
					switch (img.Kind)
					{
						case ImageDataKind.Empty:
						case ImageDataKind.Error:
							element.ResetStyle("background-color", "background-image", "background-size");
							break;

						case ImageDataKind.DataUri:
						case ImageDataKind.Url:
						default:
							element.SetStyle(
								("background-color", ""),
								("background-origin", "content-box"),
								("background-position", imgBrush.ToCssPosition()),
								("background-size", imgBrush.ToCssBackgroundSize()),
								("background-image", "url(" + img.Value + ")")
							);
							break;
					}
				});
			}
			else if (brush is AcrylicBrush acrylicBrush)
			{
				return acrylicBrush.Subscribe(element);
			}
			else
			{
				return Brush.AssignAndObserveBrush(brush, _ => SetBackgroundBrush(element, brush));
			}
		}

		public static void SetBackgroundBrush(FrameworkElement element, Brush brush)
		{
			switch (brush)
			{
				case SolidColorBrush solidColorBrush:
					var color = solidColorBrush.ColorWithOpacity;
					WindowManagerInterop.SetElementBackgroundColor(element.HtmlId, color);
					RecalculateBrushOnSizeChanged(element, false);
					break;
				case GradientBrush gradientBrush:
					WindowManagerInterop.SetElementBackgroundGradient(element.HtmlId, gradientBrush.ToCssString(element.RenderSize));
					RecalculateBrushOnSizeChanged(element, true);
					break;
				case XamlCompositionBrushBase unsupportedCompositionBrush:
					var fallbackColor = unsupportedCompositionBrush.FallbackColorWithOpacity;
					WindowManagerInterop.SetElementBackgroundColor(element.HtmlId, fallbackColor);
					RecalculateBrushOnSizeChanged(element, false);
					break;
				default:
					WindowManagerInterop.ResetElementBackground(element.HtmlId);
					RecalculateBrushOnSizeChanged(element, false);
					break;
			}
		}

		private static readonly SizeChangedEventHandler _onSizeChangedForBrushCalculation = (sender, args) =>
		{
			var fe = sender as FrameworkElement;
			SetBackgroundBrush(fe, fe.Background);
		};

		private static void RecalculateBrushOnSizeChanged(FrameworkElement element, bool shouldRecalculate)
		{
			if (shouldRecalculate)
			{
				element.SizeChanged -= _onSizeChangedForBrushCalculation;
				element.SizeChanged += _onSizeChangedForBrushCalculation;
			}
			else
			{
				element.SizeChanged -= _onSizeChangedForBrushCalculation;
			}
		}

		/// <summary>
		/// Checks whether the current brush/corner radius setup requires SVG-based border instead of CSS.
		/// </summary>
		/// <param name="element">UIElement to check.</param>
		/// <returns>True if SVG-based border is required.</returns>
		/// <remarks>
		/// We require SVG-based border if it is LinearGradientBrush and
		/// either has rounded corners (which is not possible to achieve in CSS)
		/// or uses RelativeTransform (which is not currently supported in our CSS implementation)
		/// </remarks>
		private static bool RequiresSvgBasedGradientBorder(UIElement element) =>
			element is ISupportSvgBorder borderElement &&
			borderElement.BorderBrush is LinearGradientBrush &&
			(borderElement.CornerRadius != CornerRadius.None || borderElement.BorderBrush.RelativeTransform != null);

	}
}
