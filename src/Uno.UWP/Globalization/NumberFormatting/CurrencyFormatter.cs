using System.Collections.Generic;
using Windows.Foundation.Metadata;

namespace Windows.Globalization.NumberFormatting
{
	public partial class CurrencyFormatter :
		INumberParser,
		INumberFormatter2,
		INumberFormatter,
		INumberFormatterOptions,
		ISignificantDigitsOption,
		INumberRounderOption,
		ISignedZeroOption
	{
		/// <summary>
		/// Creates a CurrencyFormatter object that is initialized with a currency identifier.
		/// </summary>
		/// <param name="currencyCode">The currency identifier to use when formatting and parsing currency values.</param>
		public CurrencyFormatter(string currencyCode)
		{
		}

		/// <summary>
		/// Creates a CurrencyFormatter object initialized with a currency identifier, language list, and geographic region.
		/// </summary>
		/// <param name="currencyCode">The currency identifier to use when formatting and parsing currency values.</param>
		/// <param name="languages">The list of language identifiers, in priority order, representing the choice of languages. See Remarks.</param>
		/// <param name="geographicRegion">The identifier for the geographic region.</param>
		public CurrencyFormatter(string currencyCode, IEnumerable<string> languages, string geographicRegion)
		{
		}

		/// <summary>
		/// Gets the identifier for the currency to be used for formatting and parsing currency values.
		/// </summary>
		public string Currency
		{
			get;
			[Deprecated("Should not be set, create a new CurrencyFormatter instance instead.", DeprecationType.Deprecate, 65536, "Windows.Foundation.UniversalApiContract")]
			set;
		}

		/// <summary>
		/// Gets or sets whether the currency is formatted with the currency symbol or currency code.
		/// </summary>
		public CurrencyFormatterMode Mode { get; set; }

		/// <summary>
		/// Gets or sets the numbering system that is used to format and parse currency values.
		/// </summary>
		public string NumeralSystem { get; set; }

		/// <summary>
		/// Gets or sets whether the integer part of the currency value should be grouped. The default value is false.
		/// </summary>
		public bool IsGrouped { get; set; }

		/// <summary>
		/// Gets or sets whether the decimal point of the currency value should always be displayed.
		/// </summary>
		public bool IsDecimalPointAlwaysDisplayed { get; set; }

		/// <summary>
		/// Gets or sets the minimum number of digits to display for the integer part of the currency value.
		/// </summary>
		public int IntegerDigits { get; set; }

		/// <summary>
		/// Gets or sets the minimum number of digits to display for the fraction part of the currency value.
		/// </summary>
		public int FractionDigits { get; set; }

		/// <summary>
		/// Gets the region that is used when formatting and parsing currency values.
		/// </summary>
		public string GeographicRegion { get; }

		/// <summary>
		/// Gets the geographic region that was most recently used to format or parse currency values.
		/// </summary>
		public string ResolvedGeographicRegion { get; }

		/// <summary>
		/// Gets the language that was most recently used to format or parse currency values.
		/// </summary>
		public string ResolvedLanguage { get; }

		/// <summary>
		/// Gets the priority list of language identifiers that is used when formatting and parsing currency values.
		/// </summary>
		public IReadOnlyList<string> Languages { get; }

		/// <summary>
		/// Gets or sets the current rounding strategy to be used when formatting currency amounts.
		/// </summary>
		public INumberRounder NumberRounder { get; set; }

		/// <summary>
		/// Gets or sets whether -0 is formatted using the conventions for negative numbers or for positive numbers. (In the Latin numeral system, the choice is "-0" or "0".)
		/// </summary>
		public bool IsZeroSigned { get; set; }

		/// <summary>
		/// Gets or sets the current padding to significant digits when a currency amount is formatted.
		/// </summary>
		public int SignificantDigits { get; set; }

		public string Format(long value)
		{
		}

		public string Format(ulong value)
		{
		}

		public string Format(double value)
		{
		}

		public string FormatInt(long value)
		{
		}

		public string FormatUInt(ulong value)
		{
		}

		public string FormatDouble(double value)
		{
		}

		/// <summary>
		/// Attempts to parse a string representation of an integer currency value.
		/// </summary>
		/// <param name="text">The text to be parsed.</param>
		/// <returns>If successful, an Int64 that corresponds to the string representation, and otherwise null.</returns>
		public long? ParseInt(string text)
		{
		}

		/// <summary>
		/// Attempts to parse a string representation of an unsigned integer currency value.
		/// </summary>
		/// <param name="text">The text to be parsed.</param>
		/// <returns>If successful, a UInt64 that corresponds to the string representation, and otherwise null.</returns>
		public ulong? ParseUInt(string text)
		{
		}

		/// <summary>
		/// Attempts to parse a string representation of a Double currency value.
		/// </summary>
		/// <param name="text">The text to be parsed.</param>
		/// <returns>If successful, a Double that corresponds to the string representation, and otherwise null.</returns>
		public double? ParseDouble(string text)
		{
		}

		/// <summary>
		/// Applies the specified rounding algorithm to the CurrencyFormatter.
		/// </summary>
		/// <param name="roundingAlgorithm">A value of the RoundingAlgorithm enumeration.</param>
		public void ApplyRoundingForCurrency(RoundingAlgorithm roundingAlgorithm)
		{
		}
	}
}
