using System.Windows.Controls;
using EasySaveGUI.viewmodel;

namespace EasySaveGUI.Views
{
    /// <summary>
    /// Logique d'interaction pour HomePage.xaml
    /// </summary>
    public partial class HomePage : UserControl
    {
        public HomePage()
        {
            ViewModelHomePage viewModel = ViewModelHomePage.getInstance();
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
