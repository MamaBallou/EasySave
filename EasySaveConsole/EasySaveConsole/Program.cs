using EasySaveConsole.controller;
using EasySaveConsole.logger;
using EasySaveConsole.model;
using EasySaveConsole.view;
using System;
using System.Collections.Generic;

namespace EasySaveConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            /*
            ControllerSave controller = new ControllerSave(new List<ModelSave>(), new ViewConsole());
            controller.chooseLanguage();
            */

            string saveName = "Save1";
            string sourcePath = @"..\..\..\MOCKS\FilesToCopy\fileA.txt";
            string targetPath = @"..\..\..\MOCKS\FilesCopied\";
            ModelLog ml = new ModelLog(saveName, sourcePath, targetPath, 3.54);

            LogLogger ll = LogLogger.getInstance();
            ll.write(ml);
        }
    }
}
