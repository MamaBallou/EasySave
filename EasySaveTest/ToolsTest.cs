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
            Uri path = new Uri(@"../../../test_files/TestTools.txt", UriKind.Relative);
            int actual = tool.getFileSize(path);
            int expected = 10;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testcheckExistance1()
        {
            Uri path = new Uri(@"../../../testtest/testTools.txt", UriKind.Relative);
            Boolean actual = tool.checkExistance(path);
            Boolean expected = false;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testcheckExistance2()
        {

            Uri path = new Uri(@"../../../test_files/TestTools.txt", UriKind.Relative);
            Boolean actual = tool.checkExistance(path);
            Boolean expected = true;
            Assert.AreEqual(expected, actual);
        }




        void testcheckAccess()
        {
            Uri path = new Uri(@"../test_files/TestTools.txt", UriKind.Relative);
            Boolean actual = tool.checkAccess(path);
            Boolean expected = true;
            Assert.AreEqual(actual, expected);
        }
        
    }
}
