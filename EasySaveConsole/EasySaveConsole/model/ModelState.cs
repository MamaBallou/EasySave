﻿using System.Text.Json.Serialization;

namespace EasySaveConsole.model
{
    public class ModelState : ModelLogger
    {
        private State state;
        [JsonIgnore]
        public State _State { get { return state; } set { state = value; } }
        public string Stata { get { return state.ToString(); } }
        private uint totalFilesToCopy;
        public uint TotalFilesToCopy { get { return totalFilesToCopy; } set { totalFilesToCopy = value; } }
        private uint totalFileSize;
        public uint TotalFileSize { get { return totalFileSize; } }
        private uint nbFilesLeftToDo;
        public uint NbFilesLeftToDo { get { return nbFilesLeftToDo; } set { nbFilesLeftToDo = value; } }
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
            return progression = ((double)(totalFileSize - nbFilesLeftToDo) / (double)totalFileSize) * 100.0;
        }
    }
}
