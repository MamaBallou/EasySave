using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using EasySaveConsole.tools;
using EasySaveGUI.views;

namespace EasySaveGUI.viewmodel
{
    internal class ViewModelSettingsView : INotifyPropertyChanged
    {
        private static ViewModelSettingsView instance;
        private bool pdf;
        private bool jpg;
        private bool docx;
        private bool txt;
        private bool exe;

        public bool Pdf
        {
            get => this.pdf;
            set
            {
                this.pdf = value;
                OnPropertyChanged("Pdf");
            }
        }
        public bool Jpg
        {
            get => this.jpg;
            set
            {
                this.jpg = value;
                OnPropertyChanged("Jpg");
            }
        }
        public bool Docx
        {
            get => this.docx;
            set
            {
                this.docx = value;
                OnPropertyChanged("Docx");
            }
        }
        public bool Txt
        {
            get => this.txt;
            set
            {
                this.txt = value;
                OnPropertyChanged("Txt");
            }
        }
        public bool Exe
        {
            get => this.exe;
            set
            {
                this.exe = value;
                OnPropertyChanged("Exe");
            }
        }

        internal static ViewModelSettingsView GetInstance()
        {
            if (instance == null)
            {
                instance = new ViewModelSettingsView();
            }
            List<string> toEncrypt = Crypter.ToEncrypt;
            instance.pdf = toEncrypt.Exists(val => val == ".pdf");
            instance.jpg = toEncrypt.Exists(val => val == ".jpg");
            instance.docx = toEncrypt.Exists(val => val == ".docx");
            instance.txt = toEncrypt.Exists(val => val == ".txt");
            instance.exe = toEncrypt.Exists(val => val == ".exe");
            return instance;
        }

        public void SetToEncryptExtension(List<string> toEncrypt)
        {
            Properties.Settings.Default.toEncrypt.Clear();
            toEncrypt.ForEach(val =>
            {
                Properties.Settings.Default.toEncrypt.Add(val);
            });
            Properties.Settings.Default.Save();
            Crypter.ToEncrypt = toEncrypt;
        }
        private ICommand command;

        public ICommand GetSaveCommand
        {
            get
            {
                if (this.command == null)
                {
                    this.command = new RelayCommand(param => DoSaveCommand(),
                                               param => CanDoCommand);
                }
                return this.command;
            }
            private set => this.command = value;
        }


        private void DoSaveCommand()
        {
            List<string> toEncrypt = new List<string>();
            if (this.pdf)
            {
                toEncrypt.Add(".pdf");
            }
            if (this.jpg)
            {
                toEncrypt.Add(".jpg");
            }
            if (this.docx)
            {
                toEncrypt.Add(".docx");
            }
            if (this.txt)
            {
                toEncrypt.Add(".txt");
            }
            if (this.exe)
            {
                toEncrypt.Add(".exe");
            }
            SetToEncryptExtension(toEncrypt);
        }

        private bool CanDoCommand => this.command != null;

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