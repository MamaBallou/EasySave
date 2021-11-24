using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
