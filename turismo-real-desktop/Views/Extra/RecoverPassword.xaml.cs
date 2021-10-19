using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace turismo_real_desktop.Views.Extra
{
    /// <summary>
    /// Interaction logic for RecoverPassword.xaml
    /// </summary>
    public partial class RecoverPassword : MetroWindow
    {
        public RecoverPassword()
        {
            InitializeComponent();
        }

        private void OpenLoginWin(object sender, RoutedEventArgs e)
        {
            Login loginWindow = new Login();
            loginWindow.Show();
            Close();
        }

        private void RequestPasswdChange(object sender, RoutedEventArgs e)
        {
            changePasswdFormGrid.Visibility = Visibility.Hidden;
            changePasswdSent.Visibility = Visibility.Visible;
        }

       
    }
}
