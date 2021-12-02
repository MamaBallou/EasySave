using System;
using System.IO;
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
        /// <returns>True if to be save, false if not.</returns>
        protected override bool checkIfToSave(string sourceFile,
            string targetPath)
        {
            // Get name of source file.
            string fileName = Path.GetFileName(sourceFile);
            // Check if file exists in target.
            if (File.Exists(@String.Concat(targetPath, fileName)))
            {
                // Get Last write dates of source and target files.
                DateTime sourceLastWrite =
                    File.GetLastWriteTimeUtc(sourceFile);
                DateTime targetLastWrite = File.GetLastWriteTimeUtc(
                    string.Concat(targetPath, fileName));
                // Check if source and target date are the same.
                // If they are, return false.
                if (DateTime.Equals(sourceLastWrite, targetLastWrite))
                {
                    return false;
                }
            }
            // Else true.
            return true;
        }
    }
}
