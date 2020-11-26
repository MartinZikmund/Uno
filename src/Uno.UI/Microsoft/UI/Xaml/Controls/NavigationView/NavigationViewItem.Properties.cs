﻿using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Microsoft.UI.Xaml.Controls
{
	public partial class NavigationViewItem
    {
		public IconElement Icon
		{
			get => (IconElement)GetValue(IconProperty);
			set => SetValue(IconProperty, value);
		}

		public static DependencyProperty IconProperty { get; } =
			DependencyProperty.Register(nameof(Icon), typeof(IconElement), typeof(NavigationViewItem), new PropertyMetadata(null));

		public double CompactPaneLength
		{
			get => (double)GetValue(CompactPaneLengthProperty);
			set => SetValue(CompactPaneLengthProperty, value);
		}

		public static DependencyProperty CompactPaneLengthProperty { get; } =
			DependencyProperty.Register(nameof(CompactPaneLength), typeof(double), typeof(NavigationViewItem), new PropertyMetadata(48.0));

		public bool SelectsOnInvoked
		{
			get => (bool)GetValue(SelectsOnInvokedProperty);
			set => SetValue(SelectsOnInvokedProperty, value);
		}

		public static DependencyProperty SelectsOnInvokedProperty { get; } =
			DependencyProperty.Register(nameof(SelectsOnInvoked), typeof(bool), typeof(NavigationViewItem), new PropertyMetadata(true));

		public bool IsExpanded
		{
			get => (bool)GetValue(IsExpandedProperty);
			set => SetValue(IsExpandedProperty, value);
		}

		public static DependencyProperty IsExpandedProperty { get; } =
			DependencyProperty.Register(nameof(IsExpanded), typeof(bool), typeof(NavigationViewItem), new PropertyMetadata(false));

		public bool HasUnrealizedChildren
		{
			get => (bool)GetValue(HasUnrealizedChildrenProperty);
			set => SetValue(HasUnrealizedChildrenProperty, value);
		}

		public static DependencyProperty HasUnrealizedChildrenProperty { get; } =
			DependencyProperty.Register(nameof(HasUnrealizedChildren), typeof(bool), typeof(NavigationViewItem), new PropertyMetadata(false));

		public bool IsChildSelected
		{
			get => (bool)GetValue(IsChildSelectedProperty);
			set => SetValue(IsChildSelectedProperty, value);
		}

		public static DependencyProperty IsChildSelectedProperty { get; } =
			DependencyProperty.Register(nameof(IsChildSelected), typeof(bool), typeof(NavigationViewItem), new PropertyMetadata(false));

		public IList<object> MenuItems
		{
			get => (IList<object>)GetValue(MenuItemsProperty);
			set => SetValue(MenuItemsProperty, value);
		}

		public static DependencyProperty MenuItemsProperty { get; } =
			DependencyProperty.Register(nameof(MenuItems), typeof(IList<object>), typeof(NavigationViewItem), new PropertyMetadata(null));

		public object MenuItemsSource
		{
			get => (object)GetValue(MenuItemsSourceProperty);
			set => SetValue(MenuItemsSourceProperty, value);
		}

		public static DependencyProperty MenuItemsSourceProperty { get; } =
			DependencyProperty.Register(nameof(MenuItemsSource), typeof(object), typeof(NavigationViewItem), new PropertyMetadata(null));
	}
}
