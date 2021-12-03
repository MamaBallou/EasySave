using System;
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
            close.Foreground = (Brush)new BrushConverter().ConvertFrom("#aa4f0a");
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
    }
}
