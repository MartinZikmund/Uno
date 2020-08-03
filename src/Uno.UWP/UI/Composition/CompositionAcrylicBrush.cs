using SkiaSharp;
using Windows.UI.Composition;

namespace Uno.UI.Composition
{
	internal class CompositionAcrylicBrush : CompositionBrush
	{
		internal CompositionAcrylicBrush() : base()
		{
		}

		internal override void UpdatePaint(SKPaint fillPaint)
		{
			
			base.UpdatePaint(fillPaint);
		}

		internal void UpdatePaint(SKSurface surface, SKPaint fillPaint)
		{
			var snap = surface.Snapshot();
			var imageShader = SKShader.CreateImage(snap);
			var opacity = 255 * Compositor.CurrentOpacity;
			var filteredImageShader = SKShader.CreateColorFilter(imageShader, SKColorFilter.CreateBlendMode(new SKColor(0xFF, 0xFF, 0xFF, (byte)opacity), SKBlendMode.Modulate));

			var blur = SKImageFilter.CreateBlur(10, 10);
			fillPaint.Shader = filteredImageShader;
			fillPaint.ImageFilter = blur;
			fillPaint.IsAntialias = true;
			fillPaint.Shader = imageShader;
		}
	}
}
