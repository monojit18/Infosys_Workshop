using System;
using System.Threading.Tasks;
using TestNativeApp;

namespace TestNativeApp.ViewModels
{
    public class RecordViewModel
    {
        public RecordViewModel()
        {
        }

        public async Task<string> RecordVoice(object context = null, object task = null)
        {

            var speechService = SharedAppInitializer.SharedInstance.GetSpeechService(context, task);
            var recordedSpeech = await speechService.RecordSpeech();
            return recordedSpeech;


        }
    }
}
