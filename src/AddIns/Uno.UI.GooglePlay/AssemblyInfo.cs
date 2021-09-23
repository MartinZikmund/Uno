using Windows.UI.ViewManagement;
using Uno.Devices.Sensors;
using Uno.Foundation.Extensibility;

[assembly: ApiExtension(typeof(IApplicationViewSpanningRects), typeof(DuoApplicationViewSpanningRects))]
[assembly: ApiExtension(typeof(INativeHingeAngleSensor), typeof(DuoHingeAngleSensor))]
