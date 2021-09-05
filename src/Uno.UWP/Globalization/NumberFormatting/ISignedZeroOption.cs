namespace Windows.Globalization.NumberFormatting
{
	/// <summary>
	/// An interface that gets and sets the option for specifying whether -0 is formatted as "-0" or "0".
	/// </summary>
	public partial interface ISignedZeroOption
	{
		/// <summary>
		/// Gets or sets whether -0 is formatted as "-0" or "0".
		/// </summary>
		bool IsZeroSigned { get; set; }
	}
}
