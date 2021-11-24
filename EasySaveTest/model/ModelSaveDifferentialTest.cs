using EasySaveConsole.model;
using NUnit.Framework;
using System;
using System.IO;

namespace EasySaveTest.model
{
    internal class ModelSaveDifferentialTest
    {
        [SetUp]
        public void Setup()
        {
            // TODO : Create c:/current
            // Try to create the directory.
            string path = @"c:\MyDir\test1.txt";
            File.Create(path);
            Console.WriteLine("The directory was created successfully .");

            // TODO : Delete c:/archive
            string subPath = @"C:\archive\test1.txt";
            File.Delete(subPath);
        }

        [Test]
        public void Test1()
        {
            ModelSaveDifferential modelSaveDifferential1 = new ModelSaveDifferential("save1", @"C:\MyDir\test1.txt", @"C:\archive\test1.txt");
            modelSaveDifferential1.save();
            // TODO : Check if archive has been created
            Assert.IsTrue(File.Exists(@"C:\archive\save1"));
            Assert.IsTrue(File.Exists(@"C:\archive\save1\test1.txt"));


        }

        public void Test2()
        {
            ModelSaveDifferential modelSaveDifferential2 = new ModelSaveDifferential("save1", @"C:\MyDir", @"C:\archive\test1.txt");
            modelSaveDifferential2.save();
            // TODO : Check if archive has been created
            File.Create(@"c:\MyDir\test2.txt");
            modelSaveDifferential2.save();
            Assert.IsTrue(File.Exists(@"C:\archive\save1\test2.txt"));

        }
    }
}
