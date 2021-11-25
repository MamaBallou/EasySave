using EasySaveConsole.model;
using EasySaveConsole.view;
using System;
using System.Collections.Generic;
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
                       //TODO: get input (view) in sourceFile, targetFile and save type
            //get number of saves
            int Nb = modelSaves.Count;
            //get path access
            bool sourceAccess = Tool.getInstance().checkExistance(sourceFile);
            bool targetAccess = Tool.getInstance().checkExistance(targetFile);

            ///////////case number of saves <5
            if (Nb <5)
            {
                try
                {
                
                            //if mode save is differential create differential save
                            //TODO: enter differential condition
                            if (true)
                            {
                                
                                ModelSaveDifferential TypeDifferential = new ModelSaveDifferential("", @"", @"");
                                ModelState modelState = new ModelState("name", "source", "target");
                                TypeDifferential.save(ref modelState);
                                modelSaves.Add(TypeDifferential);
                                Console.WriteLine("the save has been created succefull");
                            }

                            //if mode save is total create total save
                            //TODO: enter total condition
                            if (true)
                            {
                                ///check the number of file in directory to save
                                int NbFileToSave = Directory.GetFiles(sourceFile.ToString()).Length;
                                //check if the Files number to save + the saves number which exist is not more than 5
                                //if Files number to save + the saves number > 5
                                if (NbFileToSave + Nb > 5)
                                {
                                    int NbFileCanSave = 5 - Nb;
                                    Console.WriteLine("Error you can't save more than 5!!!! the number of save is so much, you can only add {0} ", NbFileCanSave);
                                }
                                //if Files number to save + the saves number <= 5
                                else
                                {
                                    ModelSaveTotal TypeTotal = new ModelSaveTotal("", @"", @"");
                                    ModelState modelState = new ModelState("name", "source", "target");
                                    TypeTotal.save(ref modelState);
                                    modelSaves.Add(TypeTotal);
                                    Console.WriteLine("the save has been created succefull");

                                }
                            }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                //if number of save >= 5
                Console.WriteLine("ERROR!!! you can't create a new save, the number of save is already 5 ");

            }
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
