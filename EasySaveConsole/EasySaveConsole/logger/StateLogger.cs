using EasySaveConsole.model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace EasySaveConsole.logger
{
    public class StateLogger
    {
        private static string PATH = "../../logs";
        private static StateLogger _instance;

        private StateLogger() { }

        public static StateLogger getInstance()
        {
            if (_instance == null)
            {
                _instance = new StateLogger();
            }
            return _instance;
        }

        public void write(List<ModelState> data)
        {
            string jsonString = JsonSerializer.Serialize(data.ToArray());
            if (!Tool.getInstance().checkExistance(PATH))
            {
                Directory.CreateDirectory(PATH);
            }
            File.WriteAllText(String.Concat(PATH, @"/log.json"), jsonString);
        }
    }
}
