using EasySaveConsole.controller;
using EasySaveConsole.exception;
using EasySaveConsole.logger;
using System;
using System.IO;

namespace EasySaveConsole.model
{
    public abstract class ModelSave
    {
        private LogLogger log = LogLogger.getInstance();
        private StateLogger stateLogger = StateLogger.getInstance();
        protected string name;
        protected string sourceFile;
        protected string targetFile;

        public ModelSave(string name, string sourceFile, string targetFile)
        {
            this.name = name;
            this.sourceFile = sourceFile;
            this.targetFile = targetFile;
        }

        protected void save(ref ModelState modelState)
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

        protected virtual void saveAFile(ref ModelState modelState, string currentFile)
        {
            throw new NotImplementedException();
        }
    }
}
