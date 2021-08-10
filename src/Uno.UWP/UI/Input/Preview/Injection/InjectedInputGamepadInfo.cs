using Windows.Gaming.Input;

namespace Windows.UI.Input.Preview.Injection
{
	/// <summary>
	/// Represents programmatically generated gamepad input.
	/// </summary>
	public partial class InjectedInputGamepadInfo
	{
		/// <summary>
		/// Creates a new InjectedInputGamepadInfo object that is used to specify
		/// the gamepad input to inject based on the current state of the gamepad.
		/// </summary>
		/// <param name="reading">The current state of the gamepad.</param>
		public InjectedInputGamepadInfo(GamepadReading reading)
		{
			RightTrigger = reading.RightTrigger;
			RightThumbstickX = reading.RightThumbstickX;
			RightThumbstickY = reading.RightThumbstickY;
			LeftTrigger = reading.LeftTrigger;
			LeftThumbstickX = reading.LeftThumbstickX;
			LeftThumbstickY = reading.LeftThumbstickY;
			Buttons = reading.Buttons;
		}

		/// <summary>
		/// Creates a new InjectedInputGamepadInfo object that is used to specify
		/// the gamepad input to inject.
		/// </summary>
		public InjectedInputGamepadInfo()
		{
		}

		/// <summary>
		/// Gets or sets the gamepad buttons used for input injection.
		/// </summary>
		/// <value>One or more gamepad buttons used for input injection.</value>
		public GamepadButtons Buttons { get; set; }

		/// <summary>
		/// Gets or sets the position of the left stick on the X-axis.
		/// </summary>
		/// <value>A value between -1.0 (pressed to the left) and 1.0 (pressed to the right).</value>
		public double LeftThumbstickX { get; set; }

		/// <summary>
		/// Gets or sets the position of the left stick on the Y-axis.
		/// </summary>
		/// <value>A value between -1.0 (pressed towards the user) and 1.0 (pressed away from the user).</value>
		public double LeftThumbstickY { get; set; }

		/// <summary>
		/// Gets or sets the position of the left trigger.
		/// </summary>
		/// <value>A value between 0.0 (not depressed) and 1.0 (fully depressed).</value>
		public double LeftTrigger { get; set; }

		/// <summary>
		/// Gets or sets the position of the right stick on the X-axis.
		/// </summary>
		/// <value>A value between -1.0 (pressed to the left) and 1.0 (pressed to the right).</value>
		public double RightThumbstickX { get; set; }

		/// <summary>
		/// Gets or sets the position of the right stick on the Y-axis.
		/// </summary>
		/// <value>A value between -1.0 (pressed towards the user) and 1.0 (pressed away from the user).</value>
		public double RightThumbstickY { get; set; }

		/// <summary>
		/// Gets or sets the position of the right trigger.
		/// </summary>
		/// <value>A value between 0.0 (not depressed) and 1.0 (fully depressed).</value>
		public double RightTrigger { get; set; }
	}
}
