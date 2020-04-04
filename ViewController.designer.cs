// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace xamatest
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSButton addunitCheck { get; set; }

		[Outlet]
		AppKit.NSButton linefeedCheck { get; set; }

		[Outlet]
		AppKit.NSTextView outputTextView { get; set; }

		[Action ("clipboardConvertButton:")]
		partial void clipboardConvertButton (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (outputTextView != null) {
				outputTextView.Dispose ();
				outputTextView = null;
			}

			if (linefeedCheck != null) {
				linefeedCheck.Dispose ();
				linefeedCheck = null;
			}

			if (addunitCheck != null) {
				addunitCheck.Dispose ();
				addunitCheck = null;
			}
		}
	}
}
