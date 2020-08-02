using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElmSharp;
using SkiaSharp;
using SkiaSharp.Views.Tizen;
using Tizen.NUI;
using Uno.Foundation.Extensibility;
using Windows.Graphics.Display;
using Windows.UI.Xaml;
using WinUI = Windows.UI.Xaml;
using WUX = Windows.UI.Xaml;

namespace Uno.UI.Runtime.Skia
{
	public class TizenHost : ISkiaHost
	{
		[ThreadStatic] private static TizenHost _current;
		private readonly Func<Application> _appBuilder;
		private readonly ElmSharp.Window _window;
		private readonly string[] _args;

		public static TizenHost Current => _current;

		/// <summary>
		/// Creates a WpfHost element to host a Uno-Skia into a WPF application.
		/// </summary>
		/// <remarks>
		/// If args are omitted, those from Environment.GetCommandLineArgs() will be used.
		/// </remarks>
		public TizenHost(Func<WinUI.Application> appBuilder, ElmSharp.Window window, string[] args = null)
		{
			_current = this;
			_appBuilder = appBuilder;
			_window = window;
			_args = args;

			_args ??= Environment
				.GetCommandLineArgs()
				.Skip(1)
				.ToArray();
			
			ApiExtensibility.Register(typeof(Windows.UI.Core.ICoreWindowExtension), o => new TizenUIElementPointersSupport(o));
			ApiExtensibility.Register(typeof(Windows.UI.ViewManagement.IApplicationViewExtension), o => new TizenApplicationViewExtension(o));
			ApiExtensibility.Register(typeof(WinUI.IApplicationExtension), o => new TizenApplicationExtension(o));
			ApiExtensibility.Register(typeof(IDisplayInformationExtension), o => new TizenDisplayInformationExtension(o, window));

			Windows.UI.Core.CoreDispatcher.DispatchOverride
				= (d) =>
				{
					//if (EcoreMainloop.IsMainThread)
					//{
					//	d();
					//}
					//else
					//{
						EcoreMainloop.PostAndWakeUp(d);
					//}
				};



			//void CreateApp(WinUI.ApplicationInitializationCallbackParams _)
			//{
			//	var app = appBuilder();
			//	app.Host = this;
			//}

			//WinUI.Application.Start(CreateApp, args);
		}

		public void Run(SKCanvasView canvas)
		{
			canvas.Resized += Canvas_Resized;
			canvas.PaintSurface += UnoCanvas_PaintSurface;
			WUX.Window.InvalidateRender
				+= () =>
				{
					canvas.Invalidate();
				};

			void CreateApp(WinUI.ApplicationInitializationCallbackParams _)
			{
				var app = _appBuilder();
				app.Host = this;
			}

			WinUI.Application.Start(CreateApp, _args);

			WinUI.Window.Current.OnNativeSizeChanged(
	new Windows.Foundation.Size(
		360,
		360
	)
);
		}

		private void Canvas_Resized(object sender, EventArgs e)
		{
			var c = (SKCanvasView)sender;
			
			var geometry = c.Geometry;

			// control is not yet fully initialized
			if (geometry.Width <= 0 || geometry.Height <= 0)
				return;


			WinUI.Window.Current.OnNativeSizeChanged(
new Windows.Foundation.Size(
geometry.Width,
geometry.Height
)
);
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
