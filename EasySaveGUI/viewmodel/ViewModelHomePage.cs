using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using EasySaveConsole.model.log;
using EasySaveConsole.model.save;
using EasySaveGUI.exception;
using EasySaveGUI.retriever;
using EasySaveGUI.views;

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
        private ICommand runOne;
        private ICommand runAll;
        private ICommand play;
        private ICommand pause;
        private ICommand stop;
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
            try
            {
                this.states.ForEach(state =>
                {
                    Thread t = new Thread(() =>
                        {
                            DoRunOne(state);
                        });
                    t.Start();
                });
            }
            catch (ConcurentProcessException)
            {
                MessageBox.Show(String.Format(Properties.languages.Resources.exception_concurent_process, "Calculator"));
            }
        }
        #endregion
        #region ICommand definition

        public ICommand GetRunAll
        {
            get
            {
                if (this.runAll == null)
                {
                    this.runAll = new RelayCommand(param => DoRunAll(),
                                               param => CanDoRunAll());
                }
                return this.runAll;
            }
            private set => this.runAll = value;
        }
        public void DoRunAll()
        {
            Thread t = new Thread(() =>
            {
                try
                {
                    RunAll();
                }
                catch (ConcurentProcessException)
                {
                    MessageBox.Show(String.Format(Properties.languages.Resources.exception_concurent_process, "Calculator"));
                }
            });
            t.Start();
        }

        private bool CanDoRunAll()
        {
            return States.Count > 0;
        }
        public ICommand GetRunOne
        {
            get
            {
                if (this.runOne == null)
                {
                    this.runOne = new RelayCommand(DoRunOne);
                }
                return this.runOne;
            }
            private set => this.runOne = value;
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
        public ICommand GetPlay
        {
            get
            {
                if (this.play == null)
                {
                    this.play = new RelayCommand(param => DoPlay());
                }
                return this.play;
            }
            private set => this.play = value;
        }

        public void DoPlay()
        {
            ModelSave.manualResetEvent.Set();
        }
        public ICommand GetPause
        {
            get
            {
                if (this.pause == null)
                {
                    this.pause = new RelayCommand(param => DoPause());
                }
                return this.pause;
            }
            private set => this.pause = value;
        }

        public void DoPause()
        {
            ModelSave.manualResetEvent.Reset();
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
