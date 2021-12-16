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
    }
}
