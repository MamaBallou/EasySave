namespace EasySaveConsole.model
{
    public class ModelLog : ModelLogger
    {
        private uint fileSize;
        private double transfertTime;

        public ModelLog(string saveName, string sourceFile, string targetFile, double transfertTime) : base(saveName, sourceFile, targetFile)
        {
            Tool tool = Tool.getInstance();
            fileSize = tool.getFileSize(sourceFile);
            this.transfertTime = transfertTime;
        }
    }
}
