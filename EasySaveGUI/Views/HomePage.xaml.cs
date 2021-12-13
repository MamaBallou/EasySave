﻿using System.Windows.Controls;
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
            this.viewModel = ViewModelHomePage.getInstance();
            DataContext = this.viewModel;
            InitializeComponent();
        }

        private void RunOneClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.viewModel.RunSave(sender);
        }

        private void RunAllClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.viewModel.RunAll();
        }
    }
}
