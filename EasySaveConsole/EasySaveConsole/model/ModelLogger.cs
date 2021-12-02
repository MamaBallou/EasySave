using System;

namespace EasySaveConsole.model
{
    /// <summary>
    /// Abstract  model class for log & state.
    /// </summary>
    public abstract class ModelLogger
    {
        /// <summary>
        /// The name of the save.
        /// </summary>
        protected string saveName;
        /// <summary>
        /// Getter and Setter of saveName.
        /// </summary>
        public string SaveName
        {
            get => this.saveName;
            set => this.saveName = value;
        }
        /// <summary>
        /// The path to the source file.
        /// </summary>
        protected string sourceFile;
        /// <summary>
        /// Getter and Setter of sourceFile.
        /// </summary>
        public string SourceFile
        {
            get => this.sourceFile;
            set => this.sourceFile = value;
        }
        /// <summary>
        /// The path to the target file.
        /// </summary>
        protected string targetFile;
        /// <summary>
        /// Getter and Setter of targetFile.
        /// </summary>
        public string TargetFile
        {
            get => this.targetFile;
            set => this.targetFile = value;
        }
        /// <summary>
        /// The date and time when the save is made.
        /// </summary>
        protected DateTime timeStamp;
        /// <summary>
        /// Getter and Setter of timeStamp.
        /// </summary>
        public DateTime TimeStamp
        {
            get => this.timeStamp;
            set => this.timeStamp = value;
        }

        /// <summary>
        /// Constructor for the ModelLogger.
        /// </summary>
        /// <param name="saveName">Defines the name of the save.</param>
        /// <param name="sourceFile">Defines the path of the source 
        /// file.</param>
        /// <param name="targetFile">Defines the path of the target 
        /// file.</param>
        public ModelLogger(string saveName, string sourceFile,
            string targetFile)
        {
            this.saveName = saveName;
            this.sourceFile = sourceFile;
            this.targetFile = targetFile;
            this.timeStamp = DateTime.Now;
        }
    }
}
