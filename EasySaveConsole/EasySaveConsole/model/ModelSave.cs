using System;

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

        public virtual void save(ref ModelState modelState)
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
            return sourceFile;
        }

        public string getTargetPath()
        {
            return targetFile;
        }
    }
}
