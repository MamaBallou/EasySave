using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using EasySaveConsole.exception;
using EasySaveConsole.model.enums;
using EasySaveConsole.model.log;
using EasySaveConsole.tools;

namespace EasySaveConsole.model.save
{
    /// <summary>
    /// Abstract Class for Save methods.
    /// </summary>
    public abstract class ModelSave
    {
        /// <summary>
        /// Logger attribute.
        /// </summary>
        protected Logger logger = Logger.getInstance();
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
        public string SourcePath => this.sourcePath;
        /// <summary>
        /// Target path.
        /// </summary>
        protected string targetPath;
        public string TargetPath => this.targetPath;

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
        public void save(ref ModelState modelState, List<ModelState> states)
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
            bool InitialExists = tool.checkExistance(@String.Concat(
                targetFolderPath, "initial/"));
            // If not, target is initial.
            if (!InitialExists)
            {
                targetFolderPath = @String.Concat(targetFolderPath, "initial/");
                Directory.CreateDirectory(targetFolderPath);
            }
            // If it does target is a DateTime named directory.
            else
            {
                targetFolderPath = @String.Concat(
                    targetFolderPath,
                    DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss"), "/");
            }

            // get the file attributes for file or directory
            FileAttributes attr = File.GetAttributes(this.sourcePath);

            modelState.initStart();
            this.logger.write(states);
            //detect whether its a directory or file
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                saveFolder(this.sourcePath, targetFolderPath, ref modelState, ref states);
            }
            else
            {
                saveAFile(this.sourcePath, targetFolderPath, ref modelState, ref states);
            }
            modelState._State = EnumState.Finish;
            this.logger.write(states);
        }

        /// <summary>
        /// Check if the file is to be saved or not.
        /// </summary>
        /// <param name="sourceFile">Source file.</param>
        /// <param name="targetPath">Target path to check the existance of the 
        /// souce one.</param>
        /// <returns>Boolean value, true if to be save, false 
        /// otherwise.</returns>
        protected abstract bool checkIfToSave(
            string sourceFile, string targetPath);

        /// <summary>
        /// Callable function for tests only.
        /// </summary>
        /// <param name="sourceFile">Source file.</param>
        /// <param name="targetPath">Target path to check the existance of the 
        /// souce one.</param>
        /// <returns>Boolean value, true if to be save, false 
        /// otherwise.</returns>
        public bool CheckIfToSave(
            string sourceFile,
            string targetPath)
        {
            return checkIfToSave(sourceFile, targetPath);
        }

        /// <summary>
        /// To delete the target folder.
        /// </summary>
        public void delete(ref List<ModelState> states)
        {
            Directory.Delete(String.Concat(this.targetPath, this.name), true);
            this.logger.write(states);
        }

        /// <summary>
        /// Save folder and subFolder files.
        /// </summary>
        /// <param name="folderSourcePath">Source folder path.</param>
        /// <param name="folderTargetPath">Target folder path.</param>
        /// <param name="modelState">Reference to model state.</param>
        protected void saveFolder(string folderSourcePath,
            string folderTargetPath, ref ModelState modelState, ref List<ModelState> states)
        {
            // Check if source directory exists.
            // If not throw Exception.
            if (!Directory.Exists(folderSourcePath))
            {
                return;
            }
            // Check if target directory exists.
            // If not create it.
            if (!Directory.Exists(folderTargetPath))
            {
                Directory.CreateDirectory(folderTargetPath);
            }
            string subdir = "";
            bool inSubDir = String.Equals(
                Path.GetFullPath(folderSourcePath),
                Path.GetFullPath(this.sourcePath),
                StringComparison.InvariantCultureIgnoreCase);
            if (!inSubDir)
            {
                string dirSrc = new DirectoryInfo(this.sourcePath).Name;
                int index = folderSourcePath.LastIndexOf(
                    dirSrc,
                    StringComparison.InvariantCultureIgnoreCase)
                    + dirSrc.Length + 1;
                subdir = String.Concat(folderSourcePath.Substring(index), "/");
            }
            // For each file in the source directory, save it in the target
            // directory if to be saved.
            foreach (string currentFile in
                Directory.EnumerateFiles(folderSourcePath))
            {
                if (checkIfToSave(currentFile, String.Concat(this.targetPath,
                    this.name, "/initial/", subdir)))
                {
                    saveAFile(currentFile, folderTargetPath, ref modelState, ref states);
                }
            }
            // For each directory in source directory, call the function itself
            // to crawl it.
            foreach (string currentDirectory in
                Directory.EnumerateDirectories(folderSourcePath))
            {
                saveFolder(currentDirectory, String.Concat(
                    folderTargetPath,
                    new DirectoryInfo(currentDirectory).Name,
                    "/"), ref modelState, ref states);
            }
        }

