﻿#nullable enable

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Windows.Storage;

namespace Uno.UI.RuntimeTests.Tests.Windows_Storage
{
	[TestClass]
	public class Given_StorageFolder_Native : Given_StorageFolder_Native_Base
	{
		protected override async Task<StorageFolder> GetRootFolderAsync()
		{
			return await StorageFolder.GetPrivateRootAsync()!;
		}

		protected override async Task CleanupRootFolderAsync()
		{
		}
	}
}
