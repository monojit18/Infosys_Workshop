using System;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Graphics;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using TestFormsApp.Droid.CapturePhotoService;
using Android.Content;

namespace TestFormsApp.Droid
{
    [Activity(Label = "TestFormsApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme",
              MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize |
              ConfigChanges.Orientation)]
    
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            var capturePermissionInfo = CheckSelfPermission(Manifest.Permission.Camera);
            if (capturePermissionInfo == Permission.Denied)
                RequestPermissions(new string[] { Manifest.Permission.Camera }, 1);
            else
                DependencyService.Register<CaptureServiceDroid>();
            
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
                                                        [GeneratedEnum] Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            if (requestCode != 1)
                return;

            if (grantResults[0] != Permission.Granted)
                return;

            DependencyService.Register<CaptureServiceDroid>();

        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode != 1)
                return;

            var bitmap = data.Extras.Get("data") as Bitmap;
            TaskCompletionSource.SetResult(bitmap);

        }

        public TaskCompletionSource<Bitmap> TaskCompletionSource { get; set; }

    }
}

