﻿#nullable enable
using Windows.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.Foundation;
using Windows.UI;

namespace Windows.UI.Xaml.Controls
{
	public partial class ScrollViewer 
	{
		internal Size ScrollBarSize => (_presenter as ScrollContentPresenter)?.ScrollBarSize ?? default;

		public Color BackgroundColor
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		private void UpdateZoomedContentAlignment() { }


		private bool ChangeViewScrollNative(double? horizontalOffset, double? verticalOffset, float? zoomFactor, bool disableAnimation)
		{
			if (_presenter is ScrollContentPresenter presenter)
			{
				bool hSuccess, vSuccess;

				if (horizontalOffset is {} hOffset)
				{
					hSuccess = presenter.SetHorizontalOffsetInternal(hOffset);
				}
				else
				{
					hSuccess = true;
				}

				if (verticalOffset is {} vOffset)
				{
					vSuccess = presenter.SetVerticalOffsetInternal(vOffset);
				}
				else
				{
					vSuccess = true;
				}

				// TODO: zoom factor

				return hSuccess && vSuccess;
			}

			return false;
		}
	}
}
