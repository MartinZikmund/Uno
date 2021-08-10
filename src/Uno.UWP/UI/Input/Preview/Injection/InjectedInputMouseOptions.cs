using System;

namespace Windows.UI.Input.Preview.Injection
{
	/// <summary>
	/// Specifies the various options, or modifiers, used to simulate mouse input through InjectedInputMouseInfo.
	/// This enumeration has a FlagsAttribute attribute that allows a bitwise combination of its member values.
	/// </summary>
	[Flags]
	public enum InjectedInputMouseOptions
	{
		/// <summary>
		/// No mouse modifier. Default.
		/// </summary>
		None = 0,

		/// <summary>
		/// Move (coalesce move messages). If a mouse event occurs and the application has not yet processed
		/// the previous mouse event, the previous one is thrown away. See MoveNoCoalesce.
		/// </summary>
		Move = 1,

		/// <summary>
		/// Left mouse button pressed.
		/// </summary>
		LeftDown = 2,

		/// <summary>
		/// Left mouse button released.
		/// </summary>
		LeftUp = 4,

		/// <summary>
		/// Right mouse button pressed.
		/// </summary>
		RightDown = 8,

		/// <summary>
		/// Right mouse button released.
		/// </summary>
		RightUp = 16,

		/// <summary>
		/// Middle mouse button pressed.
		/// </summary>
		MiddleDown = 32,

		/// <summary>
		/// Middle mouse button released.
		/// </summary>
		MiddleUp = 64,

		/// <summary>
		/// XBUTTON pressed.
		/// </summary>
		XDown = 128,

		/// <summary>
		/// XBUTTON released.
		/// </summary>
		XUp = 256,

		/// <summary>
		/// Mouse wheel.
		/// </summary>
		Wheel = 2048,

		/// <summary>
		/// Mouse tilt wheel.
		/// </summary>
		HWheel = 4096,

		/// <summary>
		/// Move (do not coalesce move messages).
		/// The application processes all mouse events since the previously processed mouse event. See Move.
		/// </summary>
		MoveNoCoalesce = 8192,

		/// <summary>
		/// Map coordinates to the entire virtual desktop.
		/// </summary>
		VirtualDesk = 16384,

		/// <summary>
		/// Normalized absolute coordinates between 0 and 65,535. If the flag is not set,
		/// relative data (the change in position since the last reported position) is used.
		/// Coordinate (0,0) maps onto the upper-left corner of the display surface;
		/// coordinate (65535,65535) maps onto the lower-right corner. In a multi-monitor
		/// system, the coordinates map to the primary monitor.
		/// </summary>
		Absolute = 32768,
	}
}
