using System.Windows.Controls;
using EasySaveGUI.viewmodel;

namespace EasySaveGUI.views
{
    /// <summary>
    /// Logique d'interaction pour HomePage.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        ViewModelHomePage viewModel;
        public HomeView()
        {
            this.viewModel = ViewModelHomePage.getInstance();
            DataContext = this.viewModel;
            InitializeComponent();
        }
    }
}
