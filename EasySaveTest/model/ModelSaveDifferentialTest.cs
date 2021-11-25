﻿using EasySaveConsole.model;
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
            ModelState modelState = new ModelState(SaveName, SourcePath, TargetPath);
            modelSaveDifferential1.save(ref modelState);
            // Check if archive/save1 has been created
            Assert.IsTrue(Directory.Exists(@String.Concat(TargetPath, SaveName)));
            // Check if testSave.txt has been created
            Assert.IsTrue(File.Exists(@String.Concat(TargetPath, SaveName, "/", FileName)));
        }

        [Test]
        public void TestSaveFile()
        {
            ModelSaveDifferential modelSaveDifferential2 = new ModelSaveDifferential(SaveName, SourcePath, TargetPath);
            ModelState modelState = new ModelState(SaveName, SourcePath, TargetPath);
            modelSaveDifferential2.save(ref modelState);
            string toFind = @String.Concat(TargetPath, SaveName, "/", FileName);
            //Check if testSave.txt is created is in save1 
            Assert.IsTrue(File.Exists(toFind));
        }

        [Test]
        public void TestSaveAgain()
        {
            ModelSaveDifferential modelSaveDifferential2 = new ModelSaveDifferential(SaveName, SourcePath, TargetPath);
            ModelState modelState = new ModelState(SaveName, SourcePath, TargetPath);
            modelSaveDifferential2.save(ref modelState);
            //Check if testSave.txt is created is in save1 
            File.Create(@String.Concat(SourcePath, "testSave2.txt"));
            modelSaveDifferential2.save(ref modelState);
            string filePath = @String.Concat(TargetPath, SaveName, "/", "testSave2.txt");
            Assert.IsTrue(File.Exists(filePath));
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
