using System;
using System.Windows;
using System.Windows.Input;
using turismo_real_business.Messages;
using turismo_real_core.Controllers.Login;
using turismo_real_desktop.Colors;
using turismo_real_desktop.Views.Administrador;

namespace turismo_real_desktop.Views.Extra
{
    public partial class Login : Window
    {
        protected LoginController loginController;

        public Login()
        {
            InitializeComponent();
            loginController = new LoginController();

        }

        private void OpenRecoverPasswdWin(object sender, MouseButtonEventArgs e)
        {
            string message = "Para poder registrarte en el sistema ponte en contacto con un administrador que ingrese tus datos.";
            MessageBox.Show(message, "Contacta con un administrador" ,MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void OpenRecoverPassWin(object sender, RoutedEventArgs e)
        {
            RecoverPassword recoverPasswdWin = new RecoverPassword();
            recoverPasswdWin.Show();
            Close();
        }

        private void LoginUser(object sender, RoutedEventArgs e)
        {
            // validar formato de correo
            string correo = txtEmail.Text;
            if (!loginController.ValidarCorreo(correo))
            {
                throw new NotImplementedException();
            }
            
            // validar credenciales
            string HashedPassword = loginController.HashPassword(txtPasswd.Password.ToString());
            LoginResponse loginResponse = loginController.Login(correo, HashedPassword);
            Console.WriteLine(loginResponse.login);
            if (!loginResponse.login)
            {
                throw new NotImplementedException();
            }

            // validar perfil
            string perfil = "administrador";
            if (loginResponse.perfil.ToUpper().CompareTo(perfil.ToUpper()) != 0)
            {
                throw new NotImplementedException();
            }

            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            Close();
        }

    }
}
