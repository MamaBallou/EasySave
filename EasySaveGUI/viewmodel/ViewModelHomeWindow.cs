using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using EasySaveGUI.model;

namespace EasySaveGUI.viewmodel
{
    public class ViewModelHomeWindow
    {
        private ICommand command;
        private RessoucesModel model = new RessoucesModel();
        private List<string> languages;

        public List<string> Languages
        {
            get => this.languages;
            set
            {
                this.languages = value;
                OnPropertyChanged("Languages");
            }
        }

        public ViewModelHomeWindow()
        {
            Languages = this.model.getLanguages();
        }

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
