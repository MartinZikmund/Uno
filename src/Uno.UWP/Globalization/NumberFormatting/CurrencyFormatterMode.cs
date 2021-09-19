namespace Windows.Globalization.NumberFormatting
{
	/// <summary>
	/// Specifies the use of symbols or codes when currency is formatted.
	/// </summary>
	public enum CurrencyFormatterMode
	{
		/// <summary>
		/// Default behavior. Currencies are formatted with the appropriate currency symbol (for example, $15).
		/// </summary>
		UseSymbol,

		/// <summary>
		/// Currencies are formatted with the Currency code provided to the CurrencyFormatter object (for example, 15 USD).
		/// </summary>
		UseCurrencyCode,
	}
}
