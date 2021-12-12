using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using EasySaveConsole.model;
using EasySaveGUI.model;

namespace EasySaveGUI.viewmodel
{
    public class ViewModelHomePage : INotifyPropertyChanged
    {
        private ICommand command;
        private List<ModelSave> saves = new List<ModelSave>();

        public List<ModelSave> Saves
        {
            get => this.saves;
            set
            {
                this.saves = value;
                OnPropertyChanged("Saves");
            }
        }

        public ViewModelHomePage()
        {
            
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
