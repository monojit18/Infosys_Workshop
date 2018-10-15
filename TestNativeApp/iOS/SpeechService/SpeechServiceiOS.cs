using System;
using System.Threading.Tasks;
using TestNativeApp.Services;
using Subsystems.CustomSpeechToText.iOS.External;

namespace TestNativeApp.iOS.SpeechService
{
    public class SpeechServiceiOS : ISpeechService
    {

        private CMPSpeechToTextIOS _speechText;

        public SpeechServiceiOS()
        {

            _speechText = new CMPSpeechToTextIOS();

        }

        public async Task<string> RecordSpeech()
        {

            var hasStopped = _speechText.StopRecording();
            if (hasStopped == true)
                return null;

            var speechToTextResult = await _speechText.StartRecordingAsync();
            if (speechToTextResult != null)
                return speechToTextResult.Item1;

            if (speechToTextResult.Item2.Item2 != null)
            {

                var errorMessageString = $"Error: {speechToTextResult.Item2.Item2}";
                return errorMessageString;

            }

            return null;


        }
    }
}
