using EasySaveConsole.model;
using NUnit.Framework;
using System;
using System.IO;

namespace EasySaveTest.model
{
    internal class ModelSaveTotalTest
    {
        const string SourcePath = "../../../test_files/MyDir/";
        const string TargetPath = "../../../test_files/archive/";
        const string FileName = "testSave.txt";
        const string SaveName = "save1";

        [SetUp]
        public void Setup()
        {
            // Create a source path
            // Try to create the directory.
            Directory.CreateDirectory(SourcePath);
            File.WriteAllText(@String.Concat(SourcePath, "/", FileName), "Hello");
            // Delete target path to be sure that save function create target path
            try
            {
                Directory.Delete(TargetPath, true);
            }
            catch { }
        }

        [Test]
        public void TestSaveFolder()
        {
            ModelSaveTotal modelSave = new ModelSaveTotal(SaveName, SourcePath, TargetPath);
            ModelState modelState = modelSave.toModelState();
            modelSave.save(ref modelState);
            // Check if archive/save1 has been created
            Assert.IsTrue(Directory.Exists(@String.Concat(TargetPath, SaveName)));
            // Check if testSave.txt has been created
            Assert.IsTrue(File.Exists(@String.Concat(TargetPath, SaveName, "/initial/", FileName)));
        }

        [Test]
        public void TestSaveFile()
        {
            ModelSaveTotal modelSave = new ModelSaveTotal(SaveName, SourcePath, TargetPath);
            ModelState modelState = modelSave.toModelState();
            modelSave.save(ref modelState);
            string toFind = @String.Concat(TargetPath, SaveName, "/initial/", FileName);
            //Check if testSave.txt is created is in save1 
            Assert.IsTrue(File.Exists(toFind));
        }

        [Test]
        public void TestSaveAgain()
        {
            ModelSaveTotal modelSave = new ModelSaveTotal(SaveName, SourcePath, TargetPath);
            ModelState modelState = modelSave.toModelState();
            modelSave.save(ref modelState);
            //Check if testSave.txt is created is in save1 
            var stream = File.Create(String.Concat(SourcePath, "testSave2.txt"));
            stream.Close();
            modelSave.save(ref modelState);
            string filePath = string.Concat(TargetPath, SaveName, "/initial/");
            Assert.IsFalse(File.Exists(filePath));
            Assert.IsTrue(File.Exists(String.Concat(filePath, FileName)));
            foreach (var dir in Directory.GetDirectories(String.Concat(TargetPath, SaveName)))
            {
                string? dirName = (new DirectoryInfo(dir)).Name;
                if (dirName != "initial")
                {
                    Assert.IsTrue(File.Exists(String.Concat(dir, "/testSave2.txt")));
                    Assert.IsTrue(File.Exists(String.Concat(dir, "/", FileName)));
                }
            }
        }

        [TearDown]
        public void TearDown()
        {
            bool success = false;
            do
            {
                try
                {
                    Directory.Delete(SourcePath, true);
                    success = true;
                }
                catch { }
            } while (!success);
            success = false;
            do
            {
                try
                {
                    Directory.Delete(TargetPath, true);
                }
                catch { }
            } while (success);
        }
    }
}

