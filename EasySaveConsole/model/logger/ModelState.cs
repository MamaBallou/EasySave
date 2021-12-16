using System.ComponentModel;
using System.Text.Json.Serialization;
using EasySaveConsole.model.enums;
using EasySaveConsole.model.save;
using EasySaveConsole.tools;

namespace EasySaveConsole.model.log
{
    /// <summary>
    /// ModelState representing the state of a save.
    /// </summary>
    public class ModelState : ModelLogger, INotifyPropertyChanged
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
        private EnumSaveTypes saveType;
        [JsonIgnore]
        public EnumSaveTypes _SaveType
        {
            get => this.saveType;
            set => this.saveType = value;
        }
        public string SaveType
        {
            get => this.saveType.ToString();
            set
            {
                switch (value)
                {
                    case "Differential":
                        this.saveType = EnumSaveTypes.Differential;
                        break;
                    case "Total":
                        this.saveType = EnumSaveTypes.Total;
                        break;
                }
            }
        }
        /// <summary>
        /// Number of files to copy from source.
        /// </summary>
        private uint totalFilesToCopy;
        /// <summary>
        /// Getter & Setter of totalFilesToCopy.
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
        public double Progression
        {
            get => this.progression;
            set
            {
                this.progression = value;
                OnPropertyChanged("Progression");
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="saveName">Save name.</param>
        /// <param name="sourceFile">Source path.</param>
        /// <param name="targetFile">Target path.</param>
        public ModelState(string saveName, string sourceFile, string targetFile, EnumSaveTypes saveType)
            : base(saveName, sourceFile, targetFile)
        {
            this.state = EnumState.NotStarted;
            Tool tool = Tool.getInstance();
            this.totalFileSize = tool.getFileSize(sourceFile);
            this.totalFilesToCopy = tool.getNbFiles(sourceFile);
            this.nbFilesLeftToDo = this.totalFilesToCopy;
            Progression = 0.0;
            this.saveType = saveType;
        }

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public ModelState() : base() { }

        /// <summary>
        /// Calculate progression rate.
        /// </summary>
        /// <returns>progression expression in double.</returns>
        public double calcProg()
        {
            return Progression =
                ((this.totalFilesToCopy - this.nbFilesLeftToDo)
                / (double)this.totalFilesToCopy) * 100.0;
        }

        public void initStart()
        {
            this.state = EnumState.OnGoing;
            this.totalFilesToCopy = Tool.getInstance().getNbFiles(this.sourceFile);
            this.nbFilesLeftToDo = this.totalFilesToCopy;
            this.totalFileSize = Tool.getInstance().getFileSize(this.sourceFile);
        }

        public ModelSave toModelSave()
        {
            ModelSave modelSave = null;
            switch (this.saveType)
            {
                case EnumSaveTypes.Differential:
                    modelSave = new ModelSaveDifferential(this.saveName, this.sourceFile, this.targetFile);
                    break;
                case EnumSaveTypes.Total:
                    modelSave = new ModelSaveTotal(this.saveName, this.sourceFile, this.targetFile);
                    break;
            }
            return modelSave;
        }
        #region INotifyPropertyChanged
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
