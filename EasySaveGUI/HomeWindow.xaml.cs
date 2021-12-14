using System.Windows;
using EasySaveGUI.viewmodel;
using EasySaveGUI.Views;

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
        }
        private void Canvas_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void GithubLogo_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = "https://github.com/MamaBallou/EasySave",
                UseShellExecute = true
            }
                );
        }
    }
}
