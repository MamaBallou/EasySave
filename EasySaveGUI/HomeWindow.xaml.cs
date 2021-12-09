using System.Globalization;
using System.Resources;
using System.Threading;
using System.Windows;
using EasySaveConsole.controller;
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
            ViewModelHomeWindow vm = new ViewModelHomeWindow();
            DataContext = vm;
            InitializeComponent();
        }
    }
}
