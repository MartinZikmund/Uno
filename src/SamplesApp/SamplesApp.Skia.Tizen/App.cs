using Tizen.Applications;
using ElmSharp;
using SkiaSharp.Views.Tizen;
using SkiaSharp;
using WUX = Windows.UI.Xaml;
using Uno.UI.Runtime.Skia;
using System.Diagnostics;

namespace SamplesApp.Skia.Tizen
{
	class App : CoreUIApplication
	{
		private readonly string[] _args;

		public App(string[] args)
		{
			_args = args;
		}

		protected override void OnCreate()
		{
			base.OnCreate();

			Window window = new Window("ElmSharpApp")
			{
				AvailableRotations = DisplayRotation.Degree_0 | DisplayRotation.Degree_180
		| DisplayRotation.Degree_270 | DisplayRotation.Degree_90
			};
			window.BackButtonPressed += (s, e) =>
			{
				Exit();
			};
			window.Show();

			var skiaView = new SKCanvasView(window);
			skiaView.Show();

			var conformant = new Conformant(window);
			conformant.Show();
			conformant.SetContent(skiaView);

			var host = new TizenHost(() => new SamplesApp.App(), window, _args);

			host.Run(skiaView);
		}

		static void Main(string[] args)
		{
			Elementary.Initialize();
			Elementary.ThemeOverlay();

			App app = new App(args);
			app.Run(args);
		}
	}
}
