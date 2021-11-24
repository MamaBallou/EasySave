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
            // Create a source path
            // Try to create the directory.
            string path = @"c:\MyDir";
            DirectoryInfo di = Directory.CreateDirectory(path);
            Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path));

            // Delete target path to be sure that save function create target path
            string subPath = @"C:\archive";
            Directory.Delete(subPath);
        }

        [Test]
        public void Test1()
        {
            ModelSaveTotal  modelSaveTotal1 = new ModelSaveTotal("save1", @"C:\MyDir", @"C:\archive");
            ModelState modelState = new ModelState("save1", @"C:\MyDir\test1.txt", @"C:\archive\test1.txt");
            modelSaveTotal1.save(ref modelState);
            // Check if archive has been created
            Assert.IsTrue(File.Exists(@"C:\archive"));

        }
    }
}

