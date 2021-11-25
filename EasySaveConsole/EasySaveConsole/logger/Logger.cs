using EasySaveConsole.model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace EasySaveConsole.logger
{
    public sealed class Logger
    {
        private static string PATH = @"..\..\..\logs\";
        private static Logger _instance;

        private Logger() { }

        /// <summary>
        /// If there's no instance of LogLogger, this creates one. Otherwise it returns the existing instance.
        /// That makes the instance unique.
        /// </summary>
        /// <returns>Returns the unique instance of LogLogger</returns>
        public static Logger getInstance()
        {
            if (_instance == null)
            {
                _instance = new Logger();
            }
            return _instance;
        }

        /// <summary>
        /// Write in a log.json file the logs of the save
        /// </summary>
        /// <param name="data">A ModelLogger : a class with all the logs to write in the log.json file</param>
        public void write(ModelLogger data)
        {
            string jsonString = JsonSerializer.Serialize(data);
            if (!Tool.getInstance().checkExistance(PATH))
            {
                Directory.CreateDirectory(PATH);
            }
            File.AppendAllText(String.Concat(PATH, @"\log.json"), jsonString + ",\n");
        }

        public void write(List<ModelState> data)
        {
            string jsonString = JsonSerializer.Serialize(data.ToArray());
            if (!Tool.getInstance().checkExistance(PATH))
            {
                Directory.CreateDirectory(PATH);
            }
            File.WriteAllText(String.Concat(PATH, @"/state.json"), jsonString);
        }
    }
}
