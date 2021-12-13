﻿using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace EasySaveGUI.Views
{
    /// <summary>
    /// Logique d'interaction pour CreateSavePage.xaml
    /// </summary>
    public partial class CreateSavePage : Page
    {
        private string saveType;

        /// <summary>
        /// Constructor for the CreateSave page.
        /// </summary>
        public CreateSavePage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When the browser button for the source path is clicked, a browser opens that allows to select folders only.
        /// When a folder is selected, its path is injected as the TextBox text so it's displayed in the GUI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SourceBrowseButton_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog openFileDialog = new System.Windows.Forms.FolderBrowserDialog();
            openFileDialog.ShowDialog();
            this.SourceFilePath.Text = openFileDialog.SelectedPath;
        }

        /// <summary>
        /// When the browser button for the target path is clicked, a browser opens that allows to select folders only.
        /// When a folder is selected, its path is injected as the TextBox text so it's displayed in the GUI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TargetBrowseButton_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog openFileDialog = new System.Windows.Forms.FolderBrowserDialog();
            openFileDialog.ShowDialog();
            this.TargetFilePath.Text = openFileDialog.SelectedPath;
        }

        /// <summary>
        /// When the total save button is clicked, whe set the save type to "total".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TotalSaveButton_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            this.saveType = "total";
            this.TotalSaveButton.Background = Brushes.LightYellow;
            this.TotalButtonText.Foreground = Brushes.Black;

            if ((this.SaveNameInput.Text.Length != 0) && (this.SourceFilePath.Text.Length != 0) && (this.TargetFilePath.Text.Length != 0))
            {
                this.ErrorSaveCreationInput.Opacity = 0;
                this.SaveCreationInputs.Visibility = System.Windows.Visibility.Hidden;
                this.SaveCreationProcess.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                this.ErrorSaveCreationInput.Opacity = 1;
            }
        }

        /// <summary>
        /// When the click on the total save button is released, it restores its changed colors.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TotalSaveButton_MouseLeftButtonUp(object sender, MouseEventArgs e)
        {
            this.TotalSaveButton.Background = Brushes.Orange;
            this.TotalButtonText.Foreground = Brushes.White;
        }

        /// <summary>
        /// When the mouse enters the total button, it changes the background and the text colors.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TotalSaveButton_MouseEnter(object sender, MouseEventArgs e)
        {
            this.TotalSaveButton.Background = Brushes.Orange;
            this.TotalButtonText.Foreground = Brushes.White;
        }

        /// <summary>
        /// When the mouse leaves the total button, it restores the background and the text colors.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TotalSaveButton_MouseLeave(object sender, MouseEventArgs e)
        {
            this.TotalSaveButton.Background = Brushes.LightYellow;
            this.TotalButtonText.Foreground = Brushes.Black;
        }

        /// <summary>
        /// When the differential save button is clicked, whe set the save type to "total".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DifferentialSaveButton_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            this.saveType = "differential";
            this.DifferentialSaveButton.Background = Brushes.LightYellow;
            this.DifferentialButtonText.Foreground = Brushes.Black;

            if ((this.SaveNameInput.Text.Length != 0) && (this.SourceFilePath.Text.Length != 0) && (this.TargetFilePath.Text.Length != 0))
            {
                this.ErrorSaveCreationInput.Opacity = 0;
                this.SaveCreationInputs.Visibility = System.Windows.Visibility.Hidden;
                this.SaveCreationProcess.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                this.ErrorSaveCreationInput.Opacity = 1;
            }
        }

        /// <summary>
        /// When the click on the differential save button is released, it restores its changed colors.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DifferentialSaveButton_MouseLeftButtonUp(object sender, MouseEventArgs e)
        {
            this.DifferentialSaveButton.Background = Brushes.Orange;
            this.DifferentialButtonText.Foreground = Brushes.White;
        }

        /// <summary>
        /// When the mouse enters the differential button, it changes the background and the text colors.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DifferentialSaveButton_MouseEnter(object sender, MouseEventArgs e)
        {
            this.DifferentialSaveButton.Background = Brushes.Orange;
            this.DifferentialButtonText.Foreground = Brushes.White;
        }

        /// <summary>
        /// When the mouse leaves the differential button, it restores the background and the text colors.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DifferentialSaveButton_MouseLeave(object sender, MouseEventArgs e)
        {
            this.DifferentialSaveButton.Background = Brushes.LightYellow;
            this.DifferentialButtonText.Foreground = Brushes.Black;
        }
    }
}