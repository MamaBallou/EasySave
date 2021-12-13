using System.Windows;
using EasySaveGUI.viewmodel;

namespace EasySaveGUI
{
    /// <summary>
    /// Logique d'interaction pour HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {

        public HomeWindow()
        {
            ViewModelHomeWindow vm = ViewModelHomeWindow.getInstance();
            DataContext = vm;
            InitializeComponent();
            InitComboxBoxLanguage();
            this.languages.SelectionChanged += ChangeLanguage;
        }

        private void InitComboxBoxLanguage()
        {
            switch (Properties.Settings.Default.language)
            {
                case "fr-FR":
                case "fr":
                    this.languages.SelectedIndex = 0;
                    break;
                case "en-GB":
                case "en":
                    this.languages.SelectedIndex = 1;
                    break;
            }
        }

        private void ChangeLanguage(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            switch (this.languages.SelectedIndex)
            {
                case 1:
                    EasySaveGUI.Properties.languages.Resources.Culture = new System.Globalization.CultureInfo("en-GB");

                    EasySaveGUI.Properties.Settings.Default.language = "en-GB";
                    break;
                case 0:
                    EasySaveGUI.Properties.languages.Resources.Culture = new System.Globalization.CultureInfo("fr-FR");
                    EasySaveGUI.Properties.Settings.Default.language = "fr-FR";
                    break;
            }
            EasySaveGUI.Properties.Settings.Default.Save();
        }
    }
}
