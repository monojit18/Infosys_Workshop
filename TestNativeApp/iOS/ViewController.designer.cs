// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace TestNativeApp.iOS
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        UIKit.UIButton Button { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton AnalyzeButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel AnalyzedLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView VoiceImageView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField VoiceTextField { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AnalyzeButton != null) {
                AnalyzeButton.Dispose ();
                AnalyzeButton = null;
            }

            if (AnalyzedLabel != null) {
                AnalyzedLabel.Dispose ();
                AnalyzedLabel = null;
            }

            if (VoiceImageView != null) {
                VoiceImageView.Dispose ();
                VoiceImageView = null;
            }

            if (VoiceTextField != null) {
                VoiceTextField.Dispose ();
                VoiceTextField = null;
            }
        }
    }
}