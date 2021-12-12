﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using EasySaveConsole.model;
using EasySaveGUI.model;
using EasySaveGUI.retriever;

namespace EasySaveGUI.viewmodel
{
    public class ViewModelHomePage : INotifyPropertyChanged
    {
        private ICommand command;
        private List<ModelSave> saves = new List<ModelSave>();
        private List<ModelState> states = new List<ModelState>();
        private static ViewModelHomePage instance;

        public static ViewModelHomePage getInstance()
        {
            if(instance == null)
            {
                instance = new ViewModelHomePage();
            }
            return instance;
        }

        public List<ModelSave> Saves
        {
            get => this.saves;
            set
            {
                this.saves = value;
                OnPropertyChanged("Saves");
            }
        }
        public List<ModelState> States
        {
            get => this.states;
            set => this.states = value;
        }

        private ViewModelHomePage()
        {
            States = DataRetriever.getModelLog();
            States.ForEach(state =>
            {
                Saves.Add(state.toModelSave());
            });
        }

        public void RunSave(object sender)
        {
            ModelSave modelSave = ((Button)sender).DataContext as ModelSave;
            ModelState state_tmp = states.FindAll(state => state.SaveName == modelSave.Name).FindAll(state => state.SourceFile == modelSave.SourcePath).Find(state => state.TargetFile == modelSave.TargetPath);
            modelSave.save(ref state_tmp, states);
        }

        public void RunAll()
        {
            saves.ForEach(save =>
            {
                ModelState state_tmp = states.FindAll(state => state.SaveName == save.Name).FindAll(state => state.SourceFile == save.SourcePath).Find(state => state.TargetFile == save.TargetPath);
                save.save(ref state_tmp, states);
            });
        }

        private RessoucesModel model = new RessoucesModel();
        public ICommand GetChooseActionCommand
        {
            get
            {
                if (this.command == null)
                {
                    this.command = new RelayCommand(param => DoCommand(),
                                               param => CanDoCommand);
                }
                return this.command;
            }
            private set => this.command = value;
        }


        private void DoCommand()
        {
        }

        private bool CanDoCommand => this.model != null;
        #region INotifyPropertyChanged Members 
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            var propertyChangedEventArgs =
                new PropertyChangedEventArgs(propertyName);
            if (PropertyChanged != null)
            {
                PropertyChanged(this, propertyChangedEventArgs);
            }
        }
        #endregion
    }
}
