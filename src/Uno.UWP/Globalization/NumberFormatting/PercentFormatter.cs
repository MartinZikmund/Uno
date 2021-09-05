using System.Collections.Generic;

namespace Windows.Globalization.NumberFormatting
{
	public partial class PercentFormatter :
		INumberFormatterOptions,
		INumberFormatter,
		INumberFormatter2,
		INumberParser,
		ISignificantDigitsOption,
		INumberRounderOption,
		ISignedZeroOption
	{
		public PercentFormatter(IEnumerable<string> languages, string geographicRegion)
		{
		}

		public PercentFormatter()
		{
			global::Windows.Foundation.Metadata.ApiInformation.TryRaiseNotImplemented("Windows.Globalization.NumberFormatting.PercentFormatter", "PercentFormatter.PercentFormatter()");
		}

		public bool IsDecimalPointAlwaysDisplayed { get; set; }

		public int IntegerDigits { get; set; }

		public bool IsGrouped { get; set; }

		public string NumeralSystem { get; set; }

		public int FractionDigits { get; set; }

		public string GeographicRegion { get; }

		public IReadOnlyList<string> Languages { get; }

		public string ResolvedGeographicRegion { get; }

		public string ResolvedLanguage { get; }

		public INumberRounder NumberRounder { get; set; }

		public bool IsZeroSigned { get; set; }

		public int SignificantDigits { get; set; }

		public string Format(long value)
		{
		}

		public string Format(ulong value)
		{
			throw new global::System.NotImplementedException("The member string PercentFormatter.Format(ulong value) is not implemented in Uno.");
		}

		public string Format(double value)
		{
			throw new global::System.NotImplementedException("The member string PercentFormatter.Format(double value) is not implemented in Uno.");
		}

		public string FormatInt(long value)
		{
			throw new global::System.NotImplementedException("The member string PercentFormatter.FormatInt(long value) is not implemented in Uno.");
		}

		public string FormatUInt(ulong value)
		{
			throw new global::System.NotImplementedException("The member string PercentFormatter.FormatUInt(ulong value) is not implemented in Uno.");
		}

		public string FormatDouble(double value)
		{
			throw new global::System.NotImplementedException("The member string PercentFormatter.FormatDouble(double value) is not implemented in Uno.");
		}

		public long? ParseInt(string text)
		{
			throw new global::System.NotImplementedException("The member long? PercentFormatter.ParseInt(string text) is not implemented in Uno.");
		}

		public ulong? ParseUInt(string text)
		{
			throw new global::System.NotImplementedException("The member ulong? PercentFormatter.ParseUInt(string text) is not implemented in Uno.");
		}
		
		public double? ParseDouble(string text)
		{
			throw new global::System.NotImplementedException("The member double? PercentFormatter.ParseDouble(string text) is not implemented in Uno.");
		}
	}
}
