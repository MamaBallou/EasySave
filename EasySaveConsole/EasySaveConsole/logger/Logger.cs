using System;
using System.Collections.Generic;
using System.Text;

namespace EasySaveConsole.logger
{
    public abstract class Logger
    {
        protected static Logger uniqueInstance;
        protected Uri filePath;

        protected Logger()
        {
            throw new NotImplementedException();
        }

        protected Logger(Uri filePath)
        {
            this.filePath = filePath;
        }

        public static Logger getInstance()
        {
            throw new NotImplementedException();
        }

        public abstract void Write(String data);
    }
}
