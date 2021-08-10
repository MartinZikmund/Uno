using System;

namespace Windows.UI.Input.Preview.Injection
{
	/// <summary>
	/// Specifies the various options, or modifiers, used to simulate
	/// input from physical or virtual keyboards through InjectedInputKeyboardInfo.
	/// This enumeration has a FlagsAttribute attribute that allows a bitwise
	/// combination of its member values.
	/// </summary>
	[Flags]
	public enum InjectedInputKeyOptions
	{
		/// <summary>
		/// No keystroke modifier. Default.
		/// </summary>
		None = 0,

		/// <summary>
		/// The key is an extended key, such as a function key or a key on the numeric keypad.
		/// </summary>
		ExtendedKey = 1,

		/// <summary>
		/// The key is released.
		/// </summary>
		KeyUp = 2,

		/// <summary>
		/// The key is a Unicode value.
		/// </summary>
		Unicode = 4,

		/// <summary>
		/// The OEM, device-dependent identifier for the key on the keyboard.A keyboard generates
		/// two scan codes when the user types a key—one when the user presses the key and
		/// another when the user releases the key.
		/// </summary>
		ScanCode = 8,
	}
}
