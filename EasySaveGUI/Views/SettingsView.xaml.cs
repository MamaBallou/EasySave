using System.Windows.Controls;
using EasySaveGUI.viewmodel;

namespace EasySaveGUI.views
{
    /// <summary>
    /// Logique d'interaction pour SettingsPage.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        private ViewModelSettingsView viewModel;
        public SettingsView()
        {
            this.viewModel = ViewModelSettingsView.GetInstance();
            DataContext = this.viewModel;
            InitializeComponent();
        }
    }
}
