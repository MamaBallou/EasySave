using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.EasySaveConsole.model;

namespace EasySaveTest.model
{
    internal class ModelSaveTotalTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            ModelSaveTotal1 = new ModelSaveTotal(save1, @"C:\current", @"C:\archive");
            void save()
            {
                bool sourceExists = ModelSaveTotal1.sourceFile.Exists();
                bool targetExists = ModelSaveTotal1.targetFile.Exixts();
                if (sourceExists & targetExists)
                {
                    try
                    {
                        var TotalFiles = Directory.EnumerateFiles(sourceFile);
                        foreach (string currentFile in TotalFiles)
                        {
                            string fileName = currentFile.Substring(sourceFile.Length + 1);
                            Directory.Move(currentFile, Path.Combine(targetFile, fileName));
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else
                {
                    Console.WriteLine("ERROR!!! Please verify directories syntax, they not exist ");
                }
            }



            Assert.Pass();
        }
    }
}

