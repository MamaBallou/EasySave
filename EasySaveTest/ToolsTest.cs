using EasySaveConsole;
using NUnit.Framework;
using System;

namespace EasySaveTest
{
    public class ToolsTest
    {
        Tool tool = Tool.getInstance();

        [Test]
        public void testGetFileSize()
        {
            string path = "../../../test_files/TestTools.txt";
            uint actual = tool.getFileSize(path);
            uint expected = 10;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testcheckExistance1()
        {
            string path = "../../../testtest/testTools.txt";
            Boolean actual = tool.checkExistance(path);
            Boolean expected = false;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testcheckExistance2()
        {

            string path = "../../../test_files/TestTools.txt";
            Boolean actual = tool.checkExistance(path);
            Boolean expected = true;
            Assert.AreEqual(expected, actual);
        }
    }
}
