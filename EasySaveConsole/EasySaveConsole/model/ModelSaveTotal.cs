
using System;
using System.IO;
using EasySaveConsole.controller;
using EasySaveConsole.logger;

namespace EasySaveConsole.model
{
    public class ModelSaveTotal : ModelSave
    {
        private Logger logger = Logger.getInstance();
        public ModelSaveTotal(string name, string sourceFile, string targetFile)
            : base(name, sourceFile, targetFile) { }
        protected override void saveAFile(ref ModelState modelState,
            string currentFile)
        {
            string fileName = Path.GetFileName(currentFile.ToString());
            string currentTarget = String.Concat(this.targetFile, this.name, "/",
                fileName);
            DateTime start = DateTime.Now;
            bool success = false;
            do
            {
                try
                {
                    File.Copy(currentFile, currentTarget, true);
                    success = true;
                }
                catch { }
            } while (!success);
            TimeSpan span = DateTime.Now - start;
            ModelLog modelLog = new ModelLog(this.name, currentFile, currentTarget,
                span.TotalMilliseconds);
            this.logger.write(modelLog);
            modelState.NbFilesLeftToDo--;
            modelState.calcProg();
            this.logger.write(ControllerSave.modelStates);
        }
    }
}
