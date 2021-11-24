using EasySaveConsole.model;
using NUnit.Framework;
using System;
using System.IO;

namespace EasySaveTest.model
{
    internal class ModelSaveTotalTest
    {
        [SetUp]
        public void Setup()
        {
            // TODO : Create c:/current
            // Try to create the directory.
            string path = @"c:\MyDir";
            DirectoryInfo di = Directory.CreateDirectory(path);
            Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path));

            // TODO : Delete c:/archive
            string subPath = @"C:\archive";
            Directory.Delete(subPath);
        }

        [Test]
        public void Test1()
        {
            ModelSaveTotal  modelSaveTotal = new ModelSaveTotal("save1", @"C:\MyDir", @"C:\archive");
            modelSaveTotal.save();
            // TODO : Check if archive has been created
            Assert.IsTrue(File.Exists(@"C:\archive"));

        }
    }
}

