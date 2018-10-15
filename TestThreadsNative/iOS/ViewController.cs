using System;
using System.Threading.Tasks;

using UIKit;

using TestThreadsNative;

namespace TestThreadsNative.iOS
{
    public partial class ViewController : UIViewController
    {
        int count = 1;

        private TestThreads _testBlocking;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Code to start the Xamarin Test Cloud Agent
#if ENABLE_TEST_CLOUD
            Xamarin.Calabash.Start ();
#endif

            // Perform any additional setup after loading the view, typically from a nib.
            BlockButton.AccessibilityIdentifier = "myButton";
            BlockButton.TouchUpInside += async delegate
            {

                _testBlocking = new TestThreads();
                await _testBlocking.RequestAccessAsync();
                Console.WriteLine("1");

            };

            PoolButton.TouchUpInside += async delegate
            {

                _testBlocking = new TestThreads();
                await _testBlocking.CallPool1Async();

            };

            ParallelButton.TouchUpInside +=  delegate
            {

                _testBlocking = new TestThreads();
                _testBlocking.CallParallel();

            };
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.        
        }
    }
}
