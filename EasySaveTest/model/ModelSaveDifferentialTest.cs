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
            // Create a source path
            // Try to create the directory.
            File.Create(@"c:\MyDir\test1.txt");
            Console.WriteLine("The directory was created successfully .");

            // Delete target path to be sure that save function create target path
            File.Delete(@"C:\archive\test1.txt");
        }

        [Test]
        public void Test1()
        {
            ModelSaveDifferential modelSaveDifferential1 = new ModelSaveDifferential("save1", @"C:\MyDir\test1.txt", @"C:\archive\test1.txt");
            ModelState modelState = new ModelState("save1", @"C:\MyDir\test1.txt", @"C:\archive\test1.txt");
            modelSaveDifferential1.save(ref modelState);
            // Check if archive has been created
            Assert.IsTrue(File.Exists(@"C:\archive\save1"));
            // Check if test1.txt has been created
            Assert.IsTrue(File.Exists(@"C:\archive\save1\test1.txt"));


        }

        public void Test2()
        {
            ModelSaveDifferential modelSaveDifferential2 = new ModelSaveDifferential("save1", @"C:\MyDir", @"C:\archive\test1.txt");
            ModelState modelState = new ModelState("save1", @"C:\MyDir\test1.txt", @"C:\archive\test1.txt");
            modelSaveDifferential2.save(ref modelState);
            File.Create(@"c:\MyDir\test2.txt");
            modelSaveDifferential2.save(ref modelState);
            //Check if test2.txt created is in save1 
            Assert.IsTrue(File.Exists(@"C:\archive\save1\test2.txt"));

        }
    }
}
