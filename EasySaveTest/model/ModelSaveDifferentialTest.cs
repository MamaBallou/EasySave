using EasySaveConsole.model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
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
            ModelSaveDifferential modelSave = new ModelSaveDifferential(SaveName, SourcePath, TargetPath);
            ModelState modelState = modelSave.toModelState();
            List<ModelState> modelStates = new List<ModelState> { modelState };
            modelSave.save(ref modelState, modelStates);
            // Check if archive/save1 has been created
            Assert.IsTrue(Directory.Exists(@String.Concat(TargetPath, SaveName, "/initial/")));
            // Check if testSave.txt has been created
            Assert.IsTrue(File.Exists(@String.Concat(TargetPath, SaveName, "/initial/", FileName)));
        }

        [Test]
        public void TestSaveFile()
        {
            ModelSaveDifferential modelSave = new ModelSaveDifferential(SaveName, SourcePath, TargetPath);
            ModelState modelState = modelSave.toModelState();
            List<ModelState> modelStates = new List<ModelState> { modelState };
            modelSave.save(ref modelState, modelStates);
            string toFind = @String.Concat(TargetPath, SaveName, "/initial/", FileName);
            //Check if testSave.txt is created is in save1 
            Assert.IsTrue(File.Exists(toFind));
        }

        [Test]
        public void TestSaveAgain()
        {
            ModelSaveDifferential modelSave = new ModelSaveDifferential(SaveName, SourcePath, TargetPath);
            ModelState modelState = modelSave.toModelState();
            List<ModelState> modelStates = new List<ModelState> { modelState };
            modelSave.save(ref modelState, modelStates);
            //Check if testSave.txt is created is in save1 
            var stream = File.Create(@String.Concat(SourcePath, FileName2));
            stream.Close();
            modelSave.save(ref modelState, modelStates);
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
            ModelSaveDifferential modelSave = new ModelSaveDifferential(SaveName, SourcePath, TargetPath);
            ModelState modelState = modelSave.toModelState();
            List<ModelState> modelStates = new List<ModelState> { modelState };
            modelSave.save(ref modelState, modelStates);
            File.WriteAllText(String.Concat(SourcePath, FileName), "Zoulou");
            modelSave.save(ref modelState, modelStates);

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
