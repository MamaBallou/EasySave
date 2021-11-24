using System;
using System.Collections.Generic;
using System.Text;
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
            throw new NotImplementedException();
        }

        public abstract void save()
        {
            throw new NotImplementedException();
        }

        public ModelState toModelState()
        {
            throw new NotImplementedException();
        }

        public ModelLog toModelLog()
        {
            throw new NotImplementedException();
        }
    }
}
