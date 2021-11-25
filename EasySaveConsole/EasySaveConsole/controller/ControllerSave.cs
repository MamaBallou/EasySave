using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Resources;
using EasySaveConsole.model;
using EasySaveConsole.view;

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
            this.chosenLanguage = EnumLanguages.English;
            this.cul = CultureInfo.CreateSpecificCulture("en");
            this.res_man = new ResourceManager("EasySaveConsole.Properties.Res", typeof(ControllerSave).Assembly);
        }

        private void chooseLanguage()
        {
            bool again = false;
            do
            {
                this.view.output(this.res_man.GetString("language_selection", this.cul));
                this.view.output(String.Concat(this.res_man.GetString("language_fr", this.cul)));
                this.view.output(String.Concat(this.res_man.GetString("language_en", this.cul)));
                string choice = this.view.input();
                int choiceIndex = 0;
                if (int.TryParse(choice, out choiceIndex))
                {
                    switch (choiceIndex)
                    {
                        case 1:
                            this.chosenLanguage = EnumLanguages.Francais;
                            this.cul = CultureInfo.CreateSpecificCulture("fr");
                            break;
                        case 2:
                            this.chosenLanguage = EnumLanguages.English;
                            this.cul = CultureInfo.CreateSpecificCulture("en");
                            break;
                        default:
                            again = true;
                            break;
                    }
                }
                else
                    again = true;
            } while (again);
        }

        public void run()
        {
            this.view.output(this.res_man.GetString("welcome", this.cul));
            int choiceIndex = 0;
            chooseLanguage();
            do
            {
                bool choiceOk = false;
                do
                {
                    this.view.output(this.res_man.GetString("choose_action", this.cul));
                    this.view.output(this.res_man.GetString("create_save", this.cul));
                    this.view.output(this.res_man.GetString("delete_save", this.cul));
                    this.view.output(this.res_man.GetString("run_all_save", this.cul));
                    this.view.output(this.res_man.GetString("run_one_save", this.cul));
                    this.view.output(this.res_man.GetString("leave", this.cul));
                    string choice = this.view.input();
                    choiceOk = int.TryParse(choice, out choiceIndex);
                } while (!choiceOk);
                switch (choiceIndex)
                {
                    case 1:
                        createNewSave();
                        break;
                    case 2:
                        deleteSave();
                        break;
                    case 3:
                        runAllSaves();
                        break;
                    case 4:
                        runSave();
                        break;
                }
            } while (choiceIndex != 0 && choiceIndex < 5);
            this.view.output(this.res_man.GetString("bye", this.cul));
        }

        private void createNewSave()
        {
            this.view.output(this.res_man.GetString("choice_save", this.cul));
            //get number of saves
            int Nb = this.modelSaves.Count;
            //case number of saves <5
            if (Nb < 5)
            {
                this.view.output(this.res_man.GetString("ask_name", this.cul));
                string name_tmp = this.view.input();
                string source_tmp, target_tmp;
                int type = 0;
                bool again = false;
                ModelSave modelSave = null;
                do
                {
                    this.view.output(this.res_man.GetString("ask_sourcepath", this.cul));
                    source_tmp = this.view.input();
                    again = !Tool.getInstance().checkExistance(source_tmp);
                } while (again);
                do
                {
                    this.view.output(this.res_man.GetString("ask_targetpath", this.cul));
                    target_tmp = this.view.input();
                    FileInfo fi = null;
                    try
                    {
                        fi = new FileInfo(target_tmp);
                    }
                    catch (ArgumentException) { }
                    catch (PathTooLongException) { }
                    catch (NotSupportedException) { }
                    again = ReferenceEquals(fi, null);
                } while (again);
                try
                {
                    Directory.CreateDirectory(target_tmp);
                } catch { }
                do
                {
                    do
                    {
                        this.view.output(this.res_man.GetString("ask_type", this.cul));
                        this.view.output(this.res_man.GetString("type_total", this.cul));
                        this.view.output(this.res_man.GetString("type_differential", this.cul));
                        string type_tmp = this.view.input();
                        again = !int.TryParse(type_tmp, out type);
                    } while (again);
                    again = false;
                    switch (type)
                    {
                        case 1:
                            modelSave = new ModelSaveTotal(name_tmp, source_tmp, target_tmp);
                            view.output(String.Format(res_man.GetString("save_created", cul), name_tmp));
                            break;
                        case 2:
                            modelSave = new ModelSaveDifferential(name_tmp, source_tmp, target_tmp);
                            view.output(String.Format(res_man.GetString("save_created", cul), name_tmp));
                            break;
                        default:
                            again = true;
                            break;
                    }
                } while (again);
                if (modelSave != null)
                {
                    ModelState modelState = new ModelState(name_tmp, source_tmp, target_tmp);
                    modelStates.Add(modelState);
                    view.output(String.Format(res_man.GetString("save_run", cul), name_tmp));
                    modelSave.save(ref modelState);
                    view.output(String.Format(res_man.GetString("save_end", cul), name_tmp));
                }
            }
            else
            {
                view.output(res_man.GetString("exception_toomuchsaves", cul));
            }
        }

        private void deleteSave()
        {
            view.output(res_man.GetString("choice_delete", cul));

        }

        private void runAllSaves()
        {
            throw new NotImplementedException();
        }

        private void runSave()
        {
            throw new NotImplementedException();
        }
    }
}
