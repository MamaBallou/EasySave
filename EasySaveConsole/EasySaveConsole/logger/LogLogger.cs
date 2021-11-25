﻿using EasySaveConsole.model;
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

        public static LogLogger getInstance()
        {
            if (_instance == null)
            {
                _instance = new LogLogger();
            }
            return _instance;
        }

        public void write(ModelLogger data)
        {
            string jsonString = JsonSerializer.Serialize(data);
            if (!Tool.getInstance().checkExistance(PATH))
            {
                Directory.CreateDirectory(PATH);
            }
            File.AppendAllText(String.Concat(PATH, @"\log.json"), jsonString);
        }
    }
}
