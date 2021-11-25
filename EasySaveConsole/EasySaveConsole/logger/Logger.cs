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
            throw new NotImplementedException();
        }

        public static Logger getInstance()
        {
            throw new NotImplementedException();
        }

        public abstract void Write(String data);
    }
}
