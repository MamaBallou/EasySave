using EasySaveConsole.model;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text.Json;

namespace EasySaveConsole.logger
{
    public sealed class LogLogger
    {
        private static string PATH = @"..\..\..\logs\";
        private static LogLogger _instance;

        private LogLogger() { }

        /// <summary>
        /// If there's no instance of LogLogger, this creates one. Otherwise it returns the existing instance.
        /// That makes the instance unique.
        /// </summary>
        /// <returns>Returns the unique instance of LogLogger</returns>
        public static LogLogger getInstance()
        {
            if (_instance == null)
            {
                _instance = new LogLogger();
            }
            return _instance;
        }

        /// <summary>
        /// Write in a log.json file the logs of the save
        /// </summary>
        /// <param name="data">A ModelLogger : a class with all the logs to write in the log.json file</param>
        public void write(ModelLogger data)
        {
            string jsonString = JsonConvert.SerializeObject(data);
            Console.WriteLine(jsonString);
            if (!Tool.getInstance().checkExistance(PATH))
            {
                Directory.CreateDirectory(PATH);
            }
            File.AppendAllText(String.Concat(PATH, @"\log.json"), jsonString + '\n');
        }
    }
}
