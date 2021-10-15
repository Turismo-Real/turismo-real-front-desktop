using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace turismo_real_desktop.Views.Extra
{
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void OpenRecoverPasswdWin(object sender, MouseButtonEventArgs e)
        {
            RecoverPassword recoverPasswdWin = new RecoverPassword();
            recoverPasswdWin.Show();
            this.Close();
        }

        private void ActiveLabel(object sender, MouseEventArgs e)
        {
            lblForgotPasswd.Foreground = new SolidColorBrush(Color.FromRgb(36,174,95));
        }

        private void DeactiveLabel(object sender, MouseEventArgs e)
        {
            lblForgotPasswd.Foreground = Brushes.Black;
        }
    }
}
