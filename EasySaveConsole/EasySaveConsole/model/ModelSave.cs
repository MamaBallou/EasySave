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
        public string Name => this.name;
        /// <summary>
        /// Source path.
        /// </summary>
        protected string sourceFile;
        /// <summary>
        /// Target path.
        /// </summary>
        protected string targetFile;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">Save name.</param>
        /// <param name="sourceFile">Source path.</param>
        /// <param name="targetFile">Target path.</param>
        public ModelSave(string name, string sourceFile, string targetFile)
        {
            this.name = name;
            this.sourceFile = sourceFile;
            this.targetFile = targetFile;
        }

        /// <summary>
        /// Save method copy files from source to target.
        /// </summary>
        /// <param name="modelState">Model state attached to the save.</param>
        /// <exception cref="PathNotFoundException">Thrown when source path not
        /// found.</exception>
        public void save(ref ModelState modelState)
        {
            string targetFolderPath =
                @String.Concat(this.targetFile, this.name, "/");
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

        /// <summary>
        /// Method to save one file passed in parameter.
        /// </summary>
        /// <param name="modelState">Model state attached to the save.</param>
        /// <param name="currentFile">File to save.</param>
        /// <exception cref="NotImplementedException">Thrown if file not
        /// found.</exception>
        protected virtual void saveAFile(ref ModelState modelState,
            string currentFile)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// To delete the target folder.
        /// </summary>
        public void delete()
        {
            Directory.Delete(String.Concat(this.targetFile, this.name), true);
        }
    }
}
