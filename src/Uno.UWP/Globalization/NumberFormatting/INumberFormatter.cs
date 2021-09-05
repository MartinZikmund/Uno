namespace Windows.Globalization.NumberFormatting
{
	/// <summary>
	/// An interface that returns a string representation of a provided value,
	/// using an overloaded Format method to format several data types.
	/// </summary>
	public partial interface INumberFormatter
	{
		/// <summary>
		/// Returns a string representation of an long value.
		/// </summary>
		/// <param name="value">The long value to be formatted.</param>
		/// <returns>A string that represents the value.</returns>
		string Format(long value);

		/// <summary>
		/// Returns a string representation of a ulong value.
		/// </summary>
		/// <param name="value">The ulong value to be formatted.</param>
		/// <returns>A string that represents the value.</returns>
		string Format(ulong value);

		/// <summary>
		/// Returns a string representation of a double value.
		/// </summary>
		/// <param name="value">The double value to be formatted.</param>
		/// <returns>A string that represents the value.</returns>
		string Format(double value);
	}
}
