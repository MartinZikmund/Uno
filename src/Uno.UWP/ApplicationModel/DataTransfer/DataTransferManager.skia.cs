#nullable enable

using System;
using System.Threading.Tasks;
using Uno.ApplicationModel.DataTransfer;
using Uno.Foundation.Extensibility;

namespace Windows.ApplicationModel.DataTransfer
{
	public partial class DataTransferManager
	{
		private static readonly Lazy<IDataTransferManagerExtension?> _dataTransferManagerExtension = new Lazy<IDataTransferManagerExtension?>(() =>
		{
			if (ApiExtensibility.CreateInstance(typeof(PackageId), out IDataTransferManagerExtension value))
			{
				return value;
			}
			return null;
		});

		public static bool IsSupported() => _dataTransferManagerExtension.Value?.IsSupported() ?? false;

		private static async Task<bool> ShowShareUIAsync(ShareUIOptions options, DataPackage dataPackage)
		{
			var extension = _dataTransferManagerExtension.Value;
			if (extension == null)
			{
				throw new NotSupportedException("Share UI is not supported on this platform yet.");
			}

			return await extension.ShowShareUIAsync(options, dataPackage);
		}
	}
}
