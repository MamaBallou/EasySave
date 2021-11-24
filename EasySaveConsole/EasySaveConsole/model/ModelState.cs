using System;
using System.Collections.Generic;
using System.Text;

namespace EasySaveConsole.model
{
    public class ModelState : ModelLogger
    {
        private string state;
        private int totalFilesToCopy;
        private int totalFileSize;
        private int nbFilesLeftToDo;
        private double progression;

        public ModelState(string saveName, string sourceFile, string targetFile, DateTime timeStamp) : base(saveName, sourceFile, targetFile, timeStamp)
        {
        }
    }
}
