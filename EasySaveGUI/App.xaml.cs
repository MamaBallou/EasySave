using System.Windows;

namespace EasySaveGUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            SetLanguageDictionary();
        }

        private void SetLanguageDictionary()
        {
            EasySaveGUI.Properties.languages.Resources.Culture = new System.Globalization.CultureInfo(EasySaveGUI.Properties.Settings.Default.language);
            EasySaveGUI.Properties.Settings.Default.language = EasySaveGUI.Properties.Settings.Default.language;
            EasySaveGUI.Properties.Settings.Default.Save();
        }
    }
}
