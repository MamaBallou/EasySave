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
        #region Singleton instance
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
        #endregion
        #region attributes
        private string saveName = "";
        private string saveSourcePath = "";
        private string saveTargetPath = "";
        private bool total = true;
        private bool differential = false;
        private ICommand createSave;
        private ICommand openFileBrowserSource;
        private ICommand openFileBrowserTarget;
        #endregion
        #region public accesser
        public string SaveName
        {
            get => this.saveName;
            set
            {
                this.saveName = value;
                OnPropertyChanged("SaveName");
            }
        }
        public string SaveSourcePath
        {
            get => this.saveSourcePath;
            set
            {
                this.saveSourcePath = value;
                OnPropertyChanged("SaveSourcePath");
            }
        }
        public string SaveTargetPath
        {
            get => this.saveTargetPath;
            set
            {
                this.saveTargetPath = value;
                OnPropertyChanged("SaveTargetPath");
            }
        }
        public bool Total
        {
            get => this.total;
            set
            {
                this.total = value; OnPropertyChanged("Total");
            }
        }
        public bool Differential
        {
            get => this.differential;
            set
            {
                this.differential = value;
                OnPropertyChanged("Differential");
            }
        }
        #endregion
        #region ICommands
        public ICommand GetCreateSave
        {
            get
            {
                if (this.createSave == null)
                {
                    this.createSave = new RelayCommand(param => DoCreateSave(),
                                               param => CanDoCreateSave);
                }
                return this.createSave;
            }
            private set => this.createSave = value;
        }
        public ICommand GetOpenBrowserSource
        {
            get
            {
                if (this.openFileBrowserSource == null)
                {
                    this.openFileBrowserSource = new RelayCommand(param => DoOpenBrowserSource());
                }
                return this.openFileBrowserSource;
            }
            private set => this.openFileBrowserSource = value;
        }
        #endregion
        #region functions
        private void DoOpenBrowserSource()
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.ShowDialog();
            SaveSourcePath = dialog.SelectedPath + "\\";
        }
        public ICommand GetOpenBrowserTarget
        {
            get
            {
                if (this.openFileBrowserTarget == null)
                {
                    this.openFileBrowserTarget = new RelayCommand(param => DoOpenBrowserTarget());
                }
                return this.openFileBrowserTarget;
            }
            private set => this.openFileBrowserTarget = value;
        }
        #endregion
        #region functions
        private void DoOpenBrowserTarget()
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.ShowDialog();
            SaveTargetPath = dialog.SelectedPath + "\\";
        }
        private void DoCreateSave()
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
        private bool CanDoCreateSave
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
        #endregion
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
