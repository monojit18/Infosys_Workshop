using System;
using NUnit.Framework;
using TestAppCenter;

namespace TestAppCenter.iOS.Tests
{
    [TestFixture]
    public class SampleTests
    {
        [Test]
        public void Pass()
        {
            Assert.True(true);
        }

        [Test]
        public void Fail()
        {
            Assert.False(true);
        }

        [Test]
        [Ignore("another time")]
        public void Ignore()
        {
            Assert.True(false);
        }

        [Test]
        public void Sample1()
        {

            var obj = new SharedCheck();
            var lst = obj.ExtractTokens("ms-pd/t3.y5");
            Assert.AreEqual(4, lst.Count, "Error in Extraction!!");

        }

    }
}
