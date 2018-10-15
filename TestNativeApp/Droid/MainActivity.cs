using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Android.Text;
using Android.Text.Style;
using Android.Graphics;
using Android.Content;
using Android.App;
using Android.Widget;
using Android.OS;
using TestNativeApp.ViewModels;

namespace TestNativeApp.Droid
{
    [Activity(Label = "TestNativeApp", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        private const string KAnalyticsKeyString = "6059a77aa12d4d61b63b6e449e5a4627";
        private RecordViewModel _recordViewModel;
        private AnalyzeViewModel _analyzeViewModel;
        private TextView _voiceTextView;
        private TextView _analyzeTextView;
        private TaskCompletionSource<Intent> _activityTask;

        private void PrepareAnalyzedText(string voiceText, double score)
        {

            if (string.IsNullOrEmpty(voiceText) == true)
                return;

            _analyzeTextView.SetTextColor((score < 0.3) ? Color.Red : Color.Blue);
            _analyzeTextView.Text = voiceText;

        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            _analyzeViewModel = new AnalyzeViewModel();
            _recordViewModel = new RecordViewModel();

            _voiceTextView = (EditText)(FindViewById(Resource.Id.voiceTextView));
            _analyzeTextView = (TextView)(FindViewById(Resource.Id.analyzedTextView));

            var recordButton = (Button)(FindViewById(Resource.Id.recordButton));
            recordButton.Click += async (object sender, EventArgs e) =>
            {

                _voiceTextView.Text = string.Empty;
                _analyzeTextView.Text = string.Empty;
                _activityTask = new TaskCompletionSource<Intent>();
                _voiceTextView.Text = await _recordViewModel.RecordVoice(this, _activityTask);

            };

            var analyzeButton = (Button)(FindViewById(Resource.Id.analyzeButton));
            analyzeButton.Click += async (object sender, EventArgs e) =>
            {

                var analyzedInfoList = await _analyzeViewModel.AnalyzeTextAsync(_voiceTextView.Text);
                var spannableString = new SpannableString(_voiceTextView.Text);

                foreach (var analyzedInfo in analyzedInfoList)
                {

                    if (analyzedInfo.Item2 > 0.3)
                        continue;

                    var voiceText = _voiceTextView.Text;
                    var foundIndex = voiceText.IndexOf(analyzedInfo.Item1, StringComparison.CurrentCultureIgnoreCase);
                    if (foundIndex == -1)
                        continue;

                    spannableString.SetSpan(new ForegroundColorSpan(Color.Red), foundIndex,
                                            foundIndex + analyzedInfo.Item1.Length, SpanTypes.ExclusiveExclusive);

                }

                _analyzeTextView.SetText(spannableString, TextView.BufferType.Spannable);

            };
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {

            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == 0)
            {

                if (resultCode == Result.Ok)
                {


                    _activityTask.SetResult(data);


                }


            }

        }
    }
}

