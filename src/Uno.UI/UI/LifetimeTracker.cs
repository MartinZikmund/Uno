using System;
using System.Collections.Generic;
using System.Text;
using Uno.Extensions;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace Uno.UI
{
	internal static class LifetimeTracker
	{
		private static bool _isScheduled = false;

		private static Queue<FrameworkElement> _loadingQueue = new Queue<FrameworkElement>();

		public static void AddLoading(FrameworkElement element)
		{
			_loadingQueue.Enqueue(element);
			ScheduleIfNeeded();
		}

		private static void ExecuteEvents()
		{
			while (_loadingQueue.Count > 0)
			{
				var element = _loadingQueue.Dequeue();
				element.OnLoading();
				element.OnLoaded();
			}
			_isScheduled = false;
		}

		private static void ScheduleIfNeeded()
		{
			if (!_isScheduled)
			{
				_isScheduled = true;
				CoreDispatcher.Main.RunAsync(CoreDispatcherPriority.High, (args) => ExecuteEvents());
			}
		}
	}
}
