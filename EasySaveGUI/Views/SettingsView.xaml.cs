using System.Collections.Generic;
using System.Windows.Controls;
using EasySaveGUI.viewmodel;

namespace EasySaveGUI.Views
{
    /// <summary>
    /// Logique d'interaction pour SettingsPage.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        private ViewModelSettingsView viewModel;
        public SettingsView()
        {
            viewModel = ViewModelSettingsView.GetInstance();
            DataContext = viewModel;
            InitializeComponent();
        }

        private void SaveClick(object sender, System.Windows.RoutedEventArgs e)
        {
            List<CheckBox> checkBoxs = new List<CheckBox> { cbx_pdf, cbx_jpg, cbx_docx, cbx_txt, cbx_exe };
            List<string> toEncrypt = new List<string>();
            checkBoxs.ForEach(checkBox =>
            {
                if(checkBox.IsChecked == true)
                {
                    toEncrypt.Add(checkBox.Content.ToString());
                }
            });
            viewModel.SetToEncryptExtension(toEncrypt);
        }
    }
}
