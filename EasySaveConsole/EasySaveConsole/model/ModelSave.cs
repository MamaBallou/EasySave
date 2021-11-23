using System;
using System.Collections.Generic;
using System.Text;

namespace EasySaveConsole.model
{
    abstract class ModelSave
    {
        protected String name;
        protected Uri sourceFile;
        protected Uri targetFile;

        public ModelSave()
        {
            throw new NotImplementedException();
        }

        public abstract void save();
    }
}
