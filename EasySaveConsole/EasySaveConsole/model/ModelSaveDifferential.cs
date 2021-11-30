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
        /// Check if the file is to be saved depending on its save type.
        /// </summary>
        /// <param name="sourceFile">Source file path</param>
        /// <param name="targetPath">Target directory path</param>
        public override bool checkIfToSave(string sourceFile, 
            string targetPath)
        {
            string fileName = Path.GetFileName(sourceFile);
            string currentTarget = String.Concat(targetPath, "/", fileName);

            if (File.Exists(@String.Concat(targetPath, fileName)))
            {
                DateTime sourceLastWrite = File.GetLastWriteTimeUtc(sourceFile);
                DateTime targetLastWrite = File.GetLastWriteTimeUtc(string.Concat(targetPath, fileName));
                if (DateTime.Equals(sourceLastWrite, targetLastWrite))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
