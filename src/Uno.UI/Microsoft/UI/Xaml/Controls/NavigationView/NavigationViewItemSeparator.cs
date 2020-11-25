﻿using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Microsoft.UI.Xaml.Controls
{
	public class NavigationViewItemSeparator : NavigationViewItemBase
	{
		private bool m_appliedTemplate = false;
		private bool m_isClosedCompact = false;
		private Grid m_rootGrid = null;
		private long m_splitViewIsPaneOpenChangedRevoker;
		private long m_splitViewDisplayModeChangedRevoker;
		private const string c_rootGrid = "NavigationViewItemSeparatorRootGrid";

		public NavigationViewItemSeparator()
		{
			DefaultStyleKey = typeof(NavigationViewItemSeparator);
		}

		private new void UpdateVisualState(bool useTransitions)
		{
			if (m_appliedTemplate)
			{
				var groupName = "NavigationSeparatorLineStates";
				var stateName = (Position != NavigationViewRepeaterPosition.TopPrimary && Position() != NavigationViewRepeaterPosition.TopFooter)
				   ? m_isClosedCompact
					   ? "HorizontalLineCompact"
					   : "HorizontalLine"
				   : "VerticalLine";

				VisualStateUtil.GoToStateIfGroupExists(this, groupName, stateName, false /*useTransitions*/);
			}
		}

		protected override void OnApplyTemplate()
		{
			// Stop UpdateVisualState before template is applied. Otherwise the visual may not the same as we expect
			m_appliedTemplate = false;
			NavigationViewItemBase.OnApplyTemplate();

			var rootGrid = GetTemplateChild(c_rootGrid) as Grid;
			if (rootGrid != null)
			{
				m_rootGrid = rootGrid;
			}

			var splitView = GetSplitView();
			if (splitView != null)
			{
				m_splitViewIsPaneOpenChangedRevoker = splitView.RegisterPropertyChangedCallback(
					SplitView.IsPaneOpenProperty,
					OnSplitViewPropertyChanged);
				m_splitViewDisplayModeChangedRevoker = splitView.RegisterPropertyChangedCallback(
					SplitView.DisplayModeProperty,
					OnSplitViewPropertyChanged);

				UpdateIsClosedCompact(false);
			}

			m_appliedTemplate = true;
			UpdateVisualState(false /*useTransition*/);
			UpdateItemIndentation();
		}

		protected override void OnNavigationViewItemBaseDepthChanged()
		{
			UpdateItemIndentation();
		}

		protected override void OnNavigationViewItemBasePositionChanged()
		{
			UpdateVisualState(false /*useTransition*/);
		}

		void OnSplitViewPropertyChanged(DependencyObject sender, DependencyProperty args)
		{
			UpdateIsClosedCompact(true);
		}

		void UpdateItemIndentation()
		{
			// Update item indentation based on its depth
			var rootGrid = m_rootGrid;
			if (rootGrid != null)
			{
				var oldMargin = rootGrid.Margin;
				var newLeftMargin = Depth * c_itemIndentation;
				rootGrid.Margin = new Thickness(
					(double)(newLeftMargin),
					oldMargin.Top,
					oldMargin.Right,
					oldMargin.Bottom);
			}
		}

		void UpdateIsClosedCompact(bool updateVisualState)
		{
			var splitView = GetSplitView();
			if (splitView != null)
			{
				// Check if the pane is closed and if the splitview is in either compact mode
				m_isClosedCompact = !splitView.IsPaneOpen
					&& (splitView.DisplayMode == SplitViewDisplayMode.CompactOverlay || splitView.DisplayMode == SplitViewDisplayMode.CompactInline);

				if (updateVisualState)
				{
					UpdateVisualState(false /*useTransition*/);
				}
			}
		}
	}
}
