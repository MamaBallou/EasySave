using EasySaveConsole.model;
using EasySaveConsole.view;
using System;
using System.Collections.Generic;
using System.Text;
using EasySaveConsole.logger;

namespace EasySaveConsole.controller
{
    class ControllerSave
    {
        private List<ModelSave> modelSaves;
        private ViewConsole view;
        private LogLogger logLogger;
        private StateLogger stateLogger;

        public ControllerSave(List<ModelSave> modelSaves, ViewConsole view)
        {
            this.modelSaves = modelSaves;
            this.view = view;
            logLogger = LogLogger.getInstance();
            stateLogger = StateLogger.getInstance();
        }

        public void run()
        {
            throw new NotImplementedException();
        }

        private void createNewSave()
        {
            throw new NotImplementedException();
        }

        private void deleteSave(int saveNumber)
        {
            throw new NotImplementedException();
        }

        private void runAllSaves()
        {
            throw new NotImplementedException();
        }

        private void runSave(int saveNumber)
        {
            throw new NotImplementedException();
        }
    }
}
