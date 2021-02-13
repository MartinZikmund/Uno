#nullable enable

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace Uno.Extensions.Storage.Pickers
{
	internal class FolderPickerExtension : IFolderPickerExtension
	{
		private readonly FolderPicker _picker;

		public FolderPickerExtension(object owner)
		{
			if (!(owner is FolderPicker picker))
			{
				throw new InvalidOperationException("Owner of FolderPickerExtension must be a FolderPicker.");
			}
			_picker = picker;
		}

		public async Task<StorageFolder?> PickSingleFolderAsync(CancellationToken token)
		{
			using var dialog = new FolderBrowserDialog();
			dialog.ShowNewFolderButton = true;
			dialog.RootFolder = PickerHelpers.GetInitialSpecialFolder(_picker.SuggestedStartLocation);

			var result = dialog.ShowDialog();
			if (result == DialogResult.OK)
			{
				return await StorageFolder.GetFolderFromPathAsync(dialog.SelectedPath);
			}
			return null;
		}
	}
}
