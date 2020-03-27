
#if __ANDROID__

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windows.Storage.Pickers
{
	public partial class FileOpenPicker
	{
		private List<string> _fileTypeFilter = new List<string>();
		private string _docUri = "";

		public PickerLocationId SuggestedStartLocation { get; set; } = PickerLocationId.DocumentsLibrary;
		public string SettingsIdentifier { get; set; } = "";
		public IList<string> FileTypeFilter => _fileTypeFilter;

		private List<string> UWPextension2Mime()
		{
			var mimeTypes = new List<string>();
			if (_fileTypeFilter.Count < 1)
			{
				mimeTypes.Add("*/*");
				return mimeTypes;
			}

			foreach (var extension in _fileTypeFilter)
			{
				// extension is in form of ".png", but GetMimeTypeFromExtension requires "png"
				string mimetype = Android.Webkit.MimeTypeMap.Singleton.GetMimeTypeFromExtension(extension.Substring(1));
				bool alreadyExist = false;
				foreach (var mime in mimeTypes)
				{
					if (mime == mimetype)
					{
						alreadyExist = true;
					}
				}
				if (!alreadyExist)
				{
					mimeTypes.Add(mimetype);
				}
			}
			return mimeTypes;
		}

		private void SetDocUri()
		{
			string settName = "_UnoFilePickerSettings" + SettingsIdentifier;

			if (Windows.Storage.ApplicationData.Current.RoamingSettings.Values.ContainsKey(settName))
			{
				// string lastFolder = Windows.Storage.ApplicationData.Current.RoamingSettings.Values[settName].ToString();
				_docUri = Windows.Storage.ApplicationData.Current.RoamingSettings.Values[settName].ToString();
				//Uri.UnescapeDataString(lastFolder);
				return;
			}

			switch (SuggestedStartLocation)
			{
				// see also Windows.Storage.KnownFolders
				case PickerLocationId.DocumentsLibrary:
					_docUri = "file://" + Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments).CanonicalPath;
					break;
				case PickerLocationId.MusicLibrary:
					_docUri = "file://" + Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryMusic).CanonicalPath;
					break;
				case PickerLocationId.PicturesLibrary:
					// Warning: Camera probably is outside this folder!
					_docUri = "file://" + Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures).CanonicalPath;
					break;
				case PickerLocationId.Downloads:
					_docUri = "file://" + Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).CanonicalPath;
					break;
				case PickerLocationId.VideosLibrary:
					_docUri = "file://" + Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryMovies).CanonicalPath;
					break;
				case PickerLocationId.Unspecified:
					_docUri = "";
					break;
				default:
					throw new NotImplementedException("FileOpenPicker unimplemented type of initial dir");

			}

		}

		public FileOpenPicker()
		{
			if (Android.OS.Build.VERSION.SdkInt < Android.OS.BuildVersionCodes.Kitkat)
			{
				throw new NotImplementedException("FileOpenPicker requires Android KitKat (API level 19) or newer");
			}
			SetDocUri();
			// check permission: READ_EXTERNAL_STORAGE
		}

		public Foundation.IAsyncOperation<Windows.Storage.StorageFile> PickSingleFileAsync()
			=> PickSingleFileAsyncTask().AsAsyncOperation<Windows.Storage.StorageFile>();

		public Foundation.IAsyncOperation<IReadOnlyList<Windows.Storage.StorageFile>> PickMultipleFilesAsync()
			=> PickMultipleFilesAsyncTask().AsAsyncOperation();

		public async Task<Windows.Storage.StorageFile> PickSingleFileAsyncTask()
		{
			var pickedFiles = await PickFilesAsync(false);
			if (pickedFiles is null)
			{
				return null;
			}

			return pickedFiles.ElementAt(0);
		}
		public async Task<IReadOnlyList<Windows.Storage.StorageFile>> PickMultipleFilesAsyncTask()
		{
			var pickedFiles = await PickFilesAsync(true);
			if (pickedFiles is null)
			{
				return null;
			}

			return pickedFiles.AsReadOnly();
		}

		private TaskCompletionSource<List<Windows.Storage.StorageFile>> completionSource;

		private string PathFromUri(Android.Net.Uri fileUri)
		{
			// basic file
			if (fileUri.Scheme.Equals("file", StringComparison.OrdinalIgnoreCase))
			{
				return fileUri.Path;
			}

			// more complicated Uris ("content://")
			if (Android.Provider.DocumentsContract.IsDocumentUri(Android.App.Application.Context, fileUri))
			{
				if (fileUri.Authority.Equals("com.microsoft.skydrive.content.StorageAccessProvider"))
				{
					throw new NotImplementedException("FileOpenPicker - Onedrive documents are not implemented yet");
				}

				string columnName = Android.Provider.MediaStore.Files.FileColumns.Data;
				string[] projection = { columnName };
				var cursor = Android.App.Application.Context.ContentResolver.Query(fileUri, projection, null, null, null);
				if (cursor is null)
					throw new Exception("FileOpenPicker - cannot get cursor");

				if (!cursor.MoveToFirst())
					throw new Exception("FileOpenPicker - cannot MoveToFirst");

				int columnNo = cursor.GetColumnIndex(columnName);
				if (columnNo < 0)
					throw new Exception("FileOpenPicker - column with data doesn't exist?");

				string filePath = cursor.GetString(columnNo);
				cursor.Close();
				cursor.Dispose();

				if (!string.IsNullOrEmpty(filePath))
				{
					return filePath;
				}

			}
			throw new NotImplementedException("FileOpenPicker - not implemented document type");

		}

		private Task<List<Windows.Storage.StorageFile>> PickFilesAsync(bool multiple)
		{
			var intent = new Android.Content.Intent(Android.App.Application.Context, typeof(FileOpenPickerActivity));
			// put parameters into Intent
			intent.PutExtra("multiple", multiple);
			intent.PutExtra("initialdir", _docUri);
			intent.PutExtra("mimetypes", UWPextension2Mime().ToArray());

			// wrap it in Task
			completionSource = new TaskCompletionSource<List<Windows.Storage.StorageFile>>();
			FileOpenPickerActivity.FilePicked += FilePickerHandler;

			Android.App.Application.Context.StartActivity(intent);

			async void FilePickerHandler(object sender, List<Android.Net.Uri> list)
			{
				FileOpenPickerActivity.FilePicked -= FilePickerHandler;

				// convert list of Uris tolist of StorageFiles
				var storageFiles = new List<Windows.Storage.StorageFile>();

				if (list.Count > 0)
				{
					string settName = "_UnoFilePickerSettings" + SettingsIdentifier;
					Windows.Storage.ApplicationData.Current.RoamingSettings.Values[settName] = list.ElementAt(0).ToString();
				}

				foreach (var fileUri in list)
				{
					Android.App.Application.Context.ContentResolver.TakePersistableUriPermission(
						fileUri, Android.Content.ActivityFlags.GrantReadUriPermission);

					string filePath = PathFromUri(fileUri);
					if (!string.IsNullOrEmpty(filePath))
					{
						var storageFile = await Windows.Storage.StorageFile.GetFileFromPathAsync(filePath);
						storageFiles.Add(storageFile);
					}

				}
				completionSource.SetResult(storageFiles);
			}

			return completionSource.Task;
		}

	}


	[Android.App.Activity(ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
	internal class FileOpenPickerActivity : Android.App.Activity
	{
		internal static event EventHandler<List<Android.Net.Uri>> FilePicked;
		private bool _multiple;

		private Android.Content.Intent CreateIntent(bool multiple, List<string> mimeTypes, string initialDir)
		{
			var intent = new Android.Content.Intent(Android.Content.Intent.ActionOpenDocument);

			// file ext / mimetypes
			if (mimeTypes.Count < 1)
			{
				intent.SetType("*/*");  // nie mamy nic - to mamy :)
			}
			else
			{
				if (mimeTypes.Count < 2)
				{
					intent.SetType(mimeTypes.ElementAt(0));
				}
				else
				{
					intent.SetType("*/*");
					intent.PutExtra(Android.Content.Intent.ExtraMimeTypes, mimeTypes.ToArray());
				}
			}

			// multiple selection
			if (multiple)
			{
				intent.PutExtra(Android.Content.Intent.ExtraAllowMultiple, true);
			}

			// initial dir
			if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O && !string.IsNullOrEmpty(initialDir))
			{
				intent.PutExtra(Android.Provider.DocumentsContract.ExtraInitialUri, initialDir);
			}

			// chyba takze
			// intent.AddCategory(Android.Content.Intent.CategoryOpenable);
			// otwieralne w sensie: ContentResolver#openFileDescriptor(Uri, String) // string: rwt (t:can truncate)

			return intent;
		}
		protected override void OnCreate(Android.OS.Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			var caller = base.Intent.Extras;
			_multiple = caller.GetBoolean("multiple", false);

			// startpicker
			var intent = CreateIntent(_multiple,
				caller.GetStringArray("mimetypes").ToList(),
				caller.GetString("initialdir", "")
				);
			// StartActivityForResult(Android.Content.Intent.CreateChooser(intent, "Select file"),10);
			StartActivityForResult(intent, 10);
		}

		protected override void OnActivityResult(int requestCode, Android.App.Result resultCode, Android.Content.Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);

			var pickedFiles = new List<Android.Net.Uri>();

			if (resultCode != Android.App.Result.Canceled)
			{
				if (_multiple)
				{
					// multiple files - ClipData
					if (data?.ClipData != null)
					{
						for (int i = 0; i < data.ClipData.ItemCount; i++)
						{
							var item = data.ClipData.GetItemAt(i);
							pickedFiles.Add(item.Uri);
						}
					}
				}
				else
				{
					// single file - Data
					if (data?.Data != null)
					{
						pickedFiles.Add(data.Data);
					}

				}
			}

			FilePicked?.Invoke(null, pickedFiles);
			Finish();
		}

	}
}

#endif 
