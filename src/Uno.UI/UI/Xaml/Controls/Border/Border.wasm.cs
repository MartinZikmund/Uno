using System;
using System.Collections.Generic;
using System.Text;
using Uno.Extensions;
using System.Linq;
using System.Drawing;
using Uno.Disposables;
using Windows.UI.Xaml.Media;
using Uno.UI;
using Uno.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;

namespace Windows.UI.Xaml.Controls
{
	public partial class Border : ISupportSvgBorder
	{
		private Shape _svgBorder = null;

		public Border()
		{
		}

		internal Shape SvgBorder
		{
			get => _svgBorder;
			set
			{
				if (_svgBorder != null)
				{
					RemoveChild(_svgBorder);
				}

				_svgBorder = value;

				if (_svgBorder != null)
				{
					AddChild(_svgBorder);
				}
			}
		}

		partial void OnChildChangedPartial(UIElement previousValue, UIElement newValue)
		{
			if (previousValue != null)
			{
				RemoveChild(previousValue);
			}

			AddChild(newValue, 0);
		}

		private void UpdateBorder()
		{
			SetBorder(BorderThickness, BorderBrush);
		}
			
		private protected override void OnLoaded()
		{
			base.OnLoaded();
			UpdateBorder();
		}

		partial void OnBorderBrushChangedPartial()
		{
			UpdateBorder();
		}

		partial void OnBorderThicknessChangedPartial(Thickness oldValue, Thickness newValue)
		{
			UpdateBorder();
		}

		partial void OnPaddingChangedPartial(Thickness oldValue, Thickness newValue)
		{
			UpdateBorder();
		}

		partial void OnCornerRadiusUpdatedPartial(CornerRadius oldValue, CornerRadius newValue)
		{
			SetCornerRadius(newValue);
		}

		protected override void OnBackgroundChanged(DependencyPropertyChangedEventArgs e)
		{
			base.OnBackgroundChanged(e);
			UpdateHitTest();
		}

		internal override bool IsViewHit()
			=> Background != null || base.IsViewHit();

		bool ICustomClippingElement.AllowClippingToLayoutSlot => !(Child is UIElement ue) || ue.RenderTransform == null;
		bool ICustomClippingElement.ForceClippingToLayoutSlot => CornerRadius != CornerRadius.None;
	}
}
