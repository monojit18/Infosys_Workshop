using System;
using System.Threading.Tasks;
using UIKit;
using Foundation;
using TestFormsApp.Services;

namespace TestFormsApp.iOS
{
    public class CapturePhotoiOS : ICapturePhotoService
    {
        private UIImagePickerController _imagePicker;
        private TaskCompletionSource<byte[]> _imageTask;

        public async Task<byte[]> CapturePhoto()
        {

            _imageTask = new TaskCompletionSource<byte[]>();

            _imagePicker = new UIImagePickerController();
            _imagePicker.PrefersStatusBarHidden();

            _imagePicker.SourceType = UIImagePickerControllerSourceType.Camera;

            //Add event handlers when user finished Capturing image or Cancel
            _imagePicker.FinishedPickingMedia += Handle_FinishedPickingMedia;
            _imagePicker.Canceled += Handle_Canceled;

            var vc = UIApplication.SharedApplication.KeyWindow.RootViewController;

            //present 
            vc.PresentViewController(_imagePicker, true, () => { });

            var imageBytes = await _imageTask.Task;
            return imageBytes;

        }

        protected void Handle_FinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs e)
        {
            // determine what was selected, video or image
            bool isImage = false;
            switch (e.Info[UIImagePickerController.MediaType].ToString())
            {
                case "public.image":
                    Console.WriteLine("Image selected");
                    isImage = true;
                    break;
                case "public.video":
                    Console.WriteLine("Video selected");
                    break;
            }

            // get common info (shared between images and video)
            var referenceURL = e.Info[new NSString("UIImagePickerControllerReferenceUrl")] as NSUrl;
            if (referenceURL != null)
                Console.WriteLine("Url:" + referenceURL.ToString());

            // if it was an image, get the other image info
            if (isImage)
            {
                // get the original image
                UIImage originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;
                if (originalImage != null)
                {
                    // do something with the image
                    Console.WriteLine("got the original image");

                    var imageBytes = originalImage.AsJPEG().ToArray();
                    _imageTask.SetResult(imageBytes);

                    // imageView.Image = originalImage; // display
                }
            }
            else
            { // if it's a video
              // get video url
                NSUrl mediaURL = e.Info[UIImagePickerController.MediaURL] as NSUrl;
                if (mediaURL != null)
                {
                    Console.WriteLine(mediaURL.ToString());
                }
            }
            // dismiss the picker
            var vc = UIApplication.SharedApplication.KeyWindow.RootViewController;
            vc.DismissViewController(false, () => { });
        }

        void Handle_Canceled(object sender, EventArgs e)
        {
            var vc = UIApplication.SharedApplication.KeyWindow.RootViewController;
            vc.DismissViewController(false, () => { });
        }
    }
}
