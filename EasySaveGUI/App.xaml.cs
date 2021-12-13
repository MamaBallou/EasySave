using System.Threading;
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
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(EasySaveGUI.Properties.Settings.Default.language);
            switch (Thread.CurrentThread.CurrentCulture.ToString())
            {
                case "en-GB":
                case "en":
                    EasySaveGUI.Properties.languages.Resources.Culture = new System.Globalization.CultureInfo("en-GB");
                    EasySaveGUI.Properties.Settings.Default.language = "en-GB";
                    break;
                case "fr-FR":
                case "fr":
                    EasySaveGUI.Properties.languages.Resources.Culture = new System.Globalization.CultureInfo("fr-FR");
                    EasySaveGUI.Properties.Settings.Default.language = "fr-FR";
                    break;
            }
            EasySaveGUI.Properties.Settings.Default.Save();
        }
    }
}
