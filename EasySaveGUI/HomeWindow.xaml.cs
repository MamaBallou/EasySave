using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows;
using System.Windows.Input;
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

        public HomeWindow()
        {
            ViewModelHomeWindow vm = ViewModelHomeWindow.getInstance();
            DataContext = vm;
            chosenLanguage = "FR";
            isLanguageSelectionVisible = false;
            InitializeComponent();
            LanguageSelectionTooltipAnimation();
        }

        public void LanguageSelectionTooltipAnimation()
        {
            if (chosenLanguage == "EN")
            {
                DoubleAnimation myDoubleAnimation = new DoubleAnimation { From = -2, To = 58, Duration = new Duration(TimeSpan.FromMilliseconds(200)) };

                Storyboard.SetTarget(myDoubleAnimation, LanguageTooltipChosenOption);
                Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath("(Canvas.Left)"));
                Storyboard myMovementStoryboard = new Storyboard();
                myMovementStoryboard.Children.Add(myDoubleAnimation);
                myMovementStoryboard.Begin();

                chosenLanguage = "FR";
            }
            else if (chosenLanguage == "FR")
            {
                DoubleAnimation myDoubleAnimation = new DoubleAnimation { From = 58, To = -2, Duration = new Duration(TimeSpan.FromMilliseconds(200)) };

                Storyboard.SetTarget(myDoubleAnimation, LanguageTooltipChosenOption);
                Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath("(Canvas.Left)"));
                Storyboard myMovementStoryboard = new Storyboard();
                myMovementStoryboard.Children.Add(myDoubleAnimation);
                myMovementStoryboard.Begin();

                chosenLanguage = "EN";
            }
        }

        private void CloseIcon_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ExpandIcon_Click(object sender, RoutedEventArgs e)
        {
            if(HomeWindowElement.WindowState == WindowState.Maximized)
            {
                HomeWindowElement.WindowState = WindowState.Normal;
                HomeWindowElement.Width = 800;
                HomeWindowElement.Height = 450;
            }
            else
            {
                HomeWindowElement.WindowState = WindowState.Maximized;
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
                this.DragMove();
        }

        private void LanguageSelection_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LanguageSelectionTooltipAnimation();
        }

        private void LanguageIcon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!isLanguageSelectionVisible)
            {
                DoubleAnimation myDoubleAnimation = new DoubleAnimation { From = 0, To = 1, Duration = new Duration(TimeSpan.FromMilliseconds(200)) };

                Storyboard.SetTarget(myDoubleAnimation, LanguageTooltip);
                Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath("Opacity"));
                Storyboard myMovementStoryboard = new Storyboard();
                myMovementStoryboard.Children.Add(myDoubleAnimation);
                myMovementStoryboard.Begin();

                LanguageTooltip.IsEnabled = true;
            }
            else if (isLanguageSelectionVisible)
            {
                DoubleAnimation myDoubleAnimation = new DoubleAnimation { From = 1, To = 0, Duration = new Duration(TimeSpan.FromMilliseconds(200)) };

                Storyboard.SetTarget(myDoubleAnimation, LanguageTooltip);
                Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath("Opacity"));
                Storyboard myMovementStoryboard = new Storyboard();
                myMovementStoryboard.Children.Add(myDoubleAnimation);
                myMovementStoryboard.Begin();

                LanguageTooltip.IsEnabled = false;
            }

            isLanguageSelectionVisible = !isLanguageSelectionVisible;
        }
    }
}
