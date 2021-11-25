using System;
using System.IO;
using EasySaveConsole.controller;
using EasySaveConsole.exception;
using EasySaveConsole.logger;

namespace EasySaveConsole.model
{
    /// <summary>
    /// Abstract Class for Save methods.
    /// </summary>
    public abstract class ModelSave
    {
        /// <summary>
        /// Logger attribute.
        /// </summary>
        private Logger logger = Logger.getInstance();
        /// <summary>
        /// Name of the Save.
        /// </summary>
        protected string name;
        /// <summary>
        /// Getter name attribute.
        /// </summary>
        public string Name { get { return name; } }
        /// <summary>
        /// Source path.
        /// </summary>
        protected string sourceFile;
        /// <summary>
        /// Target path.
        /// </summary>
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
            this.logger.write(ControllerSave.modelStates);
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
            this.logger.write(ControllerSave.modelStates);
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
