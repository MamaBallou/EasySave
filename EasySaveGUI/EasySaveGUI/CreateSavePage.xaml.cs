using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace EasySaveGUI
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
            SourceFilePath.Text = openFileDialog.SelectedPath;
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
            TargetFilePath.Text = openFileDialog.SelectedPath;
        }

        /// <summary>
        /// When the total save button is clicked, whe set the save type to "total".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TotalSaveButton_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            saveType = "total";
            TotalSaveButton.Background = Brushes.LightYellow;
            TotalButtonText.Foreground = Brushes.Black;

            if ((SaveNameInput.Text.Length != 0) && (SourceFilePath.Text.Length != 0) && (TargetFilePath.Text.Length != 0))
            {
                ErrorSaveCreationInput.Opacity = 0;
                SaveCreationInputs.Visibility = System.Windows.Visibility.Hidden;
                SaveCreationProcess.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                ErrorSaveCreationInput.Opacity = 1;
            }
        }

        /// <summary>
        /// When the click on the total save button is released, it restores its changed colors.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TotalSaveButton_MouseLeftButtonUp(object sender, MouseEventArgs e)
        {
            TotalSaveButton.Background = Brushes.Orange;
            TotalButtonText.Foreground = Brushes.White;
        }

        /// <summary>
        /// When the mouse enters the total button, it changes the background and the text colors.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TotalSaveButton_MouseEnter(object sender, MouseEventArgs e)
        {
            TotalSaveButton.Background = Brushes.Orange;
            TotalButtonText.Foreground = Brushes.White;
        }

        /// <summary>
        /// When the mouse leaves the total button, it restores the background and the text colors.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TotalSaveButton_MouseLeave(object sender, MouseEventArgs e)
        {
            TotalSaveButton.Background = Brushes.LightYellow;
            TotalButtonText.Foreground = Brushes.Black;
        }

        /// <summary>
        /// When the differential save button is clicked, whe set the save type to "total".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DifferentialSaveButton_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            saveType = "differential";
            DifferentialSaveButton.Background = Brushes.LightYellow;
            DifferentialButtonText.Foreground = Brushes.Black;

            if ((SaveNameInput.Text.Length != 0) && (SourceFilePath.Text.Length != 0) && (TargetFilePath.Text.Length != 0))
            {
                ErrorSaveCreationInput.Opacity = 0;
                SaveCreationInputs.Visibility = System.Windows.Visibility.Hidden;
                SaveCreationProcess.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                ErrorSaveCreationInput.Opacity = 1;
            }
        }

        /// <summary>
        /// When the click on the differential save button is released, it restores its changed colors.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DifferentialSaveButton_MouseLeftButtonUp(object sender, MouseEventArgs e)
        {
            DifferentialSaveButton.Background = Brushes.Orange;
            DifferentialButtonText.Foreground = Brushes.White;
        }

        /// <summary>
        /// When the mouse enters the differential button, it changes the background and the text colors.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DifferentialSaveButton_MouseEnter(object sender, MouseEventArgs e)
        {
            DifferentialSaveButton.Background = Brushes.Orange;
            DifferentialButtonText.Foreground = Brushes.White;
        }

        /// <summary>
        /// When the mouse leaves the differential button, it restores the background and the text colors.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DifferentialSaveButton_MouseLeave(object sender, MouseEventArgs e)
        {
            DifferentialSaveButton.Background = Brushes.LightYellow;
            DifferentialButtonText.Foreground = Brushes.Black;
        }
    }
}
