<<<<<<< HEAD:EasySaveGUI/EasySaveGUI/MainWindow.xaml.cs
﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace EasySaveGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Constructor of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            MainFrame.Content = new WelcomePage();
        }

        /// <summary>
        /// Getter for attribute isMenuExtended.
        /// </summary>
        /// <returns>The attribute isMenuExtended, a boolean.</returns>

        /// <summary>
        /// When we click the close button, it shuts down the window.
        /// </summary>
        /// <param name="sender">The object that sends the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void close_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// When whe hover the close button, it changes its color.
        /// </summary>
        /// <param name="sender">The object that sends the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void close_MouseEnter(object sender, MouseEventArgs e)
        {
            close.Foreground = Brushes.DarkRed;
            close.Cursor = (Cursor)new CursorConverter().ConvertFromString(CursorType.Hand.ToString());
        }

        /// <summary>
        /// When the mouse leaves the close button, it restores its original color.
        /// </summary>
        /// <param name="sender">The object that sends the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void close_MouseLeave(object sender, MouseEventArgs e)
        {
            close.Foreground = Brushes.Black;
        }

        /// <summary>
        /// When the mouse enters the menu logo, it inverts its colors.
        /// </summary>
        /// <param name="sender">The object that sends the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void menu_logo_MouseEnter(object sender, MouseEventArgs e)
        {
            menu_logo.Source = (ImageSource) new ImageSourceConverter().ConvertFromString("../../../logo_small_icon_only_inverted.png");
        }

        /// <summary>
        /// When the mouse leaves the menu logo, it restores its original colors.
        /// </summary>
        /// <param name="sender">The object that sends the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void menu_logo_MouseLeave(object sender, MouseEventArgs e)
        {
            menu_logo.Source = (ImageSource)new ImageSourceConverter().ConvertFromString("../../../logo_small_icon_only.png");
        }

        /// <summary>
        /// When we click on the menu logo, we want to open the side menu if it's not already opened.
        /// We also change the color of the icon.
        /// </summary>
        /// <param name="sender">The object that sends the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void menu_logo_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            menu_logo.Source = (ImageSource)new ImageSourceConverter().ConvertFromString("../../../logo_small_icon_only.png");

            if(dropdown_menu.Opacity == 0)
            {
                dropdown_menu.Opacity = 1;
            }
            else
            {
                dropdown_menu.Opacity = 0;
            }
        }

        /// <summary>
        /// When whe release the click on the menu logo, it restores its original color.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_logo_MouseLeftButtonUp(object sender, MouseEventArgs e)
        {
            menu_logo.Source = (ImageSource)new ImageSourceConverter().ConvertFromString("../../../logo_small_icon_only_inverted.png");
        }

        private void optionSave_MouseEnter(object sender, MouseEventArgs e)
        {
            OptionSave.Background = (Brush)new BrushConverter().ConvertFromString("#bbb");
            OptionSaveText.Foreground = Brushes.White;
        }

        private void optionSave_MouseLeave(object sender, MouseEventArgs e)
        {
            OptionSave.Background = Brushes.White;
            OptionSaveText.Foreground = Brushes.Black;
        }

        private void optionSave_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            OptionSave.Background = Brushes.White;
            OptionSaveText.Foreground = Brushes.Black;

            if(MainFrame.Content.GetType() != new CreateSavePage().GetType())
            {
                MainFrame.Content = new CreateSavePage();
                dropdown_menu.Opacity = 0;
                dropdown_menu_container.Visibility = Visibility.Hidden;
            }
        }

        private void optionSave_MouseLeftButtonUp(object sender, MouseEventArgs e)
        {
            OptionSave.Background = (Brush)new BrushConverter().ConvertFromString("#bbb");
            OptionSaveText.Foreground = Brushes.White;
        }

        private void option_MouseEnter(object sender, MouseEventArgs e)
        {
            OptionSave.Background = Brushes.White;
            OptionSaveText.Foreground = Brushes.Black;
        }

        private void option_MouseLeave(object sender, MouseEventArgs e)
        {
            OptionSave.Background = Brushes.White;
            OptionSaveText.Foreground = Brushes.Black;
        }

        private void HomeButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Content = new WelcomePage();
            dropdown_menu.Opacity = 0;
            dropdown_menu_container.Visibility = Visibility.Visible;
        }
    }
}
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EasySaveGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When we click the close button, it shuts down the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void close_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// When whe hover the close button, it changes its color.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void close_MouseEnter(object sender, MouseEventArgs e)
        {
            close.Foreground = (Brush)new BrushConverter().ConvertFrom("#5c5e5b");
            close.Cursor = (Cursor)new CursorConverter().ConvertFromString(CursorType.Hand.ToString());
        }

        /// <summary>
        /// When the mouse leaves the close button, it restores its original color.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void close_MouseLeave(object sender, MouseEventArgs e)
        {
            close.Foreground = Brushes.Black;
        }

        private void menu_logo_MouseEnter(object sender, MouseEventArgs e)
        {
            menu_logo.Source = (ImageSource) new ImageSourceConverter().ConvertFromString("../../../logo_small_icon_only_inverted.png");
        }

        private void menu_logo_MouseLeave(object sender, MouseEventArgs e)
        {
            menu_logo.Source = (ImageSource)new ImageSourceConverter().ConvertFromString("../../../logo_small_icon_only.png");
        }

        private void menu_logo_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            menu_logo.Source = (ImageSource)new ImageSourceConverter().ConvertFromString("../../../logo_small_icon_only.png");
        }

        private void menu_logo_MouseLeftButtonUp(object sender, MouseEventArgs e)
        {
            menu_logo.Source = (ImageSource)new ImageSourceConverter().ConvertFromString("../../../logo_small_icon_only_inverted.png");
        }
    }
}
>>>>>>> f76bf3aa5e56e63a20ed17dc8b74a1b09c036a86:EasySaveGUI/MainWindow.xaml.cs
