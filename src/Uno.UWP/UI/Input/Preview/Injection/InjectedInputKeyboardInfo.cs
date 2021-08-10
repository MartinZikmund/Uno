namespace Windows.UI.Input.Preview.Injection
{
	/// <summary>
	/// Represents programmatically generated keyboard input, such as a Tab or Shift+Tab (Reverse Tabbing).
	/// </summary>
	public partial class InjectedInputKeyboardInfo
	{
		/// <summary>
		/// Creates a new InjectedInputKeyboardInfo object that is used to specify the keyboard input to inject.
		/// </summary>
		public InjectedInputKeyboardInfo()
		{
		}

		/// <summary>
		/// Gets or sets a device-independent identifier mapped to a key on a physical or software keyboard.
		/// </summary>
		/// <value>The device-independent identifier for the key on the keyboard.</value>
		public ushort VirtualKey { get; set; }

		/// <summary>
		/// Gets or sets an OEM, device-dependent identifier for a key on a physical keyboard.
		/// </summary>
		/// <value>The device-dependent identifier for the key on the keyboard.</value>
		public ushort ScanCode { get; set; }

		/// <summary>
		/// Gets or sets the various options, or modifiers, used to simulate input from physical or virtual keyboards.
		/// </summary>
		/// <value>The options, or modifiers, for the keyboard input.</value>
		public InjectedInputKeyOptions KeyOptions { get; set; }
	}
}
