using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using EasySaveConsole.model.log;
using EasySaveGUI.model;
using EasySaveGUI.retriever;

namespace EasySaveGUI.viewmodel
{
    public class ViewModelHomePage : INotifyPropertyChanged
    {
        #region Singleton definition
        private static ViewModelHomePage instance;

        public static ViewModelHomePage getInstance()
        {
            if (instance == null)
            {
                instance = new ViewModelHomePage();
            }
            return instance;
        }
        protected ViewModelHomePage()
        {
            States = DataRetriever.getModelLog();
        }
        #endregion

        #region attributes
        private ICommand command;
        private List<ModelState> states = new List<ModelState>();
        #endregion
        #region Trigger Property change
        public List<ModelState> States
        {
            get => this.states;
            set
            {
                this.states = value;
                OnPropertyChanged("States");
            }
        }
        #endregion
        #region methods
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
            Thread t = new Thread(() =>
            {
                try
                {
                    this.states.ForEach(state =>
                    {
                        DoRunOne(state);
                    });
                }
                catch (ConcurentProcessException)
                {
                    MessageBox.Show(String.Format(Properties.languages.Resources.exception_concurent_process, "Calculator"));
                }
            });
        }
        #endregion
        #region ICommand definition
        private RessoucesModel model = new RessoucesModel();
        public ICommand GetRunOne
        {
            get
            {
                if (this.command == null)
                {
                    this.command = new DelegateCommand(CanDoRunOne, DoRunOne);
                }
                return this.command;
            }
            private set => this.command = value;
        }


        public void DoRunOne(object sender)
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

        private bool CanDoRunOne(object sender)
        {
            return this.model != null;
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
