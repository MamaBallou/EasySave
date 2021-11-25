using Newtonsoft.Json;

namespace EasySaveConsole.model
{
    public class ModelLog : ModelLogger
    {
        /// <summary>
        /// The size of the file we copy.
        /// </summary>
        private uint fileSize;
        public uint FileSize { get { return fileSize; } set { fileSize = value;} }
        /// <summary>
        /// The total time spent doing the transfert.
        /// </summary>
        private double transfertTime;
        public double TransfertTime { get { return transfertTime; } set { transfertTime = value; } }

        /// <summary>
        /// Constructor for ModelLog.
        /// </summary>
        /// <param name="saveName">Defines the name of the save.</param>
        /// <param name="sourceFile">Defines the path of the source file.</param>
        /// <param name="targetFile">Defines the path of the target file.</param>
        /// <param name="transfertTime">Defines the total transfert time.</param>
        public ModelLog(string saveName, string sourceFile, string targetFile, double transfertTime) : base(saveName, sourceFile, targetFile)
        {
            Tool tool = Tool.getInstance();
            fileSize = tool.getFileSize(sourceFile);
            this.transfertTime = transfertTime;
        }
    }
}
