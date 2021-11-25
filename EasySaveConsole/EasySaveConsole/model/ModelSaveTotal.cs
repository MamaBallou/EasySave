
using System;
using System.IO;
using EasySaveConsole.controller;
using EasySaveConsole.logger;

namespace EasySaveConsole.model
{
    /// <summary>
    /// Total save class, extends from ModelSave.
    /// </summary>
    public class ModelSaveTotal : ModelSave
    {
        /// <summary>
        /// Logger object.
        /// </summary>
        private Logger logger = Logger.getInstance();

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">Save name.</param>
        /// <param name="sourceFile">Source path.</param>
        /// <param name="targetFile">Target path.</param>
        public ModelSaveTotal(string name, string sourceFile, string targetFile)
            : base(name, sourceFile, targetFile) { }

        /// <summary>
        /// Save a file and overrite it if exists.
        /// </summary>
        /// <param name="modelState">ModelState attached to the save.</param>
        /// <param name="currentFile">Path of the file to copy.</param>
        protected override void saveAFile(ref ModelState modelState,
            string currentFile)
        {
            string fileName = Path.GetFileName(currentFile.ToString());
            string currentTarget = String.Concat(this.targetFile, this.name, 
                "/", fileName);
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
            ModelLog modelLog = new ModelLog(this.name, currentFile,
                currentTarget, span.TotalMilliseconds);
            this.logger.write(modelLog);
            modelState.NbFilesLeftToDo--;
            modelState.calcProg();
            this.logger.write(ControllerSave.modelStates);
        }
    }
}
