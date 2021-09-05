namespace Windows.Globalization.NumberFormatting
{
	/// <summary>
	/// An interface that returns a string representation of a provided value,
	/// using distinct format methods to format several data types.
	/// </summary>
	public partial interface INumberFormatter2
	{
		/// <summary>
		/// Returns a string representation of an long value.
		/// </summary>
		/// <param name="value">The long value to be formatted.</param>
		/// <returns>A string that represents the value.</returns>
		string FormatInt(long value);

		/// <summary>
		/// Returns a string representation of an ulong value.
		/// </summary>
		/// <param name="value">The ulong value to be formatted.</param>
		/// <returns>A string that represents the value.</returns>
		string FormatUInt(ulong value);

		/// <summary>
		/// Returns a string representation of a double value.
		/// </summary>
		/// <param name="value">The double value to be formatted.</param>
		/// <returns>A string that represents the value.</returns>
		string FormatDouble(double value);
	}
}
