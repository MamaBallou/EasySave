using System;
using System.IO;
using EasySaveConsole.model;
using NUnit.Framework;

namespace EasySaveTest.model
{
    internal class ModelSaveTest
    {
        const string SourcePath = "../../../test_files/MyDir/";
        const string subdir = "subdir/";
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
            // Create SubDir
            Directory.CreateDirectory(@String.Concat(SourcePath, subdir));
            File.WriteAllText(String.Concat(@String.Concat(SourcePath, subdir, "/", FileName)), "World");
            // Delete target path to be sure that save function create target path
            try
            {
                Directory.Delete(TargetPath, true);
            }
            catch { }
        }

        [Test]
        public void testSaveDirectory()
        {
            ModelState modelState = new ModelState(SaveName, SourcePath, TargetPath);
            ModelSave modelSave = new ModelSaveDifferential(SaveName, SourcePath, TargetPath);
            modelSave.SaveFolder(SourcePath, TargetPath, ref modelState);
            Assert.IsTrue(Directory.Exists(String.Concat(TargetPath)));
            Assert.IsTrue(File.Exists(String.Concat(TargetPath, FileName)));
            Assert.IsTrue(Directory.Exists(String.Concat(TargetPath, subdir)));
            Assert.IsTrue(File.Exists(String.Concat(TargetPath, subdir, FileName)));
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                Directory.Delete(SourcePath, true);
            } catch { }
            try
            {
                Directory.Delete(TargetPath, true);
            }catch { }
        }
    }
}
