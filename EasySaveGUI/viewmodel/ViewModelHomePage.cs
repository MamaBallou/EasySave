using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;
using EasySaveConsole.model.log;
using EasySaveConsole.model.save;
using EasySaveGUI.model;
using EasySaveGUI.retriever;
using EasySaveGUI.views;

namespace EasySaveGUI.viewmodel
{
    public class ViewModelHomePage : INotifyPropertyChanged
    {
        private ICommand command;
        private List<ModelState> states = new List<ModelState>();
        private static ViewModelHomePage instance;

        public static ViewModelHomePage getInstance()
        {
            if (instance == null)
            {
                instance = new ViewModelHomePage();
            }
            return instance;
        }
        public List<ModelState> States
        {
            get => this.states;
            set
            {
                this.states = value;
                OnPropertyChanged("States");
            }
        }

        private ViewModelHomePage()
        {
            States = DataRetriever.getModelLog();
        }

        public void RunSave(object sender)
        {
            if(Process.GetProcessesByName("Calculator").Length > 0)
            {
                throw new ConcurentProcessException();
            }
            ModelSave modelSave = ((Button)sender).DataContext as ModelSave;
            ModelState state_tmp = this.states.FindAll(state => state.SaveName == modelSave.Name).FindAll(state => state.SourceFile == modelSave.SourcePath).Find(state => state.TargetFile == modelSave.TargetPath);
            modelSave.save(ref state_tmp, ref this.states);
        }

        public void RunAll()
        {
            this.states.ForEach(state =>
            {
                if (Process.GetProcessesByName("Calculator").Length > 0)
                {
                    throw new ConcurentProcessException();
                }
                state.toModelSave().save(ref state, ref this.states);
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
