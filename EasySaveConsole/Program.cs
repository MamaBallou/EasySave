using System.Collections.Generic;
using EasySaveConsole.controller;
using EasySaveConsole.model;
using EasySaveConsole.view;

namespace EasySaveConsole
{
    /// <summary>
    /// Runtime of the ConsoleApp.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Creating controller
            ControllerSave controller = new ControllerSave(new List<ModelSave>(), new ViewConsole());
            // Calling run()
            controller.run();
        }
    }
}
