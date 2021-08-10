namespace Windows.UI.Input.Preview.Injection
{
	/// <summary>
	/// Contains the screen coordinates of the pointer in device-independent pixel (DIP).
	/// </summary>
	public partial struct InjectedInputPoint
	{
		/// <summary>
		/// The x-coordinate of the pointer in device-independent pixel (DIP).
		/// </summary>
		public int PositionX;

		/// <summary>
		/// The y-coordinate of the pointer in device-independent pixel (DIP).
		/// </summary>
		public int PositionY;
	}
}
