namespace Windows.UI.Input.Preview.Injection
{
	/// <summary>
	/// Represents programmatically generated pen input.
	/// </summary>
	public partial class InjectedInputPenInfo
	{
		/// <summary>
		/// Creates a new InjectedInputPenInfo object that is used to specify the pen input to inject.
		/// </summary>
		public InjectedInputPenInfo()
		{
		}

		/// <summary>
		/// Gets or sets the pen button options.
		/// </summary>
		/// <value>
		/// The pen button states.
		/// </value>
		public InjectedInputPenButtons PenButtons { get; set; }

		/// <summary>
		/// Gets or sets the pen states used to simulate pen input.
		/// </summary>
		/// <value>
		/// The pen states used to simulate pen input.
		/// </value>
		public InjectedInputPenParameters PenParameters { get; set; }

		/// <summary>
		/// Gets or sets basic pointer info common to pen input.
		/// </summary>
		/// <value>
		/// The pointer info.
		/// </value>
		public InjectedInputPointerInfo PointerInfo { get; set; }

		/// <summary>
		/// Gets or sets the force exerted by the pointer device on the surface of the digitizer.
		/// </summary>
		/// <value>
		/// A value between 0 and 1 that represents the pen contact force
		/// exerted on the digitizer surface. The default is 0.
		/// </value>
		public double Pressure { get; set; }

		/// <summary>
		/// Gets or sets the clockwise rotation, or twist, of the pointer.
		/// </summary>
		/// <value>
		/// The clockwise rotation, or twist, of the pointer normalized to a range between 0 and 359. The default is 0.
		/// </value>
		public double Rotation { get; set; }

		/// <summary>
		/// Gets or sets the angle of tilt of the pointer along the x-axis.
		/// </summary>
		/// <value>
		/// The angle of tilt of the pointer along the x-axis in a range of -90 to +90,
		/// with a positive value indicating a tilt to the right. The default is 0.
		/// </value>
		public int TiltX { get; set; }

		/// <summary>
		/// Gets or sets the angle of tilt of the pointer along the y-axis.
		/// </summary>
		/// <value>
		/// The angle of tilt of the pointer along the y-axis in a range of -90 to +90,
		/// with a positive value indicating a tilt toward the user. The default is 0.
		/// </value>
		public int TiltY { get; set; }
	}
}
