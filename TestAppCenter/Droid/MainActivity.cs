using DBG = System.Diagnostics;
using Java.Interop;
using Android.Content;
using Android.App;
using Android.Widget;
using Android.OS;

//using Microsoft.AppCenter;
//using Microsoft.AppCenter.Analytics;
//using Microsoft.AppCenter.Crashes;

namespace TestAppCenter.Droid
{
    [Activity(Label = "TestAppCenter", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //AppCenter.Start("b76658b1-79d5-4cb4-9e4c-7085312253d3", typeof(Analytics), typeof(Crashes));
            //Analytics.TrackEvent("Main Acitivity loaded - Android");

            // Get our button from the layout resource,
            // and attach an event to it

            var clickMeTextField = FindViewById<EditText>(Resource.Id.ClickMeTextField);
            Button clickMeButton = FindViewById<Button>(Resource.Id.ClickMeButton);
            Button viewMeButton = FindViewById<Button>(Resource.Id.ViewMeButton);


            clickMeButton.Click += delegate
            {

                clickMeTextField.Text = clickMeButton.Text;

                //Analytics.TrackEvent("Button Clicked from  - Android");
                //Crashes.GenerateTestCrash();
            };

            viewMeButton.Click += delegate
            {

                var viewMeIntent = new Intent(this, typeof(ViewMeActivity));
                viewMeIntent.PutExtra("ViewMeString", clickMeTextField.Text);
                StartActivity(viewMeIntent);

            };
        }
                
        [Export("viewMeBackdoor")]
        public void ViewMeBackdoor(string backdoorString)
        {

            DBG.Debug.WriteLine(backdoorString);

        }
    }
}

