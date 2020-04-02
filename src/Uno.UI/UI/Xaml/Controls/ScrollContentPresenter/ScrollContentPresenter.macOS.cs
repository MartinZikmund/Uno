using Uno.Extensions;
using Uno.Logging;
using Uno.UI.DataBinding;
using Windows.UI.Xaml.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using Uno.Disposables;
using System.Runtime.CompilerServices;
using System.Text;
using System.Drawing;
using AppKit;
using Uno.UI;
using Foundation;
using CoreGraphics;
using Windows.Foundation;
using Uno.UI.Extensions;

namespace Windows.UI.Xaml.Controls
{
	public partial class ScrollContentPresenter : NSScrollView, IHasSizeThatFits
	{
		public ScrollContentPresenter()
		{
			DrawsBackground = false;
			InitializeScrollContentPresenter();

			Notifications.ObserveDidLiveScroll(this, OnLiveScroll);
			InitializeScrollContentPresenter();			
			//Scrolled += OnScrolled;
			//ViewForZoomingInScrollView = _ => Content;
			//DidZoom += OnZoom;
			//DraggingStarted += OnDraggingStarted;
			//DraggingEnded += OnDraggingEnded;
			//DecelerationEnded += OnDecelerationEnded;
			//ScrollAnimationEnded += OnScrollAnimationEnded;

			//if (ScrollViewer.UseContentInsetAdjustmentBehavior)
			//{
			//	ContentInsetAdjustmentBehavior = UIScrollViewContentInsetAdjustmentBehavior.Never;
			//}
		}

		public void SetZoomScale(nfloat scale, bool animated)
		{
			Magnification = scale;
		}

		public void SetContentOffset(CGPoint contentOffset, bool animated)
		{
			(DocumentView as NSView)?.ScrollPoint(contentOffset);
		}

		public nfloat ZoomScale {
			get => Magnification;
			set => Magnification = value;
		}
		public ScrollMode HorizontalScrollMode { get; set; }

		public ScrollMode VerticalScrollMode { get; set; }

		public float MinimumZoomScale { get; set; }

		public float MaximumZoomScale { get; set; }

		private bool ShowsVerticalScrollIndicator
		{
			get => HasVerticalScroller;
			set => HasVerticalScroller = value;
		}

		private bool ShowsHorizontalScrollIndicator
		{
			get => HasHorizontalScroller;
			set => HasHorizontalScroller = value;
		}

		public override bool NeedsLayout
		{
			get => base.NeedsLayout; set
			{
				base.NeedsLayout = value;

				if (value)
				{
					_requiresMeasure = true;

					if (Superview != null)
					{
						Superview.NeedsLayout = true;
					}
				}
			}
		}

		internal CGPoint UpperScrollLimit { get { return (CGPoint)(ContentSize - Frame.Size); } }
		CGPoint INSScrollView.UpperScrollLimit { get { return UpperScrollLimit; } }

		public Rect MakeVisible(UIElement visual, Rect rectangle)
		{
			ScrollViewExtensions.BringIntoView(this, visual, BringIntoViewMode.ClosestEdge);
			return rectangle;
		}

		//private void OnScrolled(object sender, EventArgs e)
		//{
		//	InvokeOnScroll();
		//}

		//private void InvokeOnScroll()
		//{
		//	var shouldReportNegativeOffsets = (TemplatedParent as ScrollViewer)?.ShouldReportNegativeOffsets ?? false;
		//	// iOS can return, eg, negative values for offset, whereas Windows never will, even for 'elastic' scrolling
		//	var clampedOffset = shouldReportNegativeOffsets ?
		//		ContentOffset :
		//		ContentOffset.Clamp(CGPoint.Empty, UpperScrollLimit);
		//	(TemplatedParent as ScrollViewer)?.OnScrollInternal(clampedOffset.X, clampedOffset.Y, isIntermediate: _isInAnimatedScroll);
		//}

		//// Called when user starts dragging
		//private void OnDraggingStarted(object sender, EventArgs e)
		//{
		//	_isInAnimatedScroll = true;
		//}

		//// Called when user stops dragging (lifts finger)
		//private void OnDraggingEnded(object sender, DraggingEventArgs e)
		//{
		//	if (!e.Decelerate)
		//	{
		//		//No fling, send final scroll event
		//		OnAnimatedScrollEnded();
		//	}
		//}

		// Called when a user-initiated fling comes to a stop
		private void OnDecelerationEnded(object sender, EventArgs e)
		{
			OnAnimatedScrollEnded();
		}

		// Called at the end of a programmatic animated scroll
		private void OnScrollAnimationEnded(object sender, EventArgs e)
		{
			OnAnimatedScrollEnded();
		}

		private void OnAnimatedScrollEnded()
		{
			//_isInAnimatedScroll = false;
			//InvokeOnScroll();
		}

		private void OnZoom(object sender, EventArgs e)
		{
			//(TemplatedParent as ScrollViewer)?.OnZoomInternal((float)ZoomScale);
		}

		//public override void SetContentOffset(CGPoint contentOffset, bool animated)
		//{
		//	base.SetContentOffset(contentOffset, animated);
		//	if (animated)
		//	{
		//		_isInAnimatedScroll = true;
		//	}
		//}

		partial void OnContentChanged(NSView previousView, NSView newView)
		{
			DocumentView = newView;
		}

		private void OnLiveScroll(object sender, NSNotificationEventArgs e)
		{
			var offset = DocumentVisibleRect.Location;
			(TemplatedParent as ScrollViewer)?.OnScrollInternal(offset.X, offset.Y, isIntermediate: false);
		}
	}
}
