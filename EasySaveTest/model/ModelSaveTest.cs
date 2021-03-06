using System;
using System.Collections.Generic;
using System.IO;
using EasySaveConsole.model.log;
using EasySaveConsole.model.save;
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
            Directory.CreateDirectory(@TargetPath);
        }

        [Test]
        public void testSaveDirectory()
        {
            ModelSave modelSave = new ModelSaveDifferential(SaveName, SourcePath, TargetPath);
            ModelState modelState = modelSave.toModelState();
            List<ModelState> modelStates = new List<ModelState> { modelState };
            modelSave.SaveFolder(SourcePath, TargetPath, ref modelState, ref modelStates);
            Assert.IsTrue(Directory.Exists(String.Concat(TargetPath)));
            Assert.IsTrue(File.Exists(String.Concat(TargetPath, FileName)));
            Assert.IsTrue(Directory.Exists(String.Concat(TargetPath, subdir)));
            Assert.IsTrue(File.Exists(String.Concat(TargetPath, subdir, FileName)));
        }

        [Test]
        public void testCrypting1()
        {
            ModelSave modelSave = new ModelSaveDifferential(SaveName, SourcePath, TargetPath);
            ModelState modelState = modelSave.toModelState();
            modelSave.CrypeAndCopy(String.Concat(SourcePath, FileName), TargetPath);
            Assert.IsTrue(File.Exists(String.Concat(TargetPath, FileName)));
            Assert.AreNotEqual(File.ReadAllText($"{TargetPath}{FileName}"), "Hello");
        }

        [Test]
        public void testCrypting2()
        {
            File.WriteAllText($"{SourcePath}image.pdf", "Hello");

            ModelSave modelSave = new ModelSaveDifferential(SaveName, SourcePath, TargetPath);
            ModelState modelState = modelSave.toModelState();
            List<ModelState> modelStates = new List<ModelState> { modelState };
            modelSave.SaveAFile($"{SourcePath}image.pdf", TargetPath, ref modelState, ref modelStates);

            Assert.IsTrue(File.Exists(String.Concat(TargetPath, "image.pdf")));
            Assert.AreEqual(File.ReadAllText($"{TargetPath}image.pdf"), "Hello");
        }

        [Test]
        public void testCrypting3()
        {
            ModelSave modelSave = new ModelSaveDifferential(SaveName, SourcePath, TargetPath);
            ModelState modelState = modelSave.toModelState();
            List<ModelState> modelStates = new List<ModelState> { modelState };
            modelSave.SaveAFile($"{SourcePath}{FileName}", TargetPath, ref modelState, ref modelStates);

            Assert.IsTrue(File.Exists(String.Concat(TargetPath, FileName)));
            Assert.AreEqual(File.ReadAllText($"{TargetPath}{FileName}"), "Hello");
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                Directory.Delete(SourcePath, true);
            }
            catch { }
            try
            {
                Directory.Delete(TargetPath, true);
            }
            catch { }
        }
    }
}
