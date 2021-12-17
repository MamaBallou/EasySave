using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using EasySaveGUI.viewmodel;

namespace EasySaveGUI
{
    /// <summary>
    /// Logique d'interaction pour HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        private string chosenLanguage;
        private bool isLanguageSelectionVisible;
        ViewModelHomeWindow vm;

        public HomeWindow()
        {
            this.vm = ViewModelHomeWindow.getInstance();
            DataContext = this.vm;
            this.isLanguageSelectionVisible = false;
            InitializeComponent();
            LanguageSelectionTooltipAnimation();
        }

        #region Handle WndProc messages (Single Instance)
        private System.Windows.Interop.HwndSource _hwndSoure;

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            _hwndSoure = System.Windows.Interop.HwndSource.FromHwnd(new System.Windows.Interop.WindowInteropHelper(this).Handle);
            _hwndSoure?.AddHook(HwndHook);
        }

        [DebuggerStepThrough]
        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            // Single instance --> Show window
            if (msg == SingleInstance.WM_SHOWME)
            {
                BringWindowToFront();
                handled = true;
            }

            return IntPtr.Zero;
        }

        private void BringWindowToFront()
        {
            if (WindowState == WindowState.Minimized)
            {
                WindowState = WindowState.Normal;
            }

            Activate();
        }
        #endregion

        public void LanguageSelectionTooltipAnimation()
        {
            if (this.vm.Language == "en-GB")
            {
                DoubleAnimation myDoubleAnimation = new DoubleAnimation { From = -2, To = 58, Duration = new Duration(TimeSpan.FromMilliseconds(200)) };

                Storyboard.SetTarget(myDoubleAnimation, this.LanguageTooltipChosenOption);
                Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath("(Canvas.Left)"));
                Storyboard myMovementStoryboard = new Storyboard();
                myMovementStoryboard.Children.Add(myDoubleAnimation);
                myMovementStoryboard.Begin();
                this.vm.Language = "fr-FR";
            }
            else if (this.vm.Language == "fr-FR")
            {
                DoubleAnimation myDoubleAnimation = new DoubleAnimation { From = 58, To = -2, Duration = new Duration(TimeSpan.FromMilliseconds(200)) };

                Storyboard.SetTarget(myDoubleAnimation, this.LanguageTooltipChosenOption);
                Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath("(Canvas.Left)"));
                Storyboard myMovementStoryboard = new Storyboard();
                myMovementStoryboard.Children.Add(myDoubleAnimation);
                myMovementStoryboard.Begin();

                this.vm.Language = "en-GB";
            }
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

        private void LanguageSelection_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LanguageSelectionTooltipAnimation();
        }

        private void LanguageIcon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!this.isLanguageSelectionVisible)
            {
                DoubleAnimation myDoubleAnimation = new DoubleAnimation { From = 0, To = 1, Duration = new Duration(TimeSpan.FromMilliseconds(200)) };

                Storyboard.SetTarget(myDoubleAnimation, this.LanguageTooltip);
                Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath("Opacity"));
                Storyboard myMovementStoryboard = new Storyboard();
                myMovementStoryboard.Children.Add(myDoubleAnimation);
                myMovementStoryboard.Begin();

                this.LanguageTooltip.IsEnabled = true;
            }
            else if (this.isLanguageSelectionVisible)
            {
                DoubleAnimation myDoubleAnimation = new DoubleAnimation { From = 1, To = 0, Duration = new Duration(TimeSpan.FromMilliseconds(200)) };

                Storyboard.SetTarget(myDoubleAnimation, this.LanguageTooltip);
                Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath("Opacity"));
                Storyboard myMovementStoryboard = new Storyboard();
                myMovementStoryboard.Children.Add(myDoubleAnimation);
                myMovementStoryboard.Begin();

                this.LanguageTooltip.IsEnabled = false;
            }

            this.isLanguageSelectionVisible = !this.isLanguageSelectionVisible;

        }
    }
}

