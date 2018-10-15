using System;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using TestNativeApp.Services;
using Subsystems.CustomSpeechToText.Droid.External;


namespace TestNativeApp.Droid.SpeechService
{
    public class SpeechServiceDroid : ISpeechService
    {

        private CMPSpeechToTextDroid _speechToText;
        private Activity _activityContext;
        private TaskCompletionSource<Intent> _activityTask;

        public SpeechServiceDroid(Activity activityContext, TaskCompletionSource<Intent> activityTask)
        {

            _activityContext = activityContext;
            _activityTask = activityTask;
            _speechToText = new CMPSpeechToTextDroid(activityContext.PackageName);


        }

        public async Task<string> RecordSpeech()
        {
            
            var speechInput = _speechToText.GetSpeechIntent();
            _activityContext.StartActivityForResult(speechInput, 0);
            var result = await _activityTask.Task;

            var speechTextResult = _speechToText.ProcessSpeechOutput(result);
            var spokenTextString = string.Empty;

            if (speechTextResult.Item2 != null)
                spokenTextString = $"Error:{speechTextResult.Item2.Message}";
            else
                spokenTextString = speechTextResult.Item1;
            
            return spokenTextString;

        }
    }
}
