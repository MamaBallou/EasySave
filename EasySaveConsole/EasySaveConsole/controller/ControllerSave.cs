using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Resources;
using EasySaveConsole.model;
using EasySaveConsole.view;

namespace EasySaveConsole.controller
{
    /// <summary>
    /// Controller class, used to call all function and display the sequencial
    /// interraction with user.
    /// </summary>
    public class ControllerSave
    {
        /// <summary>
        /// Store the language choose.
        /// </summary>
        private EnumLanguages chosenLanguage;
        /// <summary>
        /// List of stored saves.
        /// </summary>
        private List<ModelSave> modelSaves;
        /// <summary>
        /// view object.
        /// </summary>
        private ViewConsole view;
        /// <summary>
        /// List of modelStates ordered indentically to the save it represents.
        /// </summary>
        public static List<ModelState> modelStates = new List<ModelState>();

        /// <summary>
        /// Declare Resource manager to access to specific cultureinfo.
        /// </summary>
        private ResourceManager res_man;
        /// <summary>
        /// Declare culture info.
        /// </summary>
        private CultureInfo cul;

        /// <summary>
        /// Contructor.
        /// </summary>
        /// <param name="modelSaves">List of model saves.</param>
        /// <param name="view">Instance of the view.</param>
        public ControllerSave(List<ModelSave> modelSaves, ViewConsole view)
        {
            this.modelSaves = modelSaves;
            this.view = view;
            this.chosenLanguage = EnumLanguages.English;
            this.cul = CultureInfo.CreateSpecificCulture("en");
            this.res_man = new ResourceManager("EasySaveConsole.Properties.Res",
                typeof(ControllerSave).Assembly);
        }

        /// <summary>
        /// Run function. It organizes calls to all functions.
        /// </summary>
        public void run()
        {
            this.view.output(this.res_man.GetString("welcome", this.cul));
            int choiceIndex = 0;
            // Call chooseLanguage
            chooseLanguage();
            // Ask to choose between the following options while no satisfiing
            // is put.
            do
            {
                Console.Clear();
                bool choiceOk = false;
                do
                {
                    this.view.output(this.res_man.GetString("choose_action",
                        this.cul));
                    this.view.output(this.res_man.GetString("create_save",
                        this.cul));
                    this.view.output(this.res_man.GetString("delete_save",
                        this.cul));
                    this.view.output(this.res_man.GetString("run_all_save",
                        this.cul));
                    this.view.output(this.res_man.GetString("run_one_save",
                        this.cul));
                    this.view.output(this.res_man.GetString("leave",
                        this.cul));
                    string choice = this.view.input();
                    // Check if entry is a number
                    choiceOk = int.TryParse(choice, out choiceIndex);
                } while (!choiceOk);
                // Call function according to entry
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
            // If entry 5 exit while loop and exit program
            Console.Clear();
            this.view.output(this.res_man.GetString("bye", this.cul));
        }

        /// <summary>
        /// Function to interact with user to choose language.
        /// </summary>
        private void chooseLanguage()
        {
            // Loop while no satisfiing entry if given
            bool again;
            do
            {
                again = false;
                this.view.output(this.res_man.GetString("language_selection",
                    this.cul));
                this.view.output(String.Concat(this.res_man.GetString(
                    "language_fr",
                    this.cul)));
                this.view.output(String.Concat(this.res_man.GetString(
                    "language_en",
                    this.cul)));
                string choice = this.view.input();
                int choiceIndex = 0;
                // Check if entry is integer
                if (int.TryParse(choice, out choiceIndex))
                {
                    // Change the language according to entry
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
                            Console.Clear();
                            this.view.output(this.res_man.GetString("wrong_option_selection", this.cul) + "\n");
                            again = true;
                            break;
                    }
                }
                else
                    again = true;
            } while (again);
        }

