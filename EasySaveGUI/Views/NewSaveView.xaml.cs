using System.Windows.Controls;
using EasySaveGUI.viewmodel;

namespace EasySaveGUI.views
{
    /// <summary>
    /// Logique d'interaction pour NewSaveView.xaml
    /// </summary>
    public partial class NewSaveView : UserControl
    {

        public NewSaveView()
        {
            DataContext = ViewModelNewSaveView.GetInstance();
            InitializeComponent();
        }

        private void OpenFileExplorer(TextBox textBox)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.ShowDialog();
            textBox.Text = dialog.SelectedPath + "\\";
        }

        private void sourceExplorer(object sender, System.Windows.RoutedEventArgs e)
        {
            OpenFileExplorer(this.txt_source);
        }

        private void targetExplorer(object sender, System.Windows.RoutedEventArgs e)
        {
            OpenFileExplorer(this.txt_target);
        }
    }
}
