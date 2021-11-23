using System;
using System.Collections.Generic;
using System.Text;

namespace EasySaveConsole.logger
{
    public sealed class LogLogger
    {
        private static string PATH = "../../logs";
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

        public void write(string data)
        {
            throw new NotImplementedException();
        }
    }
}
