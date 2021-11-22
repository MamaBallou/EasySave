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
            this.name = "";
            this.sourceFile = new Uri("");
            this.targetFile = new Uri("");
        }

        public abstract void save();
    }
}
