using System;
using System.Collections.Generic;
using System.Text;

namespace EasySaveConsole.logger
{
    abstract class Logger
    {
        protected static Logger uniqueInstance;
        private Uri filePath;

        protected Logger()
        {
        }

        public static Logger getInstance()
        {
            return null;
        }

        public abstract void Write(String data);
    }
}
