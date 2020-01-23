#if __ANDROID__
using System;
using System.Collections.Generic;
using System.Text;
using Android.App;
using Android.OS;
using Uno.Foundation;
using Uno.UI;

namespace Windows.UI.ViewManagement
{
	public partial class ApplicationViewTitleBar
	{
		private Color? _backgroundColor = null;

		public Color? BackgroundColor
		{
			get => _backgroundColor;
			set
			{
				if (_backgroundColor != value)
				{
					_backgroundColor = value;
					UpdateBackgroundColor();
				}
			}
		}

		private void UpdateBackgroundColor()
		{
			if (Build.VERSION.SdkInt < Android.OS.BuildVersionCodes.Lollipop)
			{
				return; //requires Lollipop, background is not updated
			}

			if (!(ContextHelper.Current is Activity activity))
			{
				throw new InvalidOperationException(
					"Current context must be an activity to set the BackgroundColor");
			}

			var window = activity.Window;
			window.AddFlags(Android.Views.WindowManagerFlags.DrawsSystemBarBackgrounds);
			window.ClearFlags(Android.Views.WindowManagerFlags.TranslucentStatus);
			window.SetStatusBarColor(.ToPlatformColor());
		}
	}
}
#endif
