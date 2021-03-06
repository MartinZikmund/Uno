﻿using Windows.Storage;

namespace Uno.Storage.Internal
{
	internal static class StorageProviders
	{
		public static StorageProvider Local { get; } = new StorageProvider("computer", "StorageProviderLocalDisplayName");

#if __WASM__
		public static StorageProvider NativeWasm { get; } = new StorageProvider("jsfileaccessapi", "StorageProviderNativeWasmDisplayName");
#endif
	}
}
