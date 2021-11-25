using System;
using System.IO;
using EasySaveConsole.controller;
using EasySaveConsole.exception;
using EasySaveConsole.logger;

namespace EasySaveConsole.model
{
    public abstract class ModelSave
    {
        private Logger log = Logger.getInstance();
        private Logger stateLogger = Logger.getInstance();
        protected string name;
        public string Name => this.name;
        protected string sourceFile;
        protected string targetFile;

        public ModelSave(string name, string sourceFile, string targetFile)
        {
            this.name = name;
            this.sourceFile = sourceFile;
            this.targetFile = targetFile;
        }

        public void save(ref ModelState modelState)
        {
            string targetFolderPath = @String.Concat(this.targetFile, this.name, "/");
            //define source and target path in bool
            Tool tool = Tool.getInstance();
            bool sourceExists = tool.checkExistance(this.sourceFile);
            bool targetExists = tool.checkExistance(targetFolderPath);


            if (!sourceExists)
            {
                //Exception if source doesn't exist do exception
                throw new PathNotFoundException();
            }
            if (!targetExists)
            {
                //Exception if source doesn't exist do exception
                Directory.CreateDirectory(targetFolderPath);
            }

            // get the file attributes for file or directory
            FileAttributes attr = File.GetAttributes(this.sourceFile);

            modelState._State = State.OnGoing;
            this.stateLogger.write(ControllerSave.modelStates);
            //detect whether its a directory or file
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                var TotalFiles = Directory.EnumerateFiles(this.sourceFile);
                //for each file in Directory, copy this.
                foreach (string currentFile in TotalFiles)
                {
                    saveAFile(ref modelState, currentFile);
                }
            }
            else
            {
                saveAFile(ref modelState, this.sourceFile);
            }
            modelState._State = State.Finish;
            this.stateLogger.write(ControllerSave.modelStates);
        }

        protected virtual void saveAFile(ref ModelState modelState,
            string currentFile)
        {
            throw new NotImplementedException();
        }

        public void delete()
        {
            Directory.Delete(String.Concat(this.targetFile, this.name), true);
        }
    }
}
