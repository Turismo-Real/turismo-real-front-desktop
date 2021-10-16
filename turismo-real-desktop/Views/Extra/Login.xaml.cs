using System.Windows;
using System.Windows.Input;
using turismo_real_desktop.Colors;

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
            Close();
        }

        private void ActiveLabel(object sender, MouseEventArgs e)
        {
            lblForgotPasswd.Foreground = UIColors.NormalGreen;
        }

        private void DeactiveLabel(object sender, MouseEventArgs e)
        {
            lblForgotPasswd.Foreground = UIColors.NormalBlack;
        }

        private void OnHoverLogin(object sender, MouseEventArgs e)
        {
            btnLogin.Background = UIColors.HoverGreen;
        }

        private void OpenSignUpWin(object sender, RoutedEventArgs e)
        {
            SignUp signUpWin = new SignUp();
            signUpWin.Show();
            Close();
        }
    }
}
