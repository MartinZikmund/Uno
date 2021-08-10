namespace Windows.UI.Input.Preview.Injection
{
	public partial class InjectedInputTouchInfo
	{
		public InjectedInputTouchInfo()
		{
		}

		public InjectedInputTouchParameters TouchParameters { get; set; }

		public double Pressure { get; set; }

		public InjectedInputPointerInfo PointerInfo { get; set; }

		public int Orientation { get; set; }

		public InjectedInputRectangle Contact { get; set; }
	}
}
