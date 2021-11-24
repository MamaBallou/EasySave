using EasySaveConsole.model;
using EasySaveConsole.view;
using System;
using System.Collections.Generic;
using System.Text;
using EasySaveConsole.logger;
using System.Globalization;
using System.Resources;

namespace EasySaveConsole.controller
{
    public class ControllerSave
    {
        private EnumLanguages chosenLanguage;
        private List<ModelSave> modelSaves;
        private ViewConsole view;
        public static List<ModelState> modelStates = new List<ModelState>();

        private ResourceManager res_man;    // declare Resource manager to access to specific cultureinfo
        private CultureInfo cul;            // declare culture info

        public ControllerSave(List<ModelSave> modelSaves, ViewConsole view)
        {
            this.modelSaves = modelSaves;
            this.view = view;
            chosenLanguage = EnumLanguages.English;
            cul = CultureInfo.CreateSpecificCulture("en");
            res_man = new ResourceManager("EasySaveConsole.Properties.Res", typeof(ControllerSave).Assembly);
        }

        public void chooseLanguage()
        {
            view.output(res_man.GetString("welcome", cul));
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
