﻿using CoreGraphics;
using Uno.UI.DataBinding;
using System;
using System.Collections.Generic;
using System.Text;
using AppKit;
using Uno.Extensions;
using Windows.UI.Xaml.Media;
using Uno.UI.Controls;
using Foundation;
using Windows.UI.Xaml.Automation.Peers;

namespace Windows.UI.Xaml.Controls
{
	public partial class SinglelineTextBoxView : NSTextField, ITextBoxView, DependencyObject, IFontScalable
	{
		//private SinglelineTextBoxDelegate _delegate;
		private readonly WeakReference<TextBox> _textBox;

		public SinglelineTextBoxView(TextBox textBox)
		{
			_textBox = new WeakReference<TextBox>(textBox);

			InitializeBinder();
			Initialize();
		}

		private void OnEditingChanged(object sender, EventArgs e)
		{
			OnTextChanged();
		}

		public override string StringValue
		{
			get => base.StringValue;
			set
			{
				value = value ?? string.Empty;
				if (base.StringValue != value)
				{
					base.StringValue = value;
					OnTextChanged();
				}
			}
		}

		private void OnTextChanged()
		{
			var textBox = _textBox?.GetTarget();
			if (textBox != null)
			{
				var text = textBox.ProcessTextInput(StringValue);
				SetTextNative(text);
			}
		}

		public void SetTextNative(string text) => StringValue = text;

		private void Initialize()
		{
			//Cell = new SingleLineTextBoxViewCell();
			Cell.DrawsBackground = false;
			Bezeled = false;

			//Set native VerticalAlignment to top-aligned (default is center) to match Windows text placement
			//base.VerticalAlignment = UIControlContentVerticalAlignment.Top;

			//Delegate = _delegate = new SinglelineTextBoxDelegate(_textBox)
			//{
			//	IsKeyboardHiddenOnEnter = true
			//};

			RegisterLoadActions(
				() =>
				{
					this.EditingBegan += OnEditingChanged;
					this.EditingEnded += OnEditingChanged;
				},
				() =>
				{
					this.EditingBegan -= OnEditingChanged;
					this.EditingEnded -= OnEditingChanged;
				}
			);
		}

		public override CGSize SizeThatFits(CGSize size)
		{
			return IFrameworkElementHelper.SizeThatFits(this, base.SizeThatFits(size));
		}

		//public override CGRect TextRect(CGRect forBounds)
		//{
		//	return GetTextRect(forBounds);
		//}

		//public override CGRect PlaceholderRect(CGRect forBounds)
		//{
		//	return GetTextRect(forBounds);
		//}

		//public override CGRect EditingRect(CGRect forBounds)
		//{
		//	return GetTextRect(forBounds);
		//}

		//private CGRect GetTextRect(CGRect forBounds)
		//{
		//	if (IsStoreInitialized)
		//	{
		//		// This test is present because most virtual methods are
		//		// called before the ctor has finished executing.

		//		return new CGRect(
		//			forBounds.X,
		//			forBounds.Y,
		//			forBounds.Width,
		//			forBounds.Height
		//		);
		//	}
		//	else
		//	{
		//		return CGRect.Empty;
		//	}
		//}

		public void UpdateFont()
		{
			var textBox = _textBox.GetTarget();

			if (textBox != null)
			{
				var newFont = NSFontHelper.TryGetFont((float)textBox.FontSize, textBox.FontWeight, textBox.FontStyle, textBox.FontFamily);

				if (newFont != null)
				{
					base.Font = newFont;
					this.InvalidateMeasure();
				}
			}
		}

		public Brush Foreground
		{
			get { return (Brush)GetValue(ForegroundProperty); }
			set { SetValue(ForegroundProperty, value); }
		}

		public static readonly DependencyProperty ForegroundProperty =
			DependencyProperty.Register(
				"Foreground",
				typeof(Brush),
				typeof(SinglelineTextBoxView),
				new FrameworkPropertyMetadata(
					defaultValue: SolidColorBrushHelper.Black,
					options: FrameworkPropertyMetadataOptions.Inherits,
					propertyChangedCallback: (s, e) => ((SinglelineTextBoxView)s).OnForegroundChanged((Brush)e.OldValue, (Brush)e.NewValue)
				)
			);

		public void OnForegroundChanged(Brush oldValue, Brush newValue)
		{
			var textBox = _textBox.GetTarget();

			if (textBox != null)
			{
				var scb = newValue as SolidColorBrush;

				if (scb != null)
				{
					this.TextColor = scb.Color;
					UpdateCaretColor();
				}
			}
		}

		public override bool BecomeFirstResponder()
		{
			var success = base.BecomeFirstResponder();
			UpdateCaretColor();
			return success;
		}

		private void UpdateCaretColor()
		{
			var textField = (NSTextView)this.CurrentEditor;
			var scb = Foreground as SolidColorBrush;
			if (textField != null && scb != null)
			{
				textField.InsertionPointColor = scb.Color;
			}
		}

		public void UpdateTextAlignment()
		{
			var textBox = _textBox.GetTarget();

			if (textBox != null)
			{
				Cell.Alignment = textBox.TextAlignment.ToNativeTextAlignment();
			}
		}

		public void RefreshFont()
		{
			UpdateFont();
		}

		public void InsertText(NSObject insertString)
		{

		}

		public void SetMarkedText(NSObject @string, NSRange selRange)
		{

		}

		public void UnmarkText()
		{

		}

		public NSAttributedString GetAttributedSubstring(NSRange range)
		{
			return null;
		}

		public CGRect GetFirstRectForCharacterRange(NSRange range)
		{
			return CGRect.Empty;
		}

		public nuint GetCharacterIndex(CGPoint point)
		{
			return 0;
		}

		//public override UITextRange SelectedTextRange
		//{
		//	get
		//	{
		//		return base.SelectedTextRange;
		//	}
		//	set
		//	{
		//		var textBox = _textBox.GetTarget();

		//		if (textBox != null && base.SelectedTextRange != value)
		//		{
		//			base.SelectedTextRange = value;
		//			textBox.OnSelectionChanged();
		//		}
		//	}
		//}

		public bool IsFirstResponder => true;

		public bool HasMarkedText => false;

		public nint ConversationIdentifier => 0;

		public NSRange MarkedRange => new NSRange();

		public NSRange SelectedRange => new NSRange();

		public NSString[] ValidAttributesForMarkedText => new NSString[0];

		public AutomationPeer GetAutomationPeer()
		{
			return null;
		}

		public string GetAccessibilityInnerText()
		{
			return null;
		}
	}
}