        /// <summary>
        /// Function to interact with user to create a new baclup.
        /// </summary>
        private void createNewSave()
        {
            Console.Clear();
            this.view.output(this.res_man.GetString("choice_save", this.cul));
            //get number of saves
            int Nb = this.modelSaves.Count;
            //case number of saves <5
            if (Nb < 5)
            {
                // Ask for backup infos : name, sourcepath, targetPath & type
                this.view.output(this.res_man.GetString("ask_name", this.cul));
                string name_tmp = this.view.input();
                string source_tmp, target_tmp;
                int type = 0;
                bool again = false;
                ModelSave modelSave = null;
                Console.Clear();
                // Ask path while path not found
                do
                {
                    this.view.output(this.res_man.GetString("ask_sourcepath",
                        this.cul));
                    source_tmp = this.view.input();
                    source_tmp = Tool.completePath(source_tmp);
                    again = !Tool.getInstance().checkExistance(source_tmp);
                    if (again)
                    {
                        Console.Clear();
                        this.view.output(this.res_man.GetString("wrong_option_selection", this.cul) + "\n");
                    }
                } while (again);
                // Ask targetPath while targetPath format not recognized
                do
                {
                    Console.Clear();
                    this.view.output(this.res_man.GetString("ask_targetpath",
                        this.cul));
                    target_tmp = this.view.input();
                    target_tmp = Tool.completePath(target_tmp);
                    FileInfo fi = null;
                    try
                    {
                        fi = new FileInfo(target_tmp);
                    }
                    catch (ArgumentException) { this.view.output("non"); }
                    catch (PathTooLongException) { this.view.output("non");  }
                    catch (NotSupportedException) { this.view.output("non");  }
                    again = ReferenceEquals(fi, null);
                    if (again)
                    {
                        Console.Clear();
                        this.view.output(this.res_man.GetString("wrong_option_selection", this.cul) + "\n");
                    }
                } while (again);
                try
                {
                    Directory.CreateDirectory(target_tmp);
                }
                catch { }
                // Ask for type while entry incorrect
                Console.Clear();
                do
                {
                    do
                    {
                        this.view.output(this.res_man.GetString(
                            "ask_type",
                            this.cul));
                        this.view.output(this.res_man.GetString(
                            "type_total",
                            this.cul));
                        this.view.output(this.res_man.GetString(
                            "type_differential", this.cul));
                        string type_tmp = this.view.input();
                        again = !int.TryParse(type_tmp, out type);
                    } while (again);
                    again = false;
                    // Create save according to infos given
                    Console.Clear();
                    switch (type)
                    {
                        case 1:
                            modelSave = new ModelSaveTotal(
                                name_tmp,
                                source_tmp,
                                target_tmp);
                            this.view.output(String.Format(
                                this.res_man.GetString("save_created", this.cul),
                                name_tmp));
                            break;
                        case 2:
                            modelSave = new ModelSaveDifferential(
                                name_tmp,
                                source_tmp,
                                target_tmp);
                            this.view.output(String.Format(
                                this.res_man.GetString("save_created", this.cul),
                                name_tmp));
                            break;
                        default:
                            again = true;
                            break;
                    }
                    this.modelSaves.Add(modelSave);
                    if (again)
                    {
                        Console.Clear();
                        this.view.output(this.res_man.GetString("wrong_option_selection", this.cul) + "\n");
                    }
                } while (again);
                // Running save
                if (modelSave != null)
                {
                    Console.Clear();
                    ModelState modelState = new ModelState(name_tmp,
                        source_tmp,
                        target_tmp);
                    modelStates.Add(modelState);
                    this.view.output(String.Format(
                        this.res_man.GetString("save_run", this.cul),
                        name_tmp));
                    modelSave.save(ref modelState);
                    this.view.output(String.Format(
                        this.res_man.GetString("save_end", this.cul),
                        name_tmp));
                }
                this.view.input();
            }
            // If too much saves (> 5)
            else
            {
                Console.Clear();
                this.view.output(
                    this.res_man.GetString("exception_toomuchsaves",
                    this.cul));
            }
        }

