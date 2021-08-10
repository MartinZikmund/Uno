namespace Windows.UI.Input.Preview.Injection
{
	/// <summary>
	/// Contains basic pointer information common to all pointer types.
	/// </summary>
	public partial struct InjectedInputPointerInfo
	{
		/// <summary>
		/// A unique identifier for the lifetime of the pointer. A pointer is
		/// created when it enters detection range and destroyed when it leaves
		/// detection range. If a pointer goes out of detection range and then returns,
		/// it is treated as a new pointer and might be assigned a new identifier.
		/// </summary>
		public uint PointerId;

		/// <summary>
		/// The various options, or modifiers, used to simulate pointer input through
		/// InjectedInputMouseInfo, InjectedInputPenInfo, and InjectedInputTouchInfo.
		/// </summary>
		public InjectedInputPointerOptions PointerOptions;
		/// <summary>
		/// The screen coordinates of the pointer in device-independent pixel (DIP).
		/// </summary>
		public InjectedInputPoint PixelLocation;

		/// <summary>
		/// The baseline, or reference value, in milliseconds, for timed input events such as a double click/tap.
		/// </summary>
		public uint TimeOffsetInMilliseconds;

		/// <summary>
		/// A high resolution (less than one microsecond) time stamp used for time-interval measurements.
		/// </summary>
		public ulong PerformanceCount;
	}
}
