using System;
using System.Collections.Generic;
using System.Text;

namespace EasySaveConsole.model
{
    public class ModelLog : ModelLogger
    {
        private double fileSize;
        private int transfertTime;

        public ModelLog(double fileSize, int transfertTime, String saveName, String sourceFile, String targetFile, DateTime timeStamp) : base(saveName, sourceFile, targetFile, timeStamp)
        {
            this.fileSize = fileSize;
            this.transfertTime = transfertTime;
        }

        public String[] toString()
        {
            String[] strTab =
            {
                "{",
                "\t\"Name\" : \"" + this.saveName + "\",",
                "\t\"FileSource\" : \"" + this.sourceFile + "\",",
                "\t\"FileTarget\" : \"" + this.targetFile + "\",",
                "\t\"destPath\" : \"\",",
                "\t\"FileSize\" : " + this.fileSize.ToString() + ",",
                "\t\"FileTransfertTime\" : " + this.transfertTime.ToString() + ",",
                "\t\"time\" : \"" + this.timeStamp.ToString() + "\"",
                "}",
                "\n"
            };

            return strTab;
        }
    }
}
