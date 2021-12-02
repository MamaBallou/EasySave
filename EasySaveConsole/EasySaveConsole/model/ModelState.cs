using System.Text.Json.Serialization;

namespace EasySaveConsole.model
{
    /// <summary>
    /// ModelState representing the state of a save.
    /// </summary>
    public class ModelState : ModelLogger
    {
        /// <summary>
        /// State of the save.
        /// </summary>
        private EnumState state;
        /// <summary>
        /// Getter & Setter of state.
        /// </summary>
        [JsonIgnore]
        public EnumState _State { get => this.state; set => this.state = value; }
        /// <summary>
        /// String Getter of state.
        /// </summary>
        public string State => this.state.ToString();
        /// <summary>
        /// Number of files to copy from source.
        /// </summary>
        private uint totalFilesToCopy;
        /// <summary>
        /// Getter & Setter of totalFilesToCopy
        /// </summary>
        public uint TotalFilesToCopy
        {
            get => this.totalFilesToCopy;
            set => this.totalFilesToCopy = value;
        }
        /// <summary>
        /// Sum of file size to copy from source to target.
        /// </summary>
        private uint totalFileSize;
        /// <summary>
        /// Getter of totalFileSize.
        /// </summary>
        public uint TotalFileSize => this.totalFileSize;
        /// <summary>
        /// Number of files left to copy from source to target.
        /// </summary>
        private uint nbFilesLeftToDo;
        /// <summary>
        /// Getter & Setter of nbFilesLeftToDo.
        /// </summary>
        public uint NbFilesLeftToDo
        {
            get => this.nbFilesLeftToDo;
            set => this.nbFilesLeftToDo = value;
        }
        /// <summary>
        /// Progression expressed in percent.
        /// </summary>
        private double progression;
        /// <summary>
        /// Getter of progression.
        /// </summary>
        public double Progression => this.progression;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="saveName">Save name.</param>
        /// <param name="sourceFile">Source path.</param>
        /// <param name="targetFile">Target path.</param>
        public ModelState(string saveName, string sourceFile, string targetFile)
            : base(saveName, sourceFile, targetFile)
        {
            this.state = EnumState.NotStarted;
            Tool tool = Tool.getInstance();
            this.totalFileSize = tool.getFileSize(sourceFile);
            this.totalFilesToCopy = tool.getNbFiles(sourceFile);
            this.nbFilesLeftToDo = this.totalFilesToCopy;
            this.progression = 0.0;
        }

        public ModelState():base() { }

        /// <summary>
        /// Calculate progression rate.
        /// </summary>
        /// <returns>progression expression in double.</returns>
        public double calcProg()
        {
            return this.progression =
                ((this.totalFilesToCopy - this.nbFilesLeftToDo)
                / (double)this.totalFilesToCopy) * 100.0;
        }
    }
}
