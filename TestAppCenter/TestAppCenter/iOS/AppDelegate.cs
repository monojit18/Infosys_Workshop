using System;
using System.Net;
using System.Net.Http;
using System.Diagnostics;
using System.Threading.Tasks;
using Foundation;
using UIKit;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Push;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace TestAppCenter.iOS
{


    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {


        //private async Task GetNewContentAsync(Action<UIBackgroundFetchResult> completionHandler)
        //{

        //    if (_httpClient == null)
        //        _httpClient = new HttpClient();

        //    var urlString = $"https://jsonplaceholder.typicode.com/posts/";
        //    var responseMessage = await _httpClient.GetAsync(urlString);
        //    var responseString = await responseMessage?.Content?.ReadAsStringAsync();
        //    Console.WriteLine(responseString);
        //    completionHandler(UIBackgroundFetchResult.NewData);

        //}

        // class-level declarations

        private HttpClient _httpClient;

        public override UIWindow Window
        {
            get;
            set;
        }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            // Override point for customization after application launch.
            // If not required for your application you can safely delete this method

            // Code to start the Xamarin Test Cloud Agent
#if ENABLE_TEST_CLOUD
            Xamarin.Calabash.Start();
#endif


            // application.SetMinimumBackgroundFetchInterval(UIApplication.BackgroundFetchIntervalMinimum);

            //if (AppCenter.Configured == false)
            //{

            //    Push.PushNotificationReceived += async (sender, e) =>
            //    {
            //            var summary = $"Push notification received:" +
            //                            $"\n\tNotification title: {e.Title}" +
            //                            $"\n\tMessage: {e.Message}";


            //            if (e.CustomData != null)
            //            {
            //                summary += "\n\tCustom data:\n";
            //                foreach (var key in e.CustomData.Keys)
            //                {
            //                    summary += $"\t\t{key} : {e.CustomData[key]}\n";
            //                System.Diagnostics.Debug.WriteLine(summary);

            //                if (_httpClient == null)
            //                    _httpClient = new HttpClient();

            //                var urlString = $"https://jsonplaceholder.typicode.com/posts/";
            //                var responseMessage = await _httpClient.GetAsync(urlString);
            //                var responseString = await responseMessage?.Content?.ReadAsStringAsync();
            //                Console.WriteLine(responseString);

            //                }
            //            }

                        

            //        };
            //    }

            //AppCenter.Start("7a16967b-a4d8-4a5f-9a94-11fe3ec22f01", typeof(Push), typeof(Analytics), typeof(Crashes));
            return true;

        }

        public override void OnResignActivation(UIApplication application)
        {
            // Invoked when the application is about to move from active to inactive state.
            // This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
            // or when the user quits the application and it begins the transition to the background state.
            // Games should use this method to pause the game.
        }

        public override void DidEnterBackground(UIApplication application)
        {
            // Use this method to release shared resources, save user data, invalidate timers and store the application state.
            // If your application supports background exection this method is called instead of WillTerminate when the user quits.
        }

        public override void WillEnterForeground(UIApplication application)
        {
            // Called as part of the transiton from background to active state.
            // Here you can undo many of the changes made on entering the background.
        }

        public override void OnActivated(UIApplication application)
        {
            // Restart any tasks that were paused (or not yet started) while the application was inactive. 
            // If the application was previously in the background, optionally refresh the user interface.
        }

        public override void WillTerminate(UIApplication application)
        {
            // Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
        }

        [Export("viewMeBackdoor:")]
        public NSString ViewMeBackdoor(NSString backdoorString)
        {

            Debug.WriteLine(backdoorString);
            return new NSString("OK");

        }
    }
}

