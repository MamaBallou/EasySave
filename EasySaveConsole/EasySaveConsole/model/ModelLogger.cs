using System;
using Newtonsoft.Json;

namespace EasySaveConsole.model
{
    public abstract class ModelLogger
    {
        /// <summary>
        /// The name of the save.
        /// </summary>
        [JsonProperty("SaveName")]
        protected string saveName;
        public string SaveName { get { return saveName; } set { saveName = value; } }
        /// <summary>
        /// The path to the source file.
        /// </summary>
        [JsonProperty("SourceFile")]
        protected string sourceFile;
        public string SourceFile { get { return sourceFile; } set { sourceFile = value; } }
        /// <summary>
        /// The path to the target file.
        /// </summary>
        [JsonProperty("TargetFile")]
        protected string targetFile;
        public string TargetFile { get { return targetFile; } set { targetFile = value; } }
        /// <summary>
        /// The date and time when the save is made.
        /// </summary>
        [JsonProperty("TimeStamp")]
        protected DateTime timeStamp;
        public DateTime TimeStamp { get { return timeStamp; } set { timeStamp = value; } }

        /// <summary>
        /// Constructor for the ModelLogger.
        /// </summary>
        /// <param name="saveName">Defines the name of the save.</param>
        /// <param name="sourceFile">Defines the path of the source file.</param>
        /// <param name="targetFile">Defines the path of the target file.</param>
        public ModelLogger(string saveName, string sourceFile,
            string targetFile)
        {
            this.saveName = saveName;
            this.sourceFile = sourceFile;
            this.targetFile = targetFile;
            this.timeStamp = DateTime.Now;
        }

        /// <summary>
        /// Converts all the attributes in String.
        /// </summary>
        /// <returns>A string with all the attributes.</returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
