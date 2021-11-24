using System;
using System.Collections.Generic;
using System.Text;

namespace EasySaveConsole.model
{
    class ModelSaveTotal : ModelSave
    {
        public ModelSaveTotal(string name, string sourceFile, string targetFile) : base(name, sourceFile, targetFile)
        {
        }

        public override void save()
        {
            LogLogger ll = LogLogger.getInstance();
        }
    }
}
