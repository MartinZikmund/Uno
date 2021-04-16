namespace Windows.UI.Xaml.Input
{
	public partial class FocusMovementResult
	{
		internal FocusMovementResult(bool succeeded)
		{
			Succeeded = succeeded;
		}

		public bool Succeeded { get; }
	}
}
