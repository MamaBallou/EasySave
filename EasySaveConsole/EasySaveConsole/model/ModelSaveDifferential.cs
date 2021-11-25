using System;
using System.IO;
using EasySaveConsole.controller;
using EasySaveConsole.logger;

namespace EasySaveConsole.model
{
    /// <summary>
    /// Differential save class, extends from ModelSave.
    /// </summary>
    public class ModelSaveDifferential : ModelSave
    {
        /// <summary>
        /// Logger object.
        /// </summary>
        private Logger logger = Logger.getInstance();

        /// <summary>
        /// Contructor.
        /// </summary>
        /// <param name="name">Save name.</param>
        /// <param name="sourceFile">Source path.</param>
        /// <param name="targetFile">Target path.</param>
        public ModelSaveDifferential(string name, string sourceFile,
            string targetFile) : base(name, sourceFile, targetFile)
        { }

        /// <summary>
        /// Method to save one file if does not exists in target.
        /// </summary>
        /// <param name="modelState"></param>
        /// <param name="currentFile"></param>
        protected override void saveAFile(ref ModelState modelState,
            string currentFile)
        {
            string fileName = Path.GetFileName(currentFile.ToString());
            string currentTarget = String.Concat(this.targetFile, this.name,
                "/", fileName);
            if (!File.Exists(currentTarget))
            {
                DateTime start = DateTime.Now;
                bool success = false;
                do
                {
                    try
                    {
                        File.Copy(currentFile, currentTarget);
                        success = true;
                    }
                    catch { }
                } while (!success);
                TimeSpan span = DateTime.Now - start;
                ModelLog modelLog = new ModelLog(this.name, currentFile,
                    currentTarget, span.TotalMilliseconds);
                this.logger.write(modelLog);
            }
            modelState.NbFilesLeftToDo--;
            modelState.calcProg();
            this.logger.write(ControllerSave.modelStates);
        }
    }
}
