using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using EasySaveConsole.model.log;
using EasySaveConsole.model.save;
using EasySaveConsole.tools;
using EasySaveGUI.viewmodel;
using System.Windows.Media;

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

        private void CreateClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.btn_create.IsEnabled = false;
            if (checkEmptiness())
            {
                MessageBox.Show(EasySaveGUI.Properties.languages.Resources.warning_fill_all_fields);
                this.btn_create.IsEnabled = true;
                return;
            }
            string source_tmp = Tool.completePath(this.txt_source.Text);
            if (!Tool.getInstance().checkExistance(source_tmp))
            {
                MessageBox.Show(EasySaveGUI.Properties.languages.Resources.warning_invalid_source);
                this.btn_create.IsEnabled = true;
                return;
            }
            string target_tmp = Tool.completePath(this.txt_target.Text);
            FileInfo fi = null;
            try
            {
                fi = new FileInfo(target_tmp);
            }
            catch (ArgumentException) { }
            catch (PathTooLongException) { }
            catch (NotSupportedException) { }
            if (fi == null)
            {
                MessageBox.Show(EasySaveGUI.Properties.languages.Resources.warning_invalid_target);
                this.btn_create.IsEnabled = true;
                return;
            }
            if (this.rdb_total.IsChecked == true)
            {
                ModelSaveTotal save = new ModelSaveTotal(this.txt_name.Text, this.txt_source.Text, this.txt_target.Text);
                ModelState saveState = save.toModelState();
                this.viewModelHomePage.Saves.Add(save);
                this.viewModelHomePage.States.Add(saveState);
                List<ModelState> tmp = this.viewModelHomePage.States;
                save.save(ref saveState, ref tmp);
                return;
            }
            if (this.rdb_differential.IsChecked == true)
            {
                ModelSaveDifferential save = new ModelSaveDifferential(this.txt_name.Text, this.txt_source.Text, this.txt_target.Text);
                ModelState saveState = save.toModelState();
                this.viewModelHomePage.Saves.Add(save);
                this.viewModelHomePage.States.Add(saveState);
                List<ModelState> tmp = this.viewModelHomePage.States;
                save.save(ref saveState, ref tmp);
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
