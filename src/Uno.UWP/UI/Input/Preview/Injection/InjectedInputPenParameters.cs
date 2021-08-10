using System;

namespace Windows.UI.Input.Preview.Injection
{
	/// <summary>
	/// Specifies the pen states used to simulate pen input through InjectedInputPenInfo.
	/// This enumeration has a FlagsAttribute attribute that allows a bitwise combination of its member values.
	/// </summary>
	[Flags]
	public enum InjectedInputPenParameters
	{
		/// <summary>
		/// No pen state reported. Default.
		/// </summary>
		None = 0,

		/// <summary>
		/// The pen contact pressure on the digitizer surface, normalized to a range between 0 and 1024.
		/// The default is 0 if the device does not report pressure.
		/// </summary>
		Pressure = 1,

		/// <summary>
		/// The clockwise rotation, or twist, of the pointer normalized
		/// in a range of 0 to 359. The default is 0.
		/// </summary>
		Rotation = 2,

		/// <summary>
		/// The angle of tilt of the pointer along the x-axis in a range of -90 to +90, with a positive
		/// value indicating a tilt to the right. The default is 0.
		/// </summary>
		TiltX = 4,

		/// <summary>
		/// The angle of tilt of the pointer along the y-axis in a range of -90 to +90, with a positive
		/// value indicating a tilt toward the user. The default is 0.
		/// </summary>
		TiltY = 8,
	}
}
