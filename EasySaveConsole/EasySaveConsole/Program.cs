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
            controller.run();
        }
    }
}
