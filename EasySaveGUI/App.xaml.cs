using System.Windows;
using EasySaveGUI.retriever;

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
            DataRetriever.SetListOfExtension();
        }

        private void SetLanguageDictionary()
        {
            EasySaveGUI.Properties.languages.Resources.Culture = new System.Globalization.CultureInfo(EasySaveGUI.Properties.Settings.Default.language);
        }
    }
}
