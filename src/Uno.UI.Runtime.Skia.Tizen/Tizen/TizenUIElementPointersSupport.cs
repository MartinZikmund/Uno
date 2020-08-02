using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace Uno.UI.Runtime.Skia
{
	public class TizenUIElementPointersSupport : ICoreWindowExtension
    {
		private readonly Windows.UI.Core.CoreWindow _owner;
		private readonly Windows.UI.Xaml.IApplicationEvents _ownerEvents;

		public TizenUIElementPointersSupport(object owner)
		{
			_owner = (CoreWindow)owner;
		}
	}
}
