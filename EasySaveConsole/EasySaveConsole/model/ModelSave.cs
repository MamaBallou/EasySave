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
        protected string sourcePath;
        /// <summary>
        /// Target path.
        /// </summary>
        protected string targetPath;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">Save name.</param>
        /// <param name="sourcePath">Source path.</param>
        /// <param name="targetFile">Target path.</param>
        public ModelSave(string name, string sourcePath, string targetFile)
        {
            this.name = name;
            this.sourcePath = sourcePath;
            this.targetPath = targetFile;
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
                @String.Concat(this.targetPath, this.name, "/");
            //define source and target path in bool
            Tool tool = Tool.getInstance();
            bool sourceExists = tool.checkExistance(this.sourcePath);
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

            // Check if initial folder exists.
            bool InitialExists = tool.checkExistance(@String.Concat(targetFolderPath, "initial/"));
            // If not, target is initial.
            if (!InitialExists)
            {
                targetFolderPath = @String.Concat(targetFolderPath, "initial/");
                Directory.CreateDirectory(targetFolderPath);
            }
            // If it does target is a DateTime named directory.
            else
            {
                targetFolderPath = @String.Concat(targetFolderPath, DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss"), "/");
            }

            // get the file attributes for file or directory
            FileAttributes attr = File.GetAttributes(this.sourcePath);

            modelState._State = State.OnGoing;
            this.logger.write(ControllerSave.modelStates);
            //detect whether its a directory or file
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                saveFolder(sourcePath, targetFolderPath, ref modelState);
            }
            else
            {
                saveAFile(sourcePath, targetFolderPath, ref modelState);
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
        public virtual bool checkIfToSave(string sourceFile, string targetPath)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// To delete the target folder.
        /// </summary>
        public void delete()
        {
            Directory.Delete(String.Concat(this.targetPath, this.name), true);
        }
        
        public void saveFolder(string folderSourcePath, string folderTargetPath, ref ModelState modelState)
        {
            if (!Directory.Exists(folderSourcePath))
            {
                return;
            }
            if(!Directory.Exists(folderTargetPath))
            {
                Directory.CreateDirectory(folderTargetPath);
            }
            foreach (string currentFile in Directory.EnumerateFiles(folderSourcePath))
            {
                if (checkIfToSave(currentFile, String.Concat(targetPath, name, "/initial/")))
                {
                    saveAFile(currentFile, folderTargetPath, ref modelState);
                }
            }
            foreach (string currentDirectory in Directory.EnumerateDirectories(folderSourcePath))
            {
                saveFolder(currentDirectory, String.Concat(folderTargetPath, new DirectoryInfo(currentDirectory).Name, "/"), ref modelState);
            }
        }

        protected void saveAFile(string currentFile, string folderTargetPath, ref ModelState modelState)
        {
            File.Copy(currentFile, String.Concat(folderTargetPath, Path.GetFileName(currentFile)));
        }
    }
}
