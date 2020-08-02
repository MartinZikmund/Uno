using System;
using System.Collections.Generic;
using System.Text;
using ElmSharp;
using SkiaSharp;
using SkiaSharp.Views.Tizen;
using WUX = Windows.UI.Xaml;

namespace Uno.UI.Runtime.Skia
{
	public class UnoCanvas : SkiaSharp.Views.Tizen.SKCanvasView
	{
		public UnoCanvas(EvasObject parent) : base(parent)
		{
			this.PaintSurface += UnoCanvas_PaintSurface;
			WUX.Window.InvalidateRender
				+= () =>
				{
					Invalidate();
				};
		}

		private void UnoCanvas_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
		{
			e.Surface.Canvas.Clear(SKColors.White);

			e.Surface.Canvas.Scale((float)1f);

			WUX.Window.Current.Compositor.Render(e.Surface, e.Info);
		}
	}
}
