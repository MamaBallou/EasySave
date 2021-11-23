using NUnit.Framework;
using EasySaveConsole.logger;

namespace EasySaveTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            LogLogger logLogger = LogLogger.getInstance();
            logLogger.Write("toto");
            string result =
            Assert.AreEqual("toto", result);
        }
    }
}