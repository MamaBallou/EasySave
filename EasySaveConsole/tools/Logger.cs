using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Xml.Serialization;
using EasySaveConsole.model.log;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace EasySaveConsole.tools
{
    /// <summary>
    /// Singleton class to log logs and states.
    /// </summary>
    public sealed class Logger
    {
        /// <summary>
        /// Semaphore that limits the log writing to one file at a time.
        /// </summary>
        private static Semaphore semLog = new Semaphore(1, 1);
        /// <summary>
        /// Semaphore that limits the state log writing to one file at a time.
        /// </summary>
        private static Semaphore semState = new Semaphore(1, 1);
        /// <summary>
        /// Path to the logs.
        /// </summary>
        private static string PATH = @".\logs\";
        /// <summary>
        /// Unique instance of the class.
        /// </summary>
        private static Logger _instance;

        private Logger() { }

        /// <summary>
        /// If there's no instance of LogLogger, this creates one. Otherwise
        /// it returns the existing instance. It makes the instance unique.
        /// </summary>
        /// <returns>Returns the unique instance of LogLogger.</returns>
        public static Logger getInstance()
        {
            if (_instance == null)
            {
                _instance = new Logger();
            }
            return _instance;
        }

        /// <summary>
        /// Write in a log.json and a log.xml file the logs of the save.
        /// </summary>
        /// <param name="data">A ModelLog : a class with all the logs to
        /// write in the log.json and the log.xml file.</param>
        public void write(ModelLog data)
        {
            string jsonString = JsonSerializer.Serialize(data);
            if (!Tool.getInstance().checkExistance(PATH))
            {
                Directory.CreateDirectory(PATH);
            }
            if (!Tool.getInstance().checkExistance(String.Concat(PATH, "json/")))
            {
                Directory.CreateDirectory(String.Concat(PATH, "json/"));
            }
            if (!Tool.getInstance().checkExistance(String.Concat(PATH, "xml/")))
            {
                Directory.CreateDirectory(String.Concat(PATH, "xml/"));
            }

            semLog.WaitOne();
            File.AppendAllText(
                String.Concat(PATH, @"\json\log.json"), jsonString + ",\n");

            XmlSerializer x = new XmlSerializer(typeof(ModelLog));
            var stream = File.AppendText(String.Concat(PATH, @"/xml/log.xml"));
            x.Serialize(stream, data);
            stream.Close();
            semLog.Release();
        }

        /// <summary>
        /// Write in a state.json and a state.xml file the logs of the save. 
        /// </summary>
        /// <param name="data">A ModelState : a class with all the logs to
        /// write in the state.json and the state.xml file.</param>
        public void write(List<ModelState> data)
        {
            string jsonString = JsonSerializer.Serialize(data.ToArray());
            if (!Tool.getInstance().checkExistance(PATH))
            {
                Directory.CreateDirectory(PATH);
            }
            if (!Tool.getInstance().checkExistance(String.Concat(PATH, "json/")))
            {
                Directory.CreateDirectory(String.Concat(PATH, "json/"));
            }
            if (!Tool.getInstance().checkExistance(String.Concat(PATH, "xml/")))
            {
                Directory.CreateDirectory(String.Concat(PATH, "xml/"));
            }
            semState.WaitOne();
            File.WriteAllText(String.Concat(PATH, @"/json/state.json"), jsonString);

            XmlSerializer x = new XmlSerializer(typeof(List<ModelState>));
            var stream = File.CreateText(String.Concat(PATH, @"/xml/state.xml"));
            x.Serialize(stream, data);
            stream.Close();
            semState.Release();
        }
    }
}
