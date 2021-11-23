using System;
using System.Collections.Generic;
using System.Text;

namespace EasySaveConsole.model
{
    class ModelState : ModelLogger
    {
        private string state;
        private int totalFilesToCopy;
        private int totalFileSize;
        private int nbFilesLeftToDo;
        private double progression;
    }
}
