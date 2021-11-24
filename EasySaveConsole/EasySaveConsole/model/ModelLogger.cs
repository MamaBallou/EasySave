using System;

namespace EasySaveConsole.model
{
    public abstract class ModelLogger
    {
        protected string saveName;
        protected string sourceFile;
        protected string targetFile;
        protected DateTime timeStamp;

        public ModelLogger(string saveName, string sourceFile,
            string targetFile)
        {
            this.saveName = saveName;
            this.sourceFile = sourceFile;
            this.targetFile = targetFile;
            timeStamp = DateTime.Now;
        }
    }
}
