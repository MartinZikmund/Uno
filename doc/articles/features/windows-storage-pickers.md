# Windows.Storage.Pickers

File pickers allow user to pick a folder or a file on the local file system so that the application can work with it. The following table shows which file picker experiences are available across platforms in Uno Platform. For detailed information see the next section.

Legend
  - ✅  Supported
  - ⏸️ Partially supported (see below for more details)
  - 🚫 Not supported
  
| Picker         | UWP | WebAssembly | Android | iOS | macOS | WPF | GTK |
|----------------|-----|-------------|---------|-----|-------|-----|-----|
| FileOpenPicker | ✅   | ✅      (1)     | ✅       | ✅   | ✅     | ✅   | 🚫  |
| FileSavePicker | ✅   | ✅  (1)         | ✅       | ✅   | ✅     | ✅   | 🚫  |
| FolderPicker   | ✅   | ✅           | ✅       | ⏸️ (2)|✅     | 🚫  | 🚫  |

(1) - Multiple implementations supported - see WebAssembly section below
(2) - See iOS section below

## Examples

### FolderPicker

``` c#
var folderPicker = new FolderPicker();
folderPicker.FileTypeFilter.Add("*"); // File type filter has no effect, but is required by UWP.
StorageFolder pickedFolder = await folderPicker.PickSingleFolderAsync();
if (pickedFolder != null)
{
    // Folder was picked you can now use it
    var files = await pickedFolder.GetFilesAsync();
}
else
{
    // No folder was picked or the dialog was cancelled.
}
```

### FileOpenPicker - picking a single file

``` c#
var fileOpenPicker = new FileOpenPicker();
fileOpenPicker.FileTypeFilter.Add(".txt");
fileOpenPicker.FileTypeFilter.Add(".csv");
StorageFile pickedFile = await fileOpenPicker.PickSingleFileAsync();
if (pickedFile != null)
{
    // File was picked, you can now use it
    var text = await FileIO.ReadTextAsync(pickedFile);
}
else
{
    // No file was picked or the dialog was cancelled.
}
```

### FileOpenPicker - picking multiple files

``` c#
var fileOpenPicker = new FileOpenPicker();
fileOpenPicker.FileTypeFilter.Add(".jpg");
fileOpenPicker.FileTypeFilter.Add(".png");
var pickedFiles = await fileOpenPicker.PickMultipleFilesAsync();
if (pickedFiles.Count > 0)
{
    // At least one file was picked, you can use them
    foreach (var file in pickedFiles)
    {
        global::System.Diagnostics.Debug(file.Name);   
    }
}
else
{
    // No file was picked or the dialog was cancelled.
}
```

### FileSavePicker

``` c#
var fileSavePicker = new FileSavePicker();
fileSavePicker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });
StorageFile saveFile = await savePicker.PickSaveFileAsync();
if (saveFile != null)
{
    // Save file was picked, you can now write in it
    await FileIO.WriteTextAsync(saveFile, "Hello, world!");
}
else
{
    // No file was picked or the dialog was cancelled.
}
```

## Picker configuration

File pickers have various configuration options that customize the experience (see the <a href="https://docs.microsoft.com/en-us/uwp/api/windows.storage.pickers.fileopenpicker" target="_blank">UWP documentation</a> for full list of properties). Not all options are supported on all target platforms, in which case these are ignored.

To set which file type extensions you want to allow, use the `FileTypeFilter` property on `FileOpenPicker` and `FolderPicker`, and the `FileTypeChoices` property on `FileSavePicker`. Extensions must be in the format ".xyz" (starting with a dot). For `FileOpenPicker` and `FolderPicker` you can also include "*" (star) entry, which represents the fact that any file extension is allowed.

Some systems use `MIME` types to specify the file type. Uno includes a list of common predefined mappings (see list in <a href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Basics_of_HTTP/MIME_types/Common_types" target="_blank">MDN Docs</a>). If a MIME type you require is missing, you can provide by adding it in the `Uno.WinRTFeatureConfiguration.FileTypes.FileTypeToMimeMapping` dictionary:

``` c#
Uno.WinRTFeatureConfiguration.FileTypes.FileTypeToMimeMapping.Add(".myextension", "some/mimetype");
```

For iOS and macOS, `UTType` is utilized for the same purpose. Here you can provide a custom mapping using `Uno.WinRTFeatureConfiguration.FileTypes.FileTypeToUTTypeMapping` dictionary:

``` c#
Uno.WinRTFeatureConfiguration.FileTypes.FileTypeToUTTypeMapping.Add(".myextension", "my.custom.UTType");
```

## WebAssembly

There are two implementations of file pickers available in WebAssembly - **File System Access API pickers** and **download/upload pickers**
  
### File System Access API pickers

The most powerful picker implementation on WebAssembly uses the <a href="https://wicg.github.io/file-system-access/" target="_blank">**File System Access API**</a>. This is not yet widely implemented across all browsers. See the following support tables for each picker:

- <a href="https://caniuse.com/?search=showDirectoryPicker" target="_blank">`FolderPicker`</a>
- <a href="https://caniuse.com/?search=showOpenFilePicker" target="_blank">`FileOpenPicker`</a>
- <a href="https://caniuse.com/?search=showSaveFilePicker" target="_blank">`FileSavePicker`</a>

`FolderPicker` is only supported for this type of pickers.

File System Access API pickers allow in-place access to the picked files and folders. This means that any modifications the user does to the files are persisted on the target file system.

### Download/upload pickers

In case the **FIle System Access API** is not available in the browser, Uno Platform also offers a fallback to "download" and "upload" experiences. The fallback to these variants happens automatically.

