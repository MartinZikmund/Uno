#if !NET461

using System;
using Uno.Helpers;

namespace Windows.ApplicationModel.DataTransfer
{
	public partial class Clipboard
    {
		private static object _syncLock = new object();

		private static StartStopEventWrapper<EventHandler<object>> _contentChangedWrapper;

		static Clipboard()
		{
			_contentChangedWrapper = new StartStopEventWrapper<EventHandler<object>>(
				() => StartContentChanged(),
				() => StopContentChanged(),
				_syncLock);
		}

		public static void Flush()
		{
			// Do nothing, data available automatically even after application closes.
		}

		public static event EventHandler<object> ContentChanged
		{
			add => _contentChangedWrapper.AddHandler(value);
			remove => _contentChangedWrapper.RemoveHandler(value);
		}

		private static void OnContentChanged()
		{
			_contentChangedWrapper.Event?.Invoke(null, null);
		}
	}
}
#endif
