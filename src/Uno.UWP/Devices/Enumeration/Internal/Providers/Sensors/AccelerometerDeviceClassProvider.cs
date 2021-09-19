using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uno.Devices.Enumeration.Internal;

namespace Windows.Devices.Enumeration.Internal.Providers.Sensors
{
	internal class AccelerometerDeviceClassProvider : IDeviceClassProvider
	{
		public bool CanWatch => throw new NotImplementedException();

		public event EventHandler<DeviceInformation> WatchAdded;
		public event EventHandler<DeviceInformation> WatchEnumerationCompleted;
		public event EventHandler<DeviceInformationUpdate> WatchRemoved;
		public event EventHandler<object> WatchStopped;
		public event EventHandler<DeviceInformationUpdate> WatchUpdated;

		public Task<DeviceInformation[]> FindAllAsync() => throw new NotImplementedException();
		public void WatchStart() => throw new NotImplementedException();
		public void WatchStop() => 
	}
}
