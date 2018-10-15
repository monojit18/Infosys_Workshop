using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Android;
using Android.Graphics;
using Android.Content;
using Android.Provider;
using Xamarin.Forms;
using TestFormsApp.Services;

namespace TestFormsApp.Droid.CapturePhotoService
{
    public class CaptureServiceDroid : ICapturePhotoService
    {

        private MainActivity _hostActivity;

        public CaptureServiceDroid()
        {

            _hostActivity = (MainActivity)Forms.Context;

        }

        public async Task<byte[]> CapturePhoto()
        {
            
            var tsc = new TaskCompletionSource<Bitmap>();
            var cameraIntent = new Intent();
            cameraIntent.SetAction(MediaStore.ActionImageCapture);

            _hostActivity.TaskCompletionSource = tsc;
            _hostActivity.StartActivityForResult(cameraIntent, 1);

            byte[] bitmapData = null;
            await Task.Run(() => 
            {

                var bitmap = tsc.Task.Result;

                using (var stream = new MemoryStream())
                {
                    bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                    bitmapData = stream.ToArray();
                }

            });
                      


            return bitmapData;

        }
    }
}
