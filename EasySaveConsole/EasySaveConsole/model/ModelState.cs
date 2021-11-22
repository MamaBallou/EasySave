using System;
using System.Collections.Generic;
using System.Text;

namespace EasySaveConsole.model
{
    class ModelState : ModelLogger
    {
        private String state;
        private int totalFilesToCopy;
        private double totalFileSize;
        private int nbFilesLeftToDo;
        private int progression;
    }
}
