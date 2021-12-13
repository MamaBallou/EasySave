namespace EasySaveConsole.model
{
    /// <summary>
    /// Log model class.
    /// </summary>
    public class ModelLog : ModelLogger
    {
        /// <summary>
        /// The size of the file we copy.
        /// </summary>
        private uint fileSize;
        /// <summary>
        /// Getter and Setter of fileSize.
        /// </summary>
        public uint FileSize
        {
            get => this.fileSize;
            set => this.fileSize = value;
        }
        /// <summary>
        /// The total time spent doing the transfert.
        /// </summary>
        private double transfertTime;
        /// <summary>
        /// Getter and Setter of transfertTime.
        /// </summary>
        public double TransfertTime
        {
            get => this.transfertTime;
            set => this.transfertTime = value;
        }

        /// <summary>
        /// Constructor for ModelLog.
        /// </summary>
        /// <param name="saveName">Defines the name of the save.</param>
        /// <param name="sourceFile">Defines the path of the source
        /// file.</param>
        /// <param name="targetFile">Defines the path of the target
        /// file.</param>
        /// <param name="transfertTime">Defines the total transfert
        /// time.</param>
        public ModelLog(string saveName, string sourceFile,
            string targetFile, double transfertTime)
            : base(saveName, sourceFile, targetFile)
        {
            Tool tool = Tool.getInstance();
            this.fileSize = tool.getFileSize(sourceFile);
            this.transfertTime = transfertTime;
        }

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public ModelLog() : base() { }
    }
}
