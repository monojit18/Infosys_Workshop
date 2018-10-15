using System;
using System.Threading.Tasks;
using TestNativeApp.Services;

#if __IOS__

using TestNativeApp.iOS.SpeechService;

#elif __ANDROID__

using Android.App;
using Android.Content;
using TestNativeApp.Droid.SpeechService;

#endif


namespace TestNativeApp
{
    public class SharedAppInitializer
    {

        private class Nested
        {

            static Nested() { }
            internal static readonly SharedAppInitializer _instance = new SharedAppInitializer();

        }

        public static SharedAppInitializer SharedInstance
        {
            get
            {
                return Nested._instance;
            }
        }

        public ISpeechService GetSpeechService(object context = null, object task = null)
        {

#if __IOS__

            if (SpeechService == null)
                SpeechService = new SpeechServiceiOS();

#elif __ANDROID__

            // if (SpeechService == null)
            {

                var activity = context as Activity;
                var activityTask = task as TaskCompletionSource<Intent>;
                SpeechService = new SpeechServiceDroid(activity, activityTask);

            }
                

#endif

            return SpeechService;

        }

        public ISpeechService SpeechService { get; set; }


    }
}
