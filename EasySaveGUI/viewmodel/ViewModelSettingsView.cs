using System.Collections.Generic;
using System.ComponentModel;
using EasySaveConsole.tools;

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

        internal void SetToEncryptExtension(List<string> toEncrypt)
        {
            Properties.Settings.Default.toEncrypt.Clear();
            toEncrypt.ForEach(val =>
            {
                Properties.Settings.Default.toEncrypt.Add(val);
            });
            Properties.Settings.Default.Save();
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