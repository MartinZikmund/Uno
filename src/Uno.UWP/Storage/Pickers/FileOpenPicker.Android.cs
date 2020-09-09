#if __ANDROID__
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.Content;
using Uno.Storage.Pickers.Internal;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace Windows.Storage.Pickers
{
	public partial class FileOpenPicker
	{
		private string _docUri = "";

		public FileOpenPicker()
		{
			if (Android.OS.Build.VERSION.SdkInt < Android.OS.BuildVersionCodes.Kitkat)
			{
				throw new NotImplementedException("FileOpenPicker requires Android KitKat (API level 19) or newer");
			}
			SetDocUri();
			// check permission: READ_EXTERNAL_STORAGE
		}

		public PickerLocationId SuggestedStartLocation { get; set; } = PickerLocationId.DocumentsLibrary;

		public string SettingsIdentifier { get; set; } = "";

		public IList<string> FileTypeFilter { get; } = new NonNullList<string>();

		public IAsyncOperation<StorageFile> PickSingleFileAsync() =>
			PickSingleFileAsyncTask().AsAsyncOperation();

		public IAsyncOperation<IReadOnlyList<StorageFile>> PickMultipleFilesAsync() =>
			PickMultipleFilesAsyncTask().AsAsyncOperation();

		private List<string> UWPextension2Mime()
		{
			var mimeTypes = new List<string>();
			if (FileTypeFilter.Count < 1)
			{
				mimeTypes.Add("*/*");
				return mimeTypes;
			}

			foreach (var extension in FileTypeFilter)
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

			if (ApplicationData.Current.RoamingSettings.Values.ContainsKey(settName))
			{
				// string lastFolder = Windows.Storage.ApplicationData.Current.RoamingSettings.Values[settName].ToString();
				_docUri = ApplicationData.Current.RoamingSettings.Values[settName].ToString();
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

		public async Task<StorageFile> PickSingleFileAsyncTask()
		{
			var pickedFiles = await PickFilesAsync(false);
			if (pickedFiles is null)
			{
				return null;
			}

			return pickedFiles.ElementAt(0);
		}
		public async Task<IReadOnlyList<StorageFile>> PickMultipleFilesAsyncTask()
		{
			var pickedFiles = await PickFilesAsync(true);
			if (pickedFiles is null)
			{
				return null;
			}

			return pickedFiles.AsReadOnly();
		}

		private TaskCompletionSource<List<StorageFile>> completionSource;

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

		private Task<List<StorageFile>> PickFilesAsync(bool multiple)
		{
			var intent = new Intent(Android.App.Application.Context, typeof(FileOpenPickerActivity));
			// put parameters into Intent
			intent.PutExtra("multiple", multiple);
			intent.PutExtra("initialdir", _docUri);
			intent.PutExtra("mimetypes", UWPextension2Mime().ToArray());

			// wrap it in Task
			completionSource = new TaskCompletionSource<List<StorageFile>>();
			FileOpenPickerActivity.FilePicked += FilePickerHandler;

			Android.App.Application.Context.StartActivity(intent);

			async void FilePickerHandler(object sender, List<Android.Net.Uri> list)
			{
				FileOpenPickerActivity.FilePicked -= FilePickerHandler;

				// convert list of Uris tolist of StorageFiles
				var storageFiles = new List<StorageFile>();

				if (list.Count > 0)
				{
					string settName = "_UnoFilePickerSettings" + SettingsIdentifier;
					ApplicationData.Current.RoamingSettings.Values[settName] = list.ElementAt(0).ToString();
				}

				foreach (var fileUri in list)
				{
					Android.App.Application.Context.ContentResolver.TakePersistableUriPermission(
						fileUri, Android.Content.ActivityFlags.GrantReadUriPermission);

					string filePath = PathFromUri(fileUri);
					if (!string.IsNullOrEmpty(filePath))
					{
						var storageFile = await StorageFile.GetFileFromPathAsync(filePath);
						storageFiles.Add(storageFile);
					}

				}
				completionSource.SetResult(storageFiles);
			}

			return completionSource.Task;
		}
	}	
}

#endif 
