using EasySaveConsole.model;
using NUnit.Framework;
using System;
using System.IO;

namespace EasySaveTest.model
{
    internal class ModelSaveDifferentialTest
    {
        const string SourcePath = "../../../test_files/MyDir/";
        const string TargetPath = "../../../test_files/archive/";
        const string FileName = "testSave.txt";
        const string SaveName = "save1";
        private const string subdir = "subDir/";
        private const string FileName2 = "testSave2.txt";

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
            ModelSaveDifferential modelSaveDifferential1 = new ModelSaveDifferential(SaveName, SourcePath, TargetPath);
            ModelState modelState = modelSaveDifferential1.toModelState();
            modelSaveDifferential1.save(ref modelState);
            // Check if archive/save1 has been created
            Assert.IsTrue(Directory.Exists(@String.Concat(TargetPath, SaveName, "/initial/")));
            // Check if testSave.txt has been created
            Assert.IsTrue(File.Exists(@String.Concat(TargetPath, SaveName, "/initial/", FileName)));
        }

        [Test]
        public void TestSaveFile()
        {
            ModelSaveDifferential modelSaveDifferential2 = new ModelSaveDifferential(SaveName, SourcePath, TargetPath);
            ModelState modelState = modelSaveDifferential2.toModelState();
            modelSaveDifferential2.save(ref modelState);
            string toFind = @String.Concat(TargetPath, SaveName, "/initial/", FileName);
            //Check if testSave.txt is created is in save1 
            Assert.IsTrue(File.Exists(toFind));
        }

        [Test]
        public void TestSaveAgain()
        {
            ModelSaveDifferential modelSaveDifferential2 = new ModelSaveDifferential(SaveName, SourcePath, TargetPath);
            ModelState modelState = modelSaveDifferential2.toModelState();
            modelSaveDifferential2.save(ref modelState);
            //Check if testSave.txt is created is in save1 
            var stream = File.Create(@String.Concat(SourcePath, FileName2));
            stream.Close();
            modelSaveDifferential2.save(ref modelState);
            string filePath = @String.Concat(TargetPath, SaveName, "/initial/");
            Assert.IsFalse(File.Exists(String.Concat(filePath, FileName2)));
            Assert.IsTrue(File.Exists(String.Concat(filePath, FileName)));
            foreach (var dir in Directory.GetDirectories(String.Concat(TargetPath, SaveName)))
            {
                string? dirName = (new DirectoryInfo(dir)).Name;
                if (dirName != "initial")
                {
                    Assert.IsTrue(File.Exists(String.Concat(dir, "/", FileName2)));
                    Assert.IsFalse(File.Exists(String.Concat(dir, "/", FileName)));
                }
            }
        }

        [Test]
        public void TestSaveSubFolder()
        {
            Directory.CreateDirectory(String.Concat(SourcePath, subdir));
            File.WriteAllText(String.Concat(SourcePath, subdir, FileName2), "World");
            ModelSaveDifferential modelSaveDifferential2 = new ModelSaveDifferential(SaveName, SourcePath, TargetPath);
            ModelState modelState = modelSaveDifferential2.toModelState();
            modelSaveDifferential2.save(ref modelState);
            File.WriteAllText(String.Concat(SourcePath, FileName), "Zoulou");
            modelSaveDifferential2.save(ref modelState);

            foreach (var dir in Directory.GetDirectories(String.Concat(TargetPath, SaveName)))
            {
                string? dirName = (new DirectoryInfo(dir)).Name;
                if (dirName != "initial")
                {
                    Assert.IsTrue(File.Exists(String.Concat(dir, "/", FileName)));
                    Assert.IsFalse(File.Exists(String.Concat(dir, "/", subdir, FileName2)));
                }
            }
        }

        [Test]
        public void TestCheckToSave()
        {
            string targetInitPath = String.Concat(TargetPath, SaveName, "/initial/");
            Directory.CreateDirectory(targetInitPath);
            ModelSave save = new ModelSaveDifferential(SaveName, SourcePath, TargetPath);
            string sourceFile = String.Concat(SourcePath, FileName);
            Assert.IsTrue(save.CheckIfToSave(sourceFile, targetInitPath));
            File.Copy(sourceFile, String.Concat(targetInitPath, FileName));
            Assert.IsFalse(save.CheckIfToSave(sourceFile, targetInitPath));
            File.WriteAllText(sourceFile, "Shololololo !!");
            Assert.IsTrue(save.CheckIfToSave(sourceFile, targetInitPath));
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
