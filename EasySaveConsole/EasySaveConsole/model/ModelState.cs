namespace EasySaveConsole.model
{
    public class ModelState : ModelLogger
    {
        private State state;
        public State State { get { return state; } set { state = value; } }
        private uint totalFilesToCopy;
        public uint TotalFilesToCopy { get { return totalFilesToCopy; } set { totalFilesToCopy = value; } }
        private uint totalFileSize;
        public uint TotalFileSize { get { return totalFileSize; } }
        private uint nbFilesLeftToDo;
        public uint NbFilesLeftToDo { get { return nbFilesLeftToDo; } }
        private double progression;
        public double Progression { get { return progression; } }

        public ModelState(string saveName, string sourceFile, string targetFile)
            : base(saveName, sourceFile, targetFile)
        {
            state = State.NotStarted;
            Tool tool = Tool.getInstance();
            totalFileSize = tool.getFileSize(sourceFile);
            totalFilesToCopy = tool.getNbFiles(sourceFile);
            nbFilesLeftToDo = totalFilesToCopy;
            progression = 0.0;
        }

        public double calcProg()
        {
            return progression = totalFilesToCopy * 100 /
                (double)(totalFilesToCopy - nbFilesLeftToDo);
        }
    }
}
