using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SamplesApp
{
	public sealed partial class MainPage : Page
	{
		public TestViewModel ViewModel { get; }

		public MainPage()
		{
			this.InitializeComponent();

			DataContext = ViewModel = new TestViewModel();
			for (int i = 0; i < 10; i++)
				ViewModel.Items.Add(new TestViewModelItem { Label = $"Test{i}" });

			//AddButton.Click += AddButton_Click;
			//RemoveButton.Click += RemoveButton_Click;
		}

		private void AddButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
		{
			//ViewModel.Items.Add(new TestViewModelItem { Label = $"Test {ViewModel.Items.Count + 1}" });
			ViewModel.IsLoaded = true;
		}

		private void RemoveButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
		{
			//if (ViewModel.Items.Count > 0)
			//    ViewModel.Items.RemoveAt(ViewModel.Items.Count - 1);
			ViewModel.IsLoaded = false;
		}
	}

	public class TestViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public ObservableCollection<TestViewModelItem> Items { get; } = new ObservableCollection<TestViewModelItem>();
		//public ObservableCollection<TestViewModelGroup> Groups { get; } = new ObservableCollection<TestViewModelGroup>();

		public bool IsLoaded
		{
			get { return _isLoaded; }
			set
			{
				if (_isLoaded != value)
				{
					_isLoaded = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsLoaded)));
				}
			}
		}
		bool _isLoaded = false;
	}

	//public class TestViewModelGroup : INotifyPropertyChanged
	//{
	//    public event PropertyChangedEventHandler PropertyChanged;

	//    public string Label { get; set; }

	//    public ObservableCollection<TestViewModelItem> Items { get; } = new ObservableCollection<TestViewModelItem>();
	//}

	public class TestViewModelItem : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public string Label { get; set; }

		public string Test1
		{
			get { return _test1; }
			set
			{
				if (_test1 != value)
				{
					_test1 = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Test1)));
				}
			}
		}
		string _test1;

		public string Test2
		{
			get { return _test2; }
			set
			{
				if (_test2 != value)
				{
					_test2 = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Test2)));
				}
			}
		}
		string _test2;
	}
}
