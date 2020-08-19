#if __IOS__
using System.Collections.Generic;
using Windows.Storage;

namespace Windows.ApplicationModel.Activation
{
	public partial interface IFileActivatedEventArgs : IActivatedEventArgs
	{
		IReadOnlyList<IStorageItem> Files { get; }
	}
}
#endif
