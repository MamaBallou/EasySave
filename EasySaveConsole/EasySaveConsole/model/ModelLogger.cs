using System;
using System.Collections.Generic;
using System.Text;

namespace EasySaveConsole.model
{
    public abstract class ModelLogger
    {
        protected String saveName;
        protected String sourceFile;
        protected String targetFile;
        protected DateTime timeStamp;

        public ModelLogger(String saveName, String sourceFile, String targetFile, DateTime timeStamp)
        {
            this.saveName = saveName;
            this.sourceFile = sourceFile;
            this.targetFile = targetFile;
            this.timeStamp = timeStamp;
        }
    }
}
