using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using EasySaveConsole.model.log;
using EasySaveGUI.model;
using EasySaveGUI.retriever;

namespace EasySaveGUI.viewmodel
{
    public class ViewModelHomePage : INotifyPropertyChanged
    {
        private ICommand command;
        private List<ModelState> states = new List<ModelState>();
        private static ViewModelHomePage instance;
        private BackgroundWorker worker;

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

        protected ViewModelHomePage()
        {
            States = DataRetriever.getModelLog();
        }

        public void RunSave(object sender)
        {
            if (Process.GetProcessesByName("Calculator").Length > 0)
            {
                throw new ConcurentProcessException();
            }
            ModelState state = sender as ModelState;
            if (Process.GetProcessesByName("Calculator").Length > 0)
            {
                throw new ConcurentProcessException();
            }
            state.toModelSave().save(ref state, ref this.states);
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
                    this.command = new DelegateCommand(CanDoCommand, DoCommand);
                }
                return this.command;
            }
            private set => this.command = value;
        }


        public void DoCommand(object sender)
        {
            Thread t = new Thread(() =>
            {
                try
                {
                    RunSave(sender);
                }
                catch (ConcurentProcessException)
                {
                    MessageBox.Show(String.Format(Properties.languages.Resources.exception_concurent_process, "Calculator"));
                }
            });
            t.Start();
        }

        private bool CanDoCommand(object sender)
        {
            return this.model != null;
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
