using System;
using System.Collections.Generic;
using System.Text;

namespace EasySaveConsole.model
{
    class ModelSaveDifferential : ModelSave
    {
        public ModelSaveDifferential(string name, string sourceFile, string targetFile) : base(name, sourceFile, targetFile)
        {
        }

        public override void save()
        {
            throw new NotImplementedException();
        }
    }
}
