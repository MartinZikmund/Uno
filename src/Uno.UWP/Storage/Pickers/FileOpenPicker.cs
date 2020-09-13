#if __MACOS__
using System;
using System.Collections.Generic;
using Windows.Foundation.Collections;
using Windows.Foundation;

namespace Windows.Storage.Pickers
{
	public partial class FileOpenPicker
	{
		public PickerLocationId SuggestedStartLocation { get; set; }

		public string CommitButtonText { get; set; }

		public IList<string> FileTypeFilter { get; } = new NonNullList<string>();

		public FileOpenPicker() => Init();

		partial void Init();

		/// <summary>
		/// Shows the file picker so that the user can pick one file.
		/// </summary>
		/// <param name="pickerOperationId">This argument is ignored and has no effect.</param>
		/// <returns>When the call to this method completes successfully,
		/// it returns a StorageFile object that represents the file that the user picked.</returns>
		public IAsyncOperation<StorageFile> PickSingleFileAsync(string pickerOperationId) =>
			PickSingleFileTaskAsync().AsAsyncOperation();

		/// <summary>
		/// Shows the file picker so that the user can pick one file.
		/// </summary>
		/// <returns>When the call to this method completes successfully,
		/// it returns a StorageFile object that represents the file that the user picked.</returns>
		public IAsyncOperation<StorageFile> PickSingleFileAsync() =>
			PickSingleFileTaskAsync().AsAsyncOperation();

		/// <summary>
		/// Shows the file picker so that the user can pick multiple files.
		/// </summary>
		/// <returns>When the call to this method completes successfully,
		/// it returns a FilePickerSelectedFilesArray object that contains all the files
		/// that were picked by the user. Picked files in this array are represented
		/// by StorageFile objects.</returns>
		public IAsyncOperation<IReadOnlyList<StorageFile>> PickMultipleFilesAsync() =>
			PickMultipleFilesTaskAsync().AsAsyncOperation();
	}
}
#endif
