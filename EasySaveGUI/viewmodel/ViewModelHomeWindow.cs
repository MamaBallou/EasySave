using System.ComponentModel;
using System.Windows.Input;
using EasySaveGUI.model;
using EasySaveGUI.views;

namespace EasySaveGUI.viewmodel
{
    public class ViewModelHomeWindow
    {
        private ICommand command;
        private RessoucesModel model = new RessoucesModel();
        private string language;
        private ViewModelHomePage viewModelHomePage;
        private static ViewModelHomeWindow instance;

        public string Language
        {
            get => this.language;
            set
            {
                this.language = value;
                ChangeLanguage();
            }
        }

        private void ChangeLanguage()
        {
            if (this.language == "fr-FR")
            {
                EasySaveGUI.Properties.Settings.Default.language = "en-GB";
                EasySaveGUI.Properties.Settings.Default.Save();
                EasySaveGUI.Properties.languages.Resources.Culture = new System.Globalization.CultureInfo(EasySaveGUI.Properties.Settings.Default.language);
            }
            else
            {
                EasySaveGUI.Properties.Settings.Default.language = "fr-FR";
                EasySaveGUI.Properties.Settings.Default.Save();
                EasySaveGUI.Properties.languages.Resources.Culture = new System.Globalization.CultureInfo(EasySaveGUI.Properties.Settings.Default.language);
            }
        }

        public ViewModelHomePage ViewModelHomePage
        {
            get => this.viewModelHomePage;
            set
            {
                this.viewModelHomePage = value;
                OnPropertyChanged("ViewModelHomePage");
            }
        }

        public static ViewModelHomeWindow getInstance()
        {
            if (instance == null)
            {
                instance = new ViewModelHomeWindow();
            }
            return instance;
        }

        private ViewModelHomeWindow()
        {
            ViewModelHomePage = ViewModelHomePage.getInstance();
            this.language = Properties.Settings.Default.language;
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
