using System;
using AppKit;
using CoreGraphics;
using Windows.UI.Xaml;

namespace Uno.UI.Extensions
{
	public static class NSViewExtensions
	{
		public static CGSize SizeThatFits(this NSView view, CGSize size)
		{
			switch (view)
			{
				case NSControl control:
					return control.SizeThatFits(size);
				case FrameworkElement element:
					return element.SizeThatFits(size);
				case IHasSizeThatFits hasSizeThatFits:
					return hasSizeThatFits.SizeThatFits(size);
				default:
					return view.FittingSize;
			}
		}
	}
}
