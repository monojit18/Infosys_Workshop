using Foundation;
using System;
using UIKit;

namespace TestAppCenter.iOS
{
    public partial class ViewMeViewController : UIViewController
    {

        public string ViewMeString { get; set; }

        public ViewMeViewController (IntPtr handle) : base (handle)
        {

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            DoneButton.TouchUpInside += delegate
            {

                DismissViewController(true, null);


            };

        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            ViewMeTextField.Text = ViewMeString ?? string.Empty;
        }

    }
}