        /// <summary>
        /// Function to interact with user to delete a backup.
        /// </summary>
        private void deleteSave()
        {
            Console.Clear();
            this.view.output(
                this.res_man.GetString("choice_delete",
                this.cul));
            // We need at least 1 save to ask the user which save they want to delete
            if (this.modelSaves.Count != 0)
            {
                bool again = false;
                int choice = 0;
                // Ask to choose the save to delete while entry incorrect
                do
                {
                    this.view.output(
                        this.res_man.GetString("choose_save",
                        this.cul));
                    byte compt = 1;
                    // If the list of saves is empty, print a message. Else display all the saves of the list.
                    foreach (var modelSave in this.modelSaves)
                    {
                        this.view.output(
                            String.Format("{0} : {1}", compt, modelSave.Name));
                    }
                    string choice_tmp = this.view.input();
                    again = !int.TryParse(choice_tmp, out choice);
                    if (again)
                    {
                        Console.Clear();
                        this.view.output(this.res_man.GetString("wrong_option_selection", this.cul) + "\n");
                    }
                } while (again && choice > 0 && choice <= this.modelSaves.Count);
                ModelSave model = this.modelSaves[choice - 1];
                again = false;
                string answer;
                // Ask if user is sure to delete while incorrect entry
                do
                {
                    Console.Clear();
                    this.view.output(String.Format(
                        this.res_man.GetString("delete_comfirm", this.cul),
                        model.Name));
                    answer = this.view.input();
                    bool y = String.Equals(
                        answer,
                        "y",
                        StringComparison.OrdinalIgnoreCase);
                    bool n = String.Equals(
                        answer,
                        "n",
                        StringComparison.OrdinalIgnoreCase);
                    again = !(y || n);
                    if (again)
                    {
                        Console.Clear();
                        this.view.output(this.res_man.GetString("wrong_option_selection", this.cul) + "\n");
                    }
                } while (again);
                switch (answer)
                {
                    case "y":
                    case "Y":
                        this.modelSaves.Remove(model);
                        modelStates.RemoveAt(choice - 1);
                        model.delete();
                        this.view.output(String.Format(
                            this.res_man.GetString("delete_end", this.cul),
                            model.Name));
                        break;
                    case "n":
                    case "N":
                        this.view.output(String.Format(
                            this.res_man.GetString("delete_end", this.cul),
                            model.Name));
                        break;
                }
            }
            else
            {
                this.view.output("\n" + this.res_man.GetString("no_save", this.cul));
                this.view.input();
            }
        }

        /// <summary>
        /// Function to interact with user to run all backups.
        /// </summary>
        private void runAllSaves()
        {
            Console.Clear();
            this.view.output(
                this.res_man.GetString("choice_runallsaves",
                this.cul));
            // We need at least 1 save to be able to run all saves
            if(this.modelSaves.Count != 0)
            {
                // Browse the saved backups and run them all
                for (byte compt = 0; compt < this.modelSaves.Count; compt++)
                {
                    ModelState modelState = modelStates[compt];
                    this.view.output(String.Format(
                        this.res_man.GetString("save_run", this.cul),
                        this.modelSaves[compt].Name));
                    this.modelSaves[compt].save(ref modelState);
                    this.view.output(String.Format(
                        this.res_man.GetString("save_end", this.cul),
                        this.modelSaves[compt].Name));
                    this.view.input();
                }
            }
            else
            {
                this.view.output("\n" + this.res_man.GetString("no_save", this.cul));
                this.view.input();
            }
        }

        /// <summary>
        /// Function to interact with user to run  one backup.
        /// </summary>
        private void runSave()
        {
            Console.Clear();
            this.view.output(
                this.res_man.GetString("choice_runonesave",
                this.cul));
            // We need at least 1 save to ask user whiwh save they want to run
            if (this.modelSaves.Count != 0)
            {
                this.view.output(this.res_man.GetString("choose_save", this.cul));
                bool again = false;
                int choice = 0;
                // Ask which backup the user wants to run while incorrect entry.
                do
                {
                    this.view.output(
                        this.res_man.GetString("choose_save",
                        this.cul));
                    byte compt = 1;
                    foreach (var modelSave in this.modelSaves)
                    {
                        this.view.output(
                            String.Format("{0} : {1}", compt, modelSave.Name));
                    }
                    string choice_tmp = this.view.input();
                    again = !int.TryParse(choice_tmp, out choice);
                    if (again)
                    {
                        Console.Clear();
                        this.view.output(this.res_man.GetString("wrong_option_selection", this.cul) + "\n");
                    }
                } while (again && choice > 0 && choice <= this.modelSaves.Count);
                ModelSave model = this.modelSaves[choice - 1];
                ModelState modelState = modelStates[choice - 1];
                this.view.output(
                    String.Format(
                        this.res_man.GetString("save_run", this.cul),
                        model.Name));
                model.save(ref modelState);
                this.view.output(String.Format(
                        this.res_man.GetString("save_end", this.cul),
                        model.Name));
            }
            else
            {
                this.view.output("\n" + this.res_man.GetString("no_save", this.cul));
                this.view.input();
            }
        }
    }
}
