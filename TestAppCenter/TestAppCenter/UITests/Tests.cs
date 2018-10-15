using System;
using System.IO;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace TestAppCenter.UITests
{
    // [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);

        }

        [Test]
        public void ClickingButtonShouldChangeItsLabel()
        {
            Func<AppQuery, AppQuery> button = c => c.Button("ClickMeButton");

            app.Tap(button);
            AppResult[] results = app.Query(button);
            app.Screenshot(results[0].Label);

        }

        [Test]
        public void TextShouldBeEqualToButtonLabel()
        {
            Func<AppQuery, AppQuery> clickMeTextField = c => c.TextField("ClickMeTextField");
            Func<AppQuery, AppQuery> clickMeButton = c => c.Button("ClickMeButton");

            app.Tap(clickMeButton);

            var buttonResults = app.Query(clickMeButton);
            var buttonText = buttonResults[0].Label;

            app.ClearText(clickMeTextField);
            app.EnterText(clickMeTextField, buttonText);

            var clickMeResults = app.Query(clickMeTextField);
            var clickMeText = clickMeResults[0].Text;

            Assert.AreEqual(clickMeText, buttonText);

        }

        [Test]
        public void ViewMeShouldPassOnButtonLabel()
        {

            Func<AppQuery, AppQuery> clickMeTextField = c => c.TextField("ClickMeTextField");
            Func<AppQuery, AppQuery> viewMeButton = c => c.Button("ViewMeButton");

            app.Tap(viewMeButton);

            var clickMeResults = app.Query(clickMeTextField);
            var clickMeText = clickMeResults[0].Text;

            Func<AppQuery, AppQuery> viewMeTextField = c => c.TextField("ViewMeTextField");
            Func<AppQuery, AppQuery> doneButton = c => c.Button("DoneButton");

            app.WaitForElement(viewMeTextField);

            var viewMeResults = app.Query(viewMeTextField);
            var viewMeText = viewMeResults[0].Text;

            Assert.AreEqual(clickMeText, viewMeText);

#if __IOS__
            app.Invoke("viewMeBackdoor:", "I am backdoor");
#elif __ANDROID__
            app.Invoke("viewMeBackdoor");
#endif
            app.Screenshot(viewMeText);
            app.Tap(doneButton);


        }

        [Ignore]
        public void CheckREPL()
        {

            app.Repl();

        }
    }
}
