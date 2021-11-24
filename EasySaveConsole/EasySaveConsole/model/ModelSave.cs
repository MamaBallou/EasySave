using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace EasySaveConsole.model
{
    public abstract class ModelSave
    {
        protected string name;
        protected string sourceFile;
        protected string targetFile;

        public ModelSave(string name, string sourceFile, string targetFile)
        {
            this.name = name;
            this.sourceFile = sourceFile;
            this.targetFile = targetFile;
        }

        public abstract void save();

        public string getSourcePath()
        {
            return this.sourceFile;
        }

        public string getTargetPath()
        {
            return this.targetFile;
        }

        public ModelState toModelState()
        {
            throw new NotImplementedException();
        }

        public ModelLog toModelLog(string sourcePath, int numSave, string targetFile)
        {
            FileInfo fi = new FileInfo(sourcePath);
            double fileSize = fi.Length;

            DateTime timeStamp = DateTime.Now;

            return new ModelLog(fileSize, 3, "Save" + numSave, sourcePath, targetFile, timeStamp);
        }
    }
}
