
using EasySaveConsole.controller;
using EasySaveConsole.exception;
using EasySaveConsole.logger;
using System;
using System.IO;

namespace EasySaveConsole.model
{
    public class ModelSaveTotal : ModelSave
    {
        private LogLogger log = LogLogger.getInstance();
        private StateLogger stateLogger = StateLogger.getInstance();
        public ModelSaveTotal(string name, string sourceFile, string targetFile) : base(name, sourceFile, targetFile) { }
        public override void save(ref ModelState modelState)
        {
            //define source and target path in bool
            Tool tool = Tool.getInstance();
            bool sourceExists = tool.checkExistance(sourceFile);
            bool targetExists = tool.checkExistance(targetFile);

            if (!sourceExists)
            {
                //Exception if source doesn't exist do exception
                throw new PathNotFoundException();
            }

            // get the file attributes for file or directory
            FileAttributes attr = File.GetAttributes(sourceFile);

            modelState.State = State.OnGoing;
            stateLogger.write(ControllerSave.modelStates);
            //detect whether its a directory or file
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                var TotalFiles = Directory.EnumerateFiles(sourceFile);
                //for each file in Directory, copy this.
                foreach (string currentFile in TotalFiles)
                {
                    saveAFile(ref modelState, currentFile);
                }
            }
            else
            {
                saveAFile(ref modelState, sourceFile);
            }
            modelState.State = State.Finish;
            stateLogger.write(ControllerSave.modelStates);
        }

        private void saveAFile(ref ModelState modelState, string currentFile)
        {
            string fileName = Path.GetFileName(currentFile.ToString());
            string currentTarget = String.Concat(targetFile, "/", fileName);
            DateTime start = DateTime.Now;
            File.Copy(currentFile, currentTarget, true);
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
