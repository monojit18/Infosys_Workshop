// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace TestThreadsNative.iOS
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        UIKit.UIButton Button { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton BlockButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ParallelButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton PoolButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (BlockButton != null) {
                BlockButton.Dispose ();
                BlockButton = null;
            }

            if (ParallelButton != null) {
                ParallelButton.Dispose ();
                ParallelButton = null;
            }

            if (PoolButton != null) {
                PoolButton.Dispose ();
                PoolButton = null;
            }
        }
    }
}