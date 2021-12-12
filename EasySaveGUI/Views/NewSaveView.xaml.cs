using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using EasySaveConsole;
using EasySaveConsole.model;
using EasySaveGUI.viewmodel;

namespace EasySaveGUI.Views
{
    /// <summary>
    /// Logique d'interaction pour NewSaveView.xaml
    /// </summary>
    public partial class NewSaveView : UserControl
    {
        private ViewModelHomePage viewModelHomePage = ViewModelHomePage.getInstance();
        public NewSaveView()
        {
            InitializeComponent();
            this.txt_name.Focus();
        }

        private void OpenFileExplorer(TextBox textBox)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.ShowDialog();
            textBox.Text = dialog.SelectedPath;
        }

        private void sourceExplorer(object sender, System.Windows.RoutedEventArgs e)
        {
            OpenFileExplorer(this.txt_source);
        }

        private void targetExplorer(object sender, System.Windows.RoutedEventArgs e)
        {
            OpenFileExplorer(this.txt_target);
        }

        private void CreateClick(object sender, System.Windows.RoutedEventArgs e)
        {
            btn_create.IsEnabled = false;
            if (checkEmptiness())
            {
                MessageBox.Show(EasySaveGUI.Properties.languages.Resources.warning_fill_all_fields);
                btn_create.IsEnabled = true;
                return;
            }
            string source_tmp = Tool.completePath(txt_source.Text);
            if(!Tool.getInstance().checkExistance(source_tmp))
            {
                MessageBox.Show(EasySaveGUI.Properties.languages.Resources.warning_invalid_source);
                btn_create.IsEnabled = true;
                return;
            }
            string target_tmp = Tool.completePath(txt_target.Text);
            FileInfo fi = null;
            try
            {
                fi = new FileInfo(target_tmp);
            }
            catch (ArgumentException) {  }
            catch (PathTooLongException) {  }
            catch (NotSupportedException) {  }
            if (fi == null)
            {
                MessageBox.Show(EasySaveGUI.Properties.languages.Resources.warning_invalid_target);
                btn_create.IsEnabled = true;
                return;
            }
            if(rdb_total.IsChecked == true)
            {
                ModelSaveTotal save = new ModelSaveTotal(txt_name.Text, txt_source.Text, txt_target.Text);
                ModelState saveState = save.toModelState();
                this.viewModelHomePage.Saves.Add(save);
                this.viewModelHomePage.States.Add(saveState);
                save.save(ref saveState, viewModelHomePage.States);
                return;
            }
        }

        private bool checkEmptiness()
        {
            bool result = false;
            if (string.IsNullOrEmpty(this.txt_name.Text))
            {
                result = true;
            }
            if (string.IsNullOrEmpty(this.txt_source.Text))
            {
                result = true;
            }
            if (string.IsNullOrEmpty(this.txt_target.Text))
            {
                result = true;
            }
            return result;
        }
    }
}
