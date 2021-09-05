namespace Windows.Globalization.NumberFormatting
{
	/// <summary>
	/// An interface that parses a string representation of a numeric value.
	/// </summary>
	public partial interface INumberParser
	{
		/// <summary>
		/// Attempts to parse a string representation of an integer numeric value.
		/// </summary>
		/// <param name="text">The text to be parsed.</param>
		/// <returns>If successful, a long that corresponds to the string representation, and otherwise null.</returns>
		long? ParseInt(string text);

		/// <summary>
		/// Attempts to parse a string representation of an integer numeric value.
		/// </summary>
		/// <param name="text">The text to be parsed.</param>
		/// <returns>If successful, a ulong that corresponds to the string representation, and otherwise null.</returns>
		ulong? ParseUInt(string text);

		/// <summary>
		/// Attempts to parse a string representation of a double numeric value.
		/// </summary>
		/// <param name="text">The text to be parsed.</param>
		/// <returns>If successful, a Double that corresponds to the string representation, and otherwise null.</returns>
		double? ParseDouble(string text);
	}
}
