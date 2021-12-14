using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using EasySaveConsole.model.log;
using EasySaveConsole.model.save;
using EasySaveConsole.tools;
using EasySaveGUI.views;

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
                if (this.command == null)
                {
                    this.command = new RelayCommand(param => DoCreateCommand(),
                                               param => CanDoCommand);
                }
                return this.command;
            }
            private set => this.command = value;
        }


        private void DoCreateCommand()
        {
            ViewModelHomePage viewModelHomePage = ViewModelHomePage.getInstance();
            ModelSave modelSave;
            if (this.total)
            {
                modelSave = new ModelSaveTotal(this.saveName, this.saveSourcePath, this.saveTargetPath);
            }
            else
            {
                modelSave = new ModelSaveDifferential(this.saveName, this.saveSourcePath, this.saveTargetPath);
            }
            ModelState saveState = modelSave.toModelState();
            viewModelHomePage.States.Add(saveState);
            List<ModelState> tmp = viewModelHomePage.States;
            if (Process.GetProcessesByName("Calculator").Length > 0)
            {
                MessageBox.Show(String.Format(Properties.languages.Resources.exception_concurent_process, "Calculator"));
                return;
            }
            modelSave.save(ref saveState, ref tmp);
        }


        private bool CanDoCommand
        {
            get
            {
                bool notEmpty = !String.IsNullOrEmpty(this.saveName) && !String.IsNullOrEmpty(this.saveSourcePath) && !String.IsNullOrEmpty(this.saveTargetPath);
                if (!Tool.getInstance().checkExistance(this.saveSourcePath))
                {
                    return false;
                }
                string target_tmp = Tool.completePath(this.saveTargetPath);
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
