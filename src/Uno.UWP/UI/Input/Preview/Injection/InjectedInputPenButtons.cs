using System;

namespace Windows.UI.Input.Preview.Injection
{
	/// <summary>
	/// Specifies the pen options used to simulate pen input through InjectedInputPenInfo.
	/// This enumeration has a FlagsAttribute attribute that allows a bitwise combination of its member values.
	/// </summary>
	[Flags]
	public enum InjectedInputPenButtons
	{
		/// <summary>
		/// No pen buttons are pressed. Default.
		/// </summary>
		None = 0,

		/// <summary>
		/// The barrel button is pressed.
		/// </summary>
		Barrel = 1,

		/// <summary>
		/// The pen is inverted.
		/// </summary>
		Inverted = 2,

		/// <summary>
		/// The eraser button is pressed.
		/// </summary>
		Eraser = 4,
	}
}
