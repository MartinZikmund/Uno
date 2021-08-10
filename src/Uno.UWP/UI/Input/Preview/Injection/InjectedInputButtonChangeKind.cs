namespace Windows.UI.Input.Preview.Injection
{
	/// <summary>
	/// Specifies the changes in state of a button associated with a pointer.
	/// </summary>
	public enum InjectedInputButtonChangeKind
	{
		/// <summary>
		/// No change in button state. Default.
		/// </summary>
		None,

		/// <summary>
		/// Indicates a primary action is initiated.
		/// A touch pointer has this flag set when
		/// it is in contact with the digitizer surface.
		/// A pen pointer has this flag set when
		/// it is in contact with the digitizer surface with no buttons pressed.
		/// A mouse pointer sets this flag when the left mouse button is down.
		/// </summary>
		FirstButtonDown,

		/// <summary>
		/// Indicates a primary action is complete.
		/// </summary>
		FirstButtonUp,

		/// <summary>
		/// Indicates a secondary action is initiated.
		/// A touch pointer does not use this flag.
		/// A pen pointer has this flag set when it is in contact with
		/// the digitizer surface with the pen barrel button pressed.
		/// A mouse pointer sets this flag when the right mouse button is down.
		/// </summary>
		SecondButtonDown,

		/// <summary>
		/// Indicates a secondary action is complete.
		/// </summary>
		SecondButtonUp,

		/// <summary>
		/// Indicates a third action is initiated.
		/// A touch pointer does not use this flag.
		/// A pen pointer does not use this flag.
		/// A mouse pointer sets this flag when the mouse wheel button is down.
		/// </summary>
		ThirdButtonDown,

		/// <summary>
		/// Indicates a third action is complete.
		/// </summary>
		ThirdButtonUp,

		/// <summary>
		/// Indicates a fourth action is initiated.
		/// A touch pointer does not use this flag.
		/// A pen pointer does not use this flag.
		/// A mouse pointer has this flag set when
		/// the first extended mouse (XBUTTON1) button is down.
		/// </summary>
		FourthButtonDown,

		/// <summary>
		/// Indicates a fourth action is complete.
		/// </summary>
		FourthButtonUp,

		/// <summary>
		/// Indicates a fifth action is initiated.
		/// A touch pointer does not use this flag.
		/// A pen pointer does not use this flag.
		/// A mouse pointer has this flag set when
		/// the second extended mouse (XBUTTON2) button is down.
		/// </summary>
		FifthButtonDown,

		/// <summary>
		/// Indicates a fifth action is complete.
		/// </summary>
		FifthButtonUp,
	}
}
