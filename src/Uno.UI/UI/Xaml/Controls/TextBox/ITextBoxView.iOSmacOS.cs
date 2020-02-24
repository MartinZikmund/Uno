using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Media;

#if __IOS__
using UIKit;
using INativeTextInput = UIKit.IUITextInput;
#else
using AppKit;
using INativeTextInput = AppKit.INSTextInput;
#endif

namespace Windows.UI.Xaml.Controls
{
	//in iOS, we need to use two different controls to be able to accept return (UITextField vs UITextView)
	//we use this interface to abstract properties that we need to modify in TextBox
	public interface ITextBoxView : INativeTextInput
	{
		void UpdateFont();
		bool BecomeFirstResponder();
		bool ResignFirstResponder();
		bool IsFirstResponder { get; }
		void UpdateTextAlignment();
		Brush Foreground { get; set; }
		void SetTextNative(string text);
	}
}
