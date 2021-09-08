#nullable enable

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Shapes;

namespace Uno.UI.Xaml.Controls.Border
{
	internal class SvgBorderManager
	{
		private UIElement _owner;
		private Shape? _currentBorder;

		public SvgBorderManager(UIElement owner)
		{
			_owner = owner ?? throw new ArgumentNullException(nameof(owner));
		}

		public void SetBorder(Shape shape)
		{
			if (_currentBorder != null)
			{
				_owner.RemoveChild(_currentBorder);
			}

			_currentBorder = shape;

			if (_currentBorder != null)
			{
				_owner.AddChild(_currentBorder);
			}
		}
	}
}
