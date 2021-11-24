using EasySaveConsole.controller;
using EasySaveConsole.exception;
using EasySaveConsole.logger;
using System;
using System.IO;

namespace EasySaveConsole.model
{
    public class ModelSaveDifferential : ModelSave
    {
        private LogLogger log = LogLogger.getInstance();
        private StateLogger stateLogger = StateLogger.getInstance();
        public ModelSaveDifferential(string name, string sourceFile,
            string targetFile) : base(name, sourceFile, targetFile)
        { }

        protected override void saveAFile(ref ModelState modelState, string currentFile)
        {
            string fileName = Path.GetFileName(currentFile.ToString());
            string currentTarget = String.Concat(targetFile, "/", fileName);
            DateTime start = DateTime.Now;
            File.Copy(currentFile, currentTarget);
            TimeSpan span = DateTime.Now - start;
            ModelLog modelLog = new ModelLog(name, currentFile, currentTarget,
                span.TotalMilliseconds);
            log.write(modelLog);
            modelState.TotalFilesToCopy--;
            modelState.calcProg();
            stateLogger.write(ControllerSave.modelStates);
        }
    }
}