        /// <summary>
        /// Callable method used in tests only.
        /// </summary>
        /// <param name="folderSourcePath">Source folder path.</param>
        /// <param name="folderTargetPath">Target folder path.</param>
        /// <param name="modelState">Reference to model state.</param>
        public void SaveFolder(string folderSourcePath,
            string folderTargetPath, ref ModelState modelState, ref List<ModelState> states)
        {
            saveFolder(folderSourcePath, folderTargetPath, ref modelState, ref states);
        }

        /// <summary>
        /// Callable method used in tests only.
        /// </summary>
        /// <param name="currentFile">Path of file to copy.</param>
        /// <param name="folderTargetPath">
        /// Path of directory where to copy.
        /// </param>
        /// <param name="modelState">Reference to model state.</param>
        public void SaveAFile(string currentFile, string folderTargetPath,
            ref ModelState modelState, ref List<ModelState> states)
        {
            saveAFile(currentFile, folderTargetPath, ref modelState, ref states);
        }

        /// <summary>
        /// Copy file to directory.
        /// </summary>
        /// <param name="currentFile">Path of file to copy.</param>
        /// <param name="folderTargetPath">
        /// Path of directory where to copy.
        /// </param>
        /// <param name="modelState">Reference to model state.</param>
        protected void saveAFile(string currentFile, string folderTargetPath,
            ref ModelState modelState, ref List<ModelState> states)
        {
            string destFilePath = String.Concat(folderTargetPath,
                            Path.GetFileName(currentFile));
            DateTime start = DateTime.Now;
            if (Path.GetExtension(currentFile) == ".img")
            {
                crypAndCopy(currentFile, folderTargetPath);
            }
            else
            {
                File.Copy(currentFile, destFilePath);
            }
            TimeSpan span = DateTime.Now - start;
            this.logger.write(new ModelLog(
                this.name,
                currentFile,
                destFilePath,
                span.TotalMilliseconds));
            modelState.NbFilesLeftToDo--;
            modelState.calcProg();
            this.logger.write(states);
        }

        /// <summary>
        /// Callable method used in tests only.
        /// </summary>
        /// <param name="currentFile">Source file.</param>
        /// <param name="folderTargetPath">Target folder.</param>
        public void CrypeAndCopy(string currentFile, string folderTargetPath)
        {
            crypAndCopy(currentFile, folderTargetPath);
        }

        /// <summary>
        /// Crypt and copy a file in a folder.
        /// </summary>
        /// <param name="currentFile">Source file.</param>
        /// <param name="folderTargetPath">Target folder.</param>
        private void crypAndCopy(string currentFile, string folderTargetPath)
        {
            Process process = new Process();
            process.StartInfo.FileName = "Cryptosoft/CryptoSoft.exe";
            string s = Path.GetFullPath(currentFile);
            string ss = Path.GetFullPath(folderTargetPath);
            process.StartInfo.Arguments = $"/e \"{s}\" \"{ss}{Path.GetFileName(s)}\" 1234567891234567";
            process.Start();
            process.WaitForExit();
            File.SetLastWriteTime($"{ss}{Path.GetFileName(s)}", File.GetLastWriteTime(currentFile));
        }

        public abstract ModelState toModelState();
    }
}
