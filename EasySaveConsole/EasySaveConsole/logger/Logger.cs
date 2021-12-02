using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using EasySaveConsole.model;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace EasySaveConsole.logger
{
    /// <summary>
    /// Singleton class to log logs and states.
    /// </summary>
    public sealed class Logger
    {
        /// <summary>
        /// Path to the logs.
        /// </summary>
        private static string PATH = @"..\..\..\logs\";
        /// <summary>
        /// Unique instance of the class.
        /// </summary>
        private static Logger _instance;

        private Logger() { }

        /// <summary>
        /// If there's no instance of LogLogger, this creates one. Otherwise
        /// it returns the existing instance. It makes the instance unique.
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
        /// Write in a log.json file the logs of the save.
        /// </summary>
        /// <param name="data">A ModelLog : a class with all the logs to
        /// write in the log.json file</param>
        public void write(ModelLog data)
        {
            string jsonString = JsonSerializer.Serialize(data);
            if (!Tool.getInstance().checkExistance(PATH))
            {
                Directory.CreateDirectory(PATH);
            }
            File.AppendAllText(
                String.Concat(PATH, @"\log.json"), jsonString + ",\n");


            XmlSerializer x = new XmlSerializer(typeof(ModelLog));
            var stream = File.CreateText(String.Concat(PATH, @"/log.xml"));
            x.Serialize(stream, data);
            stream.Close();
        }

        /// <summary>
        /// Write in a log.json file the logs of the save. 
        /// </summary>
        /// <param name="data">A ModelState : a class with all the logs to
        /// write in the state.json file</param>
        public void write(List<ModelState> data)
        {
            string jsonString = JsonSerializer.Serialize(data.ToArray());
            if (!Tool.getInstance().checkExistance(PATH))
            {
                Directory.CreateDirectory(PATH);
            }
            File.WriteAllText(String.Concat(PATH, @"/state.json"), jsonString);

            XmlSerializer x = new XmlSerializer(typeof(List<ModelState>));
            var stream = File.CreateText(String.Concat(PATH, @"/state.xml"));
            x.Serialize(stream, data);
            stream.Close();
        }
    }
}
