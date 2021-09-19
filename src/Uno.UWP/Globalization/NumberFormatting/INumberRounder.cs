namespace Windows.Globalization.NumberFormatting
{
	/// <summary>
	/// An interface that returns rounded results for provided numbers of several data types.
	/// </summary>
	public partial interface INumberRounder 
	{
		/// <summary>
		/// Rounds an Int32 number.
		/// </summary>
		/// <param name="value">The Int32 value to be rounded.</param>
		/// <returns>The rounded unsigned 64 bit integer.</returns>
		int RoundInt32(int value);

		/// <summary>
		/// Rounds a UInt32 number.
		/// </summary>
		/// <param name="value">The UInt32 value to be rounded.</param>
		/// <returns>The rounded unsigned 32 bit integer.</returns>
		uint RoundUInt32(uint value);

		/// <summary>
		/// Rounds an Int64 number.
		/// </summary>
		/// <param name="value">The Int64 value to be rounded.</param>
		/// <returns>The rounded signed 64 bit integer.</returns>
		long RoundInt64(long value);

		/// <summary>
		/// Rounds a UInt64 number.
		/// </summary>
		/// <param name="value">The UInt64 value to be rounded.</param>
		/// <returns>The rounded unsigned 64 bit integer.</returns>
		ulong RoundUInt64(ulong value);

		/// <summary>
		/// Rounds a Single number.
		/// </summary>
		/// <param name="value">The Single value to be rounded.</param>
		/// <returns>The rounded Single.</returns>
		float RoundSingle(float value);

		/// <summary>
		/// Rounds a Double number.
		/// </summary>
		/// <param name="value">The Double value to be rounded.</param>
		/// <returns>The rounded Double.</returns>
		double RoundDouble(double value);
	}
}
