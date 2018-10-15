using System;
using System.Reflection;
using Android.Runtime;
using Xamarin.Android.NUnitLite;

namespace TestappCenter.Droid.Tests
{
    public class TestInstrumentation : TestSuiteInstrumentation
    {

        protected override void AddTests()
        {

            AddTest(Assembly.GetExecutingAssembly());

        }

        public TestInstrumentation(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
        {
        }

    }
}
