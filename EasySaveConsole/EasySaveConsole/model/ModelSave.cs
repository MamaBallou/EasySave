using System;
using System.IO;

namespace EasySaveConsole.model
{
    public abstract class ModelSave
    {
        protected String name;
        protected Uri sourceFile;
        protected Uri targetFile;

        public ModelSave(string name, string sourceFile, string targetFile)
        {
            this.name = name;
            this.sourceFile = new Uri(sourceFile);
            this.targetFile = new Uri(targetFile);
        }

        public virtual void save()
        {
            throw new NotImplementedException();
        }

        public virtual ModelState toModelState()
        {
            throw new NotImplementedException();
        }

        public virtual ModelLog toModelLog()
        {
            throw new NotImplementedException();
        }

        public string getSourcePath()
        {
            return sourceFile.ToString();
        }

        public string getTargetPath()
        {
            return targetFile.ToString();
        }

        public ModelLog toModelLog(string sourcePath, int numSave, string targetFile)
        {
            FileInfo fi = new FileInfo(sourcePath);
            uint fileSize = (uint) fi.Length;

            DateTime timeStamp = DateTime.Now;

            return new ModelLog(fileSize, 3, "Save" + numSave, sourcePath, targetFile, timeStamp);
        }
    }
}
