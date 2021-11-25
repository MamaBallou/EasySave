namespace EasySaveConsole.model
{
    public class ModelState : ModelLogger
    {
        private State state;
        private uint totalFilesToCopy;
        private uint totalFileSize;
        private uint nbFilesLeftToDo;
        private double progression;

        public State State { get => state; set => state = value; }
        public uint TotalFilesToCopy { get => totalFilesToCopy; set => totalFilesToCopy = value; }
        public uint TotalFileSize { get => totalFileSize; set => totalFileSize = value; }
        public uint NbFilesLeftToDo { get => nbFilesLeftToDo; set => nbFilesLeftToDo = value; }
        public double Progression { get => progression; set => progression = value; }

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
