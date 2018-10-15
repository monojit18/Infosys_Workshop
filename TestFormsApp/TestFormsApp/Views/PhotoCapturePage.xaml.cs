using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xamarin.Forms;
using TestFormsApp.Services;

namespace TestFormsApp.Views
{
    public partial class PhotoCapturePage : ContentPage
    {

        private ICapturePhotoService _imageService;

        async Task Handle_Clicked(object sender, EventArgs e)
        {

            _imageService = DependencyService.Get<ICapturePhotoService>();
            var capturedBytes = await _imageService.CapturePhoto();
            CapturedImage.Source = ImageSource.FromStream(() =>
            {

                return (new MemoryStream(capturedBytes));

            });

        }

        public PhotoCapturePage()
        {
            InitializeComponent();
        }
    }
}
