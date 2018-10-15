using System;
using NUnit.Framework;
using TestAppCenter;

namespace TestappCenter.Droid.Tests
{
    [TestFixture]
    public class TestsSample
    {

        [SetUp]
        public void Setup() { }


        [TearDown]
        public void Tear() { }

        [Test]
        public void Pass()
        {
            Console.WriteLine("test1");
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
        public void Inconclusive()
        {
            Assert.Inconclusive("Inconclusive");
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
