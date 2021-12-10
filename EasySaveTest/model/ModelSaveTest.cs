using System;
using System.IO;
using System.Text;
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
            Directory.CreateDirectory(@TargetPath);
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

        [Test]
        public void testCrypting1()
        {
            ModelState modelState = new ModelState(SaveName, SourcePath, TargetPath);
            ModelSave modelSave = new ModelSaveDifferential(SaveName, SourcePath, TargetPath);
            modelSave.CrypeAndCopy(String.Concat(SourcePath, FileName), TargetPath);
            Assert.IsTrue(File.Exists(String.Concat(TargetPath, FileName)));
            Assert.AreEqual(File.ReadAllText($"{TargetPath}{FileName}"), "{\u0010�#n\u000e\0\bTs7�]\u0001+");
        }

        [Test]
        public void testCrypting2()
        {
            File.WriteAllText($"{SourcePath}image.img", "Hello");

            ModelState modelState = new ModelState(SaveName, SourcePath, TargetPath);
            ModelSave modelSave = new ModelSaveDifferential(SaveName, SourcePath, TargetPath);
            modelSave.SaveAFile($"{SourcePath}image.img", TargetPath, ref modelState);

            Assert.IsTrue(File.Exists(String.Concat(TargetPath, "image.img")));
            Assert.AreEqual(File.ReadAllText($"{TargetPath}image.img"), "{\u0010�#n\u000e\0\bTs7�]\u0001+");
        }

        [Test]
        public void testCrypting3()
        {
            ModelState modelState = new ModelState(SaveName, SourcePath, TargetPath);
            ModelSave modelSave = new ModelSaveDifferential(SaveName, SourcePath, TargetPath);
            modelSave.SaveAFile($"{SourcePath}{FileName}", TargetPath, ref modelState);

            Assert.IsTrue(File.Exists(String.Concat(TargetPath, FileName)));
            Assert.AreEqual(File.ReadAllText($"{TargetPath}{FileName}"), "Hello");
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
