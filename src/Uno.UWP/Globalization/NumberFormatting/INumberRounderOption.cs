namespace Windows.Globalization.NumberFormatting
{
	/// <summary>
	/// An interface that gets and sets the option for rounding numbers.
	/// </summary>
	public partial interface INumberRounderOption
	{
		/// <summary>
		/// Gets or sets the interface used to return rounded numbers.
		/// </summary>
		INumberRounder NumberRounder { get; set; }
	}
}
