using System.Windows.Controls;
using EasySaveConsole.model;
using EasySaveGUI.viewmodel;

namespace EasySaveGUI.Views
{
    /// <summary>
    /// Logique d'interaction pour HomePage.xaml
    /// </summary>
    public partial class HomePage : UserControl
    {
        ViewModelHomePage viewModel;
        public HomePage()
        {
            viewModel = ViewModelHomePage.getInstance();
            DataContext = viewModel;
            InitializeComponent();
        }

        private void RunAllBackups(object sender, System.Windows.RoutedEventArgs e)
        {
            viewModel.RunSave(sender);
        }
    }
}
