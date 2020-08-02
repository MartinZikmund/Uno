using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElmSharp;
using Windows.Graphics.Display;

namespace Uno.UI.Runtime.Skia
{
	public class TizenDisplayInformationExtension : IDisplayInformationExtension
	{
		private DisplayInformation _displayInformation;
		private Window _window;

		public TizenDisplayInformationExtension(object owner, Window window)
		{
			_displayInformation = (DisplayInformation)owner;
			_window = window;
		}

		public DisplayOrientations CurrentOrientation => DisplayOrientations.Portrait;

		public uint ScreenHeightInRawPixels => (uint)_window.ScreenSize.Height;

		public uint ScreenWidthInRawPixels => (uint)_window.ScreenSize.Width;

		public float LogicalDpi => 96;

		public double RawPixelsPerViewPixel => 1;

		public ResolutionScale ResolutionScale => ResolutionScale.Scale100Percent;

		public void StartDpiChanged()
		{
			
		}

		public void StopDpiChanged()
		{

		}
	}
}
