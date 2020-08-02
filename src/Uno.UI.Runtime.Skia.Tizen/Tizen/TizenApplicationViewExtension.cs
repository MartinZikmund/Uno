using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;

namespace Uno.UI.Runtime.Skia
{
	public class TizenApplicationViewExtension : IApplicationViewExtension
	{
		private readonly ApplicationView _owner;
		private readonly IApplicationViewEvents _ownerEvents;

		public TizenApplicationViewExtension(object owner)
		{
			_owner = (ApplicationView)owner;
			_ownerEvents = (IApplicationViewEvents)owner;
		}

		public string Title { get => ""; set { } }

		public void ExitFullScreenMode()
		{
			
		}

		public bool TryEnterFullScreenMode()
		{
			return false;
		}
	}
}
