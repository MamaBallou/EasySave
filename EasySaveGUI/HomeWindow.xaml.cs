using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using EasySaveGUI.viewmodel;

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

        private void CloseIcon_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ExpandIcon_Click(object sender, RoutedEventArgs e)
        {
            if (this.HomeWindowElement.WindowState == WindowState.Maximized)
            {
                this.HomeWindowElement.WindowState = WindowState.Normal;
                this.HomeWindowElement.Width = 800;
                this.HomeWindowElement.Height = 450;
            }
            else
            {
                this.HomeWindowElement.WindowState = WindowState.Maximized;
            }
        }

        private void GithubIcon_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://github.com/MamaBallou/EasySave",
                UseShellExecute = true
            });
        }

        private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
    }
}
