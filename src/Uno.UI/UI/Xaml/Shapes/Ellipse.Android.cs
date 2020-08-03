using Windows.Foundation;
using Android.Graphics;
using Uno.UI;

namespace Windows.UI.Xaml.Shapes
{
	public partial class Ellipse : ArbitraryShapeBase
	{
		public override void Draw(Android.Graphics.Canvas canvas)
		{
			base.Draw(canvas);
		}

		protected override void OnDraw(Canvas canvas)
		{
			base.OnDraw(canvas);
		}

		protected override Size MeasureOverride(Size availableSize)
		{
			base.MeasureOverride(availableSize);

			// Ellipse will only ask for its "minimum" defined size.
			return this.GetMinMax().min.AtLeastZero();
		}

		protected override Android.Graphics.Path GetPath(Size availableSize)
		{
			var bounds = availableSize.LogicalToPhysicalPixels();

			var output = new Android.Graphics.Path();
			output.AddOval(
				new RectF(0, 0, (float)bounds.Width, (float)bounds.Height),
				Android.Graphics.Path.Direction.Cw);

			return output;
		}
	}
}
