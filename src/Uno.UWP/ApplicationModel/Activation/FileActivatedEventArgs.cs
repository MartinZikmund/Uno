#if __IOS__
using System.Collections.Generic;
using Windows.Storage;

namespace Windows.ApplicationModel.Activation
{
	public partial class FileActivatedEventArgs : IFileActivatedEventArgs, IActivatedEventArgs
	{
		/// <summary>
		/// Internal-only constructor for protocol activation.
		/// </summary>
		/// <param name="uri">Activated uri.</param>
		/// <param name="previousExecutionState">Previous execution state.</param>
		internal FileActivatedEventArgs(IReadOnlyList<IStorageItem> files, ApplicationExecutionState previousExecutionState)
		{
			Files = files;
			PreviousExecutionState = previousExecutionState;
		}

		public ActivationKind Kind => ActivationKind.File;

		public ApplicationExecutionState PreviousExecutionState { get; }

		public IReadOnlyList<IStorageItem> Files { get; }
	}
}
#endif
