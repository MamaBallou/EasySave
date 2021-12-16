using System;
using System.Windows;
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

        private void RunOneClick(object sender, System.Windows.RoutedEventArgs e)
        {
            // TODO : Do it right honey please... MVVM will perhaps solve everything?
            
        }

        private void RunAllClick(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                this.viewModel.RunAll();
            }
            catch (ConcurentProcessException)
            {
                MessageBox.Show(String.Format(Properties.languages.Resources.exception_concurent_process, "Calculator"));
            }
        }
    }
}
