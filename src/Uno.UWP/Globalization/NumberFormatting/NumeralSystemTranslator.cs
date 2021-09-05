using System.Collections.Generic;

namespace Windows.Globalization.NumberFormatting
{
	/// <summary>
	/// Translates digits of the Latin numerical system into digits of another numerical system.
	/// </summary>
	public partial class NumeralSystemTranslator
	{
		/// <summary>
		/// Creates a NumeralSystemTranslator object initialized by the list
		/// of current runtime language values preferred by the user.
		/// </summary>
		public NumeralSystemTranslator()
		{
		}

		/// <summary>
		/// Creates a NumeralSystemTranslator object initialized by a language list.
		/// </summary>
		/// <param name="languages">
		/// A list of BCP-47 language tags, in priority order, representing the choice of languages.
		/// They must all be well-formed according to Windows.Globalization.Language.IsWellFormed.
		/// </param>
		public NumeralSystemTranslator(IEnumerable<string> languages)
		{
		}

		/// <summary>
		/// Gets or sets the numeral system that Latin digits will be converted to on calls to TranslateNumerals.
		/// </summary>
		public string NumeralSystem { get; set; }

		/// <summary>
		/// Gets the BCP-47 language tag(s) used to initialize this NumeralSystemTranslator object.
		/// </summary>
		public IReadOnlyList<string> Languages { get; }

		/// <summary>
		/// Gets the language used to determine the numeral system when this object was initialized.
		/// </summary>
		public string ResolvedLanguage { get; }

		/// <summary>
		/// Converts a string of characters containing Latin digits to a string containing
		/// the corresponding digits of NumeralSystem.
		/// </summary>
		/// <param name="value">A string of characters containing Latin digits to be converted.</param>
		/// <returns>A string containing the converted digits. This string may be a different length than value.</returns>
		public string TranslateNumerals(string value)
		{

		}
	}
}
