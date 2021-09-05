namespace Windows.Globalization.NumberFormatting
{
	/// <summary>
	/// An interface that gets and sets the option for specifying significant digits.
	/// </summary>
	public partial interface ISignificantDigitsOption
	{
		/// <summary>
		/// Gets or sets the number of significant digits used in formatting or rounding numbers.
		/// </summary>
		int SignificantDigits { get; set; }
	}
}
