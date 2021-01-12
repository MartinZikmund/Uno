using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;

namespace UITests.Resources.ResourcesInTemplate
{
	public sealed partial class SimpleControl : Control
	{
		public SimpleControl()
		{
			this.DefaultStyleKey = typeof(SimpleControl);
		}
	}
}
