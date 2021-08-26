using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Uno.Extensions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace SamplesApp
{
	public sealed partial class MainPage : Page
	{
		public MainPage()
		{
			this.InitializeComponent();
			textValue.AddHandler(TappedEvent, new TappedEventHandler(OnTapped), true);
		}

		private void OnTapped(object sender, TappedRoutedEventArgs e)
		{
			FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
		}

		private void InheritTestClick(object sender, RoutedEventArgs e)
		{
			FrameworkElement el = InheritTest;
			while (el != null)
			{
				LogStuff(el + " " + el.AllowFocusOnInteraction.ToString());
				el = el.Parent as FrameworkElement;
			}
		}

		private void OnFlyoutButtonClick(object sender, RoutedEventArgs e)
		{
			LogStuff("OnFlyoutButtonClick Before setting the Text" + ((Button)sender)?.Content?.ToString());
			
			textValue.Text = ((Button)sender)?.Content?.ToString() ?? "Cat";
			LogStuff("Updated text " + textValue.Text);
			flyout.Hide();
			FocusText();
		}

		private void OnBeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
		{
			LogStuff($"OnBeforeTextChanging - args.NewText = {args.NewText}, textValue.Text = {textValue.Text}");
			if (args.NewText == textValue.Text)
				args.Cancel = true;
		}

		private void OnTextLostFocus(object sender, RoutedEventArgs e)
		{
			LogStuff("OnTextLostFocus");
			//flyout.Hide();
		}

		private void OnTextGotFocus(object sender, RoutedEventArgs e)
		{
			LogStuff("OnTextGotFocus");
		}

		private void OnBtnGotFocus(object sender, RoutedEventArgs e)
		{
			LogStuff("OnBtnGotFocus");
		}

		private void LogStuff(string text)
		{
			this.Log().LogError(text);
			global::System.Diagnostics.Debug.WriteLine(text);
		}

		private void FocusText()
		{
			LogStuff("FocusText");
			textValue.Focus(FocusState.Pointer);
			LogStuff("Updated text before select " + textValue.Text);
			textValue.Select(textValue.Text.Length, 0);
			LogStuff("Updated text after select " + textValue.Text);
		}
	}
}
