using System;
using System.Collections.Generic;
using System.Text;

namespace EasySaveConsole.model
{
     public abstract class ModelLogger
    {
        protected String saveName;
        protected String sourceFile;
        protected String targetFile;
        protected DateTime timeStamp;
    }
}
