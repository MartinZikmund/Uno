using System;
using System.Collections.Generic;
using System.Text;
using CoreGraphics;

namespace Windows.UI.Xaml.Controls
{
	internal interface INSScrollView
	{
		CGPoint UpperScrollLimit { get; }
		void SetContentOffset(CGPoint contentOffset, bool animated);
		void SetZoomScale(nfloat scale, bool animated);
	}
}
