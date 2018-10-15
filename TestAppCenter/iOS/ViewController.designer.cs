// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace TestAppCenter.iOS
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ClickMeButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField ClickMeTextField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ViewMeButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ClickMeButton != null) {
                ClickMeButton.Dispose ();
                ClickMeButton = null;
            }

            if (ClickMeTextField != null) {
                ClickMeTextField.Dispose ();
                ClickMeTextField = null;
            }

            if (ViewMeButton != null) {
                ViewMeButton.Dispose ();
                ViewMeButton = null;
            }
        }
    }
}