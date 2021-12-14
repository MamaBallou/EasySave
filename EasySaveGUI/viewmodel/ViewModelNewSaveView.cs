using System.Collections.Generic;
using System.ComponentModel;
using EasySaveGUI.views;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Controls;
using System.Windows.Input;
using EasySaveConsole.model.log;
using EasySaveConsole.model.save;
using System.Diagnostics;
using System.Windows;
using System;
using EasySaveConsole.tools;
using System.IO;

namespace EasySaveGUI.viewmodel
{
    internal class ViewModelNewSaveView : INotifyPropertyChanged
    {
        private static ViewModelNewSaveView instance;
        private ViewModelNewSaveView() { }
        public static ViewModelNewSaveView GetInstance()
        {
            if (instance == null)
            {
                instance = new ViewModelNewSaveView();
            }
            return instance;
        }
        private string saveName = "";
        public string SaveName
        {
            get => this.saveName;
            set
            {
                this.saveName = value;
                OnPropertyChanged("SaveName");
            }
        }
        private string saveSourcePath = "";
        public string SaveSourcePath
        {
            get => this.saveSourcePath;
            set
            {
                this.saveSourcePath = value;
                OnPropertyChanged("SaveSourcePath");
            }
        }
        private string saveTargetPath = "";
        public string SaveTargetPath
        {
            get => this.saveTargetPath;
            set
            {
                this.saveTargetPath = value;
                OnPropertyChanged("SaveTargetPath");
            }
        }

        private bool total = true;
        public bool Total
        {
            get => this.total;
            set
            {
                this.total = value; OnPropertyChanged("Total");
            }
        }
        private bool differential = false;
        public bool Differential
        {
            get => this.differential;
            set
            {
                this.differential = value;
                OnPropertyChanged("Differential");
            }
        }
        private ICommand command;

        public ICommand GetCreateCommand
        {
            get
            {
                if (command == null)
                {
                    command = new RelayCommand(param => DoCreateCommand(),
                                               param => CanDoCommand);
                }
                return command;
            }
            private set { command = value; }
        }


        private void DoCreateCommand()
        {
            ViewModelHomePage viewModelHomePage = ViewModelHomePage.getInstance();
            ModelSave modelSave;
            if(total)
            {
                modelSave = new ModelSaveTotal(saveName, saveSourcePath, saveTargetPath);
            }
            else
            {
                modelSave = new ModelSaveDifferential(saveName, saveSourcePath, saveTargetPath);
            }
            ModelState saveState = modelSave.toModelState();
            viewModelHomePage.States.Add(saveState);
            List<ModelState> tmp = viewModelHomePage.States;
            if (Process.GetProcessesByName("Calculator").Length > 0)
            {
                // Todo : ShowMessage Box
                return;
            }
            modelSave.save(ref saveState, ref tmp);
        }


        private bool CanDoCommand
        {
            get {
                bool notEmpty = !String.IsNullOrEmpty(saveName) && !String.IsNullOrEmpty(saveSourcePath) && !String.IsNullOrEmpty(saveTargetPath);
                if (!Tool.getInstance().checkExistance(saveSourcePath))
                {
                    return false;
                }
                string target_tmp = Tool.completePath(saveTargetPath);
                FileInfo fi = null;
                try
                {
                    fi = new FileInfo(target_tmp);
                }
                catch (ArgumentException) { }
                catch (PathTooLongException) { }
                catch (NotSupportedException) { }
                if (fi == null)
                {
                    return false;
                }
                return notEmpty;
            }
        }

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
