﻿using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.UI.Xaml.Controls
{
	internal struct SelectedItemInfo
	{
		internal WeakReference<SelectionNode> Node;
		internal IndexPath Path;
	}

	public partial class SelectionModel
    {
		private SelectionNode m_rootNode = null;
		private bool m_singleSelect = false;

		IReadOnlyList<IndexPath> m_selectedIndicesCached = null;
		IReadOnlyList<object> m_selectedItemsCached = null;

		// Cached Event args to avoid creation cost every time
		private SelectionModelChildrenRequestedEventArgs m_childrenRequestedEventArgs;
		private SelectionModelSelectionChangedEventArgs m_selectionChangedEventArgs;

		// use just one instance of a leaf node to avoid creating a bunch of these.
		private SelectionNode m_leafNode;
	}
}
