using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using EasySaveConsole.model;

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

        public String getPATH()
        {
            return LogLogger.PATH;
        }

        public void write(ModelSave[] data)
        {
            int i = 0;

            foreach (ModelSave ms in data)
            {
                if(ms.GetType().Name == "ModelSaveTotal")
                {
                    foreach (string file in Directory.GetFiles(ms.getSourcePath()))
                    {
                        string[] lines = File.ReadAllLinesAsync(file).Result;

                        File.WriteAllLinesAsync(ms.getTargetPath() + Path.GetFileName(file), lines);

                        ModelLog ml = ms.toModelLog(ms.getSourcePath() + Path.GetFileName(file), i, ms.getTargetPath() + Path.GetFileName(file));
                        File.AppendAllLinesAsync(PATH + "log" + i + ".txt", ml.toString());
                    }
                }
                else if(ms.GetType().Name == "ModelSaveDifferential")
                {
                    Console.WriteLine("ModelSaveDifferential");
                }
                i++;
            }
        }
    }
}
