using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Wasm;

namespace Uno.UI.Xaml.Controls
{
	public class SvgBorder : Shape
	{
		private SvgElement _borderPath = new SvgElement("g");

		public SvgBorder()
		{
		}

		protected override Size ArrangeOverride(Size finalSize)
		{


			return base.ArrangeOverride(finalSize);
		}

		protected override SvgElement GetMainSvgElement() => _borderPath;


		public Brush BorderBrush
		{
			get => (Brush)GetValue(BorderBrushProperty);
			set => SetValue(BorderBrushProperty, value);
		}

		public static DependencyProperty BorderBrushProperty =
			DependencyProperty.Register(
				nameof(BorderBrush),
				typeof(Brush),
				typeof(SvgBorder),
				new FrameworkPropertyMetadata(
					null,
					(s, e) => ((SvgBorder)s)?.OnBorderBrushChanged((Brush)e.OldValue, (Brush)e.NewValue)
				)
			);

		private void OnBorderBrushChanged(Brush oldValue, Brush newValue)
		{
			UpdateBorder();
		}

		public Thickness BorderThickness
		{
			get => (Thickness)GetValue(BorderThicknessProperty);
			set => SetValue(BorderThicknessProperty, value);
		}

		public static DependencyProperty BorderThicknessProperty =
			DependencyProperty.Register(
				nameof(BorderThickness),
				typeof(Thickness),
				typeof(SvgBorder),
				new FrameworkPropertyMetadata(
					(Thickness)Thickness.Empty,
					(s, e) => ((SvgBorder)s)?.OnBorderThicknessChanged((Thickness)e.OldValue, (Thickness)e.NewValue)
				)
			);

		private void OnBorderThicknessChanged(Thickness oldValue, Thickness newValue)
		{
			UpdateBorder();
		}

		private void UpdateBorder()
		{
		}
	}
}
