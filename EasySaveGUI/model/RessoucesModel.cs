using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Resources;
using EasySaveConsole.controller;

namespace EasySaveGUI.model
{
    public class RessoucesModel
    {
        /// <summary>
        /// Declare Resource manager to access to specific cultureinfo.
        /// </summary>
        private ResourceManager res_man;
        /// <summary>
        /// Declare culture info.
        /// </summary>
        private CultureInfo cul = CultureInfo.CreateSpecificCulture("en");
        public RessoucesModel()
        {
            this.res_man = 
                new ResourceManager("EasySaveConsole.Properties.Res",
                typeof(ControllerSave).Assembly);
        }
        public string getRessource(string res)
        {
            return this.res_man.GetString(res, cul);
        }

        public List<string> getLanguages()
        {
            List<string> vs = new List<string>();
            vs.Add(getRessource("language_en"));
            vs.Add(getRessource("language_fr"));
            return vs;
        }
    }
}
