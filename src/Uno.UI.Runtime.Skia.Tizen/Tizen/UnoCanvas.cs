using System;
using System.Collections.Generic;
using System.Text;
using ElmSharp;
using SkiaSharp;
using SkiaSharp.Views.Tizen;
using WUX = Windows.UI.Xaml;

namespace Uno.UI.Runtime.Skia
{
	public class UnoCanvas : SKCanvasView
	{
		private bool _sizeInitialized = false;

		public UnoCanvas(EvasObject parent) : base(parent)
		{
			PaintSurface += UnoCanvas_PaintSurface;
			Resized += UnoCanvas_Resized;

			WUX.Window.InvalidateRender
				+= () =>
				{
					Invalidate();
				};
		}

		private void UnoCanvas_Resized(object sender, EventArgs e)
		{
			var c = (SKCanvasView)sender;

			var geometry = c.Geometry;

			// control is not yet fully initialized
			if (geometry.Width <= 0 || geometry.Height <= 0)
				return;

			WUX.Window.Current.OnNativeSizeChanged(
				new Windows.Foundation.Size(
				geometry.Width,
				geometry.Height));
		}

		private void UnoCanvas_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
		{
			e.Surface.Canvas.Clear(SKColors.Blue);
			var scale = (float)ScalingInfo.ScalingFactor;
			var scaledSize = new SKSize(e.Info.Width / scale, e.Info.Height / scale);

			e.Surface.Canvas.Scale(scale);
			WUX.Window.Current.Compositor.Render(e.Surface, e.Info);
		}
	}
}
