using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using EasySaveGUI.model;

namespace EasySaveGUI.viewmodel
{
    public class ViewModelHomePage : INotifyPropertyChanged
    {
        private ICommand command;
        private string choose_action;
        private string create_save;
        private string delete_save;
        private string run_all_save;
        private string run_one_save;
        private RessoucesModel model = new RessoucesModel();

        public string Choose_action
        {
            get => this.choose_action;
            set
            {
                this.choose_action = value;
                OnPropertyChanged("Choose_action");
            }
        }

        public string Create_save
        {
            get => this.create_save;
            set
            {
                this.create_save = value;
                OnPropertyChanged("Create_save");
            }
        }

        public string Delete_save
        {
            get => this.delete_save;
            set
            {
                this.delete_save = value;
                OnPropertyChanged("Delete_save");
            }
        }

        public string Run_all_save
        {
            get => this.run_all_save;
            set
            {
                this.run_all_save = value;
                OnPropertyChanged("Run_all_save");
            }
        }

        public string Run_one_save
        {
            get => this.run_one_save;
            set
            {
                this.run_one_save = value;
                OnPropertyChanged("Run_one_save");
            }
        }

        public ViewModelHomePage()
        {
            Choose_action = this.model.getRessource("choose_action");
            Create_save = this.model.getRessource("create_save");
            Delete_save = this.model.getRessource("delete_save");
            Run_all_save = this.model.getRessource("run_all_save");
            Run_one_save = this.model.getRessource("run_one_save");
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
