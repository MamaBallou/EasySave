using System;
using System.Collections.Generic;
using System.Text;

namespace EasySaveConsole.logger
{
    public class LogLogger : Logger
    {
        private static LogLogger instance;

        private LogLogger() { }

        public static LogLogger getInstance()
        {
            return LogLogger.instance;
        }

        public override void Write(string data)
        {
            throw new NotImplementedException();
        }
    }
}
