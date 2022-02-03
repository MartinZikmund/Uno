#nullable enable

using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Uno.Disposables;
using Uno.UI.Samples.Controls;
using Uno.UI.Samples.UITests.Helpers;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;

namespace UITests.Windows_UI_ViewManagement
{
	[Sample("Windows.UI.ViewManagement", ViewModelType = typeof(InputPaneTestViewModel))]
	public sealed partial class InputPaneTests : Page
	{
		public InputPaneTests()
		{
			InitializeComponent();
			DataContextChanged += InputPaneTests_DataContextChanged;
		}

		internal InputPaneTestViewModel? Model { get; private set; }

		private void InputPaneTests_DataContextChanged(Windows.UI.Xaml.FrameworkElement sender, Windows.UI.Xaml.DataContextChangedEventArgs args)
		{
			if (args.NewValue is InputPaneTestViewModel viewModel)
			{
				Model = viewModel;
			}
		}
	}

	internal class InputPaneTestViewModel : ViewModelBase
	{
		private InputPane? _inputPane;

		private bool _isVisible;
		private Rect _occludedRect;
		private string _status = "";

		private bool _isShowingAttached = false;
		private bool _isHidingAttached = false;

		public InputPaneTestViewModel(CoreDispatcher dispatcher) : base(dispatcher)
		{
			_inputPane = InputPane.GetForCurrentView();
			IsAvailable = _inputPane != null;
			if (!IsAvailable)
			{
				AddStatus("InputPane is not available on this device");
			}

			Disposables.Add(Disposable.Create(() =>
			{
				IsHidingAttached = false;
				IsShowingAttached = false;
			}));
		}

		public ICommand RefreshCommand => GetOrCreateCommand(Refresh);

		public ICommand TryHideCommand => GetOrCreateCommand(TryHide);

		public ICommand DelayedHideCommand => GetOrCreateCommand(DelayedHide);

		public ICommand TryShowCommand => GetOrCreateCommand(TryShow);

		public bool IsAvailable { get; }

		public bool IsVisible
		{
			get => _isVisible;
			set
			{
				_isVisible = value;
				RaisePropertyChanged();
			}
		}

		public Rect OccludedRect
		{
			get => _occludedRect;
			set
			{
				_occludedRect = value;
				RaisePropertyChanged();
			}
		}

		public string Status
		{
			get => _status;
			set
			{
				_status = value;
				RaisePropertyChanged();
			}
		}

		public bool IsShowingAttached
		{
			get => _isShowingAttached;
			set
			{
				if (_isShowingAttached != value)
				{
					_isShowingAttached = value;
					if (value && _inputPane != null)
					{
						_inputPane.Showing += OnShowing;
					}
					else if (!value && _inputPane != null)
					{
						_inputPane.Showing -= OnHiding;
					}
				}
			}
		}

		public bool IsHidingAttached
		{
			get => _isHidingAttached;
			set
			{
				if (_isHidingAttached != value)
				{
					_isHidingAttached = value;
					if (value && _inputPane != null)
					{
						_inputPane.Hiding += OnHiding;
					}
					else if (!value && _inputPane != null)
					{
						_inputPane.Hiding -= OnHiding;
					}
				}
			}
		}

		private void OnShowing(InputPane sender, InputPaneVisibilityEventArgs args)
		{
			AddStatus("Showing event executed");
			Refresh();
		}

		private void OnHiding(InputPane sender, InputPaneVisibilityEventArgs args)
		{
			AddStatus("Hiding event executed");
			Refresh();
		}

		private void Refresh()
		{
			if (_inputPane == null)
			{
				return;
			}

			IsVisible = _inputPane.Visible;
			OccludedRect = _inputPane.OccludedRect;
		}

		private void TryShow() => AddStatus("TryShow result " + _inputPane?.TryShow().ToString());

		private void TryHide() => AddStatus("TryHide result " + _inputPane?.TryShow().ToString());

		private async void DelayedHide()
		{
			await Task.Delay(3000);
			TryHide();
		}

		private void AddStatus(string message) => Status = message + Environment.NewLine + Status;
	}
}
