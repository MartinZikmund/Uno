using System.Collections.Generic;

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
		public CurrencyFormatter(string currencyCode)
		{
		}

		public CurrencyFormatter(string currencyCode, global::System.Collections.Generic.IEnumerable<string> languages, string geographicRegion)
		{
		}

		public string Currency { get; set; }

		public CurrencyFormatterMode Mode { get; set; }

		public string NumeralSystem { get; set; }

		public bool IsGrouped { get; set; }

		public bool IsDecimalPointAlwaysDisplayed { get; set; }

		public int IntegerDigits { get; set; }

		public int FractionDigits { get; set; }

		public string GeographicRegion { get; }

		public string ResolvedGeographicRegion { get; }

		public string ResolvedLanguage { get; }

		public IReadOnlyList<string> Languages { get; }

		public INumberRounder NumberRounder { get; set; }

		public bool IsZeroSigned { get; set; }

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

		public long? ParseInt(string text)
		{
		}

		public ulong? ParseUInt(string text)
		{
		}

		public double? ParseDouble(string text)
		{
		}

		public void ApplyRoundingForCurrency(RoundingAlgorithm roundingAlgorithm)
		{
		}
	}
}
