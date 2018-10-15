
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace TestAppCenter.Droid
{
    [Activity(Label = "ViewMeActivity")]
    public class ViewMeActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ViewMe);

            ViewMeString = Intent.GetStringExtra("ViewMeString");

            var doneButton = FindViewById<Button>(Resource.Id.DoneButton);
            doneButton.Click += (object sender, EventArgs e) =>
            {

                Finish();

            };

            var viewMeTextField = FindViewById<EditText>(Resource.Id.ViewMeTextField);
            viewMeTextField.Text = ViewMeString;


        }

        public string ViewMeString { get; set; }

    }
}
