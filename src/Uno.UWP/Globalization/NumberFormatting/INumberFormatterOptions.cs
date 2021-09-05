using System.Collections.Generic;

namespace Windows.Globalization.NumberFormatting
{
	/// <summary>
	/// An interface that gets and sets options for formatting numbers.
	/// </summary>
	public partial interface INumberFormatterOptions
	{
		/// <summary>
		/// Gets or sets the minimum number of digits to display for the fraction part of the number.
		/// </summary>
		int FractionDigits { get; set; }

		/// <summary>
		/// Gets the region that is used when formatting and parsing numbers.
		/// </summary>
		string GeographicRegion { get; }

		/// <summary>
		/// Gets or sets the minimum number of digits to display for the integer part of the number.
		/// </summary>
		int IntegerDigits { get; set; }

		/// <summary>
		/// Gets or sets whether the decimal point of the number should always be displayed.
		/// </summary>
		bool IsDecimalPointAlwaysDisplayed { get; set; }

		/// <summary>
		/// Gets or sets whether the integer part of the number should be grouped.
		/// </summary>
		bool IsGrouped { get; set; }

		/// <summary>
		/// Gets the priority list of language identifiers that is used when formatting and parsing numbers.
		/// </summary>
		IReadOnlyList<string> Languages { get; }

		/// <summary>
		/// Gets or sets the numbering system that is used to format and parse numbers.
		/// </summary>
		string NumeralSystem { get; set; }

		/// <summary>
		/// Gets the geographic region that was most recently used to format or parse numbers.
		/// </summary>
		string ResolvedGeographicRegion { get; }

		/// <summary>
		/// Gets the geographic region that was most recently used to format or parse numbers.
		/// </summary>
		string ResolvedLanguage { get; }
	}
}
