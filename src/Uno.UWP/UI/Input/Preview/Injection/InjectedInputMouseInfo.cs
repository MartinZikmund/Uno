namespace Windows.UI.Input.Preview.Injection
{
	/// <summary>
	/// Represents programmatically generated mouse input.
	/// </summary>
	public partial class InjectedInputMouseInfo
	{
		/// <summary>
		/// Creates a new InjectedInputMouseInfo object that is used to specify the mouse input to inject.
		/// </summary>
		public InjectedInputMouseInfo()
		{
		}

		/// <summary>
		/// Gets or sets the change in value of an x-coordinate since the last mouse wheel event.
		/// </summary>
		/// <value>
		/// The number of notches or distance thresholds crossed since the last pointer event.
		/// The default value is 0.
		/// </value>
		public int DeltaX { get; set; }

		/// <summary>
		/// Gets or sets the change in value of an x-coordinate since the last mouse wheel event.
		/// </summary>
		/// <value>
		/// The number of notches or distance thresholds crossed since the last pointer event.
		/// The default value is 0.
		/// </value>
		public int DeltaY { get; set; }

		/// <summary>
		/// Gets or sets a value used by other properties. The value is based on the MouseOptions flags set.
		/// </summary>
		/// <value>The value used by other properties.</value>
		public uint MouseData { get; set; }

		/// <summary>
		/// Gets or sets the various options, or modifiers, used to simulate mouse input.
		/// </summary>
		/// <value>The options, or modifiers, for the mouse input.</value>
		public InjectedInputMouseOptions MouseOptions { get; set; }

		/// <summary>
		/// Gets or sets the baseline, or reference value, for timed input events such as a double click/tap.
		/// </summary>
		/// <value>
		/// The reference value for timed input events in milliseconds. If TimeOffsetInMilliseconds is set to 0,
		/// the current tick count is used.
		/// </value>
		public uint TimeOffsetInMilliseconds { get; set; }
	}
}
