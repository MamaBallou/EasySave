using System;
using EasySaveConsole;
using NUnit.Framework;

namespace EasySaveTest
{
    public class ToolsTest
    {
        Tool tool = Tool.getInstance();

        [Test]
        public void testGetFileSize()
        {
            string path = "../../../test_files/TestTools.txt";
            uint actual = this.tool.getFileSize(path);
            uint expected = 10;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testcheckExistance1()
        {
            string path = "../../../testtest/testTools.txt";
            Boolean actual = this.tool.checkExistance(path);
            Boolean expected = false;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testcheckExistance2()
        {

            string path = "../../../test_files/TestTools.txt";
            Boolean actual = this.tool.checkExistance(path);
            Boolean expected = true;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testFolderSize()
        {
            string folder = "../../../test_files";
            uint expected = 10;
            uint actual = this.tool.getFileSize(folder);
            Assert.AreEqual(expected, actual);
        }
    }
}
