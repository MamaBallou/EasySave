namespace EasySaveConsole.model
{
    public class ModelState : ModelLogger
    {
        private State state;
        private uint totalFilesToCopy;
        private uint totalFileSize;
        private uint nbFilesLeftToDo;
        private double progression;

        public State State { get { return state; } set { state = value; } }
        public uint TotalFilesToCopy { get { return totalFilesToCopy; } set { totalFilesToCopy = value; } }
        public uint TotalFileSize { get { return totalFileSize; } set { totalFileSize = value; } }
        public uint NbFilesLeftToDo { get { return nbFilesLeftToDo; } set { nbFilesLeftToDo = value; } }
        public double Progression { get { return progression; } set { progression = value; } }

        public ModelState(string saveName, string sourceFile, string targetFile) 
            : base(saveName, sourceFile, targetFile)
        {
            this.state = State.NotStarted;
            Tool tool = Tool.getInstance();
            this.totalFileSize = tool.getFileSize(sourceFile);
            this.totalFilesToCopy = tool.getNbFiles(sourceFile);
            this.nbFilesLeftToDo = totalFilesToCopy;
            this.progression = 0.0;
        }

        public double calcProg()
        {
            return progression = (double)(this.totalFilesToCopy * 100) / 
                (double)(this.totalFilesToCopy - this.nbFilesLeftToDo);
        }
    }
}
