using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppKit;
using Foundation;

namespace Windows.Storage.Pickers
{
	public partial class FileOpenPicker
	{
		private const int OpenPanelSuccess = 1;

		private async Task<StorageFile> PickSingleFileTaskAsync()
		{
			var pickedFiles = PickFiles(false);
			if (pickedFiles.Length == 1)
			{
				return await StorageFile.GetFileFromPathAsync(pickedFiles[0].Path);
			}
			return null;
		}

		private async Task<IReadOnlyList<StorageFile>> PickMultipleFilesTaskAsync()
		{
			var pickedFiles = PickFiles(true);
			return pickedFiles.Select(url => new StorageFile(url.Path)).ToArray();
		}

		private NSUrl[] PickFiles(bool pickMultiple)
		{
			var openPanel = new NSOpenPanel
			{
				CanChooseFiles = true,
				CanChooseDirectories = false,
				AllowsMultipleSelection = pickMultiple,
				AllowedFileTypes = FileTypeFilter.ToArray()
			};

			var result = openPanel.RunModal();

			if (result == OpenPanelSuccess)
			{
				return openPanel.Urls;
			}
			return Array.Empty<NSUrl>();
		}
	}
}
