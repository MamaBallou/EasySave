using System;
using System.Collections.Generic;
using System.IO;
using EasySaveConsole.controller;
using EasySaveConsole.logger;

namespace EasySaveConsole.model
{
    public class ModelSaveDifferential : ModelSave
    {
        private Logger logger = Logger.getInstance();
        public ModelSaveDifferential(string name, string sourceFile,
            string targetFile) : base(name, sourceFile, targetFile)
        { }

        protected override void saveAFile(ref ModelState modelState,
            string currentFile)
        {
            string fileName = Path.GetFileName(currentFile.ToString());
            string currentTarget = String.Concat(this.targetFile, this.name,
                "/", fileName);
            if (!File.Exists(currentTarget))
            {
                DateTime start = DateTime.Now;
                bool success = false;
                do
                {
                    try
                    {
                        File.Copy(currentFile, currentTarget);
                        success = true;
                    }
                    catch { }
                } while (!success);
                TimeSpan span = DateTime.Now - start;
                ModelLog modelLog = new ModelLog(this.name, currentFile,
                    currentTarget, span.TotalMilliseconds);
                this.logger.write(modelLog);
            }
            modelState.NbFilesLeftToDo--;
            modelState.calcProg();
            this.logger.write(ControllerSave.modelStates);
        }

        public static implicit operator List<object>(ModelSaveDifferential v)
        {
            throw new NotImplementedException();
        }
    }
}
