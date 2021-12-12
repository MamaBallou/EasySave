using System.Collections.Generic;
using System.ComponentModel;
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

        public ViewModelHomePage()
        {
            States = DataRetriever.getModelLog();
            States.ForEach(state =>
            {

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